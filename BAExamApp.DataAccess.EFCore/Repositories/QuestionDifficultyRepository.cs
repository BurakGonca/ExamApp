namespace BAExamApp.DataAccess.EFCore.Repositories;

public class QuestionDifficultyRepository : EFBaseRepository<QuestionDifficulty>, IQuestionDifficultyRepository
{
    public QuestionDifficultyRepository(BAExamAppDbContext context) : base(context)
    {
    }
}
