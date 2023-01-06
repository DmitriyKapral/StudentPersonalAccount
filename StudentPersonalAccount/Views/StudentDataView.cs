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
}
