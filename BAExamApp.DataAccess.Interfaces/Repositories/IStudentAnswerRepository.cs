using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories;
public interface IStudentAnswerRepository : IAsyncRepository, IAsyncInsertableRepository<StudentAnswer>, IAsyncQueryableRepository<StudentAnswer>, IAsyncDeleteableRepository<StudentAnswer>, IAsyncFindableRepository<StudentAnswer>, IAsyncUpdateableRepository<StudentAnswer>
{
}
