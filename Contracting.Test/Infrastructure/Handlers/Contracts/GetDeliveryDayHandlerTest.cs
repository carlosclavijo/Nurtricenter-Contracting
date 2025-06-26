using Contracting.Application.Contracts.GetDeliveryDay;
using Contracting.Infrastructure.Handlers.Contracts;
using Contracting.Infrastructure.Persistence.StoredModel.Entities;
using Contracting.Infrastructure.Persistence.StoredModel;
using Microsoft.EntityFrameworkCore;

namespace Contracting.Test.Infrastructure.Handlers.Contracts;

public class GetDeliveryDayHandlerTest
{
	private readonly DbContextOptions<StoredDbContext> _options;

	public GetDeliveryDayHandlerTest()
	{
		_options = new DbContextOptionsBuilder<StoredDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
	}

	[Fact]
	public async Task HandleIsValidTest()
	{
		// Arrange
		var deliveryDayId = Guid.NewGuid();
		var contractId = Guid.NewGuid();
		var deliveryDay = new DeliveryDayStoredModel
		{
			Id = deliveryDayId,
			ContractId = contractId,
			Date = DateTime.UtcNow,
			Street = "Calle Falsa",
			Number = 123
		};

		using (var context = new StoredDbContext(_options))
		{
			context.DeliveryDay.Add(deliveryDay);
			await context.SaveChangesAsync();
		}

		using (var context = new StoredDbContext(_options))
		{
			var handler = new GetDeliveryDayHandler(context);
			var query = new GetDeliveryDayQuery(deliveryDayId);
			var result = await handler.Handle(query, CancellationToken.None);

			Assert.NotNull(result);
			Assert.True(result.IsSuccess);
			Assert.Equal(deliveryDayId, result.Value.Id);
			Assert.Equal(contractId, result.Value.ContractId);
			Assert.Equal("Calle Falsa", result.Value.Street);
			Assert.Equal(123, result.Value.Number);
		}
	}
}
