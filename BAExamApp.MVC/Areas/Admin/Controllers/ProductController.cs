using AutoMapper;
using BAExamApp.Dtos.Products;
using BAExamApp.Dtos.ProductSubjects;
using BAExamApp.Dtos.TrainerProducts;
using BAExamApp.MVC.Areas.Admin.Models.ProductVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using BAExamApp.MVC.Extensions;
using BAExamApp.Core.Enums;
using X.PagedList;
using BAExamApp.MVC.Areas.Admin.Models.GroupTypeVMs;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

public class ProductController : AdminBaseController
{
    private readonly IProductService _productService;
    private readonly ITechnicalUnitService _technicalUnitManager;
    private readonly ISubjectService _subjectService;
    private readonly ITrainerService _trainerService;
    private readonly ITrainerProductService _trainerProductService;
    private readonly IMapper _mapper;
    public ProductController(IMapper mapper, IProductService productService, ITechnicalUnitService technicalUnitManager, ISubjectService subjectService, ITrainerService trainerService, ITrainerProductService trainerProductService)
    {
        _mapper = mapper;
        _productService = productService;
        _technicalUnitManager = technicalUnitManager;
        _subjectService = subjectService;
        _trainerService = trainerService;
        _trainerProductService = trainerProductService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string productName, int? page, int pageSize = 10, bool? showAllData = null)
    {

        if (showAllData == null && HttpContext.Session.GetInt32("ShowAllData") != null)
        {
            showAllData = HttpContext.Session.GetInt32("ShowAllData") == 1;
        }


        bool showAll = showAllData ?? false;


        HttpContext.Session.SetInt32("ShowAllData", showAll ? 1 : 0);

        int pageNumber = page ?? 1;

        ViewBag.TechnicalUnits = await GetTechnicalUnitsAsync();
        ViewBag.Subjects = await GetSubjectsAsync();
        ViewBag.PageSize = pageSize;
        ViewBag.ShowAllData = showAllData;
        ViewBag.ProductName= productName;

        var productGetResult = await _productService.GetAllAsync();
        var mappedProductList = _mapper.Map<List<AdminProductListVM>>(productGetResult.Data);
        if (!string.IsNullOrEmpty(productName))
            mappedProductList = await Search(productName);
        if (!showAll)
        {
            mappedProductList = mappedProductList
                .Where(product => product.Status != Status.Deleted && product.Status != Status.Passive)
                .ToList();
        }

        var pagedProductList = mappedProductList.ToPagedList(pageNumber, pageSize);

        return View(pagedProductList);
    }

    public async Task<List<AdminProductListVM>> Search(string productName)
    {
        var productGetResult = await _productService.GetAllAsync();
        var productList = _mapper.Map<List<AdminProductListVM>>(productGetResult.Data);

        var searchList = productList
            .Where(s => s.Name.IndexOf(productName, StringComparison.OrdinalIgnoreCase) >= 0)
            .OrderBy(o => o.Name)
            .ToList();

        return searchList;
    }




    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.TechnicalUnits = await GetTechnicalUnitsAsync();
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(AdminProductCreateVM model)
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

        var product = _mapper.Map<ProductCreateDto>(model);


        product.Name = StringExtensions.TitleFormat(model.Name);

        var result = await _productService.AddAsync(product);
        if (result.IsSuccess)
        {
            NotifySuccess($"{model.Name} eğitimi başarıyla eklendi");
            return RedirectToAction("Index");
        }

        NotifyErrorLocalized(result.Message);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        ViewBag.TechnicalUnits = await GetTechnicalUnitsAsync();
        ViewBag.Subjects = await GetSubjectsAsync();
        var product = await _productService.GetDetailsByIdAsync(id);
        if (product.IsSuccess)
        {
            var mappedProduct = _mapper.Map<AdminProductDetailVM>(product.Data);
            // Check if the product is used in classrooms
            var isClassroomUsed = await _productService.IsClassroomUsedProductAsync(id);
            mappedProduct.IsClassroomUsed = isClassroomUsed;

            // Check if the product is used by trainers
            var isTrainerUsed = await _productService.IsTrainerUsedProductAsync(id);
            mappedProduct.IsTrainerUsed = isTrainerUsed;

            // Filter and order trainer products
            mappedProduct.TrainerProducts = mappedProduct.TrainerProducts
                .FindAll(x => x.ProductId == id)
                .OrderBy(x => x.CreatedDate)
                .ToList();

            // Populate trainer full names
            foreach (var trainerProduct in mappedProduct.TrainerProducts)
            {
                var trainer = await _trainerService.GetByIdAsync(trainerProduct.TrainerId);
                trainerProduct.FullName = trainer.Data.FirstName + " " + trainer.Data.LastName;
            }
            return View(mappedProduct);
        }

