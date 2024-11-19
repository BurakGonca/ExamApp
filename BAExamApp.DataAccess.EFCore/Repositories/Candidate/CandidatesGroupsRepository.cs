using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Entities.DbSets.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.EFCore.Repositories.Candidate;
public class CandidatesGroupsRepository : EFBaseRepository<CandidatesGroups>, ICandidatesGroupsRepository
{
    public CandidatesGroupsRepository(BAExamAppDbContext context) : base(context) { }
}
