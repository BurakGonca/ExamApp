using BAExamApp.Dtos.Candidate.CandidatesExams;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidatesExamsService : ICandidatesExamsService
{
    private readonly ICandidateCandidatesExamsRepository _candidateCandidatesExamsRepository;

    public CandidatesExamsService(ICandidateCandidatesExamsRepository candidateCandidatesExamsRepository)
    {
        _candidateCandidatesExamsRepository = candidateCandidatesExamsRepository;
    }

    public async Task<List<CandidatesExamsDto>> GetAll()
    {
        var candidateExams = await _candidateCandidatesExamsRepository.GetAllAsync();

        return candidateExams.ToList().Adapt<List<CandidatesExamsDto>>();
    }
}
