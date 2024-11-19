using AutoMapper;
using BAExamApp.Core.Utilities.Results;
using BAExamApp.Dtos.Questions;
using BAExamApp.MVC.Areas.Admin.Models.AdminVMs;
using BAExamApp.MVC.Areas.Admin.Models.DashboardVMs;
using BAExamApp.MVC.Areas.Admin.Models.ExamVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;
using BAExamApp.MVC.Areas.Admin.Models.TrainerVMs;
using NuGet.Protocol.Core.Types;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

public class HomeController : AdminBaseController
{
    private readonly IAdminService _adminService;
    private readonly IDashboardService _dashboardService;
    private readonly ITrainerService _trainerService;
    private readonly ISubjectService _subjectService;
    private readonly IProductService _productService;
    private readonly IQuestionService _questionService;
    private readonly IProductSubjectService _productSubjectService;
    private readonly IMapper _mapper;
    private static DashboardVM model = new DashboardVM();
    public HomeController(IAdminService adminService, IDashboardService dashboardService, IMapper mapper, ITrainerService trainerService, ISubjectService subjectService, IProductService productService, IProductSubjectService productSubjectService, IQuestionService questionService)
    {
        _adminService = adminService;
        _dashboardService = dashboardService;
        _mapper = mapper;
        _trainerService = trainerService;
        _subjectService = subjectService;
        _productService = productService;
        _productSubjectService = productSubjectService;
        _questionService = questionService;
    }
    public async Task<IActionResult> Index(Guid? productId, State status = State.Awaited, string tab = "awaited")
    {
        ViewBag.ActiveTab = tab;
        ViewBag.ProductId = productId;

        
        var productsResult = await _productService.GetAllAsync(); 
        if (productsResult.IsSuccess)
            ViewBag.ProductList = new SelectList(productsResult.Data, "Id", "Name"); 
        else
            ViewBag.ProductList = new SelectList(new List<object>(), "Id", "Name");

        var productDictionary = productsResult.Data.ToDictionary(p => p.Id, p => p.Name);
        var productSubjects = (await _productSubjectService.GetAll()).Data
                           .Where(x => productId == null || x.ProductId == productId)
                           .ToList();
        var result = await _adminService.GetByIdentityIdAsync(UserIdentityId!);
        
        ViewBag.UserName = $"{result.Data.FirstName} {result.Data.LastName}";
        if (TempData["Login"] != null)
            NotifySuccess($"Hoş Geldin {result.Data.FirstName} {result.Data.LastName}");

        IDataResult<List<Dtos.Questions.QuestionListDto>>? response = null;
        List<AdminQuestionListVM>? questions = null;

        if (productId is not null)
        {
            if (status == State.Awaited)  
                questions = await SubjectFilterByAwaited(productId);       
            else if (status == State.Reviewed)
                questions = await SubjectFilterByReviewed(productId);
            
        }
        else
        {
            
            if (status == State.Awaited)
            {
                response = await _dashboardService.GetAllByAwaitedQuestionAsync();
                questions = _mapper.Map<List<AdminQuestionListVM>>(response.Data);
                questions.Where(x => x.State == State.Awaited);
            }
            else if (status == State.Reviewed)
            {
                response = await _dashboardService.GetAllByReviewedQuestionAsync();
                questions = _mapper.Map<List<AdminQuestionListVM>>(response.Data);
            }
        }

        foreach (var question in questions)
        {
            var relatedProductSubject = productSubjects.FirstOrDefault(ps => ps.SubjectId == question.SubjectId);
            if (relatedProductSubject != null && productDictionary.TryGetValue(relatedProductSubject.ProductId, out var productName))
            {
                question.ProductName = productName;
                question.ProductId = relatedProductSubject.ProductId;
            }
            else
            {
                question.ProductName = "Ürün bulunamadı"; 
            }
        }

        List<string> addedByFullNames = new List<string>();

        foreach (var item in questions)
        {
            var addedByQuestionTrainerResponse = await _trainerService.GetByIdentityIdAsync(item.CreatedBy.ToString());
            if (addedByQuestionTrainerResponse.Data is null)
            {
                var addedByQuestionAdminResponse = await _adminService.GetByIdentityIdAsync(item.CreatedBy.ToString());

                if (addedByQuestionAdminResponse.Data is not null)
                {
                    var addedByQuestionAdmin = _mapper.Map<AdminAdminListVM>(addedByQuestionAdminResponse.Data);
                    addedByFullNames.Add($"{addedByQuestionAdmin.FirstName} {addedByQuestionAdmin.LastName}");
                    var question = questions.First(q => q.Id == item.Id);
                    question.IsAdmin = true;
                    question.AdminId = addedByQuestionAdmin.Id.ToString(); 
                }
                else
                {
                    addedByFullNames.Add("---");
                }
            }
            else
            {
                var addedByQuestion = _mapper.Map<AdminTrainerDetailsVM>(addedByQuestionTrainerResponse.Data);
                addedByFullNames.Add($"{addedByQuestion.FirstName} {addedByQuestion.LastName}");
                var question = questions.First(q => q.Id == item.Id);
                question.IsAdmin = false;
                question.TrainerId = addedByQuestion.Id.ToString(); 
            }
        }

        for (int i = 0; i < questions.Count; i++)
        {
            questions[i].AddedByFullName = addedByFullNames[i];
        }


        model.AwaitedQuestion = questions;
        model.RevisionQuestions = questions;

        
        var overviewModel= await _dashboardService.GetDashboardOverview();

        model.Overview = _mapper.Map<DashboardOverviewVM>(overviewModel.Data);

        var topRatedStudents = await _dashboardService.GetTopRatedStudentAsync();
        model.TopRatedStudents = topRatedStudents.Data.Adapt<List<StudentExamsForAdminVM>>();
        
        var activeTopRatedStudents = await _dashboardService.GetTopRatedActiveStudentAsync();       
        model.ActiveTopRatedStudents = activeTopRatedStudents.Data.Adapt<List<ActiveStudentsExamsForAdminVM>>();


        var events = await _dashboardService.GetEventsAsync(null,null);
        model.Events = _mapper.Map<List<DashboardEventVM>>(events.Data);

        return View(model);
    }

    public async Task<List<AdminQuestionListVM>> SubjectFilterByAwaited(Guid? productId)
    {
        var productsSubjectResult = (await _productSubjectService.GetAll()).Data
                                     .Where(x => x.ProductId == productId)
                                     .Select(x => x.SubjectId).ToList();

        var questionsResult = (await _questionService.GetAllAsync()).Data
                              .Where(q => productsSubjectResult.Contains(q.SubjectId) && q.State == State.Awaited)
                              .ToList();

        var searchList = _mapper.Map<List<AdminQuestionListVM>>(questionsResult); 

        return searchList;
    }

    public async Task<List<AdminQuestionListVM>> SubjectFilterByReviewed(Guid? productId)
    {
        var productsSubjectResult = (await _productSubjectService.GetAll()).Data
                                     .Where(x => x.ProductId == productId)
                                     .Select(x => x.SubjectId).ToList();

        var questionsResult = (await _questionService.GetAllAsync()).Data
                              .Where(q => productsSubjectResult.Contains(q.SubjectId) && q.State == State.Reviewed)
                              .ToList();

        var searchList = _mapper.Map<List<AdminQuestionListVM>>(questionsResult);

        return searchList;
    }


    public async Task<IActionResult> GetEvents(int? year,int? month)
    {
        var response = await _dashboardService.GetEventsAsync(year,month);
        var events = _mapper.Map<List<DashboardEventVM>>(response.Data);
        return Json(events);
    }
    


}