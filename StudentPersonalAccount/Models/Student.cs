using Newtonsoft.Json;
using StudentPersonalAccount.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPersonalAccount.Models;

public class Student : IGuidKey
{
    [Key]
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public Guid GroupId { get; set; }
    [ForeignKey("GroupId")]
    [JsonIgnore]
    public virtual Group Group { get; set; }
    [JsonIgnore]
    public virtual List<Subject> Subjects { get; set; }
    //[JsonIgnore]
    //public virtual List<Evaluation> Evaluation { get; set; }

}
