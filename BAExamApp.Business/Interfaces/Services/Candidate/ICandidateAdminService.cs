using BAExamApp.Dtos.Candidate.CandidateAdmins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services;
public interface ICandidateAdminService
{
    /// <summary>
    /// Aday yöneticiyi id ile çağırma işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateAdminDto>> GetByIdAsync(Guid id);
    /// <summary>
    /// Aday yöneticiyi identityId ile çağırma işlemi.
    /// </summary>
    /// <param name="identityId"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateAdminDto>> GetByIdentityIdAsync(string identityId);
    /// <summary>
    /// Tüm aday yöneticiler çağırma işlemi
    /// </summary>
    /// <returns></returns>
    Task<IDataResult<List<CandidateAdminListDto>>> GetAllAsync();
    /// <summary>
    /// Aday yönetici ekleme işlemi.
    /// </summary>
    /// <param name="candidateAdminCreateDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateAdminDto>> AddAsync(CandidateAdminCreateDto candidateAdminCreateDto);
    /// <summary>
    /// Aday yönetici bilgilerini güncelleme işlemi.
    /// </summary>
    /// <param name="candidateAdminUpdateDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateAdminDto>> UpdateAsync(CandidateAdminUpdateDto candidateAdminUpdateDto);
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
    Task<IDataResult<CandidateAdminDetailsDto>> GetDetailsByIdAsync(Guid id);

    /// <summary>
    /// Adayın sınava tekrar girme iznini güncelleme işlemi.
    /// </summary>
    /// <param name="candidateId">Adayın kimliği</param>
    /// <returns>İşlem sonucu</returns>
    Task<IResult> AllowCandidateToRetakeExamAsync(Guid candidateId);
}
