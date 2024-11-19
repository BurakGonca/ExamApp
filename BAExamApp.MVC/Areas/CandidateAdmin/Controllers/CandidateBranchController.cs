using BAExamApp.Business.Constants;
using BAExamApp.Business.Interfaces.Services.Candidate;
using BAExamApp.Business.Services.Candidate;
using BAExamApp.Core.Enums;
using BAExamApp.Core.Utilities.Results;
using BAExamApp.Core.Utilities.Results.Concrete;
using BAExamApp.Dtos.Branches;
using BAExamApp.Dtos.Candidate.CandidateBranches;
using BAExamApp.Dtos.Products;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.MVC.Areas.Admin.Models.BranchVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateBranchVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateGroupVMs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class CandidateBranchController : CandidateAdminBaseController
{
    private readonly ICandidateBranchService _candidateBranchService;
    private readonly ICandidateCandidatesGroupsService _candidateGroupService;
    public CandidateBranchController(ICandidateBranchService candidateBranchService, ICandidateCandidatesGroupsService candidateCandidatesGroupsService)
    {
        _candidateBranchService = candidateBranchService;
        _candidateGroupService = candidateCandidatesGroupsService;
    }
    
    public async Task<IActionResult> Index(bool showAllData = false)
    {
        var result = await _candidateBranchService.GetAllAsync();
        if (result.Data != null)
        {
            var candidateBranch = result.Data.Adapt<List<CandidateBranchListVM>>();
            if (!showAllData)
            {
                candidateBranch = candidateBranch.Where(product => product.Status != Status.Deleted && product.Status != Status.Passive).ToList();
            }
            ViewBag.ShowAllData = showAllData;
            NotifySuccessLocalized(result.Message);
            return View(candidateBranch);
        }
        else
        {
            return View(new List<CandidateBranchListVM>());
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(Guid id, bool showAllData = false)
    {
        var getBranchResponse = await _candidateBranchService.GetDetailsByIdAsync(id);

        if (getBranchResponse.IsSuccess)
        {
            var candidateBranch = getBranchResponse.Data.Adapt<CandidateBranchDetailsVM>();

            if (!showAllData && !candidateBranch.ShowPassiveGroups)
            {
                candidateBranch.CandidateGroups = candidateBranch.CandidateGroups
                    .Where(group => group.Status != Status.Passive)
                    .ToList();
            }

            ViewBag.ShowAllData = showAllData;
            return View(candidateBranch);
        }

        NotifyErrorLocalized(getBranchResponse.Message);
        return RedirectToAction(nameof(Index));
    }




    [HttpPost]
    public async Task<IActionResult> Create(CandidateBranchCreateVM candidateBranchCreateVM)
    {
        if(ModelState.IsValid)
        {
            try
            {
                // ViewModel'i DTO'ya dönüştür
                var candidateBranch = candidateBranchCreateVM.Adapt<CandidateBranchCreateDto>();
                
                // Servis katmanına DTO'yu ileterek yeni aday yöneticisi eklemeyi çağır
                var result = await _candidateBranchService.AddAsync(candidateBranch);

                // Servis işlem sonucunu kontrol et
                if (!result.IsSuccess)
                {
                    // Başarılı ise kullanıcıya başarılı mesajını göster ve Index sayfasına yönlendir
                    NotifyErrorLocalized(result.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Başarısız ise kullanıcıya hata mesajını göster ve Index sayfasına yönlendir
                    NotifySuccessLocalized(result.Message);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Hata oluştuğunda kullanıcıya genel bir hata mesajı göster ve Index sayfasına yönlendir
                NotifyError(Messages.BranchNotFound + " - " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        // ModelState.IsValid false ise ModelState hatalarını görüntülemek için aynı view'a dön
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Update(CandidateBranchUpdateVM model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index));
            }

            // ViewModel'i DTO'ya dönüştür
            var candidateBranch = model.Adapt<CandidateBranchUpdateDto>();

            // Servis katmanına DTO'yu ileterek yeni aday yöneticisi eklemeyi çağır
            var result = await _candidateBranchService.UpdateAsync(candidateBranch);

            // Servis işlem sonucunu kontrol et
            if (!result.IsSuccess)
            {
                // Başarılı ise kullanıcıya başarılı mesajını göster ve Index sayfasına yönlendir
                NotifyErrorLocalized(result.Message);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Başarısız ise kullanıcıya hata mesajını göster ve Index sayfasına yönlendir
                NotifySuccessLocalized(result.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            // Hata oluştuğunda kullanıcıya genel bir hata mesajı göster ve Index sayfasına yönlendir
            NotifyError(Messages.BranchNotFound + " - " + ex.Message);
            return RedirectToAction(nameof(Index));
        }
    }
    
    public async Task<IActionResult> Delete([FromQuery(Name = "id")] Guid id)
    {
        var deleteResult = await _candidateBranchService.DeleteAsync(id);
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
    public async Task<IActionResult> CheckActiveGroups([FromQuery(Name = "id")] Guid id)
    {
        var result = await _candidateBranchService.CheckActiveGroupsAsync(id);
        return Json(result);
    }

    
    public async Task<IActionResult> MarkBranchAsPassive([FromQuery(Name = "id")] Guid id)
    {
        var result = await _candidateBranchService.MarkBranchAsPassiveAsync(id);
        return Json(result);
    }

    [HttpPost]
    public async Task<IActionResult> SetBranchAndAnswersToInactive(Guid id)
    {
        var result = await _candidateBranchService.SetBranchAndAnswersToInactiveAsync(id);
        if (!result.IsSuccess)
        {
            return Json(new { isSuccess = false, message = result.Message });
        }
        NotifySuccessLocalized(result.Message);
        return Json(new { isSuccess = true, message = result.Message });
    }

    [HttpPost]
    public async Task<IActionResult> SetBranchAndAnswersToActive(Guid id)
    {
        var result = await _candidateBranchService.SetBranchAndAnswersToActiveAsync(id);
        if (!result.IsSuccess)
        {
            return Json(new { isSuccess = false, message = result.Message });
        }
        NotifySuccessLocalized(result.Message);
        return Json(new { isSuccess = true, message = result.Message });
    }

}
