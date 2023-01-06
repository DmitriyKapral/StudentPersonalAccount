using System.ComponentModel.DataAnnotations;

namespace StudentPersonalAccount.Interfaces;

public interface IGuidKey
{
    [Key]
    public Guid Guid { get; set; }
}
