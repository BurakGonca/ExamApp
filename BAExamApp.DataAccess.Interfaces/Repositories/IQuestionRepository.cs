using BAExamApp.Core.Utilities.Results;
using System.Linq.Expressions;

namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IQuestionRepository : IAsyncRepository, IAsyncInsertableRepository<Question>, IAsyncQueryableRepository<Question>, IAsyncFindableRepository<Question>, IAsyncUpdateableRepository<Question>, IAsyncDeleteableRepository<Question>
{
    Task<IEnumerable<Question>> GetAllWithIncludeAsync(Expression<Func<Question, bool>> expression, Expression<Func<Question, object>> include, bool tracking = true);
    Task<IResult> DeleteQuestionByStatus(Guid questionId);

    // Belirli bir kimliğe sahip soruyu ve revizyonlarını almak için asenkron olarak kullanılan metot.
    Task<Question?> GetByIdWithIncludeRevisionAsync(Guid id, bool tracking = true);


    Task<Question?> GetByIdWithStudentAnswersAsync(Guid id, bool tracking = true);



    /// <summary>
    /// Belli bir id'ye sahip soruya trackable özelliği true olan öğrenci cevaplarından kontrol eder doğru olanların sayısını döndürür
    /// </summary>
    /// <param name="id"></param>
    /// <param name="trackable"></param>
    /// <returns>doğru cevap sayısı</returns>
    public Task<int> CorrectQuestionCount(Guid id, bool trackable = true);



    /// <summary>
    /// Belli bir id'ye sahip soruya trackable özelliği true olan öğrenci cevaplarından kontrol eder boş olanların sayısını döndürür
    /// </summary>
    /// <param name="id"></param>
    /// <param name="trackable"></param>
    /// <returns>boş cevap sayısı </returns>
    public Task<int> EmptyQuestionCount(Guid id, bool trackable = true);


    /// <summary>
    /// Belli bir id'ye sahip soruya trackable özelliği true olan öğrenci cevaplarından kontrol eder yanlış olanların sayısını döndürür
    /// </summary>
    /// <param name="id"></param>
    /// <param name="trackable"></param>
    /// <returns>yanlış cevap sayısı </returns>
    public Task<int> IncorrectQuestionCount(Guid id, bool trackable = true);
}
