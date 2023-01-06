using StudentPersonalAccount.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentPersonalAccount.Models;

public class Faculty : IGuidKey
{
    [Key]
    public Guid Guid { get; set; }
    public string Name { get; set; }
}
