using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateCandidateAnswerRepository : IAsyncRepository, IAsyncInsertableRepository<CandidateCandidateAnswer>, IAsyncQueryableRepository<CandidateCandidateAnswer>, IAsyncDeleteableRepository<CandidateCandidateAnswer>, IAsyncFindableRepository<CandidateCandidateAnswer>, IRepository, IAsyncUpdateableRepository<CandidateCandidateAnswer>, IAsyncTransactionRepository
{
}
