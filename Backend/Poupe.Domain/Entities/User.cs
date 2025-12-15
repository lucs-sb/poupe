using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poupe.Domain.Entities;

/// <summary>
/// Representa uma pessoa cadastrada no sistema.
/// </summary>
[Table("tb_user", Schema = ("PoupeDB"))]
public class User
{
    /// <summary>
    /// Identificador único da pessoa.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// Nome completo da pessoa.
    /// </summary>
    [Column("name")]
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Idade da pessoa.
    /// </summary>
    [Column("age")]
    [Required]
    public int Age { get; set; }
}
