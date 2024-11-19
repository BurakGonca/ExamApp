using AutoMapper;
using BAExamApp.Business.Constants;
using BAExamApp.Core.Enums;
using BAExamApp.Dtos.ExamRules;
using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Admin.Models.BranchVMs;
using BAExamApp.MVC.Areas.Admin.Models.ExamRuleSubtopicVMs;
using BAExamApp.MVC.Areas.Admin.Models.ExamRuleVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;
using BAExamApp.MVC.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using X.PagedList;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

public class ExamRuleController : AdminBaseController
{
    private readonly IExamRuleService _examRuleManager;
    private readonly ISubjectService _subjectManager;
    private readonly ISubtopicService _subtopicManager;
    private readonly IProductService _productManager;
    private readonly IMapper _mapper;
    private readonly IQuestionDifficultyService _questionDifficultyService;
    private readonly IQuestionService _questionService;

    public ExamRuleController(IExamRuleService examRuleService, ISubjectService subjectService, IProductService productService, IMapper mapper, ISubtopicService subtopicManager, IQuestionDifficultyService questionDifficultyService, IQuestionService questionService)
    {
        _examRuleManager = examRuleService;
        _subjectManager = subjectService;
        _productManager = productService;
        _mapper = mapper;
        _subtopicManager = subtopicManager;
        _questionDifficultyService = questionDifficultyService;
        _questionService = questionService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string typeOfExamRule, int? page, int pageSize = 10, bool? showAllData = null)
    {
        if (showAllData == null && HttpContext.Session.GetInt32("ShowAllData") != null)
        {
            showAllData = HttpContext.Session.GetInt32("ShowAllData") == 1;
        }
        bool showAll = showAllData ?? false;
        HttpContext.Session.SetInt32("ShowAllData", showAll ? 1 : 0);

        ViewBag.ProductList = await GetProductsAsync();
        ViewBag.SubjectList = await GetSubjectsAsync();
        ViewBag.SubtopicList = await GetSubtopicsAsync();
        ViewBag.ExamTypes = GetExamTypes();

        var getExamListResult = await _examRuleManager.GetAllAsync();
        var examRuleList = _mapper.Map<IEnumerable<AdminExamRuleListVM>>(getExamListResult.Data);

        if (!string.IsNullOrEmpty(typeOfExamRule))
            examRuleList = await Search(typeOfExamRule);

        int pageNumber = page ?? 1;
        ViewBag.PageSize = pageSize;
        ViewBag.CurrentPage = pageNumber;
        ViewBag.TypeOfExamRule = typeOfExamRule;


        if (!showAll)
        {
            examRuleList = examRuleList.Where(examRule => examRule.Status != Status.Deleted && examRule.Status != Status.Passive);
        }
        ViewBag.ShowAllData = showAllData;
        var paginatedList = examRuleList.ToPagedList(pageNumber, pageSize);
        return View(paginatedList);
    }



    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminExamRuleCreateVM viewModel, IFormCollection collection)
    {
        List<AdminExamRuleSubtopicCreateVM> examRuleSubtopicList = JsonSerializer.Deserialize<List<AdminExamRuleSubtopicCreateVM>>(collection["examRuleSubjects"]);
        viewModel.ExamRuleSubtopics = examRuleSubtopicList;

        if (!ModelState.IsValid && (examRuleSubtopicList.Count < 1))
        {
            NotifyErrorLocalized(Messages.PleaseAddExamRuleSubject);
        }
        else
        {
            ExamRuleCreateDto examRuleCreateDto = _mapper.Map<ExamRuleCreateDto>(viewModel);

            examRuleCreateDto.Name = StringExtensions.TitleFormat(viewModel.Name);

            var result = await _examRuleManager.AddAsync(examRuleCreateDto);
            if (!result.IsSuccess)
            {
                NotifyErrorLocalized(result.Message);
            }
            else
            {
                NotifySuccessLocalized(result.Message);
            }
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var result = await _examRuleManager.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
            return RedirectToAction(nameof(Index));
        }
        var examRuleUpdateVM = _mapper.Map<AdminExamRuleUpdateVM>(result.Data);

        ViewBag.ProductList = await GetProductsAsync();
        ViewBag.examRuleSubtopicsJSON = JsonSerializer.Serialize(examRuleUpdateVM.ExamRuleSubtopics);

        return View(examRuleUpdateVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminExamRuleUpdateVM model, IFormCollection collection)
    {
        List<AdminExamRuleSubtopicUpdateVM> examRuleSubtopicList = JsonSerializer.Deserialize<List<AdminExamRuleSubtopicUpdateVM>>(collection["examRuleSubjects"]);
        model.ExamRuleSubtopics = examRuleSubtopicList;

        if (!ModelState.IsValid && (examRuleSubtopicList.Count < 1))
        {
            ViewBag.ProductList = await GetProductsAsync();
            NotifyErrorLocalized(Messages.PleaseAddExamRuleSubject);

            return RedirectToAction(nameof(Index));
        }

        var examRuleUpdate = _mapper.Map<ExamRuleUpdateDto>(model);

        examRuleUpdate.Name = StringExtensions.TitleFormat(model.Name);

        var updateResult = await _examRuleManager.UpdateAsync(examRuleUpdate);

        if (!updateResult.IsSuccess)
        {
            NotifyErrorLocalized(updateResult.Message);
            return View(model);
        }

        NotifySuccessLocalized(updateResult.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var getExamRuleResponse = await _examRuleManager.GetDetailsByIdAsync(id);

        if (getExamRuleResponse.IsSuccess)
        {
            var detailsVm = _mapper.Map<AdminExamRuleDetailsVM>(getExamRuleResponse.Data);
            detailsVm.IsRuleUsed = await _examRuleManager.IsRuleUsedInExamsAsync(id);

            return View(detailsVm);
        }

        NotifyErrorLocalized(getExamRuleResponse.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _examRuleManager.DeleteAsync(id);
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

   
    [HttpGet]
    private async Task<List<SelectListItem>> GetProductsAsync()
    {
        var getProductResult = await _productManager.GetAllAsync();

        // Sadece aktif ürünleri filtrele
        var activeProducts = getProductResult.Data.Where(x => x.IsActive ?? false).ToList();

        return activeProducts.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
    }


    [HttpGet]
    public async Task<List<SelectListItem>> GetSubjectsByProductId(string productId)
    {
        var subjectList = await _subjectManager.GetAllByProductIdAsync(Guid.Parse(productId));
        return subjectList.Data.Where(subject => subject.Status != Status.Passive && subject.Status != Status.Deleted).Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
    }

    [HttpGet]
    public async Task<List<SelectListItem>> GetSubjectsAsync()
    {
        var subjectResult = await _subjectManager.GetAllAsync();

        // Sadece aktif konuları filtrele
        var activeSubjects = subjectResult.Data.Where(x => x.Status == Status.Active).ToList();

        return activeSubjects.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
    }

    [HttpGet]
    public async Task<List<SelectListItem>> GetSubtopicsBySubjectId(string subjectId)
    {
        var subtopicList = await _subtopicManager.GetSubtopicBySubjectId(Guid.Parse(subjectId));
        return subtopicList.Data.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
    }

    [HttpGet]
    public async Task<List<SelectListItem>> GetSubtopicsAsync()
    {
        var subtopicResult = await _subtopicManager.GetAllAsync();

        // Sadece aktif alt konuları filtrele
        var activeSubtopics = subtopicResult.Data.Where(x => x.Status == Status.Active).ToList();

        return activeSubtopics.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
    }


    [HttpGet]
    public List<SelectListItem> GetQuestionTypes(string examTypeId)
    {
        if (examTypeId == "1")
        {
            return Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>()
                .Select(x => new SelectListItem
                {
                    Text = Localize(x.GetType().GetMember(x.ToString()).First().GetCustomAttribute<DisplayAttribute>()!.GetName()!),
                    Value = ((int)x).ToString()
                }).ToList();
        }

        return Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>().
            Select(x => new SelectListItem
            {
                Text = Localize(x.GetType().GetMember(x.ToString()).First().GetCustomAttribute<DisplayAttribute>()!.GetName()!),
                Value = ((int)x).ToString()
            }).ToList();
    }

    [HttpGet]
    public List<SelectListItem> GetExamTypes()
    {
        return Enum.GetValues(typeof(ExamType)).Cast<ExamType>().
            Select(x => new SelectListItem
            {
                Text = Localize(x.GetType().GetMember(x.ToString()).First().GetCustomAttribute<DisplayAttribute>()!.GetName()!),
                Value = ((int)x).ToString()
            }).ToList();
    }

    [HttpGet]
    public async Task<SelectList> GetQuestionDifficulties()
    {
        return new SelectList((await _questionDifficultyService.GetAllAsync()).Data, "Id", "Name");
    }

    [HttpGet]
    public async Task<string> GetSubjectNameBySubjectId(string subjectId)
    {
        var subjectName = await _subjectManager.GetByIdAsync(Guid.Parse(subjectId));
        return subjectName.Data.Name;
    }

    [HttpGet]
    public async Task<IActionResult> ChangeStatus(Guid id)
    {
        var changeStatusResult = await _examRuleManager.ChangeRuleStatusAsync(id);
        if (!changeStatusResult.IsSuccess)
        {
            NotifyErrorLocalized(changeStatusResult.Message);
        }
        else
        {
            NotifySuccessLocalized(changeStatusResult.Message);
        }

        return Json(changeStatusResult);
    }

    [HttpGet]
    public async Task<List<AdminQuestionListVM>> GetQuestionListBySubtopic(string subtopicId)
    {
        var questions = await _questionService.GetQuestionBySubtopicId(Guid.Parse(subtopicId));

        var questionList = _mapper.Map<List<AdminQuestionListVM>>(questions.Data);

        return questionList;
    }

    public async Task<List<AdminExamRuleListVM>> Search(string typeOfExamRule)
    {
        var examRuleGetResult = await _examRuleManager.GetAllAsync();
        var examRuleList = _mapper.Map<List<AdminExamRuleListVM>>(examRuleGetResult.Data);
        var searchList = examRuleList
            .Where(s => s.Name.IndexOf(typeOfExamRule, StringComparison.OrdinalIgnoreCase) >= 0)
            .OrderBy(o => o.Name)
            .ToList();
        return searchList;
    }
}