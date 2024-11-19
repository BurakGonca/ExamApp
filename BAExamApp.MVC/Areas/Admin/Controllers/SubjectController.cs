using AutoMapper;
using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Products;
using BAExamApp.Dtos.ProductSubjects;
using BAExamApp.Dtos.Subjects;
using BAExamApp.Entities.DbSets;
using BAExamApp.MVC.Areas.Admin.Models.GroupTypeVMs;
using BAExamApp.MVC.Areas.Admin.Models.ProductVMs;
using BAExamApp.MVC.Areas.Admin.Models.SubjectVMs;
using BAExamApp.MVC.Areas.Admin.Models.SubtopicVMs;
using BAExamApp.MVC.Extensions;
using System.Drawing.Printing;
using X.PagedList;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

public class SubjectController : AdminBaseController
{
    private readonly ISubjectService _subjectService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    public SubjectController(ISubjectService subjectService, IMapper mapper, IProductService productService)
    {
        _subjectService = subjectService;
        _mapper = mapper;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string typeOfSubject, int? page, int pageSize = 10, bool? showAllData = null)
    {
        if (showAllData == null && HttpContext.Session.GetInt32("ShowAllData") != null)
        {
            showAllData = HttpContext.Session.GetInt32("ShowAllData") == 1;
        }
        bool showAll = showAllData ?? false;


        HttpContext.Session.SetInt32("ShowAllData", showAll ? 1 : 0);

        int pageNumber = page ?? 1;
        ViewBag.Products = await GetProductsAsync();


        var getSubjectListResult = await _subjectService.GetAllAsync();
        var subjectList = _mapper.Map<IEnumerable<AdminSubjectListVM>>(getSubjectListResult.Data);
        if (!string.IsNullOrEmpty(typeOfSubject))
            subjectList = await Search(typeOfSubject);


        if (!showAll)
        {
            subjectList = subjectList.Where(subject => subject.Status != Status.Deleted && subject.Status != Status.Passive).ToList();
        }
        var pagedList = subjectList.ToPagedList(pageNumber, pageSize);

        ViewBag.PageSize = pageSize;
        ViewBag.TypeOfSubject = typeOfSubject;
        ViewBag.ShowAllData = showAllData;

        return View(pagedList);
    }

    public async Task<List<AdminSubjectListVM>> Search(string typeOfSubject)
    {
        var subjectGetResult = await _subjectService.GetAllAsync();
        var subjectList = _mapper.Map<List<AdminSubjectListVM>>(subjectGetResult.Data);

        var searchList = subjectList
            .Where(s => s.Name.IndexOf(typeOfSubject, StringComparison.OrdinalIgnoreCase) >= 0)
            .OrderBy(o => o.Name)
            .ToList();

        return searchList;
    }






    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminSubjectCreateVM viewModel)
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

        var subject = _mapper.Map<SubjectCreateDto>(viewModel);
        subject.ProductSubjects = new List<ProductSubjectDto>();

        foreach (var productId in viewModel.ProductIds)
        {
            subject.ProductSubjects.Add(new() { ProductId = productId });
        }

        subject.Name = StringExtensions.TitleFormat(viewModel.Name);

        var createSubjectResult = await _subjectService.AddAsync(subject);
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
        ViewBag.Products = await GetProductsAsync();
        var subjectDto = await _subjectService.GetDetailsByIdAsync(id);

        bool isQuestionUsed = await _subjectService.IsQuestionUsedInSubjectAsync(id);
        bool isSubtopicUsed = await _subjectService.IsSubtopicUsedInSubjectAsync(id);
        bool isProductUsed = await _subjectService.IsProductSubjectUsedInSubjectAsync(id);

        if (subjectDto.IsSuccess)
        {
            var subjectDetailVm = _mapper.Map<AdminSubjectDetailVM>(subjectDto.Data);

            subjectDetailVm.IsQuestionUsed = isQuestionUsed;
            subjectDetailVm.IsSubtopicUsed = isSubtopicUsed;
            subjectDetailVm.IsProductUsed = isProductUsed;

            return View(subjectDetailVm);
        }

        NotifyErrorLocalized(subjectDto.Message);
        return RedirectToAction(nameof(Index));
    }

    //[HttpGet]
    //public async Task<IActionResult> Update(Guid id)
    //{
    //    var result = await _subjectService.GetByIdAsync(id);
    //    if (!result.IsSuccess)
    //    {
    //        NotifyErrorLocalized(result.Message);
    //        return RedirectToAction(nameof(Index));
    //    }
    //    var subjectUpdateVM = _mapper.Map<AdminSubjectUpdateVM>(result.Data);
    //    subjectUpdateVM.ProductList = await GetProductsAsync();
    //    return PartialView("Update", subjectUpdateVM);
    //}

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AdminSubjectUpdateVM viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.ProductList = await GetProductsAsync();
            return View(nameof(Index));
        }

        var updateSubjectDto = _mapper.Map<SubjectUpdateDto>(viewModel);

        updateSubjectDto.Name = StringExtensions.TitleFormat(viewModel.Name);

        updateSubjectDto.ProductSubjects = new List<ProductSubjectDto>();

        foreach (var ProductId in viewModel.ProductIds)
        {
            updateSubjectDto.ProductSubjects.Add(new() { ProductId = ProductId });
        }

        var updateResult = await _subjectService.UpdateAsync(updateSubjectDto);
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
        var isQuestionUsed = await _subjectService.IsQuestionUsedInSubjectAsync(id);
        var isSubtopicUsed = await _subjectService.IsSubtopicUsedInSubjectAsync(id);
        var isProductUsed = await _subjectService.IsProductSubjectUsedInSubjectAsync(id);

        if (isQuestionUsed || isSubtopicUsed || isProductUsed)
        {
            return await ChangeStatus(id);
        }
        else
        {
            return await DeleteConfirmed(id);
        }
    }

    [HttpGet]
    public async Task<IActionResult> ChangeStatus(Guid id)
    {
        var changeStatusResult = await _subjectService.ChangeSubjectStatusAsync(id);
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

    private async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var deleteResult = await _subjectService.DeleteAsync(id);
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
    private async Task<SelectList> GetProductsAsync(Guid? productId = null)
    {
        var productList = (await _productService.GetAllAsync()).Data
            .GroupBy(x => x.Name)
            .Select(x => x.First());

        return new SelectList(productList.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name,
            Selected = (productId != null ? x.Id == productId.Value : false)
        }).OrderBy(x => x.Text), "Value", "Text");
    }
    public async Task<AdminSubjectUpdateVM> GetSubject(Guid subjectId)
    {
        var subjectFoundResult = await _subjectService.GetByIdAsync(subjectId);
        var subjectUpdateVM = _mapper.Map<AdminSubjectUpdateVM>(subjectFoundResult.Data);
        subjectUpdateVM.ProductList = await GetProductsAsync();

        return subjectUpdateVM;
    }
}