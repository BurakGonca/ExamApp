using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidatesGroupsRepository : IAsyncRepository, IAsyncQueryableRepository<CandidatesGroups>, IAsyncFindableRepository<CandidatesGroups>, IAsyncInsertableRepository<CandidatesGroups>, IAsyncUpdateableRepository<CandidatesGroups>, IAsyncDeleteableRepository<CandidatesGroups>
{
}
