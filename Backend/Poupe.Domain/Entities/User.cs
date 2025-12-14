using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poupe.Domain.Entities;

[Table("tb_user", Schema = ("PoupeDB"))]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid? Id { get; set; }

    [Column("identifier")]
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Column("age")]
    [Required]
    public int Age { get; set; }
}
