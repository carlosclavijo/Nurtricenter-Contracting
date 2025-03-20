using System;

namespace Contracting.Application.Administrators.GetAdministrators;

public class AdministratorDto
{
    public Guid Id { get; set; }
    public string AdministratorName { get; set; }
    public string AdministratorPhone { get; set; }
}
