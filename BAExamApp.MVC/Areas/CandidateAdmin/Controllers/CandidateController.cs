using BAExamApp.Business.Constants;
using BAExamApp.Business.Interfaces.Services.Candidate;
using BAExamApp.Dtos.Candidate.Candidates;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateAdminVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateCandidateVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidatesGroupsVMs;
using BAExamApp.MVC.Extensions;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class CandidateController : CandidateAdminBaseController
{
    private readonly ICandidateService _candidateService;
    private readonly ICandidateCandidatesGroupsService _candidateCandidatesGroupsService;
    private readonly ICandidateAdminService _candidateAdminService;

    public CandidateController(ICandidateService candidateService, ICandidateCandidatesGroupsService candidateCandidatesGroupsService,  ICandidateAdminService candidateAdminService)
    {
        _candidateService = candidateService;
        _candidateCandidatesGroupsService = candidateCandidatesGroupsService;
        _candidateAdminService = candidateAdminService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _candidateService.GetAllAsync();
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        return View(result.Data.Adapt<List<CandidateCandidateListVM>>());
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _candidateService.GetDetailsByIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }

        var viewModel = result.Data.Adapt<CandidateCandidateDetailsVM>();

        // Adayın gruplarını alma işlemi
        var groupResult = await _candidateService.GetCandidateGroupsAsync(id);
        if (groupResult.IsSuccess)
        {
            viewModel.GroupNames = groupResult.Data.Select(g => g.GroupName).ToList();
            viewModel.CandidateGroups = groupResult.Data; // İhtiyaç halinde tüm grup detaylarını da view model'e ekleyebilirsiniz.
        }

        // Adayın sınavlarını alma işlemi
        var examsResult = await _candidateService.GetCandidateExamsAsync(id);
        if (examsResult.IsSuccess)
        {
            viewModel.Exams = examsResult.Data.Exams;
        }

        return View(viewModel);
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CandidateCandidateCreateVM createVM)
    {
        var candidate = createVM.Adapt<CandidateCreateDto>();
        if (createVM.NewImage is not null)
        {
            candidate.Image = await createVM.NewImage.FileToByteArrayAsync();
        }
        var result = await _candidateService.AddAsync(candidate);
        if (result.IsSuccess)
        {
            NotifySuccessLocalized(result.Message);
        }
        else
        {
            NotifyErrorLocalized(result.Message);
        }
        return RedirectToAction("Index");
    }

    public async Task<CandidateCandidateUpdateVM> Update(Guid id)
    {
        var result = await _candidateService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        var candidate = result.Data.Adapt<CandidateCandidateUpdateVM>();
        return candidate;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Update(CandidateCandidateUpdateVM updateVM)
    {
        var result = await _candidateService.GetByIdAsync(updateVM.Id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
            return RedirectToAction("Index");
        }

        var candidateDto = updateVM.Adapt<CandidateUpdateDto>();
        if (updateVM.NewImage != null)
        {
            candidateDto.Image = await updateVM.NewImage.FileToByteArrayAsync();
        }
        var updateResult = await _candidateService.UpdateAsync(candidateDto);

        if (updateResult.IsSuccess)
        {
            NotifySuccessLocalized(updateResult.Message);
        }
        else
        {
            NotifyErrorLocalized(updateResult.Message);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _candidateService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
            return RedirectToAction("Index");
        }

        var deleteResult = await _candidateService.DeleteAsync(id);
        if (deleteResult.IsSuccess)
        {
            NotifySuccessLocalized(deleteResult.Message);
        }
        else
        {
            NotifyErrorLocalized(deleteResult.Message);
        }

        return RedirectToAction("Index");
    }

    public async Task<CandidatesGroupsCandidateGroupsVM> AssignGroup(Guid id)
    {
        var result = await _candidateCandidatesGroupsService.GetGroupsByCandidateIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }

        var candidateGroupsVM = result.Data.Adapt<CandidatesGroupsCandidateGroupsVM>();
        return candidateGroupsVM;
    }

    [HttpPost]
    public async Task<IActionResult> AssignGroup(Guid candidateId, Guid[] remainingGroups)
    {
        var result = await _candidateCandidatesGroupsService.UpdateCandidateGroupsAsync(candidateId, remainingGroups);

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

    public async Task<IActionResult> ToggleCandidateRetakePermission(Guid id)
    {
        var result = await _candidateAdminService.AllowCandidateToRetakeExamAsync(id);

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

}
