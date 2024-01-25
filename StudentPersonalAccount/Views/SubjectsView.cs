using StudentPersonalAccount.Models;

namespace StudentPersonalAccount.Views;

public class SubjectsView
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public virtual List<EvaliationView>? Evaluations { get; set; }
    public int? SumEvaluatuion { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        SubjectsView other = (SubjectsView)obj;

        return Guid == other.Guid &&
               Name == other.Name &&
               Evaluations.SequenceEqual(other.Evaluations);
    }

    public override int GetHashCode()
    {
        int hash = Guid.GetHashCode() ^ Name.GetHashCode();

        foreach (var evaluation in Evaluations)
        {
            hash = (hash * 397) ^ evaluation.GetHashCode();
        }

        return hash;
    }
}
