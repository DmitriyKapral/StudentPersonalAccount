using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using StudentPersonalAccount.EF;
using StudentPersonalAccount.Interfaces;
using StudentPersonalAccount.Models;
using System.ComponentModel;

namespace StudentPersonalAccount.Controllers.Evaluations;

[ApiController]
[Route("/[controller]")]
public class EvaluationController : BaseCRUDController<Evaluation>
{
    private readonly IRepository<Evaluation> _repository;
    private readonly PersonalAccountContext _context;

    public EvaluationController(IRepository<Evaluation> repository,
        PersonalAccountContext context) : base(repository)
    {
        _repository = repository;
        _context = context;
    }

    protected override IQueryable<Evaluation> ListWithAttachmentsAndFilter()
    {
        return _repository.GetListQuery()
            .Include(x => x.Subject)
            .Include(x => x.Student);
    }
}
