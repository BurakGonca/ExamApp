using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateQuestionRuleRepository : IAsyncRepository, IAsyncQueryableRepository<CandidateQuestionRule>, IAsyncFindableRepository<CandidateQuestionRule>, IAsyncInsertableRepository<CandidateQuestionRule>, IAsyncDeleteableRepository<CandidateQuestionRule>, IAsyncUpdateableRepository<CandidateQuestionRule>
{
}
