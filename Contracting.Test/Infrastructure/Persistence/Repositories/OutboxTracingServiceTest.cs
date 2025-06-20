using Contracting.Infrastructure.Persistence.Repositories;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using Moq;
using Nur.Store2025.Observability.Tracing;

namespace Contracting.Test.Infrastructure.Persistence.Repositories;

public class OutboxTracingServiceTest
{
	[Fact]
	public async Task AddAsyncIsValid()
	{
		var tracingProviderMock = new Mock<ITracingProvider>();
		tracingProviderMock.Setup(x => x.GetCorrelationId()).Returns("correlation-id");
		tracingProviderMock.Setup(x => x.GetTraceId()).Returns("trace-id");
		tracingProviderMock.Setup(x => x.GetSpanId()).Returns("span-id");
		var baseOutServiceMock = new Mock<IOutboxService<string>>();
		var service = new OutboxTracingService<string>(
			baseOutServiceMock.Object,
			tracingProviderMock.Object
		);

		var originalMessage = new OutboxMessage<string>("original-content");

		await service.AddAsync(originalMessage);

		baseOutServiceMock.Verify(x => x.AddAsync(It.Is<OutboxMessage<string>>(msg =>
			msg.Content == "original-content" &&
			msg.CorrelationId == "correlation-id" &&
			msg.TraceId == "trace-id" &&
			msg.SpanId == "span-id"
		)), Times.Once);

		tracingProviderMock.Verify(x => x.GetCorrelationId(), Times.Once);
		tracingProviderMock.Verify(x => x.GetTraceId(), Times.Once);
		tracingProviderMock.Verify(x => x.GetSpanId(), Times.Once);
	}
}
