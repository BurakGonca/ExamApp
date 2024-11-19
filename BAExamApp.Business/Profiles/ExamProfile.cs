using BAExamApp.Dtos.Dashboard;
using BAExamApp.Dtos.ExamClassrooms;
using BAExamApp.Dtos.Exams;
using BAExamApp.Entities.DbSets;

namespace BAExamApp.Business.Profiles;

public class ExamProfile : Profile
{
    public ExamProfile()
    {
        CreateMap<Exam, ExamDto>();
        CreateMap<Exam, ExamListDto>()
            .ForMember(dest => dest.ClassroomNames, opt => opt.MapFrom(src => src.ExamClassrooms.Select(ec => ec.Classroom.Name).ToList()))
            .ForMember(dest => dest.ExamRuleName, opt => opt.MapFrom(src => src.ExamRule.Name))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.StudentExams.Select(students => students.Student.FullName).ToList()))
            .ForMember(dest => dest.StudentExamScore, opt => opt.MapFrom(src => src.StudentExams.Select(studentExam => studentExam.Score).ToList()));



        CreateMap<Exam, ExamDetailDto>()
            .ForMember(dest => dest.ExamRuleName, opt => opt.MapFrom(src => src.ExamRule.Name))
            .ForMember(dest => dest.ClassroomName, opt => opt.MapFrom(src => String.Join(", ", src.ExamClassrooms.Select(ec => ec.Classroom.Name))));

        CreateMap<ExamCreateDto, Exam>()
            .ForMember(dest => dest.Name,
            config => config.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.StudentExams, opt => opt.MapFrom(src => src.StudentExams))
            .ForMember(dest => dest.ExamClassrooms, opt => opt.MapFrom(src => src.ExamClassroomsIds))
            .ForMember(dest => dest.ExamType, opt => opt.MapFrom(src => src.ExamType));

        CreateMap<DashboardEventDto, Exam>().ReverseMap();
    }
}
