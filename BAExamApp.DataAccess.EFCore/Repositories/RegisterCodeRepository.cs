using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.EFCore.Repositories;
public class RegisterCodeRepository : EFBaseRepository<RegisterCode>, IRegisterCodeRepository
{
    public RegisterCodeRepository(BAExamAppDbContext context) : base(context)
    {

    }
}
