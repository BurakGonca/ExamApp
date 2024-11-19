using AutoMapper;
using BAExamApp.Core.Utilities.Results;
using BAExamApp.Dtos.GroupTypes;
using BAExamApp.MVC.Areas.Admin.Models.GroupTypeVMs;
using System.Drawing.Printing;
using System.Text;
using X.PagedList;
//using X.PagedList.Extensions;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

public class GroupTypeController : AdminBaseController
{
    private readonly IGroupTypeService _groupTypeService;
    private readonly IMapper _mapper;
    public GroupTypeController(IGroupTypeService groupTypeService, IMapper mapper)
    {
        _groupTypeService = groupTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string typeOfEducation, int? page, int pageSize = 10)
    {
        int pageNumber = page ?? 1;
        var groupTypeGetResult = await _groupTypeService.GetAllAsync();
        var groupTypeList = _mapper.Map<List<AdminGroupTypeListVM>>(groupTypeGetResult.Data).OrderBy(o => o.Name).ToList();

        if (!string.IsNullOrEmpty(typeOfEducation))
            groupTypeList = await Search(typeOfEducation);

        var pagedList = groupTypeList.ToPagedList(pageNumber, pageSize);

        ViewBag.PageSize = pageSize;
        ViewBag.TypeOfEducation = typeOfEducation;

        return View(pagedList);
    }


    public async Task<List<AdminGroupTypeListVM>> Search(string typeOfEducation)
    {
        var groupTypeGetResult = await _groupTypeService.GetAllAsync();
        var groupTypeList = _mapper.Map<List<AdminGroupTypeListVM>>(groupTypeGetResult.Data);

        var searchList = groupTypeList
            .Where(s => s.Name.IndexOf(typeOfEducation, StringComparison.OrdinalIgnoreCase) >= 0)
            .OrderBy(o => o.Name)
            .ToList();

        return searchList;
    }


    [HttpPost]
    public async Task<IActionResult> Create(AdminGroupTypeCreateVM model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            var errorMessages = new StringBuilder();

            foreach (var error in errors)
            {
                if (errorMessages.Length > 0)
                {
                    errorMessages.Append(", ");

                    if (errorMessages.ToString().Contains(error.ErrorMessage))
                    {
                        continue;
                    }
                }

                errorMessages.Append(error.ErrorMessage);
            }

            NotifyError(errorMessages.ToString());
            return RedirectToAction(nameof(Index));
        }

        var addResult = await _groupTypeService.AddAsync(_mapper.Map<GroupTypeCreateDto>(model));
        if (!addResult.IsSuccess)
        {
            NotifyErrorLocalized(addResult.Message);
            return RedirectToAction(nameof(Index));
        }

        NotifySuccessLocalized(addResult.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var getGroupType = await _groupTypeService.GetByIdAsync(id);
        if (!getGroupType.IsSuccess)
        {
            NotifyErrorLocalized(getGroupType.Message);
            return RedirectToAction(nameof(Index));
        }

        return View(_mapper.Map<AdminGroupTypeDetailVM>(getGroupType.Data));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _groupTypeService.DeleteAsync(id);
        if (result.IsSuccess)
        {
            NotifySuccessLocalized(result.Message);
        }
        else
        {
            NotifyErrorLocalized(result.Message);
        }

        return Json(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminGroupTypeUpdateVM groupTypeUpdateVM)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            var errorMessages = new StringBuilder();

            foreach (var error in errors)
            {
                if (errorMessages.Length > 0)
                {
                    errorMessages.Append(", ");

                    if (errorMessages.ToString().Contains(error.ErrorMessage))
                    {
                        continue;
                    }
                }

                errorMessages.Append(error.ErrorMessage);
            }

            NotifyError(errorMessages.ToString());
            return RedirectToAction(nameof(Index));
        }

        var groupTypeUpdateDto = _mapper.Map<GroupTypeUpdateDto>(groupTypeUpdateVM);
        var groupUpdateResponse = await _groupTypeService.UpdateAsync(groupTypeUpdateDto);
        if (!groupUpdateResponse.IsSuccess)
        {
            NotifyErrorLocalized(groupUpdateResponse.Message);
            return View(nameof(Index));
        }
        else
        {
            NotifySuccessLocalized(groupUpdateResponse.Message);
        }

        return RedirectToAction(nameof(Index));
    }
}