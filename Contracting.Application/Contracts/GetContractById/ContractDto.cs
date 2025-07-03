using Contracting.Application.Contracts.GetContracts;

namespace Contracting.Application.Contracts.GetContractById;

public class ContractDto
{
    public Guid Id { get; set; }
    public Guid AdministratorId { get; set; }
	public string AdministratorName { get; set; }
	public Guid PatientId { get; set; }
	public string PatientName { get; set; }
	public required string Type { get; set; }
    public required string Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompleteDate { get; set; }
    public decimal CostValue { get; set; }
	public required IEnumerable<DeliveryDayDto> DeliveryDays { get; set; }
}
