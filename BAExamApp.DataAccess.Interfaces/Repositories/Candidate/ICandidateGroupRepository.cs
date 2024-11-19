using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateGroupRepository : IAsyncRepository, IAsyncQueryableRepository<CandidateCandidateGroup>, IAsyncFindableRepository<CandidateCandidateGroup>, IAsyncInsertableRepository<CandidateCandidateGroup>, IAsyncUpdateableRepository<CandidateCandidateGroup>, IAsyncDeleteableRepository<CandidateCandidateGroup>
{

}

