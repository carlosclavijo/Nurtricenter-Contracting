using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracting.Application.Contracts.GetContracts;

public class DeliveryDayDto
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public string Street { get; set; }
    public int Number {  get; set; }
    public DateTime DateTime { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}
