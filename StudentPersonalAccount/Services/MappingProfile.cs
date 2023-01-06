using AutoMapper;
using StudentPersonalAccount.Models;
using StudentPersonalAccount.Views;

namespace StudentPersonalAccount.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentDataView>()
            .ForMember(dest => dest.Fio, opt => opt.MapFrom(src => $"{src.Surname} {src.Name} {src.Patronymic}"));
        CreateMap<Subject, SubjectsView>();
        CreateMap<Evaluation, EvaliationView>();
    }
}
