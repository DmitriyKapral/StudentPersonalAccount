using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using StudentPersonalAccount.EF;
using StudentPersonalAccount.Interfaces;
using StudentPersonalAccount.Models;
using System.ComponentModel;

namespace StudentPersonalAccount.Controllers.Groups;

[ApiController]
[Route("/[controller]")]
public class GroupController : BaseCRUDController<Group>
{
    private readonly IRepository<Group> _repository;
    private readonly PersonalAccountContext _context;

    public GroupController(IRepository<Group> repository,
        PersonalAccountContext context) : base(repository)
    {
        _repository = repository;
        _context = context;
    }

    protected override IQueryable<Group> ListWithAttachmentsAndFilter()
    {
        return _repository.GetListQuery()
            .Include(x => x.Faculty);
    }
}
