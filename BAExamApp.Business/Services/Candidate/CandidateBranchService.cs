using BAExamApp.Business.Interfaces.Services.Candidate;
using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Dtos.Candidate.CandidateBranches;
using BAExamApp.Dtos.Candidate.CandidateGroups;
using BAExamApp.Entities.DbSets.Candidates;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateBranchService : ICandidateBranchService
{
    private readonly ICandidateBranchRepository _candidateBranchRepository;
    private readonly ICandidateGroupRepository _candidateGroupRepository;
    public CandidateBranchService(ICandidateBranchRepository candidateBranchRepository, ICandidateGroupRepository candidateGroupRepository)
    {
        _candidateBranchRepository = candidateBranchRepository;
        _candidateGroupRepository = candidateGroupRepository;
    }

    public async Task<IDataResult<CandidateBranchDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var candidateBranch = await _candidateBranchRepository.GetByIdAsync(id);

            if (candidateBranch is null)
            {
                return new ErrorDataResult<CandidateBranchDto>(Messages.BranchNotFound);
            }

            var candidateBranchDto = candidateBranch.Adapt<CandidateBranchDto>();
            return new SuccessDataResult<CandidateBranchDto>(candidateBranchDto, Messages.FoundSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateBranchDto>(Messages.BranchNotFound + " - " + ex.Message);
        }
    }

    public async Task<IDataResult<List<CandidateBranchListDto>>> GetAllAsync()
    {
        try
        {
            var candidateBranches = await _candidateBranchRepository.GetAllAsync(false);

            var candidateBranchListDtos = candidateBranches.Adapt<List<CandidateBranchListDto>>();
            return new SuccessDataResult<List<CandidateBranchListDto>>(candidateBranchListDtos, Messages.ListedSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<CandidateBranchListDto>>(Messages.BranchNotFound + " - " + ex.Message);
        }
    }

    public async Task<IDataResult<CandidateBranchDetailsDto>> GetDetailsByIdAsync(Guid id)
    {
        try
        {
            var candidateBranch = await _candidateBranchRepository.GetByIdAsync(id);

            if (candidateBranch is null)
            {
                return new ErrorDataResult<CandidateBranchDetailsDto>(Messages.BranchNotFound);
            }

            var candidateBranchDetailsDto = candidateBranch.Adapt<CandidateBranchDetailsDto>();

            candidateBranchDetailsDto.CandidateGroups = candidateBranch.CandidateGroups.Where(g => g.CandidateBranchId == id && g.Status == Core.Enums.Status.Active || g.Status == Core.Enums.Status.Passive).Select(g => g.Adapt<CandidateGroupDetailsDto>()).ToList();


            return new SuccessDataResult<CandidateBranchDetailsDto>(candidateBranchDetailsDto, Messages.FoundSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateBranchDetailsDto>(Messages.BranchNotFound + " - " + ex.Message);
        }
    }

    public async Task<IDataResult<CandidateBranchDto>> AddAsync(CandidateBranchCreateDto candidateBranchCreateDto)
    {
        try
        {
            var hasBranch = await _candidateBranchRepository.AnyAsync(branch => branch.Name.ToLower() == candidateBranchCreateDto.Name.ToLower());

            if (hasBranch)
            {
                return new ErrorDataResult<CandidateBranchDto>(Messages.AddFailAlreadyExists);
            }

            var candidateBranch = candidateBranchCreateDto.Adapt<CandidateCandidateBranch>();

            await _candidateBranchRepository.AddAsync(candidateBranch);
            await _candidateBranchRepository.SaveChangesAsync();

            var candidateBranchDto = candidateBranch.Adapt<CandidateBranchDto>();
            return new SuccessDataResult<CandidateBranchDto>(candidateBranchDto, Messages.AddSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateBranchDto>(Messages.AddError + " - " + ex.Message);
        }
    }

    public async Task<IDataResult<CandidateBranchDto>> UpdateAsync(CandidateBranchUpdateDto candidateBranchUpdateDto)
    {
        try
        {
            var hasBranch = await _candidateBranchRepository.AnyAsync(branch => branch.Id != candidateBranchUpdateDto.Id && branch.Name.ToLower() == candidateBranchUpdateDto.Name.ToLower());

            if (hasBranch)
            {
                return new ErrorDataResult<CandidateBranchDto>(Messages.AddFailAlreadyExists);
            }

            var candidateBranch = await _candidateBranchRepository.GetByIdAsync(candidateBranchUpdateDto.Id);

            if (candidateBranch is null)
            {
                return new ErrorDataResult<CandidateBranchDto>(Messages.BranchNotFound);
            }

            candidateBranchUpdateDto.Adapt(candidateBranch);

            await _candidateBranchRepository.UpdateAsync(candidateBranch);
            await _candidateBranchRepository.SaveChangesAsync();

            var updatedBranchDto = candidateBranch.Adapt<CandidateBranchDto>();
            return new SuccessDataResult<CandidateBranchDto>(updatedBranchDto, Messages.UpdateSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateBranchDto>(Messages.UpdateFail + " - " + ex.Message);
        }
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        try
        {
            var branch = await _candidateBranchRepository.GetByIdAsync(id);

            if (branch is null)
            {
                return new ErrorDataResult<CandidateBranchDto>(Messages.BranchNotFound);
            }

            var hasActiveGroups = await _candidateGroupRepository.AnyAsync(group => group.CandidateBranchId == id && group.Status == Core.Enums.Status.Active);

            if (hasActiveGroups)
            {
                return new ErrorResult(Messages.CandidateBranchGroupNotFound);
            }
            if (branch.Status == Core.Enums.Status.Active)
            {
                var passiveGroups = await _candidateGroupRepository.GetAllAsync(group => group.CandidateBranchId == id && group.Status == Core.Enums.Status.Passive);

                if (passiveGroups.Any())
                {
                    foreach (var group in passiveGroups)
                    {
                        group.Status = Core.Enums.Status.Passive;
                        await _candidateGroupRepository.UpdateAsync(group);
                    }
                    await _candidateGroupRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.PassiveSuccess);
                }
            }
            else if (branch.Status == Core.Enums.Status.Passive)
            {
                var passiveGroups = await _candidateGroupRepository.GetAllAsync(group => group.CandidateBranchId == id && group.Status == Core.Enums.Status.Passive);

                if (passiveGroups.Any())
                {
                    
                    return new SuccessResult(Messages.PassiveError);
                }
            }
            

            await _candidateBranchRepository.DeleteAsync(branch);
            await _candidateBranchRepository.SaveChangesAsync();

            return new SuccessResult(Messages.DeleteSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorResult(Messages.DeleteFail + " - " + ex.Message);
        }
    }
    public async Task<IResult> CheckActiveGroupsAsync(Guid id)
    {
        var branch = await _candidateBranchRepository.GetByIdAsync(id);

        var hasActiveGroups = await _candidateGroupRepository.AnyAsync(group => group.CandidateBranchId == id && group.Status == Core.Enums.Status.Active);

        if (hasActiveGroups)
        {
            return new ErrorResult(Messages.CandidateBranchGroupNotFound);
        }

        if (branch.Status == Core.Enums.Status.Active)
        {
            var passiveGroups = await _candidateGroupRepository.GetAllAsync(group => group.CandidateBranchId == id && group.Status == Core.Enums.Status.Passive);

            if (passiveGroups.Any())
            {
                return new SuccessResult(Messages.PassiveSuccess);
            }
        }
        else if (branch.Status == Core.Enums.Status.Passive)
        {
            var passiveGroups = await _candidateGroupRepository.GetAllAsync(group => group.CandidateBranchId == id && group.Status == Core.Enums.Status.Passive);

            if (passiveGroups.Any())
            {
                return new SuccessResult(Messages.PassiveError);
            }
        }
        return new SuccessResult();
    }
    public async Task<IResult> MarkBranchAsPassiveAsync(Guid id)
    {
        try
        {
            var branch = await _candidateBranchRepository.GetByIdAsync(id);

            if (branch == null)
            {
                return new ErrorDataResult<CandidateBranchDto>(Messages.BranchNotFound);
            }

            branch.Status = Core.Enums.Status.Passive;
            await _candidateBranchRepository.UpdateAsync(branch);
            await _candidateBranchRepository.SaveChangesAsync();

            return new SuccessResult(Messages.PassiveSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorResult(Messages.UpdateFail + " - " + ex.Message);
        }
    }

    public async Task<IDataResult<CandidateBranchDto>> SetBranchAndAnswersToActiveAsync(Guid id)
    {
        try
        {
            var candidateBranch = await _candidateBranchRepository.GetByIdAsync(id);
            if (candidateBranch == null)
            {
                return new ErrorDataResult<CandidateBranchDto>(Messages.QuestionNotFound);
            }

            candidateBranch.Status = Core.Enums.Status.Active;

            await _candidateBranchRepository.UpdateAsync(candidateBranch);
            await _candidateBranchRepository.SaveChangesAsync();

            var candidateBranchDto = candidateBranch.Adapt<CandidateBranchDto>();

            return new SuccessDataResult<CandidateBranchDto>(candidateBranchDto, Messages.CandidateBranchActive);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateBranchDto>(Messages.CandidateBranchInActive);
        }
    }

    public async Task<IDataResult<CandidateBranchDto>> SetBranchAndAnswersToInactiveAsync(Guid id)
    {
        try
        {
            var candidateBranch = await _candidateBranchRepository.GetByIdAsync(id);
            if (candidateBranch == null)
            {
                return new ErrorDataResult<CandidateBranchDto>(Messages.QuestionNotFound);
            }

            var passiveGroups = await _candidateGroupRepository.GetAllAsync(group => group.CandidateBranchId == id && group.Status == Core.Enums.Status.Active);

            if (passiveGroups.Any())
            {
                return new SuccessDataResult<CandidateBranchDto>(Messages.PassiveError);
            }

            candidateBranch.Status = Core.Enums.Status.Passive;

            await _candidateBranchRepository.UpdateAsync(candidateBranch);
            await _candidateBranchRepository.SaveChangesAsync();

            var candidateBranchDto = candidateBranch.Adapt<CandidateBranchDto>();

            return new SuccessDataResult<CandidateBranchDto>(candidateBranchDto, Messages.CandidateBranchInActive);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateBranchDto>(Messages.CandidateBranchInActive);
        }
    }


}
