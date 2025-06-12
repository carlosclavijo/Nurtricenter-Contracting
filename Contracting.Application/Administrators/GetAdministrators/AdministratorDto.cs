namespace Contracting.Application.Administrators.GetAdministrators;

public class AdministratorDto
{
    public Guid Id { get; set; }
    public required string AdministratorName { get; set; }
    public required string AdministratorPhone { get; set; }
}
