using AutoMapper;
using BAExamApp.Dtos.ClassroomProducts;
using BAExamApp.Dtos.Classrooms;
using BAExamApp.Dtos.StudentClassrooms;
using BAExamApp.Dtos.TrainerClassrooms;
using BAExamApp.Entities.DbSets;
using BAExamApp.MVC.Areas.Admin.Models.ClassroomVMs;
using BAExamApp.MVC.Areas.Admin.Models.ExamVMs;
using BAExamApp.MVC.Areas.Admin.Models.StudentVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using System.Drawing.Printing;
using X.PagedList;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

public class ClassroomController : AdminBaseController
{
    private readonly IBranchService _branchService;
    private readonly IClassroomService _classroomService;
    private readonly IClassroomProductService _classroomProductService;
    private readonly IGroupTypeService _groupTypeService;
    private readonly IProductService _productService;
    private readonly IStudentService _studentService;
    private readonly IStudentClassroomService _studentClassroomService;
    private readonly ITrainerClassroomService _trainerClassroomService;
    private readonly ITrainerService _trainerService;
    private readonly IExamService _examService;
    private readonly IExamAnalysisService _examAnalysisService;
    private readonly IStudentExamService _studentExamService;
    private readonly IExamClassroomsService _examClassroomsService;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;
    public ClassroomController(IClassroomService classroomService, IMapper mapper, IGroupTypeService groupTypeService, IProductService productService, IStudentService studentService, ITrainerService trainerService, IBranchService branchService, IStudentClassroomService studentClassroomService, ITrainerClassroomService trainerClassroomService, IClassroomProductService classroomProductService, IExamService examService, IExamAnalysisService examAnalysisService, IStudentExamService studentExamService, IExamClassroomsService examClassroomsService, IMemoryCache memoryCache)
    {
        _branchService = branchService;
        _classroomService = classroomService;
        _classroomProductService = classroomProductService;
        _groupTypeService = groupTypeService;
        _productService = productService;
        _studentClassroomService = studentClassroomService;
        _studentService = studentService;
        _trainerClassroomService = trainerClassroomService;
        _trainerService = trainerService;
        _examService = examService;
        _examAnalysisService = examAnalysisService;
        _studentExamService = studentExamService;
        _examClassroomsService = examClassroomsService;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> Index(int? page, int pageSize = 10, bool? showAllData = null)
    {

        if (showAllData == null && HttpContext.Session.GetInt32("ShowAllData") != null)
        {
            showAllData = HttpContext.Session.GetInt32("ShowAllData") == 1;
        }

        bool showAll = showAllData ?? false;

        HttpContext.Session.SetInt32("ShowAllData", showAll ? 1 : 0);

        var cacheKey = "ClassroomList_" + showAll.ToString();
        if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<AdminClassroomListVM> classroomList))
        {
            var resultAll = await _classroomService.GetAllAsync();
            var resultData = _mapper.Map<IEnumerable<AdminClassroomListVM>>(resultAll.Data);

            if (!showAll)
            {
                resultData = resultData.Where(x => x.Status == Core.Enums.Status.Active);
            }

            classroomList = resultData.ToList();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10));

            _memoryCache.Set(cacheKey, classroomList, cacheEntryOptions);
        }

        int pageNumber = page ?? 1;
        TempData["page"] = pageNumber;
        TempData["size"] = pageSize;
        
        ViewBag.PageSize = pageSize;
        ViewBag.CurrentPage = pageNumber;
        ViewBag.ProductList = await GetProducts();
        ViewBag.GroupTypeList = await GetGroupTypesAsync();
        ViewBag.BranchList = await GetBranchs();
        ViewBag.ShowAllData = showAll;

        var paginatedList = classroomList.ToPagedList(pageNumber, pageSize);
        ViewBag.ClassList = paginatedList.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();


        return View(paginatedList);
    }





    [HttpPost]
    public async Task<IActionResult> GetFilteredList(string name, string branchName, string groupType, DateTime openingDate, DateTime closedDate)
    {
        
        var getClassroomResponse = await _classroomService.GetFilteredByNameOrBranchNameOrGroupTypeOrOpeningDateOrClosedDateAsync(name, branchName, groupType, openingDate, closedDate);
        var classroomList = _mapper.Map<List<AdminClassroomListVM>>(getClassroomResponse.Data);

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
        var size = (int)TempData["size"];
        var page = (int)TempData["page"];
        var paginatedList = classroomList.ToPagedList(page, size);
        _memoryCache.Set("classroomList", paginatedList, cacheOptions);

        return RedirectToAction("GetFilteredList");
    }

    [HttpGet]
    public async Task<IActionResult> GetFilteredList()
    {
        if (_memoryCache.TryGetValue("classroomList", out var classroomList))
        {
            ViewBag.GroupTypeList = await GetGroupTypesAsync();
            ViewBag.BranchList = await GetBranchs();
            ViewBag.ProductList = await GetProducts();
            ViewBag.ClassList = await GetClasses();
            return View("Index", classroomList);
        }
        else
        {
            return RedirectToAction("Index");
        }
    }



    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        ViewBag.ProductList = await GetProducts();
        ViewBag.GroupTypeList = await GetGroupTypesAsync();
        ViewBag.BranchList = await GetBranchs();
        var getClassroomResponse = await _classroomService.GetDetailsByIdForAdminAsync(id);

        if (getClassroomResponse.IsSuccess)
        {
            var getClassroomExams = await _examService.GetExamsByClassIdAsync(id);
            ViewBag.ClassroomExams = getClassroomExams.Data;

            return View(_mapper.Map<AdminClassroomDetailsVM>(getClassroomResponse.Data));
        }
        NotifyErrorLocalized(getClassroomResponse.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Create(AdminClassroomCreateVM classroomCreateVM)
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
        var classroomCreateDto = _mapper.Map<ClassroomCreateDto>(classroomCreateVM);

        var classroomProducts = new List<ClassroomProductCreateDto>();

        foreach (var productId in classroomCreateVM.ProductIds)
        {
            classroomProducts.Add(new() { ProductId = productId });
        }

        classroomCreateDto.ClassroomProducts = classroomProducts;

        var createResult = await _classroomService.AddAsync(classroomCreateDto);

        if (!createResult.IsSuccess)
        {
            NotifyErrorLocalized(createResult.Message);
            return RedirectToAction(nameof(Index));
        }

        NotifySuccessLocalized(createResult.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var getResult = await _classroomService.GetDetailsByIdForAdminAsync(id);
        if (!getResult.IsSuccess)
            return RedirectToAction(nameof(Index));

        var classroomUpdateVm = _mapper.Map<AdminClassroomUpdateVM>(getResult.Data);

        classroomUpdateVm.ProductList = await GetProducts();
        classroomUpdateVm.GroupTypeList = await GetGroupTypesAsync();
        classroomUpdateVm.BranchList = await GetBranchs();
        return View(classroomUpdateVm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminClassroomUpdateVM model)
    {
        if (!ModelState.IsValid)
        {
            model.ProductList = await GetProducts();
            model.GroupTypeList = await GetGroupTypesAsync();
            model.BranchList = await GetBranchs();
            return View(model);
        }

        var classroomDto = _mapper.Map<ClassroomUpdateDto>(model);
        var updateResult = await _classroomService.UpdateAsync(classroomDto);
        if (updateResult.IsSuccess)
        {
            NotifySuccessLocalized(updateResult.Message);
        }
        else
        {
            NotifyErrorLocalized(updateResult.Message);
        }

        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> ClassRoomExamAnalysis(Guid examId, Guid classroomId)
    {
        var classroomDetails = await _classroomService.GetDetailsByIdForAdminAsync(classroomId);
        if (classroomDetails == null)
        {
            return View("Error", model: "Sınıf bulunamadı.");
        }
        ViewBag.ClassroomName = classroomDetails.Data.Name;

        var exam = await _examService.GetByIdAsync(examId);
        ViewBag.ExamName = exam.Data.Name;

        // Sınıfın her bir konuya ait ortalama performansını saklayacak sözlük
        var subtopicAveragePerformances = new Dictionary<string, List<double>>();


        var performance = await _examAnalysisService.AnalysisExamPerformanceAsync(examId);

        foreach (var subtopic in performance)
        {
            if (!subtopicAveragePerformances.ContainsKey(subtopic.Key))
            {
                subtopicAveragePerformances[subtopic.Key] = new List<double>();
            }
            subtopicAveragePerformances[subtopic.Key].Add(subtopic.Value);
        }

        var examSubtopicAveragePerformances = subtopicAveragePerformances.ToDictionary(
            subtopic => subtopic.Key,
            subtopic => subtopic.Value.Average()
        );

        var examPerformanceVM = new ClassroomStudentPerformanceVM
        {
            ClassroomId = classroomId,
            SubtopicPerformances = examSubtopicAveragePerformances
        };

        return View(examPerformanceVM);
    }

    [HttpGet]
    public async Task<IActionResult> AddTrainer(Guid id)
    {
        AdminClassroomAddTrainerVM viewModel = new()
        {
            ClassroomId = id,
            Trainers = await GetTrainersAsync(id)
        };
        try
        {
            viewModel.AppointedTrainersId = (await _trainerClassroomService.GetTrainersWithSpesificClassroomIdAsync(id))
                .Data
                .Select(x => x.Id.ToString())
                .ToList();
        }
        catch (Exception)
        {
            viewModel.AppointedTrainersId = new List<string>();
        }
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddTrainer(AdminClassroomAddTrainerVM viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Trainers = await GetTrainersAsync(viewModel.ClassroomId);
            return View(viewModel);
        }

        var addTrainerResponse = await _trainerClassroomService.AddTrainersToClassroomAsync(_mapper.Map<TraninerAddClassroomDto>(viewModel));
        if (addTrainerResponse.IsSuccess)
        {
            NotifySuccessLocalized(addTrainerResponse.Message);
        }
        else
        {
            NotifyErrorLocalized(addTrainerResponse.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> AddStudent(Guid id)
    {
        AdminClassroomAddStudentVM viewModel = new()
        {
            ClassroomId = id,
            Students = await GetStudentsAsync(id)
        };
        try
        {
            viewModel.AppointedStudentsId = (await _studentService.GetStudentsWithSpesificClassroomIdAsync(id)).Data
            .Select(x => x.Id.ToString())
            .ToList();
        }
        catch (Exception)
        {
            viewModel.AppointedStudentsId = new List<string>();
        }
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent(AdminClassroomAddStudentVM viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Students = await GetStudentsAsync(viewModel.ClassroomId);
            return View(viewModel);
        }

        var addStudentResult = await _studentClassroomService.AddStudentToClassroomAsync(_mapper.Map<StudentAddToClassroomDto>(viewModel));

        if (addStudentResult.IsSuccess)
        {
            NotifySuccessLocalized(addStudentResult.Message);
        }
        else
        {
            NotifyErrorLocalized(addStudentResult.Message);
        }

        return RedirectToAction(nameof(Index));
    }


    /// <summary>
    /// Sınıftan öğrenciyi siler.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> DeleteStudentByClassroom(Guid id)
    {

        var result = await _studentClassroomService.DeleteStudentByClassroom(id);

        if (result.IsSuccess)
            NotifySuccessLocalized(result.Message);
        else
            NotifyErrorLocalized(result.Message);


        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromQuery(Name = "id")] Guid id)
    {
        var deleteResult = await _classroomService.DeleteAsync(id);
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

    private async Task<SelectList> GetGroupTypesAsync()
    {
        var groupTypeList = await _groupTypeService.GetAllAsync();
        return new SelectList(groupTypeList.Data.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }), "Value", "Text");

    }
    private async Task<SelectList> GetProducts(Guid? productId = null)
    {
        var productList = (await _productService.GetAllAsync()).Data;
        return new SelectList(productList.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name,
            Selected = x.Id == (productId != null ? productId.Value : productId)
        }), "Value", "Text");

    }
    private async Task<SelectList> GetBranchs()
    {
        var branchList = await _branchService.GetAllAsync();
        return new SelectList(branchList.Data.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }), "Value", "Text");
    }
    private async Task<SelectList> GetClasses()
    {
        var classList = await _classroomService.GetAllAsync();
        return new SelectList(classList.Data.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }), "Value", "Text");
    }
    private async Task<List<SelectListItem>> GetTrainersAsync(Guid classroomId)
    {
        var getFreeTrainersResponse = await _trainerService.GetAllActiveAsync();
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
    private async Task<List<SelectListItem>> GetStudentsAsync(Guid classroomId)
    {
        var getFreeStudentsResponse = await _studentService.GetStudentsWithoutSpesificClassroomIdAsync(classroomId);
        if (getFreeStudentsResponse.IsSuccess)
        {
            var studentList = getFreeStudentsResponse.Data.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName,
            }).ToList();

            return studentList;
        }
        return new List<SelectListItem>();
    }
    public async Task<AdminClassroomUpdateVM> GetClassroom(Guid classroomId)
    {
        var classroomFoundResult = await _classroomService.GetDetailsByIdForAdminAsync(classroomId);

        var classroomUpdateVm = _mapper.Map<AdminClassroomUpdateVM>(classroomFoundResult.Data);
        //classroomUpdateVm.ProductList = await GetProducts();
        //classroomUpdateVm.GroupTypeList = await GetGroupTypesAsync();
        //classroomUpdateVm.BranchList = await GetBranchs();

        return classroomUpdateVm;
    }


    [HttpGet]
    public async Task<IActionResult> CheckRelation(Guid id)
    {
        // id parametresine göre veritabanında ilişkili kayıtları kontrol et
        bool hasRelation = await _classroomService.HasRelationship(id);

        // JSON formatında yanıt döndür
        return Json(new { hasRelation = hasRelation });
    }
}