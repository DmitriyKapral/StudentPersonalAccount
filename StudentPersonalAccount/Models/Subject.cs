using Newtonsoft.Json;
using StudentPersonalAccount.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentPersonalAccount.Models;

public class Subject : IGuidKey
{
    [Key]
    public Guid Guid { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public virtual List<Student>? Students { get; set; }
    [JsonIgnore]
    public virtual List<Evaluation> Evaluations { get; set; }
}
