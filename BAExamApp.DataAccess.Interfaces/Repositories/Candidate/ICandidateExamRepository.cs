using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateExamRepository : IAsyncRepository, IAsyncQueryableRepository<CandidateExam>, IAsyncFindableRepository<CandidateExam>, IAsyncInsertableRepository<CandidateExam>, IAsyncUpdateableRepository<CandidateExam>, IAsyncDeleteableRepository<CandidateExam>
{
}
