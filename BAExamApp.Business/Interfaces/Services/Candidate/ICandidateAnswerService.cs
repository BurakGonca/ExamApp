using BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;
using BAExamApp.Dtos.QuestionAnswers;

namespace BAExamApp.Business.Interfaces.Services.Candidate
{
    public interface ICandidateAnswerService
    {
        Task<IDataResult<CandidateAnswerDto>> GetById(Guid id, bool traking = true);
        Task<IDataResult<CandidateAnswerDto>> AddAsync(CandidateAnswerCreateDto candidateQuestionAnswerCreateDto);
        Task<IDataResult<List<CandidateAnswerDto>>> AddRangeAsync(List<CandidateAnswerCreateDto> candidateQuestionAnswersCreateDto);
        Task<IDataResult<CandidateAnswerDto>> UpdateAsync(CandidateAnswerUpdateDto candidateQuestionAnswersUpdateDto);
        Task<IDataResult<List<CandidateAnswerDto>>> UpdateRangeAsync(List<CandidateAnswerCreateDto> candidateQuestionAnswersUpdateDto);
        Task<IResult> DeleteAsync(Guid id);
        Task<IResult> DeleteRangeAsync(List<Guid> ids);
        Task<IDataResult<List<CandidateAnswerDto>>> GetByQuestionId(Guid id);
    }
}
