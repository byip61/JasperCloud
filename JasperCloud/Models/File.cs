using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperCloud.Models;

[Table("file")]
public class File
{
    [Key]
    [Column("file_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
    public int Id { get; set; }

    [Column("file_path")]
    public string? FilePath { get; set; }
}