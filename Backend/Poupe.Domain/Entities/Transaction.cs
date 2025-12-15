using Poupe.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poupe.Domain.Entities;

[Table("tb_transaction", Schema = ("PoupeDB"))]
public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid? Id { get; set; }

    [Column("description")]
    [Required]
    public string Description { get; set; } = null!;

    [Column("value")]
    [Required]
    public decimal Value { get; set; }

    [Column("type")]
    [Required]
    public TransactionType Type { get; set; }

    [Column("category_id")]
    [Required]
    public Guid CategoryId { get; set; }

    [Column("user_id")]
    [Required]
    public Guid UserId { get; set; }
}
