using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Contracting.Infrastructure.Persistence.StoredModel.Entities;

[Table("administrators")]
public class AdministratorStoredModel
{
	[Key]
	[Column("administratorId")]
	public Guid Id { get; set; }

	[Column("name")]
	[StringLength(100)]
	[Required]
	public string AdministratorName { get; set; }

	[Column("phone")]
	[StringLength(8)]
	[Required]
	public string AdministratorPhone { get; set; }
}
