namespace Contracting.Domain.Administrators;

public class AdministratorFactory : IAdministratorFactory
{
    public Administrator Create(string administratorName, string administratorPhone)
    {
        if (string.IsNullOrWhiteSpace(administratorName))
        {
            throw new ArgumentException("Administrator name is required", nameof(administratorName));
        }
        if (string.IsNullOrWhiteSpace(administratorPhone))
        {
            throw new ArgumentException("Administrator phone is required", nameof(administratorPhone));
        }
        return new Administrator(administratorName, administratorPhone);
    }
}