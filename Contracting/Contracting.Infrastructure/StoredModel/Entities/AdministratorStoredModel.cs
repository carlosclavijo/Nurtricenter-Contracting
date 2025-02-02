using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracting.Infrastructure.StoredModel.Entities;

[Table("administrators")]
internal class AdministratorStoredModel
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
