using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Dtos.Candidate.CandidateQuestionAnswers;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionAnswerVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionVMs;
using BAExamApp.MVC.Extensions;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class CandidateQuestionController : CandidateAdminBaseController
{
    private readonly ICandidateQuestionService _candidateQuestionService;
    private readonly ICandidateAnswerService _candidateAnswerService;
    private readonly ICandidateCandidateAnswerRepository _candidateCandidateAnswerRepository;
    private readonly ICandidateQuestionSubjectService _candidateQuestionSubjectService;
    private readonly IStringLocalizer<SharedModelResource> _stringLocalizer;

    public CandidateQuestionController(ICandidateQuestionService candidateQuestionService, ICandidateAnswerService candidateAnswerService, ICandidateCandidateAnswerRepository candidateCandidateAnswerRepository,ICandidateQuestionSubjectService candidateQuestionSubjectService, IStringLocalizer<SharedModelResource> stringLocalizer)
    {
        _candidateQuestionService = candidateQuestionService;
        _candidateAnswerService = candidateAnswerService;
        _candidateCandidateAnswerRepository = candidateCandidateAnswerRepository;
        _candidateQuestionSubjectService = candidateQuestionSubjectService;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<IActionResult> Index(bool showAllQuestions = false, bool showPassiveQuestions = false)
    {
        ViewBag.ShowAllQuestions = showAllQuestions;

        var vm = new CandidateQuestionIndexVM()
        {
            CandidateQuestionTypeList = GetCandidateQuestionTypeList(),
            CandidateQuestionSubjectList = await GetCandidateSubjectListAsync()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Index(CandidateQuestionIndexVM candidateQuestionIndexVM)
    {
        var result = await _candidateQuestionService.GetQuestionBySearchValues(candidateQuestionIndexVM.Content, candidateQuestionIndexVM.CandidateQuestionTypeIndex, candidateQuestionIndexVM.CandidateQuestionSubjectIndex);



        var subjects = await _candidateQuestionSubjectService.GetAllAsync();
        ViewBag.CandidateQuestionSubjects = subjects.Data;




        if (result.IsSuccess)
        {
            if (candidateQuestionIndexVM.ShowPassiveQuestions)
            {
                candidateQuestionIndexVM.QuestionList = result.Data.Adapt<List<CandidateQuestionListVM>>();
            }
            else
            {
                candidateQuestionIndexVM.QuestionList = result.Data.Where(x => x.Status == Core.Enums.Status.Active).Adapt<List<CandidateQuestionListVM>>();
            }
            NotifySuccessLocalized(result.Message);
        }
        else
        {
            NotifyErrorLocalized(result.Message);
        }
        candidateQuestionIndexVM.CandidateQuestionTypeList = GetCandidateQuestionTypeList();
        candidateQuestionIndexVM.CandidateQuestionSubjectList = await GetCandidateSubjectListAsync();
        return View(candidateQuestionIndexVM);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var subjects=await _candidateQuestionSubjectService.GetAllAsync();
        var subjectList = subjects.Data.Where(x => x.Status != Core.Enums.Status.Passive).ToList();

        ViewBag.CandidateQuestionSubjects = subjectList;
        ViewBag.CandidateQuestionType = GetCandidateQuestionTypeList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CandidateQuestionCreateVM candidateQuestionCreateVM, IFormCollection collection)
    {
        List<CandidateAnswerCreateVM> questionAnswersList = JsonSerializer.Deserialize<List<CandidateAnswerCreateVM>>(collection["questionAnswerChoices"]);

        candidateQuestionCreateVM.QuestionAnswers = questionAnswersList;

        var question = candidateQuestionCreateVM.Adapt<CandidateQuestionCreateDto>();
        if (candidateQuestionCreateVM.NewImage != null)
        {
            question.Image = await candidateQuestionCreateVM.NewImage.FileToByteArrayAsync();
        }

        var addResult = await _candidateQuestionService.AddAsync(question);
        if (!addResult.IsSuccess)
        {
            NotifyErrorLocalized(addResult.Message);

            ViewBag.CandidateQuestionType = GetCandidateQuestionTypeList();
            ViewBag.QuestionAnswersList = JsonSerializer.Serialize(questionAnswersList);

            return View(candidateQuestionCreateVM);
        }

        NotifySuccessLocalized(addResult.Message);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _candidateQuestionService.GetDetailsByIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
            return NotFound();
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }

        var candidateQuestionDetailsVM = result.Data.Adapt<CandidateQuestionDetailsVM>();

        var localizedType = _stringLocalizer[Enum.GetName(typeof(CandidateQuestionType), candidateQuestionDetailsVM.CandidateQuestionType)];
        candidateQuestionDetailsVM.CandidateQuestionTypeString = localizedType;
        if (candidateQuestionDetailsVM.CandidateQuestionSubject!=null)
        candidateQuestionDetailsVM.CandidateQuestionSubjectString = candidateQuestionDetailsVM.CandidateQuestionSubject.Name;
        return Json(candidateQuestionDetailsVM);
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var questionResult = await _candidateQuestionService.GetByIdWithFilteredAnswersAsync(id);

        if (!questionResult.IsSuccess)
            return NotFound();

     

        CandidateQuestionUpdateVM candidateQuestionUpdateVM = questionResult.Data.Adapt<CandidateQuestionUpdateVM>();
        candidateQuestionUpdateVM.CandidateQuestionTypeList = GetCandidateQuestionTypeList();
        candidateQuestionUpdateVM.QuestionAnswersList = JsonSerializer.Serialize(candidateQuestionUpdateVM.QuestionAnswers);
        ViewBag.QuestionAnswersList = candidateQuestionUpdateVM.QuestionAnswersList;
        candidateQuestionUpdateVM.Image = questionResult.Data.Image;
        return Json(candidateQuestionUpdateVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(CandidateQuestionUpdateVM candidateQuestionUpdateVM, IFormCollection collection)
    {
        var updateQuestionResult = await _candidateQuestionService.GetByIdAsync(candidateQuestionUpdateVM.Id);
        if (!updateQuestionResult.IsSuccess)
        {
            NotifyErrorLocalized(updateQuestionResult.Message);
            return RedirectToAction(nameof(Index));
        }

        List<CandidateAnswerUpdateVM> questionAnswersList = JsonSerializer.Deserialize<List<CandidateAnswerUpdateVM>>(collection["questionAnswerChoices"]);

        // Resim kaldırma durumu kontrol ediliyor
        bool removeImage = collection["removeImageHidden"].ToString().ToLower() == "true";
        if (removeImage)
        {
            candidateQuestionUpdateVM.Image = null;
        }
        else if (candidateQuestionUpdateVM.NewImage != null)
        {
            candidateQuestionUpdateVM.Image = await candidateQuestionUpdateVM.NewImage.FileToByteArrayAsync();
        }
        else
        {
            candidateQuestionUpdateVM.Image = updateQuestionResult.Data.Image;
        }


        if (questionAnswersList.Count > 0)
        {
            var existingAnswers = updateQuestionResult.Data.QuestionAnswers;
            var updatedAnswers = questionAnswersList.Select(qa => qa.Id).ToList();

            var answersToUpdate = questionAnswersList.Where(qa => existingAnswers.Any(ea => ea.Id == qa.Id)).ToList();
            var answersToAdd = questionAnswersList.Where(qa => !existingAnswers.Any(ea => ea.Id == qa.Id)).ToList();
            var answersToDelete = existingAnswers
                .Where(ea => !updatedAnswers.Contains(ea.Id) && ea.Status != (Core.Enums.Status)4)
                .ToList();

            // Cevapları güncelle
            foreach (var answerToUpdate in answersToUpdate)
            {
                var updateResult = await _candidateAnswerService.UpdateAsync(answerToUpdate.Adapt<CandidateAnswerUpdateDto>());
                if (!updateResult.IsSuccess)
                {
                    NotifyErrorLocalized(updateResult.Message);
                    return RedirectToAction(nameof(Index));
                }
            }

            // Yeni cevapları ekle
            foreach (var answerToAdd in answersToAdd)
            {
                answerToAdd.QuestionId = candidateQuestionUpdateVM.Id;
                var addResult = await _candidateAnswerService.AddAsync(answerToAdd.Adapt<CandidateAnswerCreateDto>());
                if (!addResult.IsSuccess)
                {
                    NotifyErrorLocalized(addResult.Message);
                    return RedirectToAction(nameof(Index));
                }
            }

            // Silinecek cevapları sil
            foreach (var answerToDelete in answersToDelete)
            {
                var deleteResult = await _candidateAnswerService.DeleteAsync(answerToDelete.Id);
                if (!deleteResult.IsSuccess)
                {
                    NotifyErrorLocalized(deleteResult.Message);
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // Soruyu güncelle
        var candidateQuestionUpdateDto = candidateQuestionUpdateVM.Adapt<CandidateQuestionUpdateDto>();
        var questionResult = await _candidateQuestionService.UpdateAsync(candidateQuestionUpdateDto);

        if (questionResult.IsSuccess)
        {
            NotifySuccessLocalized(questionResult.Message);
        }
        else
        {
            NotifyErrorLocalized(questionResult.Message);
        }

        ViewBag.CandidateQuestionType = GetCandidateQuestionTypeList();
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _candidateQuestionService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return Json(new { isSuccess = false, message = result.Message });
        }

        return Json(new { isSuccess = true });
    }

    [HttpPost]
    public async Task<IActionResult> SetQuestionAndAnswersToInactive(Guid id)
    {
        var result = await _candidateQuestionService.SetQuestionAndAnswersToInactiveAsync(id);
        if (!result.IsSuccess)
        {
            return Json(new { isSuccess = false, message = result.Message });
        }

        return Json(new { isSuccess = true });
    }


    [HttpPost]
    public async Task<IActionResult> SetQuestionAndAnswersToActive(Guid id)
    {
        var result = await _candidateQuestionService.SetQuestionAndAnswersToActiveAsync(id);
        if (!result.IsSuccess)
        {
            return Json(new { isSuccess = false, message = result.Message });
        }

        return Json(new { isSuccess = true });
    }

    // Helper Method
    private List<SelectListItem> GetCandidateQuestionTypeList()
    {
        return Enum.GetValues(typeof(CandidateQuestionType)).Cast<CandidateQuestionType>().
             Select(x => new SelectListItem
             {
                 Text = Localize(x.GetType().GetMember(x.ToString()).First().GetCustomAttribute<DisplayAttribute>()!.GetName()!),
                 Value = ((int)x).ToString()
             }).ToList();
    }

     private async Task<List<SelectListItem>> GetCandidateSubjectListAsync()
    {
        var subjectsResult = await _candidateQuestionSubjectService.GetAllAsync();
        return subjectsResult.Data.Select(subject => new SelectListItem
        {
            Text = subject.Name,
            Value = subject.Id.ToString()
        }).ToList();
    }
}
