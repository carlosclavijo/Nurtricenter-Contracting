using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Application.Contracts.GetContracts;

namespace Contracting.Application.Contracts.GetContractById;

public class ContractDto
{
    public Guid Id { get; set; }
    public Guid AdministratorId { get; set; }
    public Guid PatientId { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompleteDate { get; set; }
    public decimal CostValue { get; set; }
    public IEnumerable<DeliveryDayDto> DeliveryDays { get; set; }
}
