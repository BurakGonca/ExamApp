using BAExamApp.Dtos.TestExamsTesters;

namespace BAExamApp.Business.Profiles;
public class TestExamTesterProfile : Profile
{
    public TestExamTesterProfile()
    {
        CreateMap<TestExamsTesterDto, TestExamTester>();
        CreateMap<TestExamTester, TestExamsTesterDto>();
    }
}
