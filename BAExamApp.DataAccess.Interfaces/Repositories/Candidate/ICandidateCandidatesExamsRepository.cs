using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateCandidatesExamsRepository : IAsyncRepository, IAsyncQueryableRepository<CandidatesExams>, IAsyncFindableRepository<CandidatesExams>, IAsyncInsertableRepository<CandidatesExams>, IAsyncUpdateableRepository<CandidatesExams>, IAsyncDeleteableRepository<CandidatesExams>
{
}
