using AutoMapper;
using Moq;
using StudentPersonalAccount.Controllers.Students;
using StudentPersonalAccount.Interfaces;
using StudentPersonalAccount.Models;
using StudentPersonalAccount.Services;
using StudentPersonalAccount.Views;

namespace StudentPersonalAccountTest
{
    [TestClass]
    public class UnitTestStudent
    {

        [TestMethod]
        public void TestGet()
        {

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            Guid guid = Guid.NewGuid();
            IMapper mapper = mapperConfig.CreateMapper();
            var mock = new Mock<IRepository<Student>>();
            mock.Setup(repo => repo.GetListQuery()).Returns(GetStudents(guid));
            StudentController controller = new(mock.Object, mapper);

            var result = controller.Get().ToString();

            Assert.IsNotNull(result);
            //Assert.AreEqual(, result.ToString);
        }

        [TestMethod]
        public void TestAverage()
        {

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            Guid guid = Guid.NewGuid();
            var mock = new Mock<IRepository<Student>>();
            mock.Setup(repo => repo.GetListQuery()).Returns(GetStudents(guid));
            StudentController controller = new(mock.Object, mapper);

            var result = controller.AverageEvalition(guid);

            //Assert.IsNotNull(result);
            Assert.AreEqual(0, result);
            //Assert.AreEqual(, result.ToString);
        }

        private IQueryable<Student> GetStudents(Guid guid)
        {
            var students = new List<Student>()
            {
                new Student()
                {
                    Guid = guid,
                    Name = "�������",
                    Surname = "��������",
                    Patronymic = "��������������",
                    Group = new Group()
                    {
                        Name = "���-162",
                        Faculty = new Faculty()
                        {
                            Name = "����"
                        }
                    },
                    Subjects = new List<Subject>()
                    {
                        new Subject()
                        {
                            Name = "�������",
                            Evaluations = new List<Evaluation>()
                        }
                    },
                }
            };
            return students.AsQueryable();
        }

        private List<StudentDataView> StudentDataViews()
        {
            return new List<StudentDataView>()
            {
                new StudentDataView()
                {
                    Fio = "�������� ������� ��������������",
                    GroupName = "���-162",
                    FacultyName = "����",
                    Subjects = new List<SubjectsView>()
                    {
                        new SubjectsView()
                        {
                            Name = "�������",
                            SumEvaluatuion = 0,
                        }
                    }
                }
            };
        }
    }
}