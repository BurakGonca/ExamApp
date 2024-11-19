using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories;
public interface IQuestionRevisionRepository: IAsyncRepository, IAsyncInsertableRepository<QuestionRevision>, IAsyncQueryableRepository<QuestionRevision>, IAsyncDeleteableRepository<QuestionRevision>, IAsyncFindableRepository<QuestionRevision>, IAsyncUpdateableRepository<QuestionRevision>, IRepository
{
    Task<QuestionRevision> GetActive();
}
