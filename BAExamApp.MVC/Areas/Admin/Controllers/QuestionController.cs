using AutoMapper;
using BAExamApp.Business.Constants;
using BAExamApp.Core.Enums;
using BAExamApp.Dtos.QuestionAnswers;
using BAExamApp.Dtos.QuestionArranges;
using BAExamApp.Dtos.QuestionRevisions;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.QuestionSubtopics;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;
using BAExamApp.MVC.Areas.Admin.Models.QuestionArrangeVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionRevisionVMs;
using BAExamApp.MVC.Areas.Admin.Models.QuestionVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionAnswerVMs;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateQuestionVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionAnswerVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionRevisionVMs;
using BAExamApp.MVC.Areas.Trainer.Models.QuestionVMs;
using BAExamApp.MVC.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using X.PagedList;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

public class QuestionController : AdminBaseController
{

    private readonly IQuestionService _questionService;
    private readonly ISubjectService _subjectService;
    private readonly IMapper _mapper;
    private readonly IAdminService _adminService;
    private readonly IQuestionRevisionService _questionRevisionService;
    private readonly ITrainerService _trainerService;
    private readonly IProductService _productService;
    private readonly IQuestionAnswerService _questionAnswerService;
    private readonly ISubtopicService _subtopicService;
    private readonly IQuestionDifficultyService _questionDifficultyService;
    private readonly IStudentQuestionService _studentQuestionService;
    private readonly IQuestionArrangeService _questionArrangeService;
    private readonly IMemoryCache _memoryCache;

    public QuestionController(IQuestionService questionService, ISubjectService subjectService, IMapper mapper, IAdminService adminService, IQuestionRevisionService questionRevisionService, ITrainerService trainerService, IProductService productService, IQuestionAnswerService questionAnswerService, ISubtopicService subtopicService, IQuestionDifficultyService questionDifficultyService, IMemoryCache memoryCache, IStudentQuestionService studentQuestionService, IQuestionArrangeService questionArrangeService)
    {
        _questionService = questionService;
        _subjectService = subjectService;
        _mapper = mapper;
        _adminService = adminService;
        _questionRevisionService = questionRevisionService;
        _trainerService = trainerService;
        _productService = productService;
        _questionAnswerService = questionAnswerService;
        _subtopicService = subtopicService;
        _questionDifficultyService = questionDifficultyService;
        _memoryCache = memoryCache;
        _studentQuestionService = studentQuestionService;
        _questionArrangeService = questionArrangeService;
    }

    [HttpGet]
    public async Task<IActionResult> QuestionList(State state, int? page, int pageSize = 10, bool showAllQuestions = false)
    {
        int pageNumber = page ?? 1;
        if (state == 0)
        {
            state = State.Awaited; // Varsayılan bir değer atayabilirsiniz.
        }

        IEnumerable<AdminQuestionListVM> questionList = new List<AdminQuestionListVM>();
        if (showAllQuestions)
        {
            var questionListResult = await _questionService.GetAllByStateAsync(state);
            questionList = _mapper.Map<IEnumerable<AdminQuestionListVM>>(questionListResult.Data);

            _memoryCache.Set("filteredQuestionList", questionList, TimeSpan.FromMinutes(10));
            _memoryCache.Set("state", (int)state, TimeSpan.FromMinutes(10));
        }
        var pagedList = questionList.ToPagedList(pageNumber, pageSize);
        await CompleteQuestionFilters();
        ViewBag.PageSize = pageSize;
        ViewData["Question_State"] = (int)state;
        ViewBag.ShowAllQuestions = showAllQuestions;

        return View(pagedList);
    }

   
    [HttpGet]
    public async Task<IActionResult> GetQuestionsByGivenValues(int? page = 1, int pageSize = 10)
    {
        _memoryCache.TryGetValue("filteredQuestionList", out List<AdminQuestionListVM> questionList);
        _memoryCache.TryGetValue("state", out var _state);
        _memoryCache.TryGetValue("subjectList", out var subjectList);
        _memoryCache.TryGetValue("SubtopicList", out var subtopicList);
        _memoryCache.TryGetValue("questionDifficultyList", out var questionDifficultyList);

        int pageNumber = page ?? 1;

        if (questionList != null && _state != null)
        {
            // IPagedList türünde sayfalama yapısı oluştur
            var pagedList = questionList.ToPagedList(pageNumber, pageSize);

            ViewData["Question_State"] = _state;
            ViewBag.SubjectList = subjectList;
            ViewBag.SubtopicList = subtopicList;
            ViewBag.QuestionDifficultyList = questionDifficultyList;
            ViewBag.PageSize = pageSize;

            return View("QuestionList", pagedList);
        }
        else
        {
            // Eğer cache'de veri yoksa QuestionList'e yönlendir
            return RedirectToAction("QuestionList", "Question", new { state = _state, showAllQuestions = true });
        }
    }

