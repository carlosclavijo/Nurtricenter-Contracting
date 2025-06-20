using Contracting.Domain.Patients;
using Contracting.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Contracting.Infrastructure.Persistence.DomainModel.Config;

public class PatientConfig : IEntityTypeConfiguration<Patient>
{
	public void Configure(EntityTypeBuilder<Patient> builder)
	{
		builder.ToTable("patients");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id).HasColumnName("patientId");

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
