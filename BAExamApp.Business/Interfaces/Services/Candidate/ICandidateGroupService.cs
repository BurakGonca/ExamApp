using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.CandidateBranches;
using BAExamApp.Dtos.Candidate.CandidateGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateGroupService
{
    /// <summary>
    /// id ile çağırma işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateGroupDetailsDto>> GetByIdAsync(Guid id);
    /// <summary>
    /// Tüm Groupları çağırma işlemi
    /// </summary>
    /// <returns></returns>
    Task<IDataResult<List<CandidateGroupListDto>>> GetAllAsync();
    /// <summary>
    ///  Group ekleme işlemi.
    /// </summary>
    /// <param name="candidateGroupCreateDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateGroupDto>> AddAsync(CandidateGroupCreateDto candidateGroupCreateDto);
    /// <summary>
    /// Group bilgilerini güncelleme işlemi.
    /// </summary>
    /// <param name="candidateGroupUpdateDto"></param>
    /// <returns></returns>
    Task<IDataResult<CandidateGroupDto>> UpdateAsync(CandidateGroupUpdateDto candidateGroupUpdateDto);
    /// <summary>
    /// Group silme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(Guid id);
    /// <summary>
    /// Group da öğrencivar mı kontrolu
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Status> AnyStudentsInGroup(Guid id);

}
