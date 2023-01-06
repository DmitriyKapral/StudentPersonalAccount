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

namespace StudentPersonalAccount.Controllers.Students;

[ApiController]
[Route("/[controller]")]
public class StudentController : BaseCRUDController<Student>
{
    private readonly IRepository<Student> _repository;
    //private readonly PersonalAccountContext _context;
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
        var student = GetFullStudentInfoQuery();

        if (student is null)
            return BadRequest();

        List<StudentDataView> studentDataViews = new List<StudentDataView>();

        foreach (var item in student)
        {
            studentDataViews.Add(MappingStudentData(item));
        }

        return Ok(studentDataViews);
    }

    [HttpGet("{guid:guid}")]
    public override IActionResult Get(Guid guid)
    {
        var student = GetFullStudentInfoQuery()
            .FirstOrDefault(p => p.Guid == guid);

        if (student is null)
            return BadRequest();


        var data = MappingStudentData(student);

        return Ok(data);
    }

    [HttpGet("average/{guid:guid}")]
    public double? AverageEvalition(Guid guid)
    {
        var subjects = GetFullStudentInfoQuery()
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

    /*[HttpPost("test")]
    public IActionResult TestPost()
    {
        *//*List<Group> faculty = new()
        {
            new Group()
            {
                Name = "ИВТ-162",
                Faculty = _context.Faculties.FirstOrDefault(x => x.Name == "Факультет электроники и вычислительной техники")
            },
            new Group()
            {
                Name = "САПР-1.4",
                Faculty = _context.Faculties.FirstOrDefault(x => x.Name == "Факультет электроники и вычислительной техники")
            },
            new Group()
            {
                Name = "ХТМ-1",
                Faculty = _context.Faculties.FirstOrDefault(x => x.Name == "Факультет технологии пищевых производств")
            },
            new Group()
            {
                Name = "ЭВМ-1.1",
                Faculty = _context.Faculties.FirstOrDefault(x => x.Name == "Факультет электроники и вычислительной техники")
            },
            new Group()
            {
                Name = "САПР-1.3",
                Faculty = _context.Faculties.FirstOrDefault(x => x.Name == "Факультет электроники и вычислительной техники")
            },
            new Group()
            {
                Name = "САПР-1.2",
                Faculty = _context.Faculties.FirstOrDefault(x => x.Name == "Факультет электроники и вычислительной техники")
            },
            new Group()
            {
                Name = "САПР-1.1",
                Faculty = _context.Faculties.FirstOrDefault(x => x.Name == "Факультет электроники и вычислительной техники")
            },
        };*//*
        List<Student> faculty = new()
        {
            new Student()
            {
                Name = "Дмитрий",
                Surname = "Косолапов",
                Patronymic = "Константинович",
                Group = _context.Groups.FirstOrDefault(x => x.Name == "ЭВМ-1.1"),
                Subjects = _context.Subjects.ToList()
            },
            new Student()
            {
                Name = "Андрей",
                Surname = "Соколов",
                Patronymic = "Алексеевич",
                Group = _context.Groups.FirstOrDefault(x => x.Name == "САПР-1.4"),
                Subjects = _context.Subjects.ToList()
            },
            new Student()
            {
                Name = "Артём",
                Surname = "Соболев",
                Patronymic = "Алексеевич",
                Group = _context.Groups.FirstOrDefault(x => x.Name == "САПР-1.1"),
                Subjects = _context.Subjects.ToList()
            },
            new Student()
            {
                Name = "Леонид",
                Surname = "Косовов",
                Patronymic = "Евгеньевич",
                Group = _context.Groups.FirstOrDefault(x => x.Name == "ЭВМ-1.1"),
                Subjects = _context.Subjects.ToList()
            },
            new Student()
            {
                Name = "Андрей",
                Surname = "Меладзе",
                Patronymic = "Дмитриевич",
                Group = _context.Groups.FirstOrDefault(x => x.Name == "САПР-1.4"),
                Subjects = _context.Subjects.ToList()
            },
            new Student()
            {
                Name = "Дмитрий",
                Surname = "Беркут",
                Patronymic = "Валерьевич",
                Group = _context.Groups.FirstOrDefault(x => x.Name == "САПР-1.3"),
                Subjects = _context.Subjects.ToList()
            },
        };
        _context.Students.AddRange(faculty);
        _context.SaveChanges();
        return Ok(faculty);
    } */

    private IQueryable<Student> GetFullStudentInfoQuery()
    {
        return List
            .Include(x => x.Subjects)
            .ThenInclude(subjest => subjest.Evaluations)
            .Include(x => x.Group)
            .ThenInclude(group => group.Faculty);
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
}
