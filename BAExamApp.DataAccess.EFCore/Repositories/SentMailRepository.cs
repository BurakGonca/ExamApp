using BAExamApp.Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.EFCore.Repositories;
public class SentMailRepository : EFBaseRepository<SentMail>, ISentMailRepository
{
    public SentMailRepository(BAExamAppDbContext context) : base(context)
    {

    }
}
