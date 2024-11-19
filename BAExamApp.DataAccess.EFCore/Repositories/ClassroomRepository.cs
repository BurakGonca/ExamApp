using BAExamApp.Business.Constants;
using BAExamApp.Core.Utilities.Results;
using BAExamApp.Core.Utilities.Results.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BAExamApp.DataAccess.EFCore.Repositories;

public class ClassroomRepository : EFBaseRepository<Classroom>, IClassroomRepository
{
    public ClassroomRepository(BAExamAppDbContext context) : base(context)
    {
    }

    public async Task<IResult> DeleteWithRelationship(Guid id)
    {
        var classroom = await GetByIdAsync(id);

        if (classroom == null)
        {
            return new ErrorResult(Messages.ClassroomNotFound);
        }
        else if( await HasRelate(id))
        {
            classroom.Status = Core.Enums.Status.Passive;
            await SaveChangesAsync();
            return new SuccessResult(Messages.DeleteSuccess);
        }
        else
        {
            classroom.Status=Core.Enums.Status.Deleted;
            await SaveChangesAsync();
            return new SuccessResult(Messages.DeleteSuccess);
        }
        

         
    }

    public async Task<IEnumerable<Classroom>> GetAllActiveClassrooms(Expression<Func<Classroom, bool>> expression, bool tracking = true)
    {
        return await GetAllActives(tracking).ToListAsync();
    }

    public async Task<bool> HasRelate(Guid id)
    {
        var classroom = await GetByIdWithIncludeAsync(id,
            c => c.TrainerClassrooms,
            c => c.StudentClassrooms,
            c => c.ClassroomProducts,
            c => c.ExamClassrooms
        );

        if (classroom != null)
        {
            if ( classroom.ExamClassrooms.Count() > 0 || classroom.StudentClassrooms.Count() > 0 || classroom.TrainerClassrooms.Count() > 0 || classroom.ClassroomProducts.Count() > 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        return false;
    }

}
