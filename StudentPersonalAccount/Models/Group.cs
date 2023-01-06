using Newtonsoft.Json;
using StudentPersonalAccount.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPersonalAccount.Models;

public class Group : IGuidKey
{
    [Key]
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Guid FacultyId { get; set; }
    [ForeignKey("FacultyId")]
    [JsonIgnore]
    public virtual Faculty Faculty { get; set; }
}
