namespace Contracting.Infrastructure.Persistence;

public interface IDatabase : IDisposable
{
	void Migrate();
}
