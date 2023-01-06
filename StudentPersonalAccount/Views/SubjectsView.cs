namespace StudentPersonalAccount.Views;

public class SubjectsView
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public virtual List<EvaliationView>? Evaluations { get; set; }
    public int? SumEvaluatuion { get; set; }
}
