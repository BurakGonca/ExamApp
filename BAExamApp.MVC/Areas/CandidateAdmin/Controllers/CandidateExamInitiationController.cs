using BAExamApp.Core.Enums;
using BAExamApp.Core.Utilities.Results.Concrete;
using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Dtos.Candidate.CandidateExamInitiation;
using BAExamApp.Dtos.Candidate.CandidateExams;
using BAExamApp.Dtos.Candidate.Candidates;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.SendMails;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateCandidateVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateExamInitiationVMs;
using BAExamApp.MVC.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class CandidateExamInitiationController : CandidateAdminBaseController
{
    private readonly ICandidateExamInitiationService _candidateExamInitiationService;
    private readonly ICandidateService _candidateService;
    private readonly ICandidateExamService _candidateExamService;
    private readonly ICandidateCandidatesExamsRepository _candidateCandidatesExamsRepository;
    private readonly ICandidateQuestionService _candidateQuestionService;
    private readonly ICandidateQuestionRuleService _candidateQuestionRuleService;
    private readonly ICandidateExamRuleService _candidateExamRuleService;
    private readonly ICandidatesExamsService _candidatesExamsService;
    public CandidateExamInitiationController(ICandidateExamInitiationService candidateExamInitiationService, ICandidateService candidateService, ICandidateExamService candidateExamService, ICandidateCandidatesExamsRepository candidateCandidatesExamsRepository, ICandidateQuestionService candidateQuestionService, ICandidateQuestionRuleService candidateQuestionRuleService, ICandidateExamRuleService candidateExamRuleService, ICandidatesExamsService candidatesExamsService)
    {
        _candidateExamInitiationService = candidateExamInitiationService;
        _candidateService = candidateService;
        _candidateExamService = candidateExamService;
        _candidateCandidatesExamsRepository = candidateCandidatesExamsRepository;
        _candidateQuestionService = candidateQuestionService;
        _candidateQuestionRuleService = candidateQuestionRuleService;
        _candidateExamRuleService = candidateExamRuleService;
        _candidatesExamsService = candidatesExamsService;
    }




    [AllowAnonymous]
    [HttpGet]

    public async Task<IActionResult> StartExamPublic(Guid examId, Guid examRuleId)
    {
        var exams = await _candidateExamService.GetAllAsync();
        var exam = exams.Data.ToList().FirstOrDefault(x => x.Id == examId);


        if (exam == null)
        {
            return View(null);
        }

        var rule = await _candidateExamRuleService.GetByIdAsync(examRuleId);

        if (rule.Data == null)
        {
            return View(null);
        }

        ViewData["examId"] = examId;
        ViewData["examRuleId"] = examRuleId;


        return View();
    }


    [AllowAnonymous]
    [HttpPost]

    public async Task<IActionResult> StartExamPublic(string mail, string name, string surname, Guid examId, Guid examRuleId)
    {
        var candidates = await _candidateService.GetAllAsync();
        var candidate = candidates.Data.ToList().FirstOrDefault(x => x.Email.ToLower() == mail.ToLower());
        var examrule = await _candidateExamRuleService.GetByIdAsync(examRuleId);



        if (candidate == null)
        {
            var newCandidate = new CandidateCreateDto { Email = mail, FirstName = name, LastName = surname };
            var _candidate = await _candidateService.AddAsync(newCandidate);
            candidate = _candidate.Data.Adapt<CandidateListDto>();
        }
        else
        {
            if (!(bool)candidate.IsRetakeAllowed)
            {
                return RedirectToAction("MissExamForLink", new { examId = examId });
            }

        }
         

        Guid candidateId = candidate.Id;

        var exam = await _candidateExamService.GetByIdAsync(examId);


        //aday ile soruları bağla
        var questionsResult = await _candidateQuestionService.GetAllAsync();
        if (!questionsResult.IsSuccess)
        {
            NotifyErrorLocalized(questionsResult.Message);
            return View("Index");
        }
        var questions = questionsResult.Data.Where(x => x.Status == Status.Active).ToList();

        List<CandidateQuestionListDto> myQuestionLists = new List<CandidateQuestionListDto>();

        var questionRules = await _candidateQuestionRuleService.GetAllAsync();
        var questionRulesData = questionRules.Data.Where(x => x.CandidateExamRuleId == examRuleId).ToList();

        var candidatesExamsData = await _candidatesExamsService.GetAll();

        candidatesExamsData = candidatesExamsData.Where(x => x.CandidateExamId == examId && x.CandidateId == candidateId).ToList();

        //sınav bilgisi yoksa
        if (candidatesExamsData == null|| candidatesExamsData.Count==0)
        {
            //aynı sorular seçiliyse
            if ((int)examrule.Data.ExamQuestionDistribution == 1)
            {

                //aday ile sınavı bağla
                var candidatesExams = await _candidateCandidatesExamsRepository.AddAsync(new CandidatesExams() { CandidateId = candidateId, CandidateExamId = examId });

                await _candidateCandidatesExamsRepository.SaveChangesAsync();

                foreach (var item in questionRulesData) // view tarafında istenilen soru bilgilerine göre database den random sorular seçiliyor
                {

                    var filteredQuestions = questions.Where(x => x.CandidateQuestionSubject.Id == item.CandidateQuestionSubjectId && x.CandidateQuestionType == Convert.ToInt32(item.CandidateQuestionType)).ToList();

                    var randomQuestions = GetRandomQuestions(filteredQuestions, item.QuestionCount, examId);

                    myQuestionLists.AddRange(randomQuestions);




                }
                await _candidateExamService.AddQuestionsToExam(exam.Data, myQuestionLists, candidatesExams.Id, examrule.Data);

                return RedirectToAction("StartExamForLink", new { candidateId = candidateId, examId = examId });
            }

            //farklı sorular seçiliyse
            else
            {

                var _exam = exam.Data.Adapt<CandidateExamLinkCreateDto>();
                _exam.TestQuestionsCoefficient = examrule.Data.TestQuestionsCoefficient;
                _exam.ClassicQuestionsCoefficient = examrule.Data.ClassicQuestionsCoefficient;
                _exam.AlgorithmQuestionsCoefficient = examrule.Data.AlgorithmQuestionsCoefficient;
                _exam.Name = _exam.Name + " for " + candidate.FirstName + " " + candidate.LastName;
                _exam.ExamDateTime = DateTime.Now;
                var newExam = await _candidateExamService.CreateExamForLinkAsync(_exam);

                //aday ile sınavı bağla
                var candidatesExams = await _candidateCandidatesExamsRepository.AddAsync(new CandidatesExams() { CandidateId = candidateId, CandidateExamId = newExam.Data.Id, });
                await _candidateCandidatesExamsRepository.SaveChangesAsync();

                //her bir aday için belirlenen konu ve türlere göre ayrı bir sınav oluşacak.
                foreach (var item in questionRulesData) // view tarafında istenilen soru bilgilerine göre database den random sorular seçiliyor
                {

                    var filteredQuestions = questions.Where(x => x.CandidateQuestionSubject.Id == item.CandidateQuestionSubjectId && x.CandidateQuestionType == Convert.ToInt32(item.CandidateQuestionType)).ToList();

                    var randomQuestions = GetRandomQuestions(filteredQuestions, item.QuestionCount);

                    myQuestionLists.AddRange(randomQuestions);



                }
                await _candidateExamService.AddQuestionsToExam(newExam.Data, myQuestionLists, candidatesExams.Id, examrule.Data);



                return RedirectToAction("StartExamForLink", new { candidateId = candidateId, examId = newExam.Data.Id });
            }
        }

        return RedirectToAction("MissExamForLink", new { examId });

    }

    [AllowAnonymous]
    public async Task<IActionResult> StartExamForLink(Guid candidateId, Guid examId)
    {
        var exam = await _candidateExamService.GetByIdAsync(examId);
        var candidate = await _candidateService.GetByIdAsync(candidateId);



        if (DateTime.Now > exam.Data.ExamLinkEndDate)
        {
            return RedirectToAction("MissExamForLink", new { examId });
        }



        var result = await _candidateExamInitiationService.GetExamStartDetailsAsync(candidateId, examId);

        CandidateExamStartVM vm = result.Data.Adapt<CandidateExamStartVM>();
        return View(vm);

    }

    [HttpPost]
    public async Task<IActionResult> CreateCandidate(CandidateCandidateCreateVM createVM)
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

        var a = Request.Headers["Referer"].ToString();
        return Redirect(a);
    }



    [AllowAnonymous]
    public async Task<IActionResult> StartExam(Guid candidateId, Guid examId)
    {
        var result = await _candidateExamInitiationService.GetExamStartDetailsAsync(candidateId, examId);

        if (result.Data.ExamDateTime >= DateTime.Now)
        {
            ViewBag.ErrorMessage = "Exam_Not_Started";
        }

        if (result.Data.IsExamStarted)
        {
            return RedirectToAction(nameof(FinishedExam));
        }

        if (DateTime.Now <= result.Data.ExamDateTime || DateTime.Now >= result.Data.ExamDateTime && DateTime.Now <= result.Data.ExamDateTime + result.Data.ExamDuration)
        {
            var vm = result.Data.Adapt<CandidateExamStartVM>();
            return View(vm);
        }

        return RedirectToAction(nameof(MissExam), new { candidateId, examId });
    }


    [AllowAnonymous]
    public async Task<IActionResult> MissExamForLink(Guid examId)
    {
        var result = await _candidateExamService.GetByIdAsync(examId);
        var vm = result.Data.Adapt<CandidateExamStartVM>();
        return View(vm);
    }


    [AllowAnonymous]
    public async Task<IActionResult> MissExam(Guid examId, Guid candidateId)
    {
        var result = await _candidateExamInitiationService.GetExamStartDetailsAsync(candidateId, examId);
        var vm = result.Data.Adapt<CandidateExamStartVM>();
        return View(vm);
    }

    [AllowAnonymous]
    public async Task<IActionResult> FinishedExam()
    {   
        return View();
    }


    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> StartExamForLink(CandidateExamStartVM examStartVM)
    {
        var exam = await _candidateExamService.GetByIdAsync(examStartVM.ExamId);
        var updatedExam = exam.Data;


        var result = await _candidateExamInitiationService.StartExamAsync(examStartVM.CandidateId, examStartVM.ExamId);
        if (result.IsSuccess)
        {
            var candidatesExamsList = await _candidateCandidatesExamsRepository.GetAllAsync();
            var candidatesExams = candidatesExamsList.FirstOrDefault(x => x.CandidateId == examStartVM.CandidateId && x.CandidateExamId == examStartVM.ExamId);

            candidatesExams.StartDate = DateTime.Now;
            var b = await _candidateCandidatesExamsRepository.UpdateAsync(candidatesExams);

            var candidate = await _candidateService.GetByIdAsync(examStartVM.CandidateId);
            candidate.Data.IsRetakeAllowed = false;
            var a = candidate.Data.Adapt<CandidateUpdateDto>();
            await _candidateService.UpdateAsync(a);



            examStartVM.ExamDateTime = DateTime.Now;

            return RedirectToAction(nameof(GetQuestionInOrder), examStartVM);
        }

        NotifyErrorLocalized(result.Message);
        return View(examStartVM);
    }


    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> StartExam(CandidateExamStartVM examStartVM)
    {
        var result = await _candidateExamInitiationService.StartExamAsync(examStartVM.CandidateId, examStartVM.ExamId);
        if (result.IsSuccess)
        {
            return RedirectToAction(nameof(GetQuestionInOrder), examStartVM);
        }
        NotifyErrorLocalized(result.Message);
        return View(examStartVM);
    }


    [AllowAnonymous]
    public async Task<IActionResult> GetQuestionInOrder(CandidateExamGetQuestionInOrderVM questionInOrderVM)
    {
        var examResult = await _candidateExamInitiationService.GetExamStartDetailsAsync(questionInOrderVM.CandidateId, questionInOrderVM.ExamId);

        if (!examResult.Data.IsExamFinished && questionInOrderVM.QuestionInOrder < examResult.Data.QuestionCount)
        {
            var a = questionInOrderVM.Adapt<CandidateExamGetQuestionInOrderDto>();
            var result = await _candidateExamInitiationService.GetQuestionInOrderAsync(a);
            if (result.IsSuccess)
            {
                var vm = result.Data.Adapt<CandidateExamGetQuestionInOrderVM>();

                var candidatesExamsList = await _candidatesExamsService.GetAll();
                var candidatesExams = candidatesExamsList.FirstOrDefault(x => x.CandidateId == questionInOrderVM.CandidateId && x.CandidateExamId == questionInOrderVM.ExamId);
                if (candidatesExams.StartDate != null)
                    vm.ExamDateTime = (DateTime)candidatesExams.StartDate;
                return View(vm);
            }
        }
        return RedirectToAction(nameof(FinishedExam));
    }


    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AnswerQuestion(CandidateExamAnswerQuestionVM answerQuestionVM)
    {
        var examResult = await _candidateExamInitiationService.GetExamStartDetailsAsync(answerQuestionVM.CandidateId, answerQuestionVM.ExamId);
        var result = await _candidateExamInitiationService.AnswerQuestionAsync(answerQuestionVM.Adapt<CandidateExamAnswerQuestionDto>());


        return RedirectToAction(nameof(GetQuestionInOrder), answerQuestionVM);
    }


    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> FinishCandidateExam(CandidateExamAnswerQuestionVM answerQuestionVM)
    {

        var result = await _candidateExamInitiationService.AnswerQuestionAsync(answerQuestionVM.Adapt<CandidateExamAnswerQuestionDto>());

        await _candidateExamInitiationService.FinishExamAsync(answerQuestionVM.CandidateId, answerQuestionVM.ExamId);

        await _candidateExamService.ExamFinishedNotifyToCandidateAdmin(answerQuestionVM.CandidateId, answerQuestionVM.ExamId);
        return RedirectToAction(nameof(FinishedExam));
    }


    private List<CandidateQuestionListDto> GetRandomQuestions(List<CandidateQuestionListDto> questions, int count)
    {
        Random rand = new Random();
        return questions.OrderBy(q => rand.Next()).Take(count).ToList();
    }
    //aynı sınavda aynı sorular çıkması için bir seede göre rastgele sayı üretiyoruz.
    private List<CandidateQuestionListDto> GetRandomQuestions(List<CandidateQuestionListDto> questions, int count, Guid examId)
    {
        int seed = examId.GetHashCode();
        Random rand = new Random(seed);
        return questions.OrderBy(q => rand.Next()).Take(count).ToList();
    }
}
