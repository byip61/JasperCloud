using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperCloud.Models;

[Table("file_metadata")]
public class FileMetadata
{
    [Key]
    [Column("file_id")]
    public int FileId { get; set; }

    [ForeignKey("FileId")]
    public virtual File File { get; set; }

    [Column("file_name")]
    public string Name { get; set; }

    [Column("file_extension")]
    public string Extension{ get; set; }

    [Column("date_created")]
    public DateTime DateCreated { get; set; }

    [Column("date_modified")]
    public DateTime DateModified { get; set; }

    [Column("size")]
    public int Size { get; set; }
}