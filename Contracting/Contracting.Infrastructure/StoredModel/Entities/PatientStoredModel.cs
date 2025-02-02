using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracting.Infrastructure.StoredModel.Entities;

[Table("patients")]
internal class PatientStoredModel
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
