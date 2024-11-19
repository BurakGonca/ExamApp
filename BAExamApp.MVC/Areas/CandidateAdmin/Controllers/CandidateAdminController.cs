using AutoMapper;
using BAExamApp.Dtos.Candidate.CandidateAdmins;
using BAExamApp.Dtos.Emails;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateAdminVMs;
using BAExamApp.MVC.Extensions;
namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class CandidateAdminController : CandidateAdminBaseController
{
    private readonly ICandidateAdminService _candidateAdminService;
    private readonly ISendMailService _sendMailService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    public CandidateAdminController(ICandidateAdminService candidateAdminService, IMapper mapper, ISendMailService sendMailService, IEmailService emailService)
    {
        _candidateAdminService = candidateAdminService;
        _mapper = mapper;
        _sendMailService = sendMailService;
        _emailService = emailService;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _candidateAdminService.GetAllAsync();
        var candidateAdminList = _mapper.Map<IEnumerable<CandidateAdminListVM>>(result.Data);
        NotifySuccessLocalized(result.Message);
        return View(candidateAdminList);

    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View(new CandidateAdminCreateVM());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CandidateAdminCreateVM model, IFormCollection collection)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                var errorMessage = error.ErrorMessage;
            }
            return RedirectToAction(nameof(Index));
        }

        var candidateAdminDto = _mapper.Map<CandidateAdminCreateDto>(model);

        if (model.NewImage != null)
        {
            candidateAdminDto.Image = await model.NewImage.FileToStringAsync();
        }

        candidateAdminDto.FirstName = model.FirstName.TitleFormat();
        candidateAdminDto.LastName = model.LastName.TitleFormat();

        var addAdminResult = await _candidateAdminService.AddAsync(candidateAdminDto);
        if (!addAdminResult.IsSuccess)
        {
            NotifyErrorLocalized(addAdminResult.Message);
            return RedirectToAction(nameof(Index));
        }

        var adminOtherEmailList = new List<EmailCreateDto>();
        var otherEmailsList = collection["otherEmails"].ToList();
        var identityId = addAdminResult.Data.IdentityId;

        foreach (var candidateAdminOtherEmail in otherEmailsList)
        {
            adminOtherEmailList.Add(new EmailCreateDto() { EmailAddress = candidateAdminOtherEmail, IdentityId = identityId });
        }

        var addEmailResult = await _emailService.AddRangeAsync(adminOtherEmailList);
        if (!addEmailResult.IsSuccess)
        {
            NotifyErrorLocalized(addEmailResult.Message);
            return RedirectToAction(nameof(Index));
        }
        NotifySuccessLocalized(addEmailResult.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var candidateAdminDeleteResponse = await _candidateAdminService.DeleteAsync(id);

        if (candidateAdminDeleteResponse.IsSuccess)
            NotifySuccessLocalized(candidateAdminDeleteResponse.Message);
        else
            NotifyErrorLocalized(candidateAdminDeleteResponse.Message);

        return RedirectToAction(nameof(Index));


    }

    public async Task<IActionResult> Details(Guid Id)
    {

        var getCandidateAdmin = await _candidateAdminService.GetDetailsByIdAsync(Id);
        if (getCandidateAdmin.IsSuccess)
        {
            var candidateAdminDetailsVM = _mapper.Map<CandidateAdminDetailsVM>(getCandidateAdmin.Data);
            candidateAdminDetailsVM.OtherEmails = (await _emailService.GetEmailAddressesByIdentityIdAsync(getCandidateAdmin.Data.IdentityId)).Data;
            return View(candidateAdminDetailsVM);
        }

        NotifyError(getCandidateAdmin.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var getCandidateAdminResult = await _candidateAdminService.GetByIdAsync(id);
        if (!getCandidateAdminResult.IsSuccess)
        {
            NotifyErrorLocalized(getCandidateAdminResult.Message);
            return RedirectToAction(nameof(Index));
        }

        var candidateAdminDto = getCandidateAdminResult.Data;
        var candidateAdminUpdateVM = _mapper.Map<CandidateAdminUpdateVM>(candidateAdminDto);
       
        
        return View(candidateAdminUpdateVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CandidateAdminUpdateVM model, IFormCollection collection)
    {

        if (!ModelState.IsValid)
        {
           return View(model);
        }
        var updateCandidateAdmin= _mapper.Map<CandidateAdminUpdateDto>(model);
        if (model.NewImage != null)
        {
            updateCandidateAdmin.Image = await model.NewImage.FileToStringAsync();
        }
        updateCandidateAdmin.FirstName = StringExtensions.TitleFormat(model.FirstName);
        updateCandidateAdmin.LastName = StringExtensions.TitleFormat(model.LastName);

        var updateCandidateAdminResult = await _candidateAdminService.UpdateAsync(updateCandidateAdmin);
        if (!updateCandidateAdminResult.IsSuccess)
        {
            NotifyErrorLocalized(updateCandidateAdminResult.Message);
            return View(model);
        }

        var otherEmailsList = collection["otherEmails"].ToList();
        var candidateAdminOtherEmailList = new List<EmailCreateDto>();
        var identityId = updateCandidateAdminResult.Data.IdentityId;

        foreach (var candidateAdminOtherEmail in otherEmailsList)
        {
            candidateAdminOtherEmailList.Add(new EmailCreateDto() { EmailAddress = candidateAdminOtherEmail, IdentityId = identityId });
        }
        var addEmailResult = await _emailService.UpdateRangeAsync(candidateAdminOtherEmailList, identityId);

        NotifySuccessLocalized(updateCandidateAdminResult.Message);
        return RedirectToAction(nameof(Index));
    }

    public async Task<CandidateAdminUpdateVM> GetCandidateAdmin(Guid candidateAdminId)
    {
        var getCandidateAdminResult = await _candidateAdminService.GetByIdAsync(candidateAdminId);
        var candidateAdminDto = getCandidateAdminResult.Data;
        var candidateAdminUpdateVM = _mapper.Map<CandidateAdminUpdateVM>(candidateAdminDto);
        
        
        return candidateAdminUpdateVM;
    }



}
