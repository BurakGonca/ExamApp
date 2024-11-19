using AutoMapper;
using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;
using BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;
using BAExamApp.MVC.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class CandidateQuestionSubjectController : CandidateAdminBaseController
{
    private readonly ICandidateQuestionSubjectService _candidateQuestionSubjectService;
    private readonly IMapper _mapper;
    private readonly ICandidateQuestionService _candidateQuestionService;

    public CandidateQuestionSubjectController(ICandidateQuestionSubjectService candidateQuestionSubjectService, IMapper mapper, ICandidateQuestionService candidateQuestionService)
    {
        _candidateQuestionSubjectService = candidateQuestionSubjectService;
        _mapper = mapper;
        _candidateQuestionService = candidateQuestionService;
    }


    [HttpGet]
    public async Task<IActionResult> Index(bool showAllData = false)
    {
        ViewBag.SubjectList = await _candidateQuestionSubjectService.GetAllAsync();

        var getSubjectListResult = await _candidateQuestionSubjectService.GetAllAsync();
        var subjectList = getSubjectListResult.Data.Adapt<IEnumerable<CandidateAdminQuestionSubjectListVM>>();
        //var subjectList = _mapper.Map<IEnumerable<CandidateAdminQuestionSubjectListVM>>(getSubjectListResult.Data);
        if (!showAllData)
        {
            subjectList = subjectList.Where(subject => subject.Status != Status.Deleted && subject.Status != Status.Passive);
        }
        ViewBag.ShowAllData = showAllData;
        return View(subjectList);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CandidateAdminQuestionSubjectCreateVM viewModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            string errorMessages = null!;

            foreach (var error in errors)
            {
                errorMessages += " ," + error.ErrorMessage;
            }

            NotifyError(errorMessages);

            return RedirectToAction(nameof(Index));
        }

        var subjectCreateVmToDto = viewModel.Adapt<CandidateQuestionSubjectCreateDto>();

        subjectCreateVmToDto.Name = StringExtensions.TitleFormat(viewModel.Name);

        var createSubjectResult = await _candidateQuestionSubjectService.AddAsync(subjectCreateVmToDto);
        if (!createSubjectResult.IsSuccess)
        {
            NotifyErrorLocalized(createSubjectResult.Message);
            return RedirectToAction(nameof(Index));
        }
        NotifySuccessLocalized(createSubjectResult.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        // Konu detaylarını id ile getir
        var subjectDto = await _candidateQuestionSubjectService.GetDetailsByIdAsync(id);

        // Konu içindeki soruların kullanılıp kullanılmadığını kontrol et
        bool isQuestionUsed = await _candidateQuestionSubjectService.IsQuestionUsedInSubjectAsync(id);

        if (subjectDto.IsSuccess)
        {
            // Konu detaylarını ViewModel'e dönüştür
            var subjectDetailDtoToVm = subjectDto.Data.Adapt<CandidateAdminQuestionSubjectDetailVM>();

            subjectDetailDtoToVm.IsQuestionUsed = isQuestionUsed;

            // View'a dön
            return View(subjectDetailDtoToVm);
        }

        // Eğer konu bulunamazsa hata mesajı göster ve Index sayfasına yönlendir
        NotifyErrorLocalized(subjectDto.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AdminSubjectUpdateVM viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Index));
        }

        var updateDtoToVm = viewModel.Adapt<CandidateQuestionSubjectUpdateDto>();

        updateDtoToVm.Name = StringExtensions.TitleFormat(viewModel.Name);

        var updateResult = await _candidateQuestionSubjectService.UpdateAsync(updateDtoToVm);
        if (!updateResult.IsSuccess)
        {
            NotifyErrorLocalized(updateResult.Message);
        }
        else
        {
            NotifySuccessLocalized(updateResult.Message);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isQuestionUsed = await _candidateQuestionSubjectService.IsQuestionUsedInSubjectAsync(id);

        if (isQuestionUsed)
        {
            return await InactivateSubjectAndRelatedQuestionsWithAnswersAsync(id);
        }
        else
        {
            return await DeleteConfirmed(id);
        }
    }

    [HttpPost]
    public async Task<IActionResult> ActivateSubjectAndRelatedQuestionsWithAnswers(Guid id)
    {
        var result = await _candidateQuestionSubjectService.ActivateSubjectAndRelatedQuestionsWithAnswersAsync(id);
        if (!result.IsSuccess)
        {
            return Json(new { isSuccess = false, message = result.Message });
        }

        return Json(new { isSuccess = true });
    }

    [HttpPost]

    public async Task<IActionResult> InactivateSubjectAndRelatedQuestionsWithAnswersAsync(Guid id)
    {
        var result = await _candidateQuestionSubjectService.InactivateSubjectAndRelatedQuestionsWithAnswersAsync(id);
        if (!result.IsSuccess)
        {
            return Json(new { isSuccess = false, message = result.Message });
        }

        return Json(new { isSuccess = true });
    }


    // Helper Methods
    private async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var deleteResult = await _candidateQuestionSubjectService.DeleteAsync(id);
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

    public async Task<IActionResult> CheckIfQuestionIsActive(Guid id)
    {
        // Konu ile ilişkili soruları al
        var result = await _candidateQuestionService.GetQuestionsBySubjectIdAsync(id);

        if (!result.IsSuccess)
        {
            return Json(new { hasActiveQuestions = false });
        }

        if (result.Data == null || !result.Data.Any())
        {
            return Json(new { hasActiveQuestions = false });
        }

        bool hasActiveQuestions = result.Data.Any(cq => cq.Status == Status.Active);

        // Sonucu döndür
        return Json(new { hasActiveQuestions });
    }
}
