using BAExamApp.Business.Constants;
using BAExamApp.Core.Entities.Interfaces;
using BAExamApp.Core.Enums;
using BAExamApp.Core.Utilities.Results;
using BAExamApp.Core.Utilities.Results.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BAExamApp.DataAccess.EFCore.Repositories;

public class QuestionRepository : EFBaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(BAExamAppDbContext context) : base(context) {}

    public async Task<IResult> DeleteQuestionByStatus(Guid questionId)
    {
        var questionToDelete = await GetByIdAsync(questionId);
        if (questionToDelete != null)
        {
            questionToDelete.Status = Status.Deleted;
            try
            {
                await SaveChangesAsync();
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
        else
        {
            return new ErrorResult(Messages.QuestionNotFound);
        }
    }

    public async Task<IEnumerable<Question>> GetAllWithIncludeAsync(Expression<Func<Question, bool>> expression, Expression<Func<Question, object>> include, bool tracking = true)
    {
        return await GetAllActives(tracking).Include(include).Where(expression).ToListAsync();
    }

    // Belirli bir kimliğe sahip soruyu ve revizyonlarını almak için kullanılan metot.
    public async Task<Question?> GetByIdWithIncludeRevisionAsync(Guid id, bool tracking = true)
    {
        var values = GetAllActives(tracking);

        if (tracking)
        {
            values = values.Include(x => x.QuestionRevisions);
        }
        else
        {
            values = values.Include(x => x.QuestionRevisions).AsNoTracking();
        }

        return await values.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    ///  Belli bir id'ye sahip soruyu QuestionAnswerlar ile getirir
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tracking">Takip (tracking) durumu. True ise değişiklikler izlenir, false ise izlenmez.</param>
    /// <returns>Question</returns>
    public async Task<Question?> GetByIdWithStudentAnswersAsync(Guid id, bool tracking = true)
    {
        IQueryable<Question> query = GetAllActives(tracking);

        var question = await query
            .Include(q => q.StudentQuestions)
                .ThenInclude(sq => sq.StudentAnswers)
            .FirstOrDefaultAsync(q => q.Id == id);

        return question;
    }
    /// <summary>
    /// Belli bir id'ye sahip soruya trackable özelliği true olan öğrenci cevaplarından kontrol eder doğru olanların sayısını döndürür
    /// </summary>
    /// <param name="id"></param>
    /// <param name="trackable"></param>
    /// <returns>doğru cevap sayısı</returns>
    public async Task<int> CorrectQuestionCount(Guid id, bool trackable = true)
    {
        IQueryable<Question> query = _table;

        if (!trackable)
        {
            query = query.AsNoTracking();
        }

        var question = await query
            .Include(q => q.StudentQuestions)
                .ThenInclude(sq => sq.StudentAnswers)
                    .ThenInclude(sa => sa.QuestionAnswer) 
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
        {
            return 0; 
        }

        int correctCount = question.StudentQuestions
            .Sum(sq => sq.StudentAnswers.Count(sa =>
                sa.IsSelected && sa.QuestionAnswer != null && sa.QuestionAnswer.IsRightAnswer));

        return correctCount;
    }

    /// <summary>
    /// Belli bir id'ye sahip soruya trackable özelliği true olan öğrenci cevaplarından kontrol eder boş olanların sayısını döndürür
    /// </summary>
    /// <param name="id"></param>
    /// <param name="trackable"></param>
    /// <returns>boş cevap sayısı </returns>
    public async Task<int> EmptyQuestionCount(Guid id, bool trackable = true)
    {
        IQueryable<Question> query = _table;

        if (!trackable)
        {
            query = query.AsNoTracking();
        }

        var question = await query
            .Include(q => q.StudentQuestions)
                .ThenInclude(sq => sq.StudentAnswers)
                    .ThenInclude(sa => sa.QuestionAnswer)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
        {
            return 0;
        }

        int emptyCount = question.StudentQuestions
            .Sum(sq => sq.StudentAnswers.Count(sa =>
                !sa.IsSelected));

        return emptyCount;
    }
    /// <summary>
    /// Belli bir id'ye sahip soruya trackable özelliği true olan öğrenci cevaplarından kontrol eder yanlış olanların sayısını döndürür
    /// </summary>
    /// <param name="id"></param>
    /// <param name="trackable"></param>
    /// <returns>yanlış cevap sayısı </returns>
    public async Task<int> IncorrectQuestionCount(Guid id, bool trackable = true)
    {
        IQueryable<Question> query = _table;

        if (!trackable)
        {
            query = query.AsNoTracking();
        }

        var question = await query
            .Include(q => q.StudentQuestions)
                .ThenInclude(sq => sq.StudentAnswers)
                    .ThenInclude(sa => sa.QuestionAnswer)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
        {
            return 0;
        }

        int incorrectCount = question.StudentQuestions
            .Sum(sq => sq.StudentAnswers.Count(sa =>
                sa.IsSelected && sa.QuestionAnswer != null && !sa.QuestionAnswer.IsRightAnswer));

        return incorrectCount;
    }


}
