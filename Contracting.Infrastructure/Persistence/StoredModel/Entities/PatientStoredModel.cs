using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracting.Infrastructure.Persistence.StoredModel.Entities;

[Table("patients")]
public class PatientStoredModel
{
	[Key]
	[Column("patientId")]
	public Guid Id { get; set; }

	[Column("name")]
	[StringLength(100)]
	[Required]
	public string PatientName { get; set; }

	[Column("phone")]
	[StringLength(8)]
	[Required]
	public string PatientPhone { get; set; }
}
