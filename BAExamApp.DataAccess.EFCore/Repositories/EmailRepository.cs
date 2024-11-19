using BAExamApp.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.EFCore.Repositories;
public class EmailRepository : EFBaseRepository<Email>, IEmailRepository
{
    public EmailRepository(BAExamAppDbContext context) : base(context)
    {

    }
}
