using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using Nur.Store2025.Observability.Tracing;

namespace Contracting.Infrastructure.Persistence.Repositories;

public class OutboxTracingService<T>(IOutboxService<T> BaseOutService, ITracingProvider TracingProvider) : IOutboxService<T>
{
	public async Task AddAsync(OutboxMessage<T> message)
	{
		OutboxMessage<T> outboxMessage = new(message.Content,
			TracingProvider.GetCorrelationId(),
			TracingProvider.GetTraceId(),
			TracingProvider.GetSpanId());

		await BaseOutService.AddAsync(outboxMessage);
	}
}

