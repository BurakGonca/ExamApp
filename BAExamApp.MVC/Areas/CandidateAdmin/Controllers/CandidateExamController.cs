using BAExamApp.Business.Constants;
using BAExamApp.Core.Enums;
using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Dtos.Candidate.CandidateExamRule;
using BAExamApp.Dtos.Candidate.CandidateExams;
using BAExamApp.Dtos.Candidate.CandidateQuestionRule;
using BAExamApp.Dtos.SendMails;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionRuleVMs;
using BAExamApp.MVC.Areas.Student.Models.StudentExamVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class CandidateExamController : CandidateAdminBaseController
{
    private readonly ICandidateExamService _candidateExamService;
    private readonly ICandidateGroupService _candidateGroupService;
    private readonly ICandidateService _candidateService;
    private readonly ICandidateQuestionService _candidateQuestionService;
    private readonly IStringLocalizer<SharedModelResource> _stringLocalizer;
    private readonly ICandidateCandidateAnswerRepository _candidateCandidateAnswerRepository;
    private readonly ICandidateQuestionSubjectService _candidateQuestionSubjectService;
    private readonly ICandidateExamRuleService _candidateExamRuleService;
    private readonly ICandidateQuestionRuleService _candidateQuestionRuleService;

    public CandidateExamController(ICandidateExamService candidateExamService, ICandidateGroupService candidateGroupService, ICandidateService candidateService, ICandidateQuestionService candidateQuestionService, IStringLocalizer<SharedModelResource> stringLocalizer, ICandidateCandidateAnswerRepository candidateCandidateAnswerRepository,
        ICandidateQuestionSubjectService candidateQuestionSubjectService, ICandidateExamRuleService candidateExamRuleService, ICandidateQuestionRuleService candidateQuestionRuleService)
    {
        _candidateExamService = candidateExamService;
        _candidateGroupService = candidateGroupService;
        _candidateService = candidateService;
        _candidateQuestionService = candidateQuestionService;
        _stringLocalizer = stringLocalizer;
        _candidateCandidateAnswerRepository = candidateCandidateAnswerRepository;
        _candidateQuestionSubjectService = candidateQuestionSubjectService;
        _candidateExamRuleService = candidateExamRuleService;
        _candidateQuestionRuleService = candidateQuestionRuleService;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _candidateExamService.GetAllAsync();

        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }

        return View(result.Data.Adapt<List<CandidateExamListVM>>());
    }

    public async Task<IActionResult> Create()
    {
        var groups = await _candidateGroupService.GetAllAsync();
        if (!groups.IsSuccess)
        {
            NotifyErrorLocalized(groups.Message);
        }
        else
        {
            ViewBag.GroupList = groups.Data.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }

        var candidates = await _candidateService.GetAllAsync();
        if (!candidates.IsSuccess)
        {
            NotifyErrorLocalized(candidates.Message);
        }
        else
        {
            ViewBag.CandidateList = candidates.Data.Select(x => new SelectListItem { Text = x.FirstName + " " + x.LastName, Value = x.Id.ToString() }).ToList();
        }


        ViewBag.QuestionCounts = await _candidateQuestionSubjectService.GetAllQuestionsCountsBySubjectAndType();

        var candidateQuestionSubjects = await _candidateQuestionSubjectService.GetAllAsync();
        if (!candidateQuestionSubjects.IsSuccess)
        {
            NotifyErrorLocalized(candidateQuestionSubjects.Message);
        }
        else
        {
            ViewBag.candidateQuestionSubjectsList = candidateQuestionSubjects.Data.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CandidateExamQuestionItemsCreateVM candidateExamQuestionItemsCreateVM)
    {
        //2 vm kullanabilmek icin tek vm altinda topladim. Isimlendirmesinin kisaltilmasi icin esitliyorum
        var questionItemsVM = candidateExamQuestionItemsCreateVM.CandidateExamQuestionItemsVM;
        var examCreateVM = candidateExamQuestionItemsCreateVM.CandidateExamCreateVM;
        ViewBag.QuestionCounts = await _candidateQuestionSubjectService.GetAllQuestionsCountsBySubjectAndType();

        var questionsResult = await _candidateQuestionService.GetAllAsync();
        if (!questionsResult.IsSuccess)
        {
            NotifyErrorLocalized(questionsResult.Message);
            return View("Index");
        }
        var questions = questionsResult.Data.Where(x => x.Status == Status.Active).ToList();

        List<CandidateQuestionListDto> myQuestionLists = new List<CandidateQuestionListDto>();


        foreach (var item in questionItemsVM) // view tarafında istenilen soru bilgilerine göre database den random sorular seçiliyor
        {
            var filteredQuestions = questions.Where(x => x.CandidateQuestionSubject.Id == item.CandidateQuestionSubject.Id && x.CandidateQuestionType == Convert.ToInt32(item.CandidateQuestionType)).ToList();
            if (item.QuestionCount > filteredQuestions.Count)
            {
                NotifyError("Lütfen soru sayılarını kontrol ediniz.");
                ModelState.AddModelError("TestQuestionCount", string.Format(_stringLocalizer["Not_Enough_Questions"]) + $" ( {filteredQuestions.Count} )");
            }
            var randomQuestions = GetRandomQuestions(filteredQuestions, item.QuestionCount);
            myQuestionLists.AddRange(randomQuestions);
        }

        //Soru tiplerine gore secilen sorularin toplamlari
        examCreateVM.TestQuestionCount = myQuestionLists.Where(x=>x.CandidateQuestionType == 1).Count();
        examCreateVM.ClassicQuestionCount = myQuestionLists.Where(x => x.CandidateQuestionType == 3).Count();
        examCreateVM.AlgorithmQuestionCount = myQuestionLists.Where(x => x.CandidateQuestionType == 2).Count();


        //Soru tipine gore database soru havuzunun toplami
        var totalTestCount = questions.Where(x => x.CandidateQuestionType == 1).Count();
        var totalClassicCount = questions.Where(x => x.CandidateQuestionType == 3).Count();
        var totalAlgorithmCount = questions.Where(x => x.CandidateQuestionType == 2).Count(); 



        if (examCreateVM.Name == null || examCreateVM.Name.Length == 0)
            ModelState.AddModelError("Name", "Sınav adı zorunludur.");


        if (examCreateVM.TestQuestionCount > totalTestCount)
            ModelState.AddModelError("TestQuestionCount", string.Format(_stringLocalizer["Not_Enough_Questions"]) + $" ( {totalTestCount} )");

        if (examCreateVM.ClassicQuestionCount > totalClassicCount)
            ModelState.AddModelError("ClassicQuestionCount", string.Format(_stringLocalizer["Not_Enough_Questions"]) + $" ( {totalClassicCount} )");

        if (examCreateVM.AlgorithmQuestionCount > totalAlgorithmCount)
            ModelState.AddModelError("AlgorithmQuestionCount", string.Format(_stringLocalizer["Not_Enough_Questions"]) + $" ( {totalAlgorithmCount} )");

        if (examCreateVM.forGroup && examCreateVM.GroupIds == null)
            ModelState.AddModelError("GroupIds", "Grup seçmediniz.");

        if (!examCreateVM.forGroup && examCreateVM.CandidateIds == null)
            ModelState.AddModelError("CandidateIds", "Aday Seçmediniz.");

        if (examCreateVM.TestQuestionCount == 0 && examCreateVM.ClassicQuestionCount == 0 && examCreateVM.AlgorithmQuestionCount == 0)
            ModelState.AddModelError("AlgorithmQuestionCount", "Soru Seçmediniz.");
        if (examCreateVM.TestQuestionsCoefficient + examCreateVM.ClassicQuestionsCoefficient + examCreateVM.AlgorithmQuestionsCoefficient != 100)
            ModelState.AddModelError("AlgorithmQuestionsCoefficient", "Puan ağırlıkları toplamı 100 olmalı.");

        if (examCreateVM.ExamDuration == TimeSpan.Zero)
        {
            ModelState.AddModelError("ExamDuration", "Sınav süresi 0 olamaz.");
        }
        if (!ModelState.IsValid)
        {
            var groups = await _candidateGroupService.GetAllAsync();
            if (!groups.IsSuccess)
            {
                NotifyErrorLocalized(groups.Message);
            }
            else
            {
                ViewBag.GroupList = groups.Data.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            }

            var candidates = await _candidateService.GetAllAsync();
            if (!candidates.IsSuccess)
            {
                NotifyErrorLocalized(candidates.Message);
            }
            else
            {
                ViewBag.CandidateList = candidates.Data.Select(x => new SelectListItem { Text = x.FirstName + " " + x.LastName, Value = x.Id.ToString() }).ToList();
            }

            var candidateQuestionSubjects = await _candidateQuestionSubjectService.GetAllAsync();
            if (!candidateQuestionSubjects.IsSuccess)
            {
                NotifyErrorLocalized(candidateQuestionSubjects.Message);
            }
            else
            {
                ViewBag.candidateQuestionSubjectsList = candidateQuestionSubjects.Data.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            }

            return View(candidateExamQuestionItemsCreateVM);
        }


        var createDto = examCreateVM.Adapt<CandidateExamCreateDto>();

        var result = await _candidateExamService.AddAsync(createDto, myQuestionLists);// exam service'e sinav bilgileri ve random olusturulmus soru listesi gonderiliyor

        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            string baseUrl = Url.Action("StartExam", "CandidateExamInitiation", new { Area = "CandidateAdmin", examId = result.Data.Id }, Request.Scheme);
            await _candidateExamService.NotifyCandidateAdminAboutExamCreationAsync(result.Data.Id, baseUrl);

            NotifySuccessLocalized(result.Message);
        }

        return RedirectToAction("Index");

    }

    public async Task<IActionResult> CreateLink()
    {
        ViewBag.QuestionCounts = await _candidateQuestionSubjectService.GetAllQuestionsCountsBySubjectAndType();

        var candidateQuestionSubjects = await _candidateQuestionSubjectService.GetAllAsync();
        if (!candidateQuestionSubjects.IsSuccess)
        {
            NotifyErrorLocalized(candidateQuestionSubjects.Message);
        }
        else
        {
            ViewBag.candidateQuestionSubjectsList = candidateQuestionSubjects.Data.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateLink(CandidateExamLinkCreateVM candidateExamLinkCreateVM)
    {
        ViewBag.QuestionCounts = await _candidateQuestionSubjectService.GetAllQuestionsCountsBySubjectAndType();

        //2 vm kullanabilmek icin tek vm altinda topladim. Isimlendirmesinin kisaltilmasi icin esitliyorum
        var questionRulesCreteVM = candidateExamLinkCreateVM.CandidateQuestionRuleCreateVM;
        var examCreateVM = candidateExamLinkCreateVM.CandidateExamCreateVM;
        var examRuleCreateVM = candidateExamLinkCreateVM.CandidateExamRuleCreateVM;

        var questionsResult = await _candidateQuestionService.GetAllAsync();
        if (!questionsResult.IsSuccess)
        {
            NotifyErrorLocalized(questionsResult.Message);
            return View("Index");
        }
        var questions = questionsResult.Data.Where(x => x.Status == Status.Active).ToList();

        List<CandidateQuestionListDto> myQuestionLists = new List<CandidateQuestionListDto>();


        foreach (var item in questionRulesCreteVM) // view tarafında istenilen soru bilgilerine göre database den random sorular seçiliyor
        {
            var filteredQuestions = questions.Where(x => x.CandidateQuestionSubject.Id == item.CandidateQuestionSubjectId && x.CandidateQuestionType == Convert.ToInt32(item.CandidateQuestionType)).ToList();

            var randomQuestions = GetRandomQuestions(filteredQuestions, item.QuestionCount);
            myQuestionLists.AddRange(randomQuestions);
        }

        //Soru tiplerine gore secilen sorularin toplamlari
        examCreateVM.TestQuestionCount = myQuestionLists.Where(x => x.CandidateQuestionType == 1).Count();
        examCreateVM.ClassicQuestionCount = myQuestionLists.Where(x => x.CandidateQuestionType == 3).Count();
        examCreateVM.AlgorithmQuestionCount = myQuestionLists.Where(x => x.CandidateQuestionType ==2).Count();


        //Soru tipine gore database soru havuzunun toplami
        var totalTestCount = questions.Where(x => x.CandidateQuestionType == 1).Count();
        var totalClassicCount = questions.Where(x => x.CandidateQuestionType == 3).Count();
        var totalAlgorithmCount = questions.Where(x => x.CandidateQuestionType == 2).Count();



        if (examCreateVM.Name == null || examCreateVM.Name.Length == 0)
            ModelState.AddModelError("Name", "Sınav adı zorunludur.");


        if (examCreateVM.TestQuestionCount > totalTestCount)
            ModelState.AddModelError("TestQuestionCount", string.Format(_stringLocalizer["Not_Enough_Questions"]) + $" ( {totalTestCount} )");

        if (examCreateVM.ClassicQuestionCount > totalClassicCount)
            ModelState.AddModelError("ClassicQuestionCount", string.Format(_stringLocalizer["Not_Enough_Questions"]) + $" ( {totalClassicCount} )");

        if (examCreateVM.AlgorithmQuestionCount > totalAlgorithmCount)
            ModelState.AddModelError("AlgorithmQuestionCount", string.Format(_stringLocalizer["Not_Enough_Questions"]) + $" ( {totalAlgorithmCount} )");


        if (examCreateVM.TestQuestionCount == 0 && examCreateVM.ClassicQuestionCount == 0 && examCreateVM.AlgorithmQuestionCount == 0)
            ModelState.AddModelError("AlgorithmQuestionCount", "Soru Seçmediniz.");
        if (examCreateVM.TestQuestionsCoefficient + examCreateVM.ClassicQuestionsCoefficient + examCreateVM.AlgorithmQuestionsCoefficient != 100)
        {
            ModelState.AddModelError("AlgorithmQuestionsCoefficient", "Puan ağırlıkları toplamı 100 olmalı.");

        }
         

        if (!ModelState.IsValid)
        {
            var candidateQuestionSubjects = await _candidateQuestionSubjectService.GetAllAsync();
            if (!candidateQuestionSubjects.IsSuccess)
            {
                NotifyErrorLocalized(candidateQuestionSubjects.Message);
            }
            else
            {
                ViewBag.candidateQuestionSubjectsList = candidateQuestionSubjects.Data.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            }

            return View(candidateExamLinkCreateVM);
        }

        foreach (var item in questionRulesCreteVM)
        {
            if (examRuleCreateVM.CandidateQuestionRules == null)
            {
                examRuleCreateVM.CandidateQuestionRules = new List<CandidateQuestionRuleCreateVM>();
              
            }

            examRuleCreateVM.CandidateQuestionRules.Add(item);
        }


        int daysToAdd = examCreateVM.LinkValidityPeriod ?? 1;
        examCreateVM.ExamLinkEndDate = DateTime.Now.AddDays(daysToAdd);

        var createExamLinkDto = examCreateVM.Adapt<CandidateExamLinkCreateDto>();
        var result = await _candidateExamService.CreateExamForLinkAsync(createExamLinkDto);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }

        examRuleCreateVM.CandidateExamId = result.Data.Id;
        examRuleCreateVM.TestQuestionsCoefficient = examCreateVM.TestQuestionsCoefficient;
        examRuleCreateVM.ClassicQuestionsCoefficient = examCreateVM.ClassicQuestionsCoefficient;
        examRuleCreateVM.AlgorithmQuestionsCoefficient = examCreateVM.AlgorithmQuestionsCoefficient;
        var createExamRuleDto = examRuleCreateVM.Adapt<CandidateExamRuleCreateDto>();
        var examRuleResult = await _candidateExamRuleService.AddAsync(createExamRuleDto);
       
        if (!examRuleResult.IsSuccess)
        {
            NotifyErrorLocalized(examRuleResult.Message);
        }

        




        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
        }
        else
        {
            //Bu link güncellenecek
            string baseUrl = Url.Action("StartExamPublic", "CandidateExamInitiation", new { Area = "CandidateAdmin", examId = result.Data.Id , examRuleId = examRuleResult.Data.Id}, Request.Scheme);
            await _candidateExamService.NotifyCandidateAdminAboutExamLinkCreationAsync(result.Data.Id, baseUrl);

            NotifySuccessLocalized(result.Message);
        }

        return RedirectToAction("Index");

    }
















    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _candidateExamService.GetDetailsByIdAsync(id);

        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);

            return RedirectToAction("Index");
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }

        var vm = result.Data.Adapt<CandidateExamDetailsVM>();

        return View(vm);
    }

    public async Task<IActionResult> CandidateExamDetails(Guid examId, Guid candidateId)
    {
        var result = await _candidateExamService.GetExamQuestionsByCandidateIdAsync(examId, candidateId);

        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
            return RedirectToAction("Details");
        }
        else
        {
            NotifySuccessLocalized(result.Message);
        }

        var answerResult = await _candidateExamService.GetGivenAnswersAsync(examId, candidateId);

        ViewBag.GivenAnswers = answerResult == null ? null : answerResult.Data.Adapt<List<CandidateCandidateAnswerListVM>>();

        var vm = result.Data.Adapt<CandidateExamQuestionsByCandidateVM>();

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> StartExamAndNotifyCandidates(Guid id)
    {
        string baseUrl = Url.Action("StartExam", "CandidateExamInitiation", new { Area = "CandidateAdmin", examId = id }, Request.Scheme);

        var examResult = await _candidateExamService.StartExamAndNotifyCandidates(id, baseUrl);

        if (!examResult.IsSuccess)
        {
            NotifyWarningLocalized(examResult.Message);
            return Json(new { success = false, message = examResult.Message });
        }

        NotifySuccessLocalized(Messages.ExamStartedSuccessfully);

        return Json(new { success = true, message = Messages.ExamStartedSuccessfully });
    }

    [HttpPost]
    public async Task<IActionResult> AddCandidateAnswers(Guid itemid, string answer, Guid examId, Guid candidateId)
    {
        var result = await _candidateExamService.AddCandidateAnswersAsync(itemid, answer);

        if (result.IsSuccess)
        {
            NotifySuccessLocalized(result.Message);
            return RedirectToAction("CandidateExamDetails", new { examId = examId, candidateId = candidateId });
        }
        else
        {
            NotifyErrorLocalized(result.Message);
            return RedirectToAction("CandidateExamDetails", new { examId = examId, candidateId = candidateId });
        }
    }

    private List<CandidateQuestionListDto> GetRandomQuestions(List<CandidateQuestionListDto> questions, int count)
    {
        Random rand = new Random();
        return questions.OrderBy(q => rand.Next()).Take(count).ToList();
    }


}
