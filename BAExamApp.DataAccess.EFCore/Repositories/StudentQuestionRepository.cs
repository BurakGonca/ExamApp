namespace BAExamApp.DataAccess.EFCore.Repositories;

public class StudentQuestionRepository : EFBaseRepository<StudentQuestion>, IStudentQuestionRepository
{
    public StudentQuestionRepository(BAExamAppDbContext context) : base(context) { }
}