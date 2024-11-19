using BAExamApp.Dtos.QuestionArranges;

namespace BAExamApp.Business.Interfaces.Services;

public interface IQuestionArrangeService
{
    /// <summary>
    /// Onaylanan sorunun id'sine göre o soruya yapılan düzenleme yorumlarını getirir
    /// </summary>
    /// <param name="id"></param>
    /// <returns>QuestionArrangeListDto listesi döner</returns>
    Task<List<QuestionArrangeListDto>> GetAllByQuestionIdAsync(Guid id);
    /// <summary>
    /// Onaylanan Sorular için yapılan düzenleme yorumlarının databaseye eklenmesini sağlar.
    /// </summary>
    /// <param name="questionFeedbackCreate"></param>
    /// <returns>QuestionArrangeDto</returns>
    Task<IDataResult<QuestionArrangeDto>> AddAsync(QuestionArrangeCreateDto questionArrangeCreate);
}
