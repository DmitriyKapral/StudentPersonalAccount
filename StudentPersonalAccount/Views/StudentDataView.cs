using DocumentFormat.OpenXml.Wordprocessing;
using StudentPersonalAccount.Models;

namespace StudentPersonalAccount.Views;

public class StudentDataView
{
    public Guid Guid { get; set; }
    public string Fio { get; set; }
    public Guid GroupId { get; set; }
    public string GroupName { get; set; }
    public Guid FacultyId { get; set; }
    public string FacultyName { get; set; }
    public virtual List<SubjectsView> Subjects { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        StudentDataView other = (StudentDataView)obj;

        return Guid == other.Guid &&
               Fio == other.Fio &&
               GroupId == other.GroupId &&
               FacultyId == other.FacultyId &&
               // Добавьте дополнительные проверки для свойств, если необходимо
               Subjects.SequenceEqual(other.Subjects);
    }

    public override int GetHashCode()
    {
        int hash = Guid.GetHashCode() ^ Fio.GetHashCode() ^ GroupId.GetHashCode() ^ FacultyId.GetHashCode();

        foreach (var subject in Subjects)
        {
            hash = (hash * 397) ^ subject.GetHashCode();
        }

        return hash;
    }
}
