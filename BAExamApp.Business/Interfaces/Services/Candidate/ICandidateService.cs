using BAExamApp.Dtos.Candidate.CandidateAdmins;
using BAExamApp.Dtos.Candidate.CandidateGroups;
using BAExamApp.Dtos.Candidate.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateService
{
    /// <summary>
    /// Adayı id ile çağırma işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateDto>> GetByIdAsync(Guid id);

    /// <summary>
    /// Tüm adayları çağırma işlemi
    /// </summary>
    /// <returns></returns>
    Task<IDataResult<List<CandidateListDto>>> GetAllAsync();

    /// <summary>
    /// Aday ekleme işlemi.
    /// </summary>
    /// <param name="candidateCreateDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateDto>> AddAsync(CandidateCreateDto candidateCreateDto);

    /// <summary>
    /// Aday bilgilerini güncelleme işlemi.
    /// </summary>
    /// <param name="candidateUpdateDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateDto>> UpdateAsync(CandidateUpdateDto candidateUpdateDto);

    /// <summary>
    /// Aday silme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(Guid id);

    /// <summary>
    /// Adaya ait detayları getirme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateDetailsDto>> GetDetailsByIdAsync(Guid id);
    Task<IDataResult<List<CandidateGroupsDetailsDto>>> GetCandidateGroupsAsync(Guid candidateId);
    Task<IDataResult<CandidateExamsDto>> GetCandidateExamsAsync(Guid candidateId);
}
