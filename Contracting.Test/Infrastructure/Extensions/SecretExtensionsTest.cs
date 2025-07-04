using Contracting.Domain.Abstractions;
using Contracting.Domain.Patients;
using Contracting.Infrastructure.Extensions;
using Contracting.Infrastructure.Persistence;
using Joseco.Communication.External.RabbitMQ.Services;
using Joseco.Secrets.Contrats;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Nur.Store2025.Observability.Config;
using Nur.Store2025.Security.Config;

namespace Contracting.Test.Infrastructure.Extensions;

public class SecretExtensionsTest
{
	[Fact]
    public void AddSecretsWhenSecretManagerIsFalse()
    {
        var inMemorySettings = new Dictionary<string, string?>
       {
           { "UseSecretManager", "false" }
       };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var services = new ServiceCollection();
        var rabbitSettings = new RabbitMqSettings
        {
            Host = "localhost",
            UserName = "storeUser",
            Password = "storePassword",
            VirtualHost = "/"
        };

        var databaseSettings = new DatabaseSettings
        {
            ConnectionString = "Host=localhost;Port=5432;Database=test;Username=postgres;Password=pass"
        };

        var jaegerSettings = new JeagerSettings
        {
            Host = "localhost",
            Port = 6831,
        };

        var environment = new Mock<IHostEnvironment>();
        environment.Setup(e => e.EnvironmentName).Returns("Development");

        services.AddSingleton(rabbitSettings);
        services.AddSingleton(databaseSettings);
        services.AddSingleton(jaegerSettings);
        services.AddSingleton(environment.Object);
        services.AddSecrets(configuration, environment.Object);
        services.AddRabbitMQ();
        services.AddDatabase();
        services.AddObservability(environment.Object, "test-service");

        var provider = services.BuildServiceProvider();

        Assert.NotNull(provider.GetService<RabbitMqSettings>());
        Assert.NotNull(provider.GetService<IUnitOfWork>());
        Assert.NotNull(provider.GetService<IDatabase>());
        Assert.NotNull(provider.GetService<IPatientRepository>());
    }

	[Fact]
    public void LoadSecretsFromVaultTest()
    {
		var services = new ServiceCollection();
		var secretManagerMock = new Mock<ISecretManager>();

		secretManagerMock
			.Setup(sm => sm.Get<JwtOptions>(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(new JwtOptions
			{
				Lifetime = 30,
				SecretKey = "mock-key"
			});

		secretManagerMock
			.Setup(sm => sm.Get<RabbitMqSettings>(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(new RabbitMqSettings
			{
				Host = "localhost",
				UserName = "user",
				Password = "pass",
				VirtualHost = "/"
			});

		secretManagerMock
			.Setup(sm => sm.Get<DatabaseSettings>(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(new DatabaseSettings
			{
				ConnectionString = "Host=localhost"
			});

		secretManagerMock
			.Setup(sm => sm.Get<JeagerSettings>(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(new JeagerSettings
			{
				Host = "localhost",
				Port = 6831
			});

		var scopedServices = new ServiceCollection();
		scopedServices.AddSingleton(secretManagerMock.Object);
		var scopedProvider = scopedServices.BuildServiceProvider();
		var scopeMock = new Mock<IServiceScope>();
		scopeMock.Setup(x => x.ServiceProvider).Returns(scopedProvider);
		var scopeFactoryMock = new Mock<IServiceScopeFactory>();
		scopeFactoryMock.Setup(f => f.CreateScope()).Returns(scopeMock.Object);
		services.AddSingleton<IServiceScopeFactory>(scopeFactoryMock.Object);
		services.AddSingleton(secretManagerMock.Object);
		services.LoadSecretsFromVault();
		var provider = services.BuildServiceProvider();

		var jwtOptions = provider.GetService<JwtOptions>();
		var rabbit = provider.GetService<RabbitMqSettings>();
		var db = provider.GetService<DatabaseSettings>();
		var jaeger = provider.GetService<JeagerSettings>();

		Assert.NotNull(jwtOptions);
		Assert.NotNull(rabbit);
		Assert.NotNull(db);
		Assert.NotNull(jaeger);
		Assert.Equal("mock-key", jwtOptions.SecretKey);
		Assert.Equal("localhost", rabbit.Host);
		Assert.Equal("Host=localhost", db.ConnectionString);
		Assert.Equal(6831, jaeger.Port);
	}

	[Fact]
	public async Task LoadAndRegisterTTest()
	{
		var services = new ServiceCollection();
		var expectedJwtOptions = new JwtOptions
		{
			Lifetime = 60,
			SecretKey = "test-secret",
			ValidAudience = "test-aud",
			ValidIssuer = "test-issuer",
			ValidateAudience = true,
			ValidateIssuer = true,
			ValidateLifetime = true
		};
		var secretManagerMock = new Mock<ISecretManager>();

		secretManagerMock
			.Setup(sm => sm.Get<JwtOptions>("JwtOptions", "secrets"))
			.ReturnsAsync(expectedJwtOptions);

		await SecretExtensions.LoadAndRegister<JwtOptions>(
			secretManagerMock.Object,
			services,
			"JwtOptions",
			"secrets"
		);

		var provider = services.BuildServiceProvider();
		var jwtOptions = provider.GetService<JwtOptions>();

		Assert.NotNull(jwtOptions);
		Assert.Same(expectedJwtOptions, jwtOptions);
		secretManagerMock.Verify(sm => sm.Get<JwtOptions>("JwtOptions", "secrets"), Times.Once);
	}
}