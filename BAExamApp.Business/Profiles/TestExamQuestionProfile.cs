using BAExamApp.Dtos.TestExamQuestions;

namespace BAExamApp.Business.Profiles;
public class TestExamQuestionProfile : Profile
{
    public TestExamQuestionProfile()
    {
        CreateMap<TestExamQuestionDto, TestExamQuestion>();
        CreateMap<TestExamQuestion, TestExamQuestionDto>();
    }
}