    [HttpPost]
    public async Task<IActionResult> GetQuestionsByGivenValues(AdminQuestionListVM adminQuestionListVM, State state, int? page, int pageSize = 10)
    {
        int pageNumber = page ?? 1;

        if (adminQuestionListVM.SubtopicName == null)
        {
            adminQuestionListVM.SubtopicName = new List<string>();
        }

        var getQuestionResponse = await _questionService.GetQuestionBySearchValues(
            adminQuestionListVM.Content,
            adminQuestionListVM.SubjectName,
            adminQuestionListVM.SubtopicName.FirstOrDefault(),
            adminQuestionListVM.QuestionDifficultyName,
            adminQuestionListVM.CreatedDate.ToShortDateString(),
            state
        );

        var questionList = _mapper.Map<List<AdminQuestionListVM>>(getQuestionResponse.Data);
        var pagedList = questionList.ToPagedList(pageNumber, pageSize);

        // Cache'e filtrelenmiş soru listesini ekleyin
        _memoryCache.Set("filteredQuestionList", questionList, TimeSpan.FromMinutes(10));
        _memoryCache.Set("state", (int)state, TimeSpan.FromMinutes(10));

        // ViewBag ve ViewData bilgilerini ayarlayın
        ViewBag.Content = adminQuestionListVM.Content;
        ViewBag.PageSize = pageSize;
        ViewData["Question_State"] = state;

        var subjectList = await GetSubjects();
        ViewBag.SubjectList = new SelectList(subjectList, "Value", "Text");

        var selectedSubjectItem = subjectList.FirstOrDefault(x => x.Value == adminQuestionListVM.SubjectName || x.Text == adminQuestionListVM.SubjectName);
        if (selectedSubjectItem != null)
        {
            selectedSubjectItem.Selected = true;
        }

        var subtopicList = selectedSubjectItem != null ? await GetSubtopic(selectedSubjectItem.Value) : await GetSubtopic();
        ViewBag.SubtopicList = new SelectList(subtopicList, "Value", "Text");

        var questionDifficultyList = await GetQuestionDifficulty();
        ViewBag.QuestionDifficultyList = new SelectList(questionDifficultyList, "Value", "Text");

        await CompleteQuestionFilters(subjectList, subtopicList, questionDifficultyList);

        // Sayfalandırılmış veriyle görünümü geri döndürün
        return View("QuestionList", pagedList);
    }


    [HttpGet]
    public async Task<IActionResult> QuestionDetails(Guid id)
    {

        var questionRevisions = await _questionRevisionService.GetAllByQuestionId(id);

        if (questionRevisions.Any())
        {
            var questionRevisionsVM = _mapper.Map<IEnumerable<QuestionRevisionListVM>>(questionRevisions);
            ViewBag.QuestionRevisionVB = questionRevisionsVM;

        }

        var questionDetailResult = await _questionService.GetDetailsByIdForAdminAsync(id);
        var trainers = await _trainerService.GetAllAsync();
        var model = _mapper.Map<AdminQuestionApprovedVM>(questionDetailResult.Data);

        ViewBag.TrainerList = trainers.Data;
        _memoryCache.TryGetValue("state", out var _state);

        ViewData["Question_State"] = _state;

        return View("QuestionDetails", model);
    }

