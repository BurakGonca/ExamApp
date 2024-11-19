using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateExamRuleRepository : IAsyncRepository, IAsyncQueryableRepository<CandidateExamRule>, IAsyncFindableRepository<CandidateExamRule>, IAsyncInsertableRepository<CandidateExamRule>, IAsyncDeleteableRepository<CandidateExamRule>, IAsyncUpdateableRepository<CandidateExamRule>
{
}
