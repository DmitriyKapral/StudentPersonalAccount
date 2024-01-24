using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using StudentPersonalAccount.EF;
using StudentPersonalAccount.Interfaces;
using StudentPersonalAccount.Models;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StudentPersonalAccount.Views;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace StudentPersonalAccount.Controllers.Students;

[ApiController]
[Route("/[controller]")]
public class StudentController : BaseCRUDController<Student>
{
    private readonly IRepository<Student> _repository;
    /*private readonly PersonalAccountContext _context;*/
    private readonly IMapper _mapper;

    public StudentController(IRepository<Student> repository,
        /*PersonalAccountContext context,*/
        IMapper mapper) : base(repository)
    {
        _mapper = mapper;
        _repository = repository;
        /*_context = context;*/
    }

    [HttpGet]
    public override IActionResult Get()
    {
        var student = ListWithAttachmentsAndFilter();

        if (student is null)
            return BadRequest();

        List<StudentDataView> studentDataViews = new List<StudentDataView>();

        foreach (var item in student)
        {
            studentDataViews.Add(MappingStudentData(item));
        }

        return Ok(studentDataViews);
    }

    //[Authorize]
    [HttpGet("{guid:guid}")]
    public override IActionResult Get(Guid guid)
    {
        var student = ListWithAttachmentsAndFilter()
            .FirstOrDefault(p => p.Guid == guid);

        if (student is null)
            return BadRequest();


        var data = MappingStudentData(student);

        return Ok(data);
    }

    [HttpGet("average/{guid:guid}")]
    public double? AverageEvalition(Guid guid)
    {
        var subjects = ListWithAttachmentsAndFilter()
            .FirstOrDefault(x => x.Guid == guid)
            ?.Subjects;

        if (subjects is null)
            return -1;

        List<int?> sum = new();

        subjects.ForEach(s =>
        {
            sum.Add(s.Evaluations?.Sum(x => x.Quantity) ?? 0);
        });

        return sum.Average();
    }

    [HttpGet("dataAllSubjects/{guid:guid}")]
    public IActionResult DataAllSubjectsForStudent(Guid guid)
    {
        var student = ListWithAttachmentsAndFilter()
            .FirstOrDefault(x => x.Guid == guid);

        if (student is null)
            return BadRequest();
        List<string> subjects = new();
        List<int> sumEvalitions = new();
        student.Subjects.ForEach(subject =>
        {
            subjects.Add(subject.Name);
            var evaluations = subject.Evaluations?.Where(s => s.StudentId == student.Guid);
            sumEvalitions.Add(evaluations?.Sum(x => x.Quantity) ?? 0);
        });

        return Ok(new { subjects, sumEvalitions });

    }

    private StudentDataView MappingStudentData(Student student)
    {
        var data = _mapper.Map<Student, StudentDataView>(student);

        data.FacultyId = student.Group.Faculty.Guid;
        data.FacultyName = student.Group.Faculty.Name;

        data.Subjects.ForEach(x =>
        {
            var evaluations = x.Evaluations?.Where(s => s.StudentId == student.Guid);
            x.SumEvaluatuion = evaluations?.Sum(s => s.Quantity) ?? 0;
            x.Evaluations = evaluations?.ToList();
        });
        return data;
    }

    protected override IQueryable<Student> ListWithAttachmentsAndFilter()
    {
        return _repository.GetListQuery()
            .Include(x => x.Subjects)
            .ThenInclude(subjest => subjest.Evaluations)
            .AsSplitQuery()
            .Include(x => x.Group)
            .ThenInclude(group => group.Faculty);
    }
}
