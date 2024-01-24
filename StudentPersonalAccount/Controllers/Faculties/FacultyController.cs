using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using StudentPersonalAccount.EF;
using StudentPersonalAccount.Interfaces;
using StudentPersonalAccount.Models;
using System.ComponentModel;

namespace StudentPersonalAccount.Controllers.Faculties;

[ApiController]
[Route("/[controller]")]
public class FacultyController : BaseCRUDController<Faculty>
{
    private readonly IRepository<Faculty> _repository;
    private readonly PersonalAccountContext _context;

    public FacultyController(IRepository<Faculty> repository,
        PersonalAccountContext context) : base(repository)
    {
        _repository = repository;
        _context = context;
    }

    protected override IQueryable<Faculty> ListWithAttachmentsAndFilter()
    {
        return _repository.GetListQuery();
    }
}
