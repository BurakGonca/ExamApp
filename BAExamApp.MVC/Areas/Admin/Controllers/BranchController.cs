using AutoMapper;
using BAExamApp.Business.Constants;
using BAExamApp.Dtos.Branches;
using BAExamApp.MVC.Areas.Admin.Models.BranchVMs;
using BAExamApp.MVC.Areas.Admin.Models.GroupTypeVMs;
using X.PagedList;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

public class BranchController : AdminBaseController
{
    private readonly IBranchService _branchService;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public BranchController(IBranchService branchService, IMapper mapper, IProductService productService)
    {
        _branchService = branchService;
        _mapper = mapper;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string typeOfBranch, int? page, int pageSize = 10)
    {
        int pageNumber = page ?? 1;
        var branchGetResult = await _branchService.GetAllAsync();
        var branchList = _mapper.Map<List<AdminBranchListVM>>(branchGetResult.Data).OrderBy(n => n.Name).ToList();

        if (!string.IsNullOrEmpty(typeOfBranch))
            branchList = await Search(typeOfBranch);

        var pagedList = branchList.ToPagedList(pageNumber, pageSize);
        ViewBag.PageSize = pageSize;
        ViewBag.TypeOfBranch = typeOfBranch;

        return View(pagedList);
    }

    public async Task<List<AdminBranchListVM>> Search(string typeOfBranch)
    {
        var branchGetResult = await _branchService.GetAllAsync();
        var branchList = _mapper.Map<List<AdminBranchListVM>>(branchGetResult.Data);
        var searchList = branchList
            .Where(s => s.Name.IndexOf(typeOfBranch, StringComparison.OrdinalIgnoreCase) >= 0)
            .OrderBy(o => o.Name)
            .ToList();

        return searchList;

    }




    [HttpGet]
    public async Task<IActionResult> Details(Guid id, string status)
    {
        var getBranchResponse = await _branchService.GetDetailsByIdAsync(id);

        if (getBranchResponse.IsSuccess)
        {

            var branchDetails = _mapper.Map<AdminBranchDetailsVM>(getBranchResponse.Data);
            branchDetails.IsBranchUsedInClass = await _branchService.IsBranchUsedInClass(id);
            return View(branchDetails);
        }

        NotifyErrorLocalized(getBranchResponse.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Create(AdminBranchCreateVM model)
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

        var branch = _mapper.Map<BranchCreateDto>(model);
        var addResult = await _branchService.AddAsync(branch);
        if (!addResult.IsSuccess)
        {
            NotifyErrorLocalized(addResult.Message);
            return RedirectToAction(nameof(Index));
        }

        NotifySuccessLocalized(addResult.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminBranchUpdateVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Index));
        }

        var branchUpdateDto = _mapper.Map<BranchUpdateDto>(model);
        var updateResult = await _branchService.UpdateAsync(branchUpdateDto);
        if (!updateResult.IsSuccess)
        {
            NotifyErrorLocalized(updateResult.Message);
            return View(nameof(Index));
        }

        NotifySuccessLocalized(updateResult.Message);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromQuery(Name = "id")] Guid id)
    {
        var deleteResult = await _branchService.DeleteAsync(id);
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


}