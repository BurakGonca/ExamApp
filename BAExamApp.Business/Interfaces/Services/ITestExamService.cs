using BAExamApp.Dtos.TestExams;

namespace BAExamApp.Business.Interfaces.Services;
public interface ITestExamService
{
    Task<IDataResult<TestExamDto>> CreateTestExamAsync(TestExamCreateDto examCreateDto);
}
