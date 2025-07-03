﻿using Contracting.Infrastructure.Persistence;
using Joseco.Communication.External.RabbitMQ.Services;
using Joseco.Secrets.Contrats;
using Joseco.Secrets.HashicorpVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nur.Store2025.Observability.Config;
using Nur.Store2025.Security.Config;

namespace Contracting.Infrastructure.Extensions;

public static class SecretExtensions
{
	private const string JwtOptionsSecretName = "JwtOptions";
	private const string RabbitMqSettingsSecretName = "RabbitMqSettings";
	private const string ContractingDatabaseConnectionStringSecretName = "ContractingDatabaseConnectionString";
	private const string JeagerSettingsSecretName = "JaegerSettings";

	public static IServiceCollection AddSecrets(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		bool useSecretManager = configuration.GetValue<bool>("UseSecretManager", false);
		/*if (environment.IsDevelopment() && !useSecretManager)
		{*/
			configuration
				.LoadAndRegister<RabbitMqSettings>(services, RabbitMqSettingsSecretName)
				.LoadAndRegister<DatabaseSettings>(services, ContractingDatabaseConnectionStringSecretName)
				.LoadAndRegister<JwtOptions>(services, JwtOptionsSecretName)
				.LoadAndRegister<JeagerSettings>(services, JeagerSettingsSecretName);

			return services;
		/*}

		string? vaultUrl = Environment.GetEnvironmentVariable("VAULT_URL");
		string? vaultToken = Environment.GetEnvironmentVariable("VAULT_TOKEN");

		if (string.IsNullOrEmpty(vaultUrl) || string.IsNullOrEmpty(vaultToken))
		{
			throw new InvalidOperationException("Vault URL or Token is not set in environment variables.");
		}

		var settings = new VaultSettings()
		{
			VaultUrl = vaultUrl,
			VaultToken = vaultToken
		};

		services.AddHashicorpVault(settings)
			.LoadSecretsFromVault();

		return services;*/
	}

	public static void LoadSecretsFromVault(this IServiceCollection services)
	{
		using var serviceProvider = services.BuildServiceProvider();
		var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

		using var scope = scopeFactory.CreateScope();
		var secretManager = scope.ServiceProvider.GetRequiredService<ISecretManager>();

		string VaultMountPoint = "secrets";

		Task[] tasks = [
				LoadAndRegister<JwtOptions>(secretManager, services, JwtOptionsSecretName, VaultMountPoint),
				LoadAndRegister<RabbitMqSettings>(secretManager, services, RabbitMqSettingsSecretName, VaultMountPoint),
				LoadAndRegister<DatabaseSettings>(secretManager, services, ContractingDatabaseConnectionStringSecretName, VaultMountPoint),
				LoadAndRegister<JeagerSettings>(secretManager, services, JeagerSettingsSecretName, VaultMountPoint)
			];

		Task.WaitAll(tasks);
	}

	public static async Task LoadAndRegister<T>(ISecretManager secretManager, IServiceCollection services,
		string secretName, string mountPoint) where T : class, new()
	{
		T secret = await secretManager.Get<T>(secretName, mountPoint);
		services.AddSingleton<T>(secret);
	}

	public static IConfiguration LoadAndRegister<T>(this IConfiguration configuration, IServiceCollection services,
		string secretName) where T : class, new()
	{
		T secret = Activator.CreateInstance<T>();
		configuration.Bind(secretName, secret);
		services.AddSingleton<T>(secret);
		return configuration;
	}

}