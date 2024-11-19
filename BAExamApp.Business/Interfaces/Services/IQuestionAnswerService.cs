using BAExamApp.Dtos.QuestionAnswers;

namespace BAExamApp.Business.Interfaces.Services;

public interface IQuestionAnswerService
{
    Task<IDataResult<QuestionAnswerDto>> GetById(Guid id,bool traking=true);
    Task<IDataResult<QuestionAnswerDto>> AddAsync(QuestionAnswerCreateDto questionAnswerCreateDto);
    Task<IDataResult<List<QuestionAnswerDto>>> AddRangeAsync(List<QuestionAnswerCreateDto> questionAnswersCreateDto);
    Task<IDataResult<List<QuestionAnswerDto>>> UpdateRangeAsync(List<QuestionAnswerCreateDto> questionAnswersUpdateDto);
    Task<IResult> DeleteAsync(Guid id);
    Task<IResult> DeleteRangeAsync(List<Guid> ids);
    Task<IDataResult<List<QuestionAnswerDto>>> GetByQuestionId(Guid Id);
    Task<IDataResult<List<QuestionAnswerListDto>>> GetAllAsync();
    Task<IDataResult<List<QuestionAnswerDto>>> Update(List<QuestionAnswerDto> questionAnswersUpdateDto);
}
