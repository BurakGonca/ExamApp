using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.EFCore.Repositories;
public class TrainerProductRepository : EFBaseRepository<TrainerProduct>, ITrainerProductRepository
{
    public TrainerProductRepository(BAExamAppDbContext context) : base(context)
    {

    }
}
