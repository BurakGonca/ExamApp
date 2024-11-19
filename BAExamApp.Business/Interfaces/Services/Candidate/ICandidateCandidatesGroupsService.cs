using BAExamApp.Dtos.Candidate.CandidatesGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateCandidatesGroupsService
{
    /// <summary>
    /// Candidate id ile adaya ait grupları çağırma işlemi.
    /// </summary>
    /// <param name="candidateId"></param>
    /// <returns></returns>
    Task<IDataResult<CandidatesGroupsCandidateGroupsDto>> GetGroupsByCandidateIdAsync(Guid candidateId);

    /// <summary>
    /// Candidate id üzerinden adaye grup atamalarının yapılma işlemi.
    /// </summary>
    /// <param name="groupIds"></param>
    /// <returns></returns>
    Task<IResult> UpdateCandidateGroupsAsync(Guid candidateId, Guid[] groupIds);

    /// <summary>
    /// Group id ile gruba ait adayları çağırma işlemi.
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>

    Task<IDataResult<CandidatesGroupsGroupCandidatesDto>> GetCandidatesByGroupIdAsync(Guid groupId);

    /// <summary>
    /// Group id üzerinden gruba aday atamalarının yapılma işlemi.
    /// </summary>
    /// <param name="candidateIds"></param>
    /// <returns></returns>

    Task<IResult> UpdateGroupCandidatesAsync(Guid groupId, Guid[] candidateIds);


}
