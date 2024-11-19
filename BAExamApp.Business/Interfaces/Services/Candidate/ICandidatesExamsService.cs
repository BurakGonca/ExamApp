using BAExamApp.Dtos.Candidate.CandidatesExams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidatesExamsService
{
    public Task<List<CandidatesExamsDto>> GetAll();

}
