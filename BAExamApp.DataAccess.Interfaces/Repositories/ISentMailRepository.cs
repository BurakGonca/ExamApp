using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories;
public interface ISentMailRepository : IRepository, IAsyncQueryableRepository<SentMail>, IAsyncFindableRepository<SentMail>, IAsyncUpdateableRepository<SentMail>, IAsyncInsertableRepository<SentMail>, IAsyncDeleteableRepository<SentMail>
{

}
