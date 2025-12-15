using Poupe.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poupe.Domain.Entities;

/// <summary>
/// Representa uma categoria cadastrada no sistema.
/// </summary>
[Table("tb_category", Schema = ("PoupeDB"))]
public class Category
{
    /// <summary>
    /// Identificador único da categoria.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// Descrição sobre o que a categoria representa.
    /// </summary>
    [Column("description")]
    [Required]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Finalidade da categoria (Despesa, Receita, Ambas).
    /// </summary>
    [Column("purpose")]
    [Required]
    public CategoryType Purpose { get; set; }
}
