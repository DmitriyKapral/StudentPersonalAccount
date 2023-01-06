using Newtonsoft.Json;
using StudentPersonalAccount.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPersonalAccount.Models;

public class Evaluation : IGuidKey
{
    [Key]
    public Guid Guid { get; set; }
    public int Quantity { get; set; }
    public DateTime DateTime { get; set; }
    /*[JsonIgnore]
    public virtual List<Subject>? Subjects { get; set; }*/
    public Guid SubjectId { get; set; }
    [ForeignKey("SubjectId")]
    [JsonIgnore]
    public virtual Subject Subject { get; set; }
    public Guid StudentId { get; set; }
    [ForeignKey("StudentId")]
    [JsonIgnore]
    public virtual Student Student { get; set; }
}
