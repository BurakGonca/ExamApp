using AutoMapper;
using BAExamApp.Business.Constants;
using BAExamApp.Core.Enums;
using BAExamApp.Dtos.ExamClassrooms;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.SendMails;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Entities.DbSets;
using BAExamApp.MVC.Areas.Admin.Models.ExamEvaluatorVMs;
using BAExamApp.MVC.Areas.Admin.Models.ExamVMs;
using BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;
using Hangfire;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

public class ExamController : AdminBaseController
{
    private readonly IExamService _examService;
    private readonly IMapper _mapper;
    private readonly IClassroomProductService _classroomProductService;
    private readonly ITrainerProductService _trainerProductService;
    private readonly IExamEvaluatorService _examEvaluatorService;
    private readonly IStudentService _studentService;
    private readonly IStudentExamService _studentExamService;
    private readonly IStudentQuestionService _studentQuestionService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IClassroomService _classroomService;
    private readonly IExamRuleService _examRuleService;
    private ITrainerService _trainerService;
    private readonly IUserService _userService;
    private readonly ISendMailService _sendMailService;
    private readonly IMemoryCache _memoryCache;

    public ExamController(IExamService examService, IMapper mapper, IClassroomProductService classroomProductService, ITrainerProductService trainerProductService, IExamEvaluatorService examEvaluatorService, IStudentService studentService, IStudentExamService studentExamService, IStudentQuestionService studentQuestionService, IHttpContextAccessor contextAccessor, IClassroomService classroomService, IExamRuleService examRuleService, ITrainerService trainerService, IUserService userService, ISendMailService sendMailService, IMemoryCache memoryCache)
    {
        _examService = examService;
        _mapper = mapper;
        _classroomProductService = classroomProductService;
        _trainerProductService = trainerProductService;
        _examEvaluatorService = examEvaluatorService;
        _studentService = studentService;
        _studentExamService = studentExamService;
        _studentQuestionService = studentQuestionService;
        _contextAccessor = contextAccessor;
        _classroomService = classroomService;
        _examRuleService = examRuleService;
        _trainerService = trainerService;
        _userService = userService;
        _sendMailService = sendMailService;
        _memoryCache = memoryCache;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {



        ViewBag.Classrooms = await GetClassrooms();
        ViewBag.ExamRules = await GetExamRules();
        ViewBag.ExamCreationTypes = GetExamCreationTypes();
        ViewBag.ExamTypes = GetExamTypes();

        return View(new List<AdminExamListVM>());
    }

    [HttpGet]
    public async Task<IActionResult> StartExam(Guid id)
    {
        string link = Url.Action("StartExam", "exam", new { Area = "Student" }, Request.Scheme);

        var examResult = await _examService.GetStudentsInfosByExamIdAsync(id, link);
        var examStarted = await _examService.GetByIdAsync(id);
        if (!examResult.IsSuccess)
        {
            NotifyWarningLocalized(examResult.Message);
            return Json(new { success = true, message = Messages.ExamStartedMailError });
        }
        NotifySuccessLocalized(Messages.ExamStartedSuccessfully);
        _memoryCache.Set("examStarted", examStarted.Data.Id, TimeSpan.FromMinutes(10));
        return Json(new { success = true, message = Messages.ExamStartedSuccessfully });
    }
    

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminExamCreateVM examCreateVM)
    {
        if (ModelState.IsValid)
        {
            var uniqueStudentExamIds = new HashSet<Guid>();
            var studentExams = new List<StudentExamCreateDto>();
            var examClassrooms = new List<ExamClassroomsCreateDto>();
            var examCreateDto = _mapper.Map<ExamCreateDto>(examCreateVM);
            examCreateVM.ExamDateTime = DateTime.ParseExact(DateTime.Now.ToString("dd.MM.yyyy HH:mm"), "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            try
            {
                if (examCreateVM.forClassroom && examCreateVM.ExamClassroomsIds != null)
                {
                    foreach (var classroomId in examCreateVM.ExamClassroomsIds)
                    {
                        var students = await _studentService.GetAllByClassroomIdAsync(classroomId);
                        examClassrooms.Add(new() { ClassroomId = classroomId });
                        if (students.IsSuccess && students.Data != null && students.Data.Count > 0)
                        {
                            foreach (var student in students.Data)
                            {
                                if (uniqueStudentExamIds.Add(student.Id))
                                {
                                    studentExams.Add(new() { StudentId = student.Id });
                                }
                            }
                        }
                        else
                        {
                            NotifyErrorLocalized(students.Message);
                        }
                    }
                }
                else
                {
                    if (examCreateVM.StudentIds != null && examCreateVM.StudentIds.Count > 0)
                    {
                        foreach (var studentId in examCreateVM.StudentIds)
                        {
                            if (uniqueStudentExamIds.Add(studentId))
                            {
                                studentExams.Add(new() { StudentId = studentId });
                            }
                        }
                        examClassrooms = examCreateVM.ExamClassroomsIds.Select(id => new ExamClassroomsCreateDto { ClassroomId = id }).ToList();
                    }
                    else
                    {
                        NotifyErrorLocalized(Messages.NoAvailableStudent);
                        return RedirectToAction(nameof(Index));
                    }
                }

                examCreateDto.StudentExams = studentExams;
                examCreateDto.ExamClassroomsIds = examClassrooms;

                var examRule = (await _examRuleService.GetByIdAsync(examCreateVM.ExamRuleId)).Data;
                var totalGivenTimeResult = await _studentQuestionService.TotalGivenTimeAsync(examRule.ExamRuleSubtopics);

                if (!totalGivenTimeResult.IsSuccess)
                {
                    NotifyErrorLocalized(totalGivenTimeResult.Message);
                    return RedirectToAction(nameof(Index));
                }

                TimeSpan totalTimeSpan;
                if (!TimeSpan.TryParse(totalGivenTimeResult.Data, out totalTimeSpan))
                {
                    NotifyErrorLocalized(Messages.InvalidCalculatedTotalTime);
                    return RedirectToAction(nameof(Index));
                }

                var examDurationInMinutes = (int)examCreateVM.ExamDuration.TotalMinutes;
                var totalTimeInMinutes = (int)totalTimeSpan.TotalMinutes;

                if (examDurationInMinutes < totalTimeInMinutes)
                {
                    NotifyErrorLocalized(Messages.ExamDurationLessThanTotalTime);
                    return RedirectToAction(nameof(Index));
                }
                var questionListResult = await _studentQuestionService.CreateQuestionPoolForExamRuleSubtopicsAsync(examRule.ExamRuleSubtopics);


                if (!questionListResult.IsSuccess)
                {
                    NotifyErrorLocalized(questionListResult.Message);
                    return RedirectToAction(nameof(Index));
                }

                var adminId = _contextAccessor?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var adminEmail = await _userService.GetEmailByUserId(adminId, Roles.Admin);

                var examResult = await _examService.AddAsync(examCreateDto);

                if (examResult.IsSuccess)
                {
                    var studentQuestionResult = await _studentQuestionService.AddRangeForExamIdAsync(examResult.Data.Id);

                    if (studentQuestionResult.IsSuccess)
                    {
                        var studentExamResult = await _studentExamService.GetAllByExamIdAsync(examResult.Data.Id);
                        if (studentExamResult.IsSuccess)
                        {
                            string link = Url.Action("StartExam", "exam", new { Area = "Student" }, Request.Scheme);
                            List<string> examDetail = new List<string>();

                            foreach (var studentExam in studentExamResult.Data)
                            {
                                var studentResult = await _studentService.GetByIdAsync(studentExam.StudentId);
                                if (studentResult.IsSuccess)
                                {
                                    var email = await _sendMailService.GetStudentEmailById(studentExam.StudentId);

                                    if (examCreateVM.forClassroom)
                                    {
                                        var classRoomResult = await _classroomService.GetAsync(x => examCreateVM.ExamClassroomsIds.Contains(x.Id) && studentExam.ClassroomNames.Contains(x.Name));

                                        examDetail.Add($"{studentExam.ExamName}*?*{classRoomResult.Data.Name}*?*{studentExam.ExamDateTime}*?*{email}*?*{link}/{studentExam.Id}");
                                    }
                                    else
                                    {
                                        examDetail.Add($"{studentExam.ExamName}*?*{studentExam.ExamDateTime}*?*{email}*?*{link}/{studentExam.Id}");
                                    }
                                }
                            }

                            var newUserMailDto = new TrainerNewExamMailDto()
                            {
                                TrainerEmailAdress = adminEmail,
                                StudentContents = examDetail
                            };

                            // Use Hangfire to enqueue the email sending task
                            BackgroundJob.Enqueue(() => _sendMailService.SendEmailToTrainerNewExam(newUserMailDto));

                            NotifySuccessLocalized(examResult.Message);
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            NotifyErrorLocalized(studentExamResult.Message);
                        }
                    }
                    else
                    {
                        NotifyErrorLocalized(studentQuestionResult.Message);
                    }
                }
                else
                {
                    NotifyErrorLocalized(examResult.Message);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda genel hata işleme
                NotifyErrorLocalized(ex.Message);
            }
        }

        // Gerekli view bag değişkenlerini set et
        ViewBag.Classrooms = await GetClassrooms();
        ViewBag.ExamRules = await GetExamRules();
        ViewBag.ExamTypes = GetExamTypes();
        ViewBag.ExamCreationTypes = GetExamCreationTypes();

        return View(examCreateVM);
    }

    [HttpGet]
    public async Task<IActionResult> GetTotalTimeByRule(Guid examRuleId)
    {
        var examRule = await _examRuleService.GetByIdAsync(examRuleId);
        var totalGivenTimeResult = await _studentQuestionService.TotalGivenTimeAsync(examRule.Data.ExamRuleSubtopics);

        if (totalGivenTimeResult.IsSuccess)
        {
            return Json(new { totalTime = totalGivenTimeResult.Data });
        }

        return Json(new { totalTime = "0" });
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var exam = await _examService.GetDetailsByIdAsync(id);
        if (!exam.IsSuccess)
        {
            NotifyErrorLocalized(exam.Message);
            return RedirectToAction(nameof(Index));
        }

        var examDetailVM = _mapper.Map<AdminExamDetailVM>(exam.Data);

        var studentExam = await _studentExamService.GetAllByExamIdAsync(id);

        if (!studentExam.IsSuccess)
        {
            NotifyError(studentExam.Message);
            return RedirectToAction(nameof(Index));
        }


        var studentExamVm = _mapper.Map<IEnumerable<StudentExamDetailForAdminVM>>(studentExam.Data);

        foreach (var item in studentExamVm)
        {
            item.ExamType = exam.Data.ExamType;
        }


        var combinedExamDetailsVM = new AdminCombinedExamDetailsVM
        {
            ExamDetail = examDetailVM,
            StudentExamDetails = studentExamVm
        };

        return View(combinedExamDetailsVM);
    }
    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var examResult = await _examService.GetByIdAsync(id);
        if (!examResult.IsSuccess)
        {
            NotifyErrorLocalized(examResult.Message);
            return RedirectToAction(nameof(Index));
        }

        var examEditVM = _mapper.Map<AdminExamUpdateVM>(examResult.Data);
        return View(examEditVM);
    }
    [HttpPost]
    public async Task<IActionResult> Update(AdminExamUpdateVM model)
    {
        if (!ModelState.IsValid)
        {
            NotifyErrorLocalized("Invalid model state.");
            return View(model);
        }
        var examToUpdate = _mapper.Map<ExamUpdateDto>(model);
        var result = await _examService.UpdateAsync(examToUpdate);

        if (result.IsSuccess)
        {
            NotifySuccessLocalized(result.Message);
            return RedirectToAction(nameof(Index));
        }
        else
        {
            NotifyErrorLocalized(result.Message);
            return View(model);
        }
    }



    [HttpGet]
    public async Task<IActionResult> AddEvaluators(Guid id)
    {
        var exam = await _examService.GetDetailsByIdAsync(id);

        if (!exam.IsSuccess)
        {
            NotifyErrorLocalized(exam.Message);
            return RedirectToAction(nameof(Index));
        }

        var examAddEvaluators = _mapper.Map<AdminExamEvaluatorCreateVM>(exam.Data);

        examAddEvaluators.TrainerIds = new List<Guid>();

        foreach (var examEvaluator in exam.Data.ExamEvaluators)
        {
            examAddEvaluators.TrainerIds.Add(examEvaluator.EvaluatorId);
        }

        ViewBag.TrainerList = await GetTrainersAsync();

        return View(examAddEvaluators);
    }

    [HttpPost]
    public async Task<IActionResult> AddEvaluators(AdminExamEvaluatorCreateVM viewModel)
    {
        if (ModelState.IsValid)
        {
            var selectedTrainers = viewModel.TrainerIds;

            var addTrainerResponse = await _examEvaluatorService.UpdateExamEvaluatorsAsync(selectedTrainers, viewModel.Id);
            if (addTrainerResponse.IsSuccess)
            {
                NotifySuccessLocalized(addTrainerResponse.Message);
                return RedirectToAction(nameof(Index));
            }
            NotifyErrorLocalized(addTrainerResponse.Message);
        }

        ViewBag.TrainerList = await GetTrainersAsync();

        return View(viewModel);
    }

    private async Task<List<SelectListItem>> GetTrainersAsync()
    {
        var getFreeTrainersResponse = await _trainerService.GetAllAsync();
        if (getFreeTrainersResponse.IsSuccess)
        {
            var trainerList = getFreeTrainersResponse.Data.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName,
            }).ToList();

            return trainerList;
        }
        return new List<SelectListItem>();
    }

    ///// <summary>
    ///// Sınav detaylarını ExamStudentId'ye göre bulur ve getirir.
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns>List<ExamStudentResultDto></returns>
    //[HttpGet]
    //public async Task<IActionResult> GetExamDetails(Guid id)
    //{
    //    var examsOfStudent = await _studentAnswerService.GetStudentAnswerById(id);

    //    //var studentAnswers = _mapper.Map<List<StudentAnswer>>(examsOfStudent);
    //    return View(examsOfStudent);
    //}

    //[HttpGet]
    //public IActionResult GetExamStudentWithParameters()
    //{
    //    Expression<Func<StudentExam, object>>[] expressions = { examStudent => examStudent.Exam, examStudent => examStudent.Student };
    //    return View(_examStudentService.GetExamStudentWithParameters(expressions));
    //}

    private List<SelectListItem> GetExamTypes()
    {
        return Enum.GetValues(typeof(ExamType)).Cast<ExamType>().
            Select(x => new SelectListItem
            {
                Text = Localize(x.GetType().GetMember(x.ToString()).First().GetCustomAttribute<DisplayAttribute>()!.GetName()!),
                Value = ((int)x).ToString()
            }).ToList();
    }

    private List<SelectListItem> GetExamCreationTypes()
    {
        return Enum.GetValues(typeof(ExamCreationType)).Cast<ExamCreationType>().
             Select(x => new SelectListItem
             {
                 Text = Localize(x.GetType().GetMember(x.ToString()).First().GetCustomAttribute<DisplayAttribute>()!.GetName()!),
                 Value = ((int)x).ToString()
             }).ToList();
    }

    private async Task<List<SelectListItem>> GetExamRules()
    {
        var getExamRulesResult = await _examRuleService.GetAllExamRulesByFilter();

        var examRuleList = getExamRulesResult.Data.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();

        return examRuleList;
    }

    private async Task<List<SelectListItem>> GetClassrooms()
    {
        var classroomsResult = await _classroomService.GetAllClassroomByFilter();

        var classroomList = classroomsResult.Data.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();

        return classroomList;
    }

    [HttpGet]
    public async Task<SelectList> GetExamRulesByExamType(string examTypeId)
    {
        return new SelectList((await _examRuleService.GetExamRulesByExamTypeAsync(examTypeId)).Data, "Id", "Name");
    }





    [HttpGet]
    public async Task<SelectList> GetStudentsByClassroom(Guid classroomId)
    {
        var filteredStudents = await _studentService.GetAllByClassroomIdAsync(classroomId);
        return new SelectList(filteredStudents.Data.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.FirstName + " " + x.LastName
        }), "Value", "Text");
    }

    [HttpPost]
    public async Task<IActionResult> GetExamsByStatus(string className, string ruleName, string startDate, string endDate)
    {

        var exams = await _examService.GetExamsByFiltered(className, ruleName, startDate, endDate, false);

        var examList = _mapper.Map<List<AdminExamListVM>>(exams.Data);
        var x = await _examService.GetExamsByExamIdAsync(examList.Select(x => x.Id).ToList());
        examList = _mapper.Map<List<AdminExamListVM>>(x.Data);

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        _memoryCache.Set("examList", examList, cacheEntryOptions);

        return RedirectToAction("GetExamsByStatus");

    }

    [HttpGet]
    public async Task<IActionResult> GetExamsByStatus()
    {
        if (_memoryCache.TryGetValue("examList", out var examList))
        {
            _memoryCache.TryGetValue("examStarted", out var examStartedId);
            if (examStartedId != null)
            {
                var examStarted = await _examService.GetByIdAsync((Guid)examStartedId);

                foreach (var item in (List<AdminExamListVM>)examList)
                {
                    if (item.Id.ToString() == examStarted.Data.Id.ToString())
                    {
                        item.IsStarted = true;
                    }
                }
            }


            ViewBag.Classrooms = await GetClassrooms();
            ViewBag.ExamRules = await GetExamRules();
            ViewBag.ExamCreationTypes = GetExamCreationTypes();
            ViewBag.ExamTypes = GetExamTypes();

            return View("Index", examList);
        }
        else
        {
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteExam(Guid examId)
    {
        var examResult = await _examService.SoftDeleteAsync(examId);
        if (examResult.IsSuccess)
            NotifySuccessLocalized(examResult.Message);
        else
            NotifyErrorLocalized(examResult.Message);

        return Json(examResult);
    }

    [HttpPost]
    public async Task<IActionResult> IsCanceled(Guid examId)
    {
        var examResult = await _examService.IsCanceledAsync(examId);

        if (examResult.IsSuccess)
        {

            var studentExamResult = await _studentExamService.GetAllByExamIdAsync(examId);

            if (studentExamResult.IsSuccess && studentExamResult.Data != null)
            {
                foreach (var studentExam in studentExamResult.Data)
                {
                    var studentResult = await _studentService.GetByIdAsync(studentExam.StudentId);
                    if (studentResult.IsSuccess)
                    {
                        var email = await _sendMailService.GetStudentEmailById(studentExam.StudentId);

                        var cancelExamMailDto = new StudentTrainerCancelExamMailDto
                        {
                            ExamName = studentExam.ExamName,
                            EmailAdress = email,
                            ExamDate=studentExam.ExamDateTime
                        };

                        BackgroundJob.Enqueue(() => _sendMailService.SendEmailToStudentCancelExam(cancelExamMailDto));
                    }
                }

                //Sınavı oluşturan kişiye iptal maili atma. (Trainer ya da admin kim oluşturduysa)

                var createByEmail = await _examService.CreateByIdAsync(examId);

                var examData = studentExamResult.Data.FirstOrDefault();

                var trainerCancelExamMailDto = new StudentTrainerCancelExamMailDto
                {
                    ExamName = examData.ExamName,
                    EmailAdress = createByEmail.Data,
                    ExamDate = examData.ExamDateTime
                };

                BackgroundJob.Enqueue(() => _sendMailService.SendEmailToStudentCancelExam(trainerCancelExamMailDto));

                NotifySuccessLocalized(examResult.Message);
            }
            else
            {
                NotifyErrorLocalized(studentExamResult.Message);
            }
        }
        else
        {
            NotifyErrorLocalized(examResult.Message);
        }

        return Json(examResult);    
    
    }
}