using BAExamApp.Dtos.Questions;
using BAExamApp.Entities.Enums;

namespace BAExamApp.Business.Interfaces.Services;

public interface IQuestionService
{
    Task<IDataResult<QuestionDto>> GetByIdAsync(Guid id);
    Task<IDataResult<List<QuestionListDto>>> GetAllAsync();
    Task<IDataResult<List<QuestionListDto>>> GetAllByStateAsync(State state);
    Task<IDataResult<List<QuestionListDto>>> GetQuestionBySearchValues(string? content, string? subject, string? subtopic, string? questionDifficulty, string? questionCreatedDate,State state);
    /// <summary>
    /// Belirtilen soru durumuna göre eğitmene ait tüm soruları getirir.
    /// </summary>
    /// <param name="trainerIdentityId">İşlemi yapan eğitmenin identity id'si</param>
    /// <param name="trainerId">İşlemi yapan eğitmenin id'si</param>
    /// <param name="state">Listelenmek istenen soru durumu</param>
    /// <returns>Soru listeme modelini içeren sonuç nesnesi</returns>
    Task<IDataResult<List<QuestionListDto>>> GetAllByStateAndTrainerIdAsync(string trainerIdentityId, string trainerId, State state);
    Task<IDataResult<List<QuestionListDto>>> GetAllByFilterAsync(QuestionFilterDto questionFilterDto);
    /// <summary>
    /// sınav kuralında olması istenen özelliklerdeki soruları sorgular ve liste olarak getirir.
    /// </summary>
    /// <param name="questionDifficultyId"> seçilen kuralın içeriğindeki sorunun istenen leveli </param>
    /// <param name="questionType"> seçilen kuralın içeriğindeki sorunun tipi</param>
    /// <param name="subjectId"> seçilen kuralın içeriğindeki subject id'si</param>
    /// <returns> istenen özelliklerdeki soruları listeleyerek döner </returns>
    Task<IDataResult<List<QuestionListDto>>> GetAllByExamRuleSubtopicAsync(Guid questionDifficultyId, int questionType, List<Guid> subtopicId);
    Task<IDataResult<QuestionDetailsDto>> GetDetailsByIdAsync(Guid id);
    Task<IDataResult<QuestionDetailsForAdminDto>> GetDetailsByIdForAdminAsync(Guid id);
    /// <summary>
    /// Yöneticinin tarafından tekrar gözden geçirilmesi istenen tüm soruları getirir.
    /// </summary>
    /// <returns>IDataResult<List<QuestionRevisedListDto>> tipinde soruları barındıran data objesi.</returns>
    /// <summary>
    /// Verilen filtreye uygun soruları getirir
    /// </summary>
    /// <param name="model">Filtre modeli</param>
    /// <returns>IDataResult<List<QuestionListDto>> tipinde soruları barındıran data objesi</returns>
    Task<IDataResult<QuestionDto>> AddAsync(QuestionCreateDto questionCreateDto);
    /// <summary>
    /// Soruyu database üzerinde günceller.
    /// </summary>
    /// <param name="questionUpdateDto">Güncellenecek bilgileri içeren soru modeli</param>
    /// <returns>Güncellenen soruyu döndürür</returns>
    Task<IDataResult<QuestionDto>> UpdateAsync(QuestionUpdateDto questionUpdateDto);
    Task<IResult> UpdateStateAsync(Guid id, State state);

    /// <summary>
    /// Reddedilen sorulara reddedilme nedeni eklenerek update eder
    /// </summary>
    /// <param name="id">Soruların id'si</param>
    /// <param name="state">Soruların state'i</param>
    /// <param name="rejectComment">Reddedilme yorumu</param>
    /// <returns>IResult döndürür. İşlem başarılı mı değil mi?</returns>
    Task<IResult> UpdateStateWithCommentAsync(Guid id, State state, string rejectComment);
    /// <summary>



    /// <summary>
    /// Silinen sorular veritabanından silinmez status'unu passive'e çeker
    /// </summary>
    /// <param name="questionId">Soruların id'si</param>
    /// <returns>IResult döndürür</returns>

    Task<IResult> DeleteByStatusAsync(Guid questionId);
    Task<IResult> SetIsActive(Guid id, bool isActive);
    public Task<bool> IsRevisionWanted(Guid questionId);

    /// <summary>
    /// subtopicId'e ait tüm aktif ve onaylanmış soruların listesini getirir.
    /// </summary>
    /// <param name="subtopicId">alt konu id'si</param>
    /// <returns>Task<IDataResult<List<QuestionListDto>>> döndürür.</returns>
    Task<IDataResult<List<QuestionListDto>>> GetQuestionBySubtopicId(Guid subtopicId);

    /// <summary>
    /// Eğitmene ait soruların seçilen filtrelere göre listelenmesini sağlayan metot.
    /// </summary>
    /// <param name="content"></param>
    /// <param name="subject"></param>
    /// <param name="subtopics"></param>
    /// <param name="questionDifficulty"></param>
    /// <param name="questionCreatedDate"></param>
    /// <param name="state">Listelenmek istenen soru durumu</param>
    /// <param name="trainerIdentityId">İşlemi yapan eğitmenin identity id'si</param>
    /// <param name="trainerId">İşlemi yapan eğitmenin id'si</param>
    /// <returns>Task<IDataResult<List<QuestionListDto>>> döndürür.</returns>
    Task<IDataResult<List<QuestionListDto>>> GetQuestionBySearchValuesByTrainerId(string? content, string? subject, string? subtopics, string? questionDifficulty, string? questionCreatedDate, State state, string? trainerIdentityId, string? trainerId);
}