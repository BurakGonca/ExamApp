namespace BAExamApp.DataAccess.EFCore.Repositories;

public class QuestionAnswerRepository : EFBaseRepository<QuestionAnswer>, IQuestionAnswerRepository
{
    public QuestionAnswerRepository(BAExamAppDbContext context) : base(context)
    {
    }
}
