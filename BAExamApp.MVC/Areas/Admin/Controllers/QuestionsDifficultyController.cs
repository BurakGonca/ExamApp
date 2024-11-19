using AutoMapper;
using BAExamApp.Dtos.QuestionDifficulty;
using BAExamApp.MVC.Areas.Admin.Models.QuestionDifficultyVMs;

namespace BAExamApp.MVC.Areas.Admin.Controllers;
public class QuestionsDifficultyController : AdminBaseController
{

    private readonly IQuestionDifficultyService _questionDifficultyService;
    private readonly IMapper _mapper;
    
    public QuestionsDifficultyController(IQuestionDifficultyService questionDifficultyService, IMapper mapper)
    {
        _questionDifficultyService = questionDifficultyService;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var questionDifficulty = await _questionDifficultyService.GetAllAsync();
        var questionDifficultyList = _mapper.Map<List<AdminQuestionDifficultyListVM>>(questionDifficulty.Data);
        return View(questionDifficultyList);
    }


    [HttpPost]
    public async Task<IActionResult> Create(AdminQuestionDifficultyCreateVM model)
    {
        if (!ModelState.IsValid)
        {
            var erros = ModelState.Values.SelectMany(x => x.Errors);
            string errorMessages = null!;
            foreach (var error in erros)
            {
                errorMessages += " ," + error.ErrorMessage;
            }
            NotifyError(errorMessages);
            return RedirectToAction(nameof(Index));
        }

        var questionDificulty = _mapper.Map<QuestionDifficultyCreateDto>(model);
        var result = await _questionDifficultyService.AddAsync(questionDificulty);
        if (result.IsSuccess)
        {
           
            NotifySuccess(Localize(result.Message));
            return RedirectToAction(nameof(Index));
        }

        NotifyErrorLocalized(result.Message);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var questionDifficulty = await _questionDifficultyService.GetDetailsByIdAsync(id);
        if (questionDifficulty.IsSuccess)
        {
            var questionDifficultyVm = _mapper.Map<AdminQuestionDifficultyDetailVM>(questionDifficulty.Data);
            return View(questionDifficultyVm);
        }
        NotifyErrorLocalized(questionDifficulty.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var questionDifficultyResult = await _questionDifficultyService.GetDetailsByIdAsync(id);
        if (!questionDifficultyResult.IsSuccess)
        {
            NotifyErrorLocalized(questionDifficultyResult.Message);
            return RedirectToAction(nameof(Index));
        }
        var updateQuestionDifficultyResult = _mapper.Map<AdminQuestionDifficultyUpdateVM>(questionDifficultyResult.Data);
        return PartialView("Update",updateQuestionDifficultyResult);
    }


    [HttpPost]
    public async Task<IActionResult> Update(AdminQuestionDifficultyUpdateVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var updateQuestionDifficultyDto = _mapper.Map<QuestionDifficultyUpdateDto>(model);
        var updateQuestionDifficultyResult = await _questionDifficultyService.UpdateAsync(updateQuestionDifficultyDto);
        if (!updateQuestionDifficultyResult.IsSuccess)
        {
            NotifyErrorLocalized(updateQuestionDifficultyResult.Message);
            return View(model);
        }
        NotifySuccessLocalized(updateQuestionDifficultyResult.Message);
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var questionDifficultyResponse = await _questionDifficultyService.DeleteAsync(id);
        if (questionDifficultyResponse.IsSuccess)
        {
            NotifySuccessLocalized(questionDifficultyResponse.Message);
        }
        else
        {
            NotifyErrorLocalized(questionDifficultyResponse.Message);
        }
        return RedirectToAction(nameof(Index));
    }

}
