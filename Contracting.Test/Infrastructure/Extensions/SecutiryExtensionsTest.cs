using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Nur.Store2025.Security.Config;

namespace Contracting.Test.Infrastructure.Extensions;

public class SecutiryExtensionsTest
{
	[Fact]
	public void AddSecurity_WhenEnvironmentIsWebHost_AddsJwtOptions()
	{
		var services = new ServiceCollection();
		var jwtOptions = new JwtOptions
		{
			Lifetime = 30,
			SecretKey = "supersecretkey",
			ValidAudience = "aud",
			ValidIssuer = "issuer",
			ValidateAudience = true,
			ValidateIssuer = true,
			ValidateLifetime = true
		};

		var environmentMock = new Mock<IWebHostEnvironment>();
		environmentMock.Setup(e => e.EnvironmentName).Returns("Development");

		services.AddSingleton(jwtOptions);
		services.AddSingleton<IHostEnvironment>(environmentMock.Object);

		services.AddSecurity(environmentMock.Object);

		var provider = services.BuildServiceProvider();

		var resolvedJwtOptions = provider.GetService<JwtOptions>();
		Assert.NotNull(resolvedJwtOptions);
		Assert.Equal("supersecretkey", resolvedJwtOptions.SecretKey);
	}

	[Fact]
	public void AddSecurityInvalid()
	{
		var services = new ServiceCollection();

		var environmentMock = new Mock<IHostEnvironment>();
		environmentMock.Setup(e => e.EnvironmentName).Returns("Development");

		services.AddSingleton<IHostEnvironment>(environmentMock.Object);

		services.AddSecurity(environmentMock.Object);

		var provider = services.BuildServiceProvider();

		var resolvedJwtOptions = provider.GetService<JwtOptions>();
		Assert.Null(resolvedJwtOptions);
	}
}
