using BAExamApp.Dtos.TestExams;

namespace BAExamApp.Business.Profiles;
public class TestExamProfile : Profile
{
    public TestExamProfile()
    {
        CreateMap<TestExamDto, TestExam>();
        CreateMap<TestExam, TestExamDto>();

        CreateMap<TestExam, TestExamCreateDto>();
        CreateMap<TestExamCreateDto, TestExam>();
    }
}
