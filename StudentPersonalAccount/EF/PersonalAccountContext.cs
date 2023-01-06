using Microsoft.EntityFrameworkCore;
using StudentPersonalAccount.Models;

namespace StudentPersonalAccount.EF;

public class PersonalAccountContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Auth> Auths { get; set; }

    public PersonalAccountContext(DbContextOptions<PersonalAccountContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Student>().Navigation(x => x.Evaluation).AutoInclude();
    }
}
