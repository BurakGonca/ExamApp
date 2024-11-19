using AutoMapper;
using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Subtopics;
using BAExamApp.Entities.DbSets;
using BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;
using BAExamApp.MVC.Areas.Admin.Models.SubtopicVMs;
using BAExamApp.MVC.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace BAExamApp.MVC.Areas.Admin.Controllers;
public class SubtopicController : AdminBaseController
{
    private readonly ISubtopicService _subtopicService;
    private readonly ISubjectService _subjectService;
    private readonly IMapper _mapper;

    public SubtopicController(IMapper mapper,ISubtopicService subtopicService, ISubjectService subjectService)
    {
        _mapper = mapper;
        _subtopicService = subtopicService;
        _subjectService = subjectService;
    }

    // GET: SubtopicController
    [HttpGet]
    public async Task<IActionResult> Index(string typeOfSubtopic, int? page, int pageSize = 10, bool? showAllData = null )
    {
        if (showAllData == null && HttpContext.Session.GetInt32("ShowAllData") != null)
        {
            showAllData = HttpContext.Session.GetInt32("ShowAllData") == 1;
        }
        bool showAll = showAllData ?? false;


        HttpContext.Session.SetInt32("ShowAllData", showAll ? 1 : 0);

        int pageNumber = page ?? 1;

        ViewBag.SubjectList = await GetSubjectsAsync();

        var getSubtopicListResult = await _subtopicService.GetAllAsync();
        var subtopicList = _mapper.Map<IEnumerable<AdminSubtopicListVM>>(getSubtopicListResult.Data);

        if (!string.IsNullOrEmpty(typeOfSubtopic))
            subtopicList = await Search(typeOfSubtopic);

        if (!showAll)
        {
            subtopicList = subtopicList.Where(subtopic => subtopic.Status != Status.Deleted && subtopic.Status  != Status.Passive).ToList();
        }

        var pagedList = subtopicList.ToPagedList(pageNumber, pageSize);

        ViewBag.PageSize = pageSize;
        ViewBag.TypeOfSubtopic = typeOfSubtopic;
        ViewBag.ShowAllData = showAllData;

        return View(pagedList);
    }



    public async Task<List<AdminSubtopicListVM>> Search(string typeOfSubtopic)
    {
        var subtopicGetResult = await _subtopicService.GetAllAsync();
        var subtopicList = _mapper.Map<List<AdminSubtopicListVM>>(subtopicGetResult.Data);

        var searchList = subtopicList
            .Where(s => s.Name.IndexOf(typeOfSubtopic, StringComparison.OrdinalIgnoreCase) >= 0)
            .OrderBy(o => o.Name)
            .ToList();

        return searchList;
    }



    // GET: SubtopicController/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        ViewBag.SubjectList = await GetSubjectsAsync();
        var subtopicDto = await _subtopicService.GetDetailsByIdAsync(id);
        if (subtopicDto.IsSuccess)
        {
            var subtopicDetailVm = _mapper.Map<AdminSubtopicDetailVM>(subtopicDto.Data);
            subtopicDetailVm.SubjectName = (await _subjectService.GetByIdAsync(subtopicDto.Data.SubjectId)).Data.Name;

            // Set IsQuestionUsed and IsExamRuleUsed directly from service results
            subtopicDetailVm.IsQuestionUsed = await _subtopicService.IsQuestionUsedSubtopicAsync(id);
            subtopicDetailVm.IsExamRuleUsed = await _subtopicService.IsRuleUsedSubtopicAsync(id);

            return View(subtopicDetailVm);
        }
        NotifyErrorLocalized(subtopicDto.Message);
        return RedirectToAction(nameof(Index));
    }

    // GET: SubtopicController/Create
    [HttpPost]
    public async Task<IActionResult> Create(AdminSubtopicCreateVm viewModel)
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
            ViewBag.SubjectList = await GetSubjectsAsync(); // Pasif olmayan konuları tekrar yükleyin
            return PartialView("_CreatePartial", viewModel); // PartialView döndürülüyor
        }

        SubtopicCreateDto subtopicCreateDto = _mapper.Map<SubtopicCreateDto>(viewModel);
        subtopicCreateDto.Name = StringExtensions.TitleFormat(viewModel.Name);
        var createSubtopicResult = await _subtopicService.AddAsync(subtopicCreateDto);
        if (!createSubtopicResult.IsSuccess)
        {
            NotifyErrorLocalized(createSubtopicResult.Message);
            ViewBag.SubjectList = await GetSubjectsAsync(); // Pasif olmayan konuları tekrar yükleyin
            //return PartialView("_CreatePartial", viewModel); // PartialView döndürülüyor
            return RedirectToAction(nameof(Index));
        }
        NotifySuccessLocalized(createSubtopicResult.Message);
        return RedirectToAction(nameof(Index));
    }


    // POST: SubtopicController/Update/5
    [HttpPost]
    public async Task<IActionResult> Update(AdminSubtopicUpdateVM viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        var updateSubtopicDto = _mapper.Map<SubtopicUpdateDto>(viewModel);

        updateSubtopicDto.Name = StringExtensions.TitleFormat(viewModel.Name);

        var updateResult = await _subtopicService.UpdateAsync(updateSubtopicDto);
        if (!updateResult.IsSuccess)
        {
            NotifyErrorLocalized(updateResult.Message);
            return View(viewModel);
        }
        NotifySuccessLocalized(updateResult.Message);
        return RedirectToAction(nameof(Index));
    }

    // GET: SubtopicController/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var subtopicDeleteResponse = await _subtopicService.DeleteAsync(id);
        if (subtopicDeleteResponse.IsSuccess)
        {
            NotifySuccessLocalized(subtopicDeleteResponse.Message);
        }
        else
        {
            NotifyErrorLocalized(subtopicDeleteResponse.Message);
        }
        return Json(subtopicDeleteResponse);
    }
    /// <summary>
    /// Passif olmayan konuları döndüren liste.
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    private async Task<SelectList> GetSubjectsAsync(Guid? subjectId = null)
    {
        var subjectList = (await _subjectService.GetAllAsync()).Data
            .Where(x => x.Status != Status.Passive) // Pasif olmayanları filtreler
            .GroupBy(x => x.Name)
            .Select(x => x.First());
        return new SelectList(subjectList.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name,
            Selected = (subjectId != null ? x.Id == subjectId.Value : false)
        }).OrderBy(x => x.Text), "Value", "Text");
    }

    /// <summary>
    /// Bu action, update modalı açıldığında verileri sayfaya taşır.
    /// </summary>
    /// <param name="subtopicId"></param>
    /// <returns></returns>
    public async Task<AdminSubtopicUpdateVM> GetSubtopic(Guid subtopicId)
    {
        var subtopicFoundResult = await _subtopicService.GetSubtopicById(subtopicId);
        var subtopicUpdateVM = _mapper.Map<AdminSubtopicUpdateVM>(subtopicFoundResult.Data);
        return subtopicUpdateVM;
    }

    [HttpGet]
    public async Task<IActionResult> ChangeStatus(Guid id)
    {
        var activateResult = await _subtopicService.ChangeRuleStatusAsync(id);
        if (!activateResult.IsSuccess)
        {
            NotifyErrorLocalized(activateResult.Message);
        }
        else
        {
            NotifySuccessLocalized(activateResult.Message);
        }
        return Json(activateResult);
    }


   
}