        NotifyErrorLocalized(product.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyErrorLocalized(result.Message);
            return RedirectToAction(nameof(Index));
        }
        var productUpdateVM = _mapper.Map<AdminProductUpdateVM>(result.Data);
        productUpdateVM.SubjectList = await GetSubjectsAsync();
        productUpdateVM.TechnicalUnitList = await GetTechnicalUnitsAsync();
        return PartialView("Update", productUpdateVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminProductUpdateVM model)
    {
        if (!ModelState.IsValid)
        {
            model.SubjectList = await GetSubjectsAsync();
            model.TechnicalUnitList = await GetTechnicalUnitsAsync();
            return View(model);
        }

        if (model.IsActive == true)
        {
            model.Status = Status.Active;
        }
        else
            model.Status = Status.Passive;

        var productUpdate = _mapper.Map<ProductUpdateDto>(model);

        productUpdate.Name = StringExtensions.TitleFormat(model.Name);

        productUpdate.ProductSubjects = new List<ProductSubjectDto>();

        foreach (var subjectId in model.SubjectIds)
        {
            productUpdate.ProductSubjects.Add(new() { SubjectId = subjectId });
        }
        var updateResult = await _productService.UpdateAsync(productUpdate);
        if (!updateResult.IsSuccess)
        {
            NotifyErrorLocalized(updateResult.Message);
            return View(model);
        }

        NotifySuccessLocalized(updateResult.Message);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete([FromQuery(Name = "id")] Guid id)
    {
        var deleteResult = await _productService.DeleteAsync(id);
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



    private async Task<SelectList> GetTechnicalUnitsAsync()
    {
        var technicalUnitList = await _technicalUnitManager.GetAllAsync();
        if (technicalUnitList == null || technicalUnitList.Data == null)
        {
            throw new Exception("Technical units not found");
        }

        var technicalUnits = technicalUnitList.Data.ToList();
        if (!technicalUnits.Any())
        {
            throw new Exception("No technical units found");
        }

        // Check if the Id property exists and is valid
        foreach (var technicalUnit in technicalUnits)
        {
            if (!technicalUnit.GetType().GetProperty("Id")?.CanRead ?? false)
            {
                throw new Exception($"Id property not found or not readable in {technicalUnit.GetType().Name}");
            }

            if (technicalUnit.Id == null)
            {
                throw new Exception($"Id property is null for {technicalUnit.Name}");
            }
        }

        foreach (var technicalUnit in technicalUnits)
        {
            if (!technicalUnit.GetType().GetProperty("Name")?.CanRead ?? false)
            {
                throw new Exception($"Name property not found or not readable in {technicalUnit.GetType().Name}");
            }

            if (string.IsNullOrEmpty(technicalUnit.Name))
            {
                throw new Exception($"Name property is empty or null for {technicalUnit.Id}");
            }
        }

        return new SelectList(technicalUnits, "Id", "Name");
    }

    /// <summary>
    /// GetSubjectsAsync methotu asenkron olarak tüm konuları alır ve bir SelectList döndürür. Eğer subjectId belirtilmişse, seçili konuyu belirlemek için kullanılır.
    /// subjectList diye bir değişkene eğitmen olarak eklenen 1 den fazla aynı isimde konu var ise sadece 1 ini döndürmesini istedik.
    /// return new SelectList ile birlikte öğelerin her biri SelectListItem sınıfından bir listedir.
    /// SelectListItem bir seçenek olarak Value(Saklanacak olan değer), Text(Kullanıcıya gösterilecek metin değeri)
    /// </summary>
    /// <param name="subjectId">Seçili konu Id'si </param>
    /// <returns>Seçenekleri içeren bir SelectList</returns>
    private async Task<SelectList> GetSubjectsAsync(Guid? subjectId = null)
    {

        var subjectList = (await _subjectService.GetAllAsync()).Data
            .GroupBy(x => x.Name)
            .Select(x => x.First());
        return new SelectList(subjectList.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.Name,
            Selected = (subjectId != null ? x.Id == subjectId.Value : false)
        }).OrderBy(x => x.Text), "Value", "Text");
    }

    [HttpGet]
    public async Task<IActionResult> AddTrainer(Guid id)
    {
        AdminProductAddTrainerVM viewModel = new()
        {
            ProductId = id,
            Trainers = await GetTrainersAsync()
        };
        try
        {
            viewModel.AppointedTrainersId = (await _trainerService.GetTrainersWithSpesificProductIdAsync(id)).Data.Select(x => x.Id.ToString()).ToList();
        }
        catch (Exception)
        {
            viewModel.AppointedTrainersId = new List<string>();
        }
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddTrainer(AdminProductAddTrainerVM viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Trainers = await GetTrainersAsync();
            return View(viewModel);
        }
        var mappedTrainers = _mapper.Map<ProductAddTrainerDto>(viewModel);
        var addTrainerResponse = await _trainerProductService.AddTrainersToProductAsync(mappedTrainers);

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

    private async Task<List<SelectListItem>> GetTrainersAsync()
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
    public async Task<AdminProductUpdateVM> GetProduct(Guid productId)
    {
        var productFoundResult = await _productService.GetByIdAsync(productId);

        var productUpdateVM = _mapper.Map<AdminProductUpdateVM>(productFoundResult.Data);
        productUpdateVM.TechnicalUnitList = await GetTechnicalUnitsAsync();
        productUpdateVM.SubjectList = await GetSubjectsAsync();

        return productUpdateVM;
    }
}