using AutoMapper;
using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Trainer.Models.DashboardVMs;
using BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;
using BAExamApp.MVC.Areas.Trainer.Models.StudentExamVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace BAExamApp.MVC.Areas.Trainer.Controllers;
public class HomeController : TrainerBaseController
{
    private readonly ITrainerService _trainerManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IQuestionService _questionService;
    private readonly IDashboardService _dashboardService;
    private readonly IMapper _mapper;
    public static TrainerDashboardVM model = new TrainerDashboardVM();

    public HomeController(ITrainerService trainerManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IQuestionService questionService, IDashboardService dashboardService, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _trainerManager = trainerManager;
        _questionService = questionService;
        _dashboardService = dashboardService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = (await _trainerManager.GetByIdentityIdAsync(UserIdentityId)).Data;

        if (TempData["Login"] != null)
            NotifySuccess($"Hoş Geldin {user.FirstName} {user.LastName}");

        var events = await _dashboardService.GetEventsWithSpesificTrainerIdAsync(null, null, user.Id);
        model.Events = _mapper.Map<List<DashboardEventVM>>(events.Data);

        var topRatedStudents = await _dashboardService.GetTopRatedStudentForTrainerAsync(user.IdentityId);
        var mappedStudents = _mapper.Map<List<StudentExamsForTrainerVM>>(topRatedStudents.Data);
        model.TrainerTopratedStudents = mappedStudents;

        var allTopRatedStudents = await _dashboardService.GetTopRatedStudentAsync();
        var mappedAllStudents = _mapper.Map<List<AllStudentExamForTrainerVM>>(allTopRatedStudents.Data);
        model.AllTopratedStudents = mappedAllStudents;

        var activeStudents = await _dashboardService.GetTopRatedActiveStudentAsync();
        var mappedActiveStudents = _mapper.Map<List<ActiveStudentExamForTrinerVM>>(activeStudents.Data);
        model.ActiveStudents = mappedActiveStudents;

        var response = await _dashboardService.GetWaitedRevisedApprovedQuestionByTrainerIdAsync(UserIdentityId);
        var questionListByTrainer = _mapper.Map<List<DashboardQuestionVM>>(response.Data);
        model.QuestionList = questionListByTrainer;

        return View(model);
    }
    public async Task<IActionResult> GetEvents(int? year, int? month)
    {
        var user = (await _trainerManager.GetByIdentityIdAsync(UserIdentityId)).Data;

        var response = await _dashboardService.GetEventsWithSpesificTrainerIdAsync(year, month, user.Id);
        var events = _mapper.Map<List<DashboardEventVM>>(response.Data);
        return Json(events);
    }
    public async Task<IActionResult> LoginAsAdmin(string adminId)
    {
        var infoOfAdmin = await _userManager.FindByIdAsync(adminId);

        HttpContext.Session.Remove("changeSession");
        HttpContext.Session.Remove("adminId");

        await _signInManager.SignInAsync(infoOfAdmin!, isPersistent: false);

        return RedirectToAction("Index", "Home", new { area = "Admin" });
    }
}