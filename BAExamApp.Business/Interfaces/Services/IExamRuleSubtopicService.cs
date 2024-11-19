namespace BAExamApp.Business.Interfaces.Services;

public interface IExamRuleSubtopicService
{

    /// <summary>
    /// sınav oluşturmak için seçilen ilgili kuralın subjectini getirir
    /// </summary>
    /// <param name="id"> ilgili kural subjectini getirmek için kuralın id bilgisini kullanır </param>
    /// <returns> sorguya uyan kural subjectlerini listeleyerek döner </returns>
    Task<IDataResult<List<ExamRuleSubtopic>>> GetAllAsync(Guid id);
}
