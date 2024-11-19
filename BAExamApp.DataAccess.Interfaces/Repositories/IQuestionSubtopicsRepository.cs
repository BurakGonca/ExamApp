using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories;
public interface IQuestionSubtopicsRepository : IRepository, IAsyncRepository, IAsyncQueryableRepository<QuestionSubtopics>, IAsyncFindableRepository<QuestionSubtopics>, IAsyncUpdateableRepository<QuestionSubtopics>, IAsyncInsertableRepository<QuestionSubtopics>, IAsyncDeleteableRepository<QuestionSubtopics>
{

}
