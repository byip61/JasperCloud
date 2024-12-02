using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperCloud.Models;

[Table("ai_consent")]
public class AIConsent
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [Column("is_consented")]
    public bool IsConsented { get; set; }
}