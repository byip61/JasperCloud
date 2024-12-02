using System.ComponentModel.DataAnnotations.Schema;

namespace JasperCloud.Models;

[Table("file")]
public class File
{
    [Column("file_guid")]
    public Guid FileGuid { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [Column("file_name")]
    public string Name { get; set; }

    [Column("file_extension")]
    public string Extension{ get; set; }

    [Column("size")]
    public long Size { get; set; }

    [Column("upload_date")]
    public DateTime UploadDate { get; set; }
}