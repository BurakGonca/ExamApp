namespace BAExamApp.DataAccess.EFCore.Repositories;

internal class QuestionFeedbackRepository : EFBaseRepository<QuestionFeedback>, IQuestionFeedbackRepository
{
    public QuestionFeedbackRepository(BAExamAppDbContext context) : base(context)
    {
    }
}
