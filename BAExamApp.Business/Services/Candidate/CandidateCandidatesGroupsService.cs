using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.Candidates;
using BAExamApp.Dtos.Candidate.CandidatesGroups;
using BAExamApp.Entities.DbSets.Candidates;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateCandidatesGroupsService : ICandidateCandidatesGroupsService
{
    private readonly ICandidateCandidatesGroupsRepository _candidateCandidatesGroupsRepository;
    private readonly ICandidateGroupRepository _candidateGroupRepository;
    private readonly ICandidateRepository _candidateRepository;

    public CandidateCandidatesGroupsService(ICandidateCandidatesGroupsRepository candidateCandidatesGroupsRepository, ICandidateGroupRepository candidateGroupRepository, ICandidateRepository candidateRepository)
    {
        _candidateCandidatesGroupsRepository = candidateCandidatesGroupsRepository;
        _candidateGroupRepository = candidateGroupRepository;
        _candidateRepository = candidateRepository;
    }


    /// <summary>
    /// Candidate id ile adaya ait grupları çağırma işlemi.
    /// </summary>
    /// <param name="candidateId"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidatesGroupsCandidateGroupsDto>> GetGroupsByCandidateIdAsync(Guid candidateId)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(candidateId);

            if (candidate is null)
            {
                return new ErrorDataResult<CandidatesGroupsCandidateGroupsDto>(Messages.CandidateNotFound);
            }


            var candidatesGroupsList = await _candidateCandidatesGroupsRepository.GetAllAsync(x => x.CandidateId == candidateId);

            var groupsListData = await _candidateGroupRepository.GetAllAsync(x=>x.Status==Status.Active);

            if (groupsListData is null || groupsListData.Count() == 0)
            {
                return new ErrorDataResult<CandidatesGroupsCandidateGroupsDto>(new CandidatesGroupsCandidateGroupsDto(), Messages.CandidateGroupListEmpty);
            }

            var allGroups = groupsListData.Select(x => x.Adapt<CandidatesGroupsGroupListDto>()).ToList();


            MapsterConfig();
            var groupList = candidatesGroupsList.Select(x => x.Adapt<CandidatesGroupsGroupListDto>()).ToList();
            var candidateGroups = candidate.Adapt<CandidatesGroupsCandidateGroupsDto>();
            candidateGroups.Groups = groupList;
            candidateGroups.GroupList = allGroups;

            return new SuccessDataResult<CandidatesGroupsCandidateGroupsDto>(candidateGroups, Messages.ListedSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidatesGroupsCandidateGroupsDto>(new CandidatesGroupsCandidateGroupsDto(), Messages.CandidateGroupListError + " - " + ex.Message);
        }
    }

    /// <summary>
    /// Candidate id üzerinden adaye grup atamalarının yapılma işlemi.
    /// </summary>
    /// <param name="groupIds"></param>
    /// <returns></returns>
    public async Task<IResult> UpdateCandidateGroupsAsync(Guid candidateId, Guid[] groupIds)
    {
        try
        {
            var candidateGroupsAll = await _candidateCandidatesGroupsRepository.GetAllAsync(x => x.CandidateId == candidateId);

            if (groupIds is null || groupIds.Count() == 0)
            {
                await _candidateCandidatesGroupsRepository.DeleteRangeAsync(candidateGroupsAll);
            }
            else
            {
                var candidateGroupsDelete = candidateGroupsAll.Where(x => !groupIds.Any(y => y == x.CandidateGroupId)).ToList();

                if (candidateGroupsDelete is not null && candidateGroupsDelete.Count() != 0)
                {
                    await _candidateCandidatesGroupsRepository.DeleteRangeAsync(candidateGroupsDelete);
                }
            }

            var newGroupIds = groupIds.Where(x => !candidateGroupsAll.Any(y => y.CandidateGroupId == x)).ToList();

            if (newGroupIds is not null && newGroupIds.Count() != 0)
            {
                foreach (var item in newGroupIds)
                {
                    await _candidateCandidatesGroupsRepository.AddAsync(new CandidatesGroups() { CandidateId = candidateId, CandidateGroupId = item });
                    await _candidateCandidatesGroupsRepository.SaveChangesAsync();
                }
            }

        }
        catch (Exception)
        {
            return new ErrorResult(Messages.UpdateFail);
        }

        return new SuccessResult(Messages.UpdateSuccess);

    }


    /// <summary>
    /// Group id ile gruba ait adayları çağırma işlemi.
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidatesGroupsGroupCandidatesDto>> GetCandidatesByGroupIdAsync(Guid groupId)
    {
        try
        {
            var group = await _candidateGroupRepository.GetByIdAsync(groupId);

            if (group is null)
            {
                return new ErrorDataResult<CandidatesGroupsGroupCandidatesDto>(Messages.CandidateGroupNotFound);
            }

            var groupCandidateList = await _candidateCandidatesGroupsRepository.GetAllAsync(x => x.CandidateGroupId == groupId);

            var candidateListData = await _candidateRepository.GetAllAsync();

            if (candidateListData is null || candidateListData.Count() == 0)
            {
                return new ErrorDataResult<CandidatesGroupsGroupCandidatesDto>(new CandidatesGroupsGroupCandidatesDto(), Messages.CandidateNotFound);
            }

            MapsterConfig();

            var allCandidates = candidateListData.Select(x => x.Adapt<CandidatesGroupsCandidateListDto>()).ToList();

            var candidateList = groupCandidateList.Select(x => x.Adapt<CandidatesGroupsCandidateListDto>()).ToList();
            var groupCandidates = group.Adapt<CandidatesGroupsGroupCandidatesDto>();
            groupCandidates.Candidates = candidateList;
            groupCandidates.CandidateList = allCandidates;

            return new SuccessDataResult<CandidatesGroupsGroupCandidatesDto>(groupCandidates, Messages.ListedSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidatesGroupsGroupCandidatesDto>(new CandidatesGroupsGroupCandidatesDto(), Messages.CandidateGroupListError + " - " + ex.Message);
        }

    }

    /// <summary>
    /// Group id üzerinden gruba aday atamalarının yapılma işlemi.
    /// </summary>
    /// <param name="candidateIds"></param>
    /// <returns></returns>
    public async Task<IResult> UpdateGroupCandidatesAsync(Guid groupId, Guid[] candidateIds)
    {
        try
        {
            var groupCandidatesAll = await _candidateCandidatesGroupsRepository.GetAllAsync(x => x.CandidateGroupId == groupId);

            if (candidateIds is null || candidateIds.Count() == 0)
            {
                await _candidateCandidatesGroupsRepository.DeleteRangeAsync(groupCandidatesAll);
            }
            else
            {
                var groupCandidatesDelete = groupCandidatesAll.Where(x => !candidateIds.Any(y => y == x.CandidateId)).ToList();

                if (groupCandidatesDelete is not null && groupCandidatesDelete.Count() != 0)
                {
                    await _candidateCandidatesGroupsRepository.DeleteRangeAsync(groupCandidatesDelete);
                }
            }

            var newGroupIds = candidateIds.Where(x => !groupCandidatesAll.Any(y => y.CandidateId == x)).ToList();

            if (newGroupIds is not null && newGroupIds.Count() != 0)
            {
                foreach (var item in newGroupIds)
                {
                    await _candidateCandidatesGroupsRepository.AddAsync(new CandidatesGroups() { CandidateId = item, CandidateGroupId = groupId });
                    await _candidateCandidatesGroupsRepository.SaveChangesAsync();
                }
            }

        }
        catch (Exception)
        {
            return new ErrorResult(Messages.UpdateFail);
        }

        return new SuccessResult(Messages.UpdateSuccess);

    }
    private static void MapsterConfig()
    {
        //Candidate Controller 

        TypeAdapterConfig<CandidateCandidate, CandidatesGroupsCandidateGroupsDto>.NewConfig()
            .Map(dest => dest.FullName, source => (source.FirstName + source.LastName).Length > 50 ?
            $"{source.FirstName.First()}. {source.LastName}" : $"{source.FirstName} {source.LastName}")
            .Map(dest => dest.CandidateId, source => source.Id);

        TypeAdapterConfig<CandidatesGroups, CandidatesGroupsGroupListDto>.NewConfig()
            .Map(dest => dest.Name, source => $"{source.CandidateGroup.Name} ({source.CandidateGroup.CandidateBranch.Name})")
            .Map(dest => dest.Id, source => source.CandidateGroupId);


        //Group controller

        TypeAdapterConfig<CandidateCandidate, CandidatesGroupsCandidateListDto>.NewConfig()
        .Map(dest => dest.FullName, source => (source.FirstName + source.LastName).Length > 50 ?
        $"{source.FirstName.First()}. {source.LastName} ( {source.Email} )" :$"{source.FirstName} {source.LastName} ( {source.Email} )");

        TypeAdapterConfig<CandidatesGroups, CandidatesGroupsCandidateListDto>.NewConfig()
        .Map(dest => dest.FullName, source => (source.Candidate.FirstName + source.Candidate.LastName).Length > 50 ?
        $"{source.Candidate.FirstName.First()}. {source.Candidate.LastName} ( {source.Candidate.Email} )" : 
        $"{source.Candidate.FirstName} {source.Candidate.LastName} ( {source.Candidate.Email} )")
        .Map(dest=>dest.Id,source=>source.CandidateId);


        //TypeAdapterConfig<CandidatesGroups, CandidatesGroupsGroupCandidatesDto>.NewConfig()
        //    .Map(dest => dest.Name, source => $"{source.CandidateGroup.Name}")
        //    .Map(dest => dest.GroupId, source => source.CandidateGroup.Id);

        TypeAdapterConfig<CandidateCandidateGroup, CandidatesGroupsGroupCandidatesDto>.NewConfig()
            .Map(dest=>dest.GroupId,source=>source.Id)
            .Map(dest => dest.Name, source => $"{source.Name} ({source.CandidateBranch.Name})");
    }
}
