using BAExamApp.Business.Interfaces.Services.Candidate;
using BAExamApp.Business.Services.Candidate;
using BAExamApp.Core.Enums;
using BAExamApp.Core.Utilities.Results.Concrete;
using BAExamApp.Dtos.Branches;
using BAExamApp.Dtos.Candidate.CandidateBranches;
using BAExamApp.Dtos.Candidate.CandidateGroups;
using BAExamApp.Dtos.Products;
using BAExamApp.Dtos.ProductSubjects;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.MVC.Areas.Admin.Models.BranchVMs;
using BAExamApp.MVC.Areas.Admin.Models.ProductVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateGroupVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidatesGroupsVMs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class CandidateGroupController : CandidateAdminBaseController
{
    private readonly ICandidateBranchService _candidateBranchService;
    private readonly ICandidateGroupService _candidateGroupService;
    private readonly ICandidateCandidatesGroupsService _candidateCandidatesGroupsService;

    public CandidateGroupController(ICandidateBranchService candidateBranchService, ICandidateGroupService candidateGroupService, ICandidateCandidatesGroupsService candidateCandidatesGroupsService)
    {
        _candidateBranchService = candidateBranchService;
        _candidateGroupService = candidateGroupService;
        _candidateCandidatesGroupsService = candidateCandidatesGroupsService;
    }
    [HttpGet]
    public async Task<IActionResult> Index(bool showAllData = false)
    {
        var resultGroup = await _candidateGroupService.GetAllAsync();

        ViewBag.BranchList = await GetBranchssAsync();

        if (resultGroup.Data != null)
        {
            var candidateGroupListVMs = resultGroup.Data.Adapt<IEnumerable<CandidateGroupListVM>>();

            if (!showAllData)
            {
                candidateGroupListVMs = candidateGroupListVMs
                    .Where(x => x.Status != Status.Deleted && x.Status != Status.Passive)
                    .ToList();
            }

            ViewBag.ShowAllData = showAllData;

            foreach (var item in candidateGroupListVMs)
            {
                item.CandidateStatus= (int?)await _candidateGroupService.AnyStudentsInGroup(item.Id);
            }

            return View(candidateGroupListVMs);
        }
        else
        {
            return View(new List<CandidateGroupListVM>());
        }
    }

    private async Task<SelectList> GetBranchssAsync()
    {

        var BranchList = await _candidateBranchService.GetAllAsync();
        return new SelectList(BranchList.Data.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }), "Value", "Text");
    }
    public async Task<CandidateGroupEditVM> GetGroup(Guid groupId)
    {

        var groupFoundResult = await _candidateGroupService.GetByIdAsync(groupId);

        var groupUpdateVM = groupFoundResult.Data.Adapt<CandidateGroupEditVM>();
        groupUpdateVM.BranchList = await GetBranchssAsync();

        return groupUpdateVM;
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id, string status)
    {
        var resultGroup = await _candidateGroupService.GetByIdAsync(id);
        var resultBranch = await _candidateBranchService.GetByIdAsync(resultGroup.Data.CandidateBranchId);
        ViewBag.BranchList = await GetBranchssAsync();
        if (resultGroup.IsSuccess)
        {
            var groupDetails = resultGroup.Data.Adapt<CandidateGroupDetailVM>();
            ViewBag.Status = groupDetails.Status;
            groupDetails.Status = await _candidateGroupService.AnyStudentsInGroup(id);
            if (resultBranch != null)
            {
                groupDetails.BranchName = resultBranch.Data.Name;
            }

            return View(groupDetails);
        }

        NotifyErrorLocalized(resultGroup.Message);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete([FromQuery(Name = "id")] Guid id)
    {
        var deleteResult = await _candidateGroupService.DeleteAsync(id);
        if (!deleteResult.IsSuccess)
        {
            NotifyErrorLocalized(deleteResult.Message);
        }
        else
        {
            NotifySuccessLocalized(deleteResult.Message);
        }

        return Json(deleteResult);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CandidateGroupAddVM model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            string errorMessages = string.Join(", ", errors.Select(e => e.ErrorMessage));
            NotifyError(errorMessages);
            return RedirectToAction(nameof(Index));
        }

        var group = model.Adapt<CandidateGroupCreateDto>();
        var addResult = await _candidateGroupService.AddAsync(group);
        if (!addResult.IsSuccess)
        {
            NotifyErrorLocalized(addResult.Message);
            return RedirectToAction(nameof(Index));
        }

        NotifySuccessLocalized(addResult.Message);
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Update(CandidateGroupEditVM model)
    {

        if (!ModelState.IsValid)
        {
            model.BranchList = await GetBranchssAsync();
            return View(model);
        }

        var groupUpdate = model.Adapt<CandidateGroupUpdateDto>();
        var updateResult = await _candidateGroupService.UpdateAsync(groupUpdate);

        if (!updateResult.IsSuccess)
        {
            NotifyErrorLocalized(updateResult.Message);
            return View(model);
        }

        NotifySuccessLocalized(updateResult.Message);
        return RedirectToAction(nameof(Index));
    }

    public async Task<CandidatesGroupsGroupCandidatesVM> AssignCandidate(Guid id)
    {
        var result = await _candidateCandidatesGroupsService.GetCandidatesByGroupIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }

        var candidateGroupsVM = result.Data.Adapt<CandidatesGroupsGroupCandidatesVM>();

        return candidateGroupsVM;
    }

    [HttpPost]
    public async Task<IActionResult> AssignCandidate(Guid groupId, Guid[] remainingCandidates)
    {
        var result = await _candidateCandidatesGroupsService.UpdateGroupCandidatesAsync(groupId, remainingCandidates);

        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }


        return RedirectToAction("Index");
    }
    public async Task<IActionResult> ChangeStatus([FromQuery(Name = "id")] Guid id)
    {
        // Fetch the group by ID
        var activeResult = await _candidateGroupService.GetByIdAsync(id);

        if (activeResult == null || !activeResult.IsSuccess)
        {
            // Return a JSON response indicating failure if the group is not found
            return Json(new { IsSuccess = false, Message = "Group not found." });
        }

        var candidateGroup = activeResult.Data;

        // Toggle the status
        candidateGroup.Status = candidateGroup.Status == Status.Active ? Status.Passive : Status.Active;

        // Update the group status
        var updateResult = await _candidateGroupService.UpdateAsync(candidateGroup.Adapt<CandidateGroupUpdateDto>());

        if (!updateResult.IsSuccess)
        {
            NotifyErrorLocalized(updateResult.Message);
            // Return a JSON response indicating failure if the update operation failed
            return Json(new { IsSuccess = false, Message = updateResult.Message });
        }
        NotifySuccessLocalized(updateResult.Message);
        // Return a JSON response indicating success
        return Json(new { IsSuccess = true, Message = updateResult.Message });
    }
}
