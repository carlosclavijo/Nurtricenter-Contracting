namespace Contracting.Application.Contracts.GetContracts;

public class DeliveryDayDto
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public required string Street { get; set; }
    public int Number {  get; set; }
    public DateTime DateTime { get; set; }
}
