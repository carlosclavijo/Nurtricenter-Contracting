using Contracting.Domain.Administrators;
using Contracting.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Contracting.Infrastructure.Persistence.DomainModel.Config;
using Contracting.Domain.Patients;

namespace Contracting.Test.Infrastructure.Persistence.DomainModel.Config;

public class PatientConfigTest
{
	[Fact]
	public void PhoneConfigIsValid()
	{
		var modelBuilder = new ModelBuilder();
		var config = new PatientConfig();

		config.Configure(modelBuilder.Entity<Patient>());
		var model = modelBuilder.Model;

		var entity = model.FindEntityType(typeof(Patient));
		Assert.NotNull(entity);
		Assert.Equal("patients", entity.GetTableName());

		var key = entity.FindPrimaryKey();
		Assert.NotNull(key);
		Assert.Contains(key.Properties, p => p.Name == "Id");

		Assert.Equal("patientId", entity.FindProperty("Id")?.GetColumnName());
		Assert.Equal("name", entity.FindProperty("Name")?.GetColumnName());
		Assert.Equal("phone", entity.FindProperty("Phone")?.GetColumnName());

		Assert.Null(entity.FindProperty("_domainEvents"));
		Assert.Null(entity.FindProperty("DomainEvents"));

		var nameProp = entity.FindProperty("Name");
		var nameConverter = nameProp?.GetValueConverter();
		Assert.NotNull(nameConverter);
		Assert.Equal("Carlos", nameConverter.ConvertToProvider.Invoke(new FullNameValue("Carlos")));
		var fullNameValue = nameConverter.ConvertFromProvider.Invoke("Carlos") as FullNameValue;
		Assert.Equal("Carlos", fullNameValue?.Name);

		var phoneProp = entity.FindProperty("Phone");
		var phoneConverter = phoneProp?.GetValueConverter();
		Assert.NotNull(phoneConverter);
		Assert.Equal("77141516", phoneConverter.ConvertToProvider.Invoke(new PhoneNumberValue("77141516")));
		var phoneValue = phoneConverter.ConvertFromProvider.Invoke("77141516") as PhoneNumberValue;
		Assert.Equal("77141516", phoneValue?.Phone);
	}
}
