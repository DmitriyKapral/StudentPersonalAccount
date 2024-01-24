using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPersonalAccount.EF;
using StudentPersonalAccount.Interfaces;
using StudentPersonalAccount.Models;

namespace StudentPersonalAccount.Controllers.Subjects;

[ApiController]
[Route("/[controller]")]
public class SubjectController : BaseCRUDController<Subject>
{
    private readonly IRepository<Subject> _repository;
    private readonly PersonalAccountContext _context;

    public SubjectController(IRepository<Subject> repository,
        PersonalAccountContext context) : base(repository)
    {
        _repository = repository;
        _context = context;
    }

    protected override IQueryable<Subject> ListWithAttachmentsAndFilter()
    {
        return _repository.GetListQuery()
            .Include(x => x.Students)
            .Include(x => x.Evaluations);
    }
}
