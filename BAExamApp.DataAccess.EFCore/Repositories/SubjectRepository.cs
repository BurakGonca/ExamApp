namespace BAExamApp.DataAccess.EFCore.Repositories;

public class SubjectRepository : EFBaseRepository<Subject>, ISubjectRepository
{
    public SubjectRepository(BAExamAppDbContext context) : base(context) { }
}