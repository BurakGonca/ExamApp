using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
public interface ICandidateQuestionSubjectRepository : IAsyncRepository, IAsyncQueryableRepository<CandidateQuestionSubject>, IAsyncFindableRepository<CandidateQuestionSubject>, IAsyncInsertableRepository<CandidateQuestionSubject>, IAsyncDeleteableRepository<CandidateQuestionSubject>, IAsyncUpdateableRepository<CandidateQuestionSubject>
{
}