    [HttpGet]
    public async Task<IActionResult> Review(Guid id)
    {
        var questionDetailResult = await _questionService.GetDetailsByIdForAdminAsync(id);
        var trainers = await _trainerService.GetAllAsync();
        var model = _mapper.Map<AdminQuestionReviewVM>(questionDetailResult.Data);
        var creatorId = trainers.Data.Where(x => x.FullName == model.CreatedBy).FirstOrDefault().Id;
        model.TrainerID = creatorId;
        ViewBag.TrainerList = trainers.Data;
        return View("QuestionDetails", model);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(Guid id)
    {
        var updateStateResult = await _questionService.UpdateStateAsync(id, State.Approved);
        if (updateStateResult.IsSuccess) NotifySuccessLocalized(updateStateResult.Message);
        else NotifyErrorLocalized(updateStateResult.Message);

        return RedirectToAction(nameof(QuestionList), new { state = State.Approved });
    }

    [HttpPost]
    public async Task<IActionResult> Reject(Guid id, [FromForm] string? rejectComment)
    {
        var updateStateResult = await _questionService.UpdateStateWithCommentAsync(id, State.Rejected, rejectComment);
        if (updateStateResult.IsSuccess)
            NotifySuccessLocalized(updateStateResult.Message);
        else
            NotifyErrorLocalized(updateStateResult.Message);

        return RedirectToAction(nameof(QuestionList), new { state = State.Rejected });
    }

    [HttpPost]
    public async Task<IActionResult> Review(AdminQuestionReviewVM model)
    {
        if (ModelState.IsValid)
        {
            var currentAdmin = await _adminService.GetByIdentityIdAsync(UserIdentityId);
            if (currentAdmin.IsSuccess)
            {
                var questionRevision = new QuestionRevisionCreateDto() { QuestionId = model.Id, RequestDescription = model.RequestDescription, RequestedTrainerId = model.TrainerID, RequesterAdminId = currentAdmin.Data.Id };

                var result = await _questionRevisionService.AddAsync(questionRevision);
                if (!result.IsSuccess)
                {
                    NotifyErrorLocalized(result.Message);
                }
                else
                {
                    var questionresult = await _questionService.UpdateStateAsync(model.Id, State.Reviewed);
                    if (!questionresult.IsSuccess)
                    {
                        NotifyErrorLocalized(questionresult.Message);
                    }
                }
            }
            NotifySuccessLocalized(currentAdmin.Message);
            return RedirectToAction(nameof(QuestionList), new { state = State.Awaited });
        }
        var questionDetailResult = await _questionService.GetDetailsByIdForAdminAsync(model.Id);
        ViewBag.TrainerList = await GetTrainersAsync(model.TrainerID);
        var mappedQuestion = _mapper.Map<AdminQuestionReviewVM>(questionDetailResult.Data);
        mappedQuestion.TrainerID = model.TrainerID;
        mappedQuestion.RequestDescription = model.RequestDescription;

        return View(mappedQuestion);
    }
    [HttpPost]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var updateStateResult = await _questionService.UpdateStateAsync(id, State.Deactivated);
        if (updateStateResult.IsSuccess)
            NotifySuccessLocalized(updateStateResult.Message);
        else
            NotifyErrorLocalized(updateStateResult.Message);

        return RedirectToAction(nameof(QuestionList), new { state = State.Deactivated });
    }
    [HttpPost]
    public async Task<IActionResult> Activate(Guid id)
    {
        var updateStateResult = await _questionService.UpdateStateAsync(id, State.Approved);
        if (updateStateResult.IsSuccess)
            NotifySuccessLocalized(updateStateResult.Message);
        else
            NotifyErrorLocalized(updateStateResult.Message);

        return RedirectToAction(nameof(QuestionList), new { state = State.Approved });
    }


