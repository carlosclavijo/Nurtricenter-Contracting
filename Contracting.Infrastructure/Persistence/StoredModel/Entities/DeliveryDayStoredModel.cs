﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracting.Infrastructure.Persistence.StoredModel.Entities;

[Table("deliverydays")]
public class DeliveryDayStoredModel
{
	[Key]
	[Column("deliveryDayId")]
	public Guid DeliveryDayId { get; set; }

	[Required]
	[Column("contractId")]
	public Guid ContractId { get; set; }
	public ContractStoredModel Contract { get; set; }

	[Required]
	[Column("date")]
	public DateTime Date { get; set; }

	[Required]
	[Column("street")]
	[StringLength(100)]
	public string Street { get; set; }

	[Required]
	[Column("number")]
	public int Number { get; set; }

	[Required]
	[Column("longitude")]
	public double Longitude { get; set; }

	[Required]
	[Column("latitude")]
	public double Latitude { get; set; }
}
