using System;
using Contracting.Domain.Administrators;
using Contracting.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Contracting.Infrastructure.Persistence.DomainModel.Config;

internal class AdministratorConfig : IEntityTypeConfiguration<Administrator>
{
	public void Configure(EntityTypeBuilder<Administrator> builder)
	{
		builder.ToTable("administrators");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id).HasColumnName("administratorId");

		var nameConverter = new ValueConverter<FullNameValue, string>(
			FullNameValue => FullNameValue.Name,
			name => new FullNameValue(name)
			);

		builder.Property(x => x.Name).HasColumnName("name").HasConversion(nameConverter);

		var phoneConverter = new ValueConverter<PhoneNumberValue, string>(
			PhoneNumberValue => PhoneNumberValue.Phone,
			phone => new PhoneNumberValue(phone)
			);

		builder.Property(x => x.Phone).HasColumnName("phone").HasConversion(phoneConverter);

		builder.Ignore("_domainEvents");
		builder.Ignore(x => x.DomainEvents);
	}
}
