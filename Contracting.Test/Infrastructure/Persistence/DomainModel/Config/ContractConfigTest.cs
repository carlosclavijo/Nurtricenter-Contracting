using Contracting.Domain.Contracts;
using Contracting.Domain.Delivery;
using Contracting.Domain.Shared;
using Contracting.Infrastructure.Persistence.DomainModel.Config;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Test.Infrastructure.Persistence.DomainModel.Config;

public class ContractConfigTest
{
	[Fact]
	public void ContractConfigIsValid()
	{
		var modelBuilder = new ModelBuilder();
		var config = new ContractConfig();

		config.Configure(modelBuilder.Entity<Contract>());
		config.Configure(modelBuilder.Entity<DeliveryDay>());

		var model = modelBuilder.Model;
		var contractEntity = model.FindEntityType(typeof(Contract));

		Assert.NotNull(contractEntity);
		Assert.Equal("contracts", contractEntity.GetTableName());

		Assert.Equal("contractId", contractEntity.FindProperty("Id")?.GetColumnName());
		Assert.Equal("administratorId", contractEntity.FindProperty("AdministratorId")?.GetColumnName());
		Assert.Equal("patientId", contractEntity.FindProperty("PatientId")?.GetColumnName());
		Assert.Equal("transactionType", contractEntity.FindProperty("Type")?.GetColumnName());
		Assert.Equal("transactionStatus", contractEntity.FindProperty("Status")?.GetColumnName());
		Assert.Equal("creationDate", contractEntity.FindProperty("CreationDate")?.GetColumnName());
		Assert.Equal("startDate", contractEntity.FindProperty("StartDate")?.GetColumnName());
		Assert.Equal("completedDate", contractEntity.FindProperty("CompletedDate")?.GetColumnName());
		Assert.Equal("totalCost", contractEntity.FindProperty("Cost")?.GetColumnName());

		Assert.Null(contractEntity.FindProperty("_domainEvents"));
		Assert.Null(contractEntity.FindProperty("DomainEvents"));

		var typeConverter = contractEntity.FindProperty("Type")?.GetValueConverter();
		Assert.NotNull(typeConverter);
		Assert.Equal("FullMonth", typeConverter.ConvertToProvider.Invoke(ContractType.FullMonth));
		Assert.Equal(ContractType.FullMonth, typeConverter.ConvertFromProvider.Invoke("FullMonth"));

		var statusConverter = contractEntity.FindProperty("Status")?.GetValueConverter();
		Assert.NotNull(statusConverter);
		Assert.Equal("Created", statusConverter.ConvertToProvider.Invoke(ContractStatus.Created));
		Assert.Equal(ContractStatus.Created, statusConverter.ConvertFromProvider.Invoke("Created"));

		var costConverter = contractEntity.FindProperty("Cost")?.GetValueConverter();
		Assert.NotNull(costConverter);
		Assert.Equal(1500m, costConverter.ConvertToProvider.Invoke(new CostValue(1500m)));
		Assert.Equal(new CostValue(1500m), costConverter.ConvertFromProvider.Invoke(1500m));

		var deliveryDayEntity = model.FindEntityType(typeof(DeliveryDay));
		Assert.NotNull(deliveryDayEntity);
		Assert.Equal("deliverydays", deliveryDayEntity.GetTableName());

		Assert.Equal("deliveryDayId", deliveryDayEntity.FindProperty("Id")?.GetColumnName());
		Assert.Equal("contractId", deliveryDayEntity.FindProperty("ContractId")?.GetColumnName());
		Assert.Equal("street", deliveryDayEntity.FindProperty("Street")?.GetColumnName());
		Assert.Equal("number", deliveryDayEntity.FindProperty("Number")?.GetColumnName());
		Assert.Equal("longitude", deliveryDayEntity.FindProperty("Longitude")?.GetColumnName());
		Assert.Equal("latitude", deliveryDayEntity.FindProperty("Latitude")?.GetColumnName());
		Assert.Equal("date", deliveryDayEntity.FindProperty("Date")?.GetColumnName());

		var dateConverter = deliveryDayEntity.FindProperty("Date")?.GetValueConverter();
		Assert.NotNull(dateConverter);

		var localDate = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Local);
        var utc = dateConverter.ConvertToProvider.Invoke(localDate) as DateTime? ?? throw new InvalidOperationException("Conversion to UTC failed.");
		Assert.Equal(DateTimeKind.Utc, utc.Kind);
	}
}
