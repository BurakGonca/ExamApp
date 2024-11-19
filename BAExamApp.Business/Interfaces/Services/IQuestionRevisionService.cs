using BAExamApp.Dtos.QuestionRevisions;

namespace BAExamApp.Business.Interfaces.Services;
public interface IQuestionRevisionService
{
    Task<List<QuestionRevisionListDto>> GetAllAsync();
    Task<IResult> AddAsync(QuestionRevisionCreateDto questionRevisionCreateDto);
    Task<QuestionRevisionDto> GetByIdAsync(Guid id);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<QuestionRevisionDto>> UpdateAsync(QuestionRevisionUpdateDto questionRevisionUpdateDto);
    Task<List<QuestionRevisionListDto>> GetAllByQuestionId(Guid questionId);
    Task<QuestionRevisionDto> GetActiveByQuestionId(Guid questionId);
    Task<bool> AnyActive(Guid questionId);
}
