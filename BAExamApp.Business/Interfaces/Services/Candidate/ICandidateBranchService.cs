using BAExamApp.Dtos.Candidate.CandidateBranches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateBranchService
{
    /// <summary>
    /// Aday yöneticiyi id ile çağırma işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateBranchDto>> GetByIdAsync(Guid id);
    /// <summary>
    /// Tüm aday yöneticiler çağırma işlemi
    /// </summary>
    /// <returns></returns>
    Task<IDataResult<List<CandidateBranchListDto>>> GetAllAsync();
    /// <summary>
    /// Aday yönetici ekleme işlemi.
    /// </summary>
    /// <param name="candidateBranchCreateDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateBranchDto>> AddAsync(CandidateBranchCreateDto candidateBranchCreateDto);
    /// <summary>
    /// Aday yönetici bilgilerini güncelleme işlemi.
    /// </summary>
    /// <param name="candidateBranchUpdateDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateBranchDto>> UpdateAsync(CandidateBranchUpdateDto candidateBranchUpdateDto);
    /// <summary>
    /// Aday yönetici silme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(Guid id);
    /// <summary>
    /// Aday yöneticiye ait detayları getirme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateBranchDetailsDto>> GetDetailsByIdAsync(Guid id);
    Task<IResult> CheckActiveGroupsAsync(Guid id);
    Task<IResult> MarkBranchAsPassiveAsync(Guid id);
    Task<IDataResult<CandidateBranchDto>> SetBranchAndAnswersToActiveAsync(Guid id);

    Task<IDataResult<CandidateBranchDto>> SetBranchAndAnswersToInactiveAsync(Guid id);
}
