using Poupe.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poupe.Domain.Entities;

[Table("tb_category", Schema = ("PoupeDB"))]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid? Id { get; set; }

    [Column("description")]
    [Required]
    public string Description { get; set; } = null!;

    [Column("purpose")]
    [Required]
    public CategoryType Purpose { get; set; }
}
