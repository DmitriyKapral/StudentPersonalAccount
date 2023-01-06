using Newtonsoft.Json;
using StudentPersonalAccount.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPersonalAccount.Models;

public class Auth : IGuidKey
{
    [Key]
    public Guid Guid { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Access { get; set; }
    public Guid StudentId { get; set; }
    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }
}
