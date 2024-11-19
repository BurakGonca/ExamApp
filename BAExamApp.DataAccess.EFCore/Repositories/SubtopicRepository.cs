namespace BAExamApp.DataAccess.EFCore.Repositories;

public class SubtopicRepository : EFBaseRepository<Subtopic>, ISubtopicRepository
{
    public SubtopicRepository(BAExamAppDbContext context) : base(context) { }
}
