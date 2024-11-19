namespace BAExamApp.DataAccess.EFCore.Repositories;

public class TrainerClassroomRepository : EFBaseRepository<TrainerClassroom>, ITrainerClassroomRepository
{
    public TrainerClassroomRepository(BAExamAppDbContext context) : base(context)
    {

    }
}
