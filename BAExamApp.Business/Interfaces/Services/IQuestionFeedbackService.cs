using BAExamApp.Dtos.QuestionFeedbacks;

namespace BAExamApp.Business.Interfaces.Services;
public interface IQuestionFeedbackService
{
    /// <summary>
    /// Sorunun id'sine göre o soruya yapılan yorumları getirir
    /// </summary>
    /// <param name="id"></param>
    /// <returns>QuestionCommentDto listesi döner</returns>
    Task<IDataResult<List<QuestionFeedbackListDto>>> GetAllByQuestionIdAsync(Guid id);
    /// <summary>
    /// Sorular için yapılan yorumların databaseye eklenmesini sağlar.
    /// </summary>
    /// <param name="questionFeedbackCreate"></param>
    /// <returns>QuestionCommentDto</returns>
    Task<IDataResult<QuestionFeedbackDto>> AddAsync(QuestionFeedbackCreateDto questionFeedbackCreate);
}