    [HttpGet]
    public async Task<IActionResult> QuestionRevisionList(Guid id)
    {
        var questionRevisions = await _questionRevisionService.GetAllByQuestionId(id);

        if (questionRevisions.Any())
        {
            var questionRevisionsVM = _mapper.Map<IEnumerable<QuestionRevisionListVM>>(questionRevisions);
            ViewBag.QuestionRevisionVB = questionRevisionsVM;
            return View(questionRevisionsVM);
        }
        else
        {
            // Revize işlemi yoksa, bir bilgi mesajı döndür
            return Content("Revize işlemi yoktur");
        }
    }

    private async Task<SelectList> GetTrainersAsync(Guid? trainerID)
    {
        var trainers = await _trainerService.GetAllAsync();
        return new SelectList(trainers.Data.Select(x => new SelectListItem
        {
            Value = x.Id.ToString(),
            Text = x.FullName,
            Selected = x.Id.ToString() == trainerID.ToString(),
        }), "Value", "Text", trainerID.ToString());
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var questionResult = await _questionService.GetByIdAsync(id);

        if (!questionResult.IsSuccess)
            return NotFound();

        var questionUpdateVM = _mapper.Map<AdminQuestionUpdateVM>(questionResult.Data);
        if (await _questionRevisionService.AnyActive(id))
            questionUpdateVM.ReviewComment = (await _questionRevisionService.GetActiveByQuestionId(id)).RequestDescription;

        var product = (await _productService.GetAllBySubjectIdAsync(questionUpdateVM.SubjectId)).Data.ToList().FirstOrDefault();

        if (product != null)
        {
            questionUpdateVM.ProductId = product.Id;
        }
        else
        {
            NotifyError(questionResult.Message);

            return RedirectToAction("QuestionList");

        }


        questionUpdateVM.ProductList = await GetProductsAsync();
        questionUpdateVM.QuestionTypeList = GetQuestionTypes();
        questionUpdateVM.SubjectList = await GetSubjectsByProductId(questionUpdateVM.ProductId.ToString());
        questionUpdateVM.SubtopicList = await GetSubtopicsBySubjectId(questionUpdateVM.SubjectId.ToString());
        questionUpdateVM.QuestionDifficultyList = await GetQuestionDifficulties();
        questionUpdateVM.QuestionAnswersList = System.Text.Json.JsonSerializer.Serialize(questionUpdateVM.QuestionAnswers);

        var timeGiven = await GetQuestionTimeByDifficultyId(questionUpdateVM.QuestionDifficultyId.ToString());
        questionUpdateVM.TimeGiven = timeGiven;
        questionUpdateVM.Image = questionResult.Data.Image;
        ViewBag.QuestionDifficultiesUpdate = await GetQuestionDifficultiesUpdateAsync();
        return Json(questionUpdateVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminQuestionUpdateVM updateQuestionVM, IFormCollection collection)
    {

        List<TrainerQuestionAnswerCreateVM>? questionAnswersList = System.Text.Json.JsonSerializer.Deserialize<List<TrainerQuestionAnswerCreateVM>>(collection["questionAnswerChoices"]);

        if (ModelState.IsValid)
        {
            bool questionRevisionUpdateFailed = false;


            if (updateQuestionVM.NewImage is not null)
            {
                updateQuestionVM.Image = await updateQuestionVM.NewImage.FileToStringAsync();
            }
            else
            {
                var image = await _questionService.GetByIdAsync(updateQuestionVM.Id);
                updateQuestionVM.Image = image.Data.Image;
            }


            if (updateQuestionVM.State == State.Reviewed)
            {
                var questionRevision = await _questionRevisionService.GetActiveByQuestionId(updateQuestionVM.Id);

                questionRevision.RevisionConclusion = updateQuestionVM.ReviseComment;
                questionRevision.Status = Status.Modified;

                var questionRevisionResult = await _questionRevisionService.UpdateAsync(_mapper.Map<QuestionRevisionUpdateDto>(questionRevision));

                if (!questionRevisionResult.IsSuccess)
                {
                    questionRevisionUpdateFailed = true;
                    NotifyErrorLocalized(questionRevisionResult.Message);
                }
            }
            if (!questionRevisionUpdateFailed)
            {
                if (updateQuestionVM.State != State.Approved)
                {
                    updateQuestionVM.State = State.Awaited;
                }
                var question = _mapper.Map<QuestionUpdateDto>(updateQuestionVM);

                question.SubtopicId = updateQuestionVM.SubtopicId.Select(subtopicId => new QuestionSubtopicsUpdateDto { SubtopicId = subtopicId }).ToList();
                var questionResult = await _questionService.UpdateAsync(question);

                if (questionResult.IsSuccess)
                {

                    foreach (var questionAnswer in questionAnswersList)
                    {
                        questionAnswer.QuestionId = updateQuestionVM.Id;
                    }

                    var questionAnswersResult = await _questionAnswerService.UpdateRangeAsync(_mapper.Map<List<QuestionAnswerCreateDto>>(questionAnswersList));

                    if (questionAnswersResult.IsSuccess)
                    {

                        //TRIGGERA GEÇİLİRSE BU METOT SİLİNECEK
                        //await _studentQuestionService.ScoringOfTheExam(questionResult.Data.Id);
                        NotifySuccessLocalized(questionResult.Message);
                        if (updateQuestionVM.State != State.Approved)
                        {
                            return RedirectToAction("QuestionList", new { state = State.Awaited });
                        }
                        else
                        {
                            var currentAdmin = await _adminService.GetByIdentityIdAsync(UserIdentityId);
                            var questionArrange = _mapper.Map<QuestionArrangeCreateDto>(updateQuestionVM);
                            questionArrange.ArrangerAdminId = currentAdmin.Data.Id;
                            await _questionArrangeService.AddAsync(questionArrange);
                            return RedirectToAction("QuestionList", new { state = State.Approved });
                        }
                    }
                    if (questionResult.Data.State == State.Approved)
                        NotifyErrorLocalized(Messages.TheQuestionTypeOfTheApprovedQuestionCannotBeChanged);
                    else
                        NotifyErrorLocalized(Messages.QuestionTypeCannotBeChanged);
                }
                NotifySuccessLocalized(questionResult.Message);
            }

        }

        ViewBag.ProductList = await GetProductsAsync();
        ViewBag.QuestionTypes = GetQuestionTypes();
        ViewBag.SubjectId = updateQuestionVM.SubjectId;
        ViewBag.SubtopicId = updateQuestionVM.SubtopicId;
        ViewBag.QuestionAnswersList = System.Text.Json.JsonSerializer.Serialize(questionAnswersList);

        return RedirectToAction("QuestionList");
        //return RedirectToAction(nameof(QuestionList), new { state = updateQuestionVM.State });
    }

    [HttpGet]
    public async Task<IActionResult> UpdateReviewed(Guid id)
    {
        var questionResult = await _questionService.GetByIdAsync(id);

        if (!questionResult.IsSuccess)
            return NotFound();

        AdminQuestionUpdateVM questionUpdateVM = _mapper.Map<AdminQuestionUpdateVM>(questionResult.Data);

        if (await _questionRevisionService.AnyActive(id))
            questionUpdateVM.ReviewComment = (await _questionRevisionService.GetActiveByQuestionId(id)).RequestDescription;

        var productList = (await _productService.GetAllBySubjectIdAsync(questionUpdateVM.SubjectId)).Data.ToList();
        questionUpdateVM.ProductId = productList.FirstOrDefault().Id;

        questionUpdateVM.ProductList = await GetProductsAsync();
        questionUpdateVM.QuestionTypeList = GetQuestionTypes();
        questionUpdateVM.SubjectList = await GetSubjectsByProductId(questionUpdateVM.ProductId.ToString());
        questionUpdateVM.SubtopicList = await GetSubtopicsBySubjectId(questionUpdateVM.SubjectId.ToString());
        questionUpdateVM.QuestionDifficultyList = await GetQuestionDifficulties();
        questionUpdateVM.QuestionAnswersList = System.Text.Json.JsonSerializer.Serialize(questionUpdateVM.QuestionAnswers);
        var timeGiven = await GetQuestionTimeByDifficultyId(questionUpdateVM.QuestionDifficultyId.ToString());
        questionUpdateVM.TimeGiven = timeGiven;
        questionUpdateVM.Image = questionResult.Data.Image;
        ViewBag.QuestionDifficultiesUpdate = await GetQuestionDifficultiesUpdateAsync();


        return Json(questionUpdateVM);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateReviewed(AdminQuestionUpdateVM updateQuestionVM, IFormCollection collection)
    {

        List<TrainerQuestionAnswerCreateVM>? questionAnswersList = System.Text.Json.JsonSerializer.Deserialize<List<TrainerQuestionAnswerCreateVM>>(collection["questionAnswerChoicesReviewed"]);

        if (ModelState.IsValid)
        {

            if (questionAnswersList.Count >= 1)
            {
                bool questionRevisionUpdateFailed = false;


                if (updateQuestionVM.NewImage is not null)
                {
                    updateQuestionVM.Image = await updateQuestionVM.NewImage.FileToStringAsync();
                }


                if (updateQuestionVM.State == State.Reviewed)
                {
                    var questionRevision = await _questionRevisionService.GetActiveByQuestionId(updateQuestionVM.Id);

                    questionRevision.RevisionConclusion = updateQuestionVM.ReviseComment;
                    questionRevision.Status = Status.Modified;

                    var questionRevisionResult = await _questionRevisionService.UpdateAsync(_mapper.Map<QuestionRevisionUpdateDto>(questionRevision));

                    if (!questionRevisionResult.IsSuccess)
                    {
                        questionRevisionUpdateFailed = true;
                        NotifyErrorLocalized(questionRevisionResult.Message);
                    }
                }
                if (!questionRevisionUpdateFailed)
                {
                    updateQuestionVM.State = State.Awaited;
                    var question = _mapper.Map<QuestionUpdateDto>(updateQuestionVM);

                    question.SubtopicId = updateQuestionVM.SubtopicId.Select(subtopicId => new QuestionSubtopicsUpdateDto { SubtopicId = subtopicId }).ToList();


                    var questionResult = await _questionService.UpdateAsync(question);

                    if (questionResult.IsSuccess)
                    {
                        foreach (var questionAnswer in questionAnswersList)
                        {
                            questionAnswer.QuestionId = updateQuestionVM.Id;
                        }

                        var questionAnswersResult = await _questionAnswerService.UpdateRangeAsync(_mapper.Map<List<QuestionAnswerCreateDto>>(questionAnswersList));

                        if (questionAnswersResult.IsSuccess)
                        {
                            NotifySuccessLocalized(questionResult.Message);
                            return RedirectToAction("QuestionList", new { state = State.Awaited });
                        }
                        NotifySuccessLocalized(questionAnswersResult.Message);
                    }
                    NotifyErrorLocalized(questionResult.Message);
                }
            }
            else
                NotifyErrorLocalized(Messages.AddAtLeastOneAnswer);
        }

        foreach (var modelState in ModelState.Values)
        {
            foreach (var error in modelState.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }


        ViewBag.ProductList = await GetProductsAsync();
        ViewBag.QuestionTypes = GetQuestionTypes();
        ViewBag.SubjectId = updateQuestionVM.SubjectId;
        ViewBag.SubtopicId = updateQuestionVM.SubtopicId;
        ViewBag.QuestionAnswersList = System.Text.Json.JsonSerializer.Serialize(questionAnswersList);

        return RedirectToAction(nameof(QuestionList), new { state = updateQuestionVM.State });
    }

    [HttpGet]
    public async Task<IActionResult> QuestionArrangesList(string questionId)
    {
        var questionArranges = await _questionArrangeService.GetAllByQuestionIdAsync(Guid.Parse(questionId));
        var questionArrangesVM = _mapper.Map<List<QuestionArrangeListVM>>(questionArranges);
        foreach (var questionArrange in questionArrangesVM)
        {
            var admin = await _adminService.GetByIdAsync(questionArrange.ArrangerAdminId);
            questionArrange.AdminName = admin.Data.FirstName + " " + admin.Data.LastName;
        }

        return Json(questionArrangesVM);
    }

    [HttpGet]
    private async Task<List<SelectListItem>> GetProductsAsync()
    {
        var getProductResult = await _productService.GetAllAsync();

        return getProductResult.Data.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
    }


    private async Task<List<SelectListItem>> GetSubjects()
    {
        var subjectsResult = await _subjectService.GetAllAsync();

        var subjectList = subjectsResult.Data.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();

        return subjectList;
    }

    private async Task<List<SelectListItem>> GetSubtopic()
    {
        var subtopicResult = await _subtopicService.GetAllAsync();

        var subtopicList = subtopicResult.Data.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();

        return subtopicList;
    }

    private async Task<List<SelectListItem>> GetSubtopic(string subjectId)
    {
        var subtopicResult = await _subtopicService.GetSubtopicBySubjectId(Guid.Parse(subjectId));

        var subtopicList = subtopicResult.Data.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();

        return subtopicList;
    }

    private async Task<List<SelectListItem>> GetQuestionDifficulty()
    {
        var questionDifficultyResult = await _questionDifficultyService.GetAllAsync();

        var questionDifficultyList = questionDifficultyResult.Data.Select(x => new SelectListItem()
        {
            Value = x.Name,
            Text = x.Name
        }).ToList();

        return questionDifficultyList;
    }

    private async Task CompleteQuestionFilters(List<SelectListItem> subjectList = null, List<SelectListItem> subtopicList = null, List<SelectListItem> questionDifficultyList = null)
    {


        if (subjectList != null || subtopicList != null || questionDifficultyList != null)
        {
            _memoryCache.Set("subjectList", subjectList, TimeSpan.FromMinutes(10));
            _memoryCache.Set("SubtopicList", subtopicList, TimeSpan.FromMinutes(10));
            _memoryCache.Set("questionDifficultyList", questionDifficultyList, TimeSpan.FromMinutes(10));
        }
        else
        {

            ViewBag.SubjectList = await GetSubjects();
            ViewBag.SubtopicList = await GetSubtopic();
            ViewBag.QuestionDifficultyList = await GetQuestionDifficulty();

            _memoryCache.Set("subjectList", await GetSubjects(), TimeSpan.FromMinutes(10));
            _memoryCache.Set("SubtopicList", await GetSubtopic(), TimeSpan.FromMinutes(10));
            _memoryCache.Set("questionDifficultyList", await GetQuestionDifficulty(), TimeSpan.FromMinutes(10));

        }


    }

    [HttpGet]
    public async Task<List<SelectListItem>> GetSubjectsByProductId(string productId)
    {
        var subjectList = await _subjectService.GetAllByProductIdAsync(Guid.Parse(productId));
        return subjectList.Data.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
    }

    [HttpGet]
    public async Task<List<SelectListItem>> GetSubtopicsBySubjectId(string subjectId)
    {
        var subtopicListId = await _subtopicService.GetSubtopicBySubjectId(Guid.Parse(subjectId));
        return subtopicListId.Data.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();


    }

    [HttpGet]
    public List<SelectListItem> GetQuestionTypes()
    {
        return Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>().
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
    public async Task<List<SelectListItem>> GetQuestionDifficultiesUpdateAsync()
    {
        var getQuestionDifficultyResult = await _questionDifficultyService.GetAllAsync();

        return getQuestionDifficultyResult.Data.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Name
        }).ToList();
    }

    [HttpGet]
    public async Task<TimeSpan> GetQuestionTimeByDifficultyId(string difficultyId)
    {
        return (await _questionDifficultyService.GetDetailsByIdAsync(Guid.Parse(difficultyId))).Data.TimeGiven;
    }

    [HttpGet]
    public async Task<IActionResult> QuestionRevisionLists(Guid id)
    {
        var questionRevisions = await _questionRevisionService.GetAllByQuestionId(id);

        if (questionRevisions.Any())
        {
            var questionRevisionsVM = _mapper.Map<IEnumerable<TrainerQuestionRevisionListVM>>(questionRevisions);
            return View(questionRevisionsVM);
        }
        else
        {
            return Content("Revize işlemi yoktur");
        }
    }

    [HttpGet]
    public async Task<IActionResult> QuestionCreate()
    {
        ViewBag.ProductList = await GetProductsAsync();
        ViewBag.QuestionTypes = GetQuestionTypes();
        ViewBag.SubjectList = await GetSubjects();
        ViewBag.SubtopicList = new List<SelectListItem>();

        var adminQuestionCreateVM = new AdminQuestionCreateVM();

        return View(adminQuestionCreateVM);
    }

    [HttpPost]
    public async Task<IActionResult> QuestionCreate(AdminQuestionCreateVM adminQuestionCreateVM, IFormCollection collection)
    {
        if (ModelState.IsValid)
        {
            List<QuestionAnswerCreateDto> questionAnswersList = JsonSerializer.Deserialize<List<QuestionAnswerCreateDto>>(collection["questionAnswerChoices"]);

            var questionCreateDto = _mapper.Map<QuestionCreateDto>(adminQuestionCreateVM);

            questionCreateDto.QuestionAnswers = questionAnswersList;
            questionCreateDto.SubtopicId = adminQuestionCreateVM.SubtopicId
                .Select(id => new QuestionSubtopicsCreateDto { SubtopicId = id }).ToList();

            var createResult = await _questionService.AddAsync(questionCreateDto);

            if (createResult.IsSuccess)
            {
                NotifySuccessLocalized(createResult.Message);
                var createdQuestionId = createResult.Data.Id;
                return RedirectToAction(nameof(QuestionDetails), new { id = createdQuestionId });
            }
            else
            {
                NotifyErrorLocalized(createResult.Message);
            }
        }

        ViewBag.ProductList = await GetProductsAsync();
        ViewBag.QuestionTypes = GetQuestionTypes();
        ViewBag.SubjectList = await GetSubjects();
        ViewBag.SubtopicList = await GetSubtopics(adminQuestionCreateVM.SubjectId);

        return View(adminQuestionCreateVM);
    }

    [HttpGet]
    public async Task<IActionResult> GetSubtopics(Guid subjectId)
    {
        var subtopicsResult = await _subtopicService.GetSubtopicBySubjectId(subjectId);

        if (!subtopicsResult.IsSuccess) // or .Succeeded, or any other relevant property
        {
            return Json(new List<SelectListItem>());
        }

        var subtopicList = subtopicsResult.Data.Select(subtopic => new SelectListItem
        {
            Value = subtopic.Id.ToString(),
            Text = subtopic.Name
        }).ToList();

        return Json(subtopicList);
    }



}
