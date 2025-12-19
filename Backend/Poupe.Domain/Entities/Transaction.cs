using Microsoft.EntityFrameworkCore;
using Poupe.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poupe.Domain.Entities;

/// <summary>
/// Representa uma transação cadastrada no sistema.
/// </summary>
[Table("tb_transaction", Schema = ("PoupeDB"))]
public class Transaction
{
    /// <summary>
    /// Identificador único da transação.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// Breve descrição sobre a transação.
    /// </summary>
    [Column("description")]
    [Required]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Valor da transação.
    /// </summary>
    [Column("value")]
    [Precision(18, 2)]
    [Required]
    public decimal Value { get; set; }

    /// <summary>
    /// Tipo da transação (Despesa, Receita).
    /// </summary>
    [Column("type")]
    [Required]
    public TransactionType Type { get; set; }

    /// <summary>
    /// Categoria da transação.
    /// </summary>
    [Column("category_id")]
    [Required]
    public Category Category { get; set; }

    /// <summary>
    /// Usuário vinculado a transação.
    /// </summary>
    [Column("user_id")]
    [Required]
    public User User { get; set; }
}
