namespace Contracting.Application.Abstractions;

public interface ICorrelationIdProvider
{
	string GetCorrelationId();
	void SetCorrelationId(string correlationId);
}
