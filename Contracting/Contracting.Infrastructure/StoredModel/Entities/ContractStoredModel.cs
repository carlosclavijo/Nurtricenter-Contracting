using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracting.Infrastructure.StoredModel.Entities;

[Table("contracts")]
internal class ContractStoredModel
{
    [Key]
    [Column("contractId")]
    public Guid Id { get; set; }

    [Required]
    [Column("administratorId")]
    public Guid AdministratorId { get; set; }
    public AdministratorStoredModel Administrator { get; set; }

    [Required]
    [Column("patientId")]
    public Guid PatientId { get; set; }
    public PatientStoredModel Patient { get; set; }

    [Required]
    [Column("transactionType")]
    [MaxLength(20)]
    public string TransactionType { get; set; }

    [Required]
    [Column("transactionStatus")]
    [MaxLength(20)]
    public string TransactionStatus { get; set; }

    [Required]
    [Column("creationDate", TypeName = "timestamp")]
    public DateTime CreationDate { get; set; }

    [Required]
    [Column("startDate", TypeName = "timestamp")]
    public DateTime StartDate { get; set; }

    [Column("completedDate", TypeName = "timestamp")]
    public DateTime? CompletedDate { get; set; }

    [Column("totalCost", TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }

    public List<DeliveryDayStoredModel> DeliveryDays { get; set; }
}
