using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace JasperCloud.Models;

[Table("user_information")]
[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    [Column("user_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("username")]
    [MaxLength(50)]
    public string Username { get; set; }

    [Column("email")]
    [MaxLength(255)]
    public string Email { get; set; }

    [Column("password")]
    public string PasswordHash { get; set; }

    [Column("salt")]
    public string PasswordSalt { get; set; }
}