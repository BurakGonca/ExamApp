using BAExamApp.Business.Interfaces.Services.Candidate;
using BAExamApp.Core.Enums;
using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Dtos.Candidate.CandidateBranches;
using BAExamApp.Dtos.Candidate.CandidateGroups;
using BAExamApp.Dtos.Candidate.Candidates;
using BAExamApp.Dtos.Products;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.DbSets.Candidates;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateGroupService : ICandidateGroupService
{
    private readonly ICandidateGroupRepository _candidateGroupRepository;
    private readonly ICandidateCandidatesGroupsRepository _candidateCAndidateGroupRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICandidateBranchRepository _candidateBranchRepository;
    private readonly ICandidatesGroupsRepository _candidatesGroupsRepository;


    public CandidateGroupService(ICandidateGroupRepository candidateGroupRepository, ICandidateCandidatesGroupsRepository candidateCAndidateGroupRepository, ICandidateRepository candidateRepository, ICandidateBranchRepository candidateBranchRepository, ICandidatesGroupsRepository candidatesGroupsRepository)
    {
        _candidateGroupRepository = candidateGroupRepository;
        _candidateCAndidateGroupRepository = candidateCAndidateGroupRepository;
        _candidateRepository = candidateRepository;
        _candidateBranchRepository = candidateBranchRepository;
        _candidatesGroupsRepository = candidatesGroupsRepository;
    }

    /// <summary>
    /// Yeni bir Grp oluşturur.
    /// </summary>
    /// <param name="candidateGroupCreateDto">CandidateCandidateGroup alanları için ozellikler içerir</param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateGroupDto>> AddAsync(CandidateGroupCreateDto candidateGroupCreateDto)
    {

        try
        {
            var hasGroup = await _candidateGroupRepository.AnyAsync(group => group.Name.ToLower() == candidateGroupCreateDto.Name.ToLower());

            if (hasGroup)
            {
                return new ErrorDataResult<CandidateGroupDto>(candidateGroupCreateDto.Adapt<CandidateGroupDto>(), Messages.AddFailAlreadyExists);
            }

            var candidateGroup = candidateGroupCreateDto.Adapt<CandidateCandidateGroup>();

            await _candidateGroupRepository.AddAsync(candidateGroup);
            await _candidateGroupRepository.SaveChangesAsync();

            return new SuccessDataResult<CandidateGroupDto>(candidateGroup.Adapt<CandidateGroupDto>(), Messages.AddSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateGroupDto>(candidateGroupCreateDto.Adapt<CandidateGroupDto>(), Messages.AddError + " - " + ex.Message);
        }
    }
    /// <summary>
    /// Tüm grupları liste olarak döndürür
    /// </summary>
    /// <returns></returns>
    public async Task<IDataResult<List<CandidateGroupListDto>>> GetAllAsync()
    {
        try
        {
            var groups = await _candidateGroupRepository.GetAllAsync();
            var resultBranch = await _candidateBranchRepository.GetAllAsync();
            var groupList = groups.Adapt<List<CandidateGroupListDto>>() ?? new List<CandidateGroupListDto>();//not null hatası için

            if (groupList is null || groupList.Count == 0)
            {
                return new ErrorDataResult<List<CandidateGroupListDto>>(groupList, Messages.CandidateGroupListEmpty);
            }

            // Branch isimlerini atama işlemi
            foreach (var item in groupList)
            {
                var branch = resultBranch.FirstOrDefault(x => x.Id == item.CandidateBranchId);
                if (branch != null)
                {
                    item.BranchName = branch.Name;
                }
            }

            return new SuccessDataResult<List<CandidateGroupListDto>>(groupList, Messages.ListedSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<CandidateGroupListDto>>(new List<CandidateGroupListDto>(), Messages.CandidateGroupListError + " - " + ex.Message);
        }
    }


    /// <summary>
    /// Verilen Id değerine sahip grobu döndürür.
    /// </summary>
    /// <param name="id">Group Id değeri</param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateGroupDetailsDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var group = await _candidateGroupRepository.GetByIdAsync(id);
            if (group is null)
            {
                return new ErrorDataResult<CandidateGroupDetailsDto>(Messages.CandidateGroupNotFound);
            }

            var candidateGroupDetailsDto = group.Adapt<CandidateGroupDetailsDto>();

            candidateGroupDetailsDto.Candidates = group.Candidates
                .Where(c => c.Status == Status.Active)
                .Select(cg => cg.Candidate.Adapt<CandidateDetailsDto>())
                .ToList();

            return new SuccessDataResult<CandidateGroupDetailsDto>(candidateGroupDetailsDto, Messages.CandidateGroupGetSucces);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateGroupDetailsDto>(Messages.CandidateGroupNotFound + " - " + ex.Message);
        }
    }

    /// <summary>
    /// ID'sine karşılık gelen ürünü verilen özellikler ile günceller
    /// </summary>
    /// <param name="candidateGroupUpdateDto"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateGroupDto>> UpdateAsync(CandidateGroupUpdateDto candidateGroupUpdateDto)
    {
        try
        {
            var oldGroup = await _candidateGroupRepository.GetByIdAsync(candidateGroupUpdateDto.Id);
            if (oldGroup is null)
            {
                return new ErrorDataResult<CandidateGroupDto>(Messages.CandidateGroupNotFound);
            }

            var updatedProduct = candidateGroupUpdateDto.Adapt(oldGroup);

            await _candidateGroupRepository.UpdateAsync(updatedProduct);
            await _candidateGroupRepository.SaveChangesAsync();

            return new SuccessDataResult<CandidateGroupDto>(updatedProduct.Adapt<CandidateGroupDto>(), Messages.UpdateSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateGroupDto>(Messages.UpdateFail + " - " + ex.Message);
        }
    }
    /// <summary>
    /// Id'si verilen grubu siler
    /// </summary>
    /// <param name="id">Silinecek olan grup Id değeri</param>
    /// <returns></returns>
    public async Task<IResult> DeleteAsync(Guid id)
    {
        try
        {
            var group = await _candidateGroupRepository.GetByIdAsync(id);
            var status = await AnyStudentsInGroup(id);
            if (group is null)
            {
                return new ErrorDataResult<CandidateGroupDto>(Messages.CandidateGroupNotFound);
            }
            if (status == Status.Passive)
            {
                group.Status = Core.Enums.Status.Passive;
                await _candidateGroupRepository.SaveChangesAsync();
                return new SuccessResult(Messages.UpdateSuccess);
            }
            await _candidateGroupRepository.DeleteAsync(group);
            await _candidateGroupRepository.SaveChangesAsync();

            return new SuccessResult(Messages.DeleteSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorResult(Messages.DeleteFail + " - " + ex.Message);
        }
    }
    /// <summary>
    /// Id'si verilen gruba ait adayvar mı kontrolu
    /// </summary>
    /// <param name="id">grup Id değeri</param>
    /// <returns></returns>
    public async Task<Status> AnyStudentsInGroup(Guid id)
    {
        var allCandidate = await _candidateCAndidateGroupRepository.GetAllAsync(x => x.CandidateGroupId == id);

        if (allCandidate == null || !allCandidate.Any())
        {
            return Status.Deleted; // Eğer hiç aday yoksa veya null ise Deleted durumu döndürülür
        }

        foreach (var item in allCandidate)
        {
            var candidate = await _candidateRepository.GetByIdAsync(item.CandidateId);

            if (candidate == null)
            {
                continue; // Eğer aday null ise, bu adayı atla
            }

            if (candidate.Status == Status.Active)
            {
                return Status.Active; // En az bir aday aktifse Active durumu döndürülür
            }
        }

        // Eğer döngü sonuna kadar hiçbir aday aktif bulunmazsa Passive durumu döndürülür
        return Status.Passive;
    }

}

