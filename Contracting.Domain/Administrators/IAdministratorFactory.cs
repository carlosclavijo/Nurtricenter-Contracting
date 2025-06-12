namespace Contracting.Domain.Administrators;

public interface IAdministratorFactory
{
    Administrator Create(string administratorName, string administratorPhone);
}