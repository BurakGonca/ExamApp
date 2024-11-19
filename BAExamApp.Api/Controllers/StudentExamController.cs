using BAExamApp.Business.ApiServices.Concrete;
using BAExamApp.Business.ApiServices.Interfaces;
using BAExamApp.Business.Interfaces.Services;
using BAExamApp.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAExamApp.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class StudentExamController : ControllerBase
{
    private readonly IStudentExamApiService _studentExamApiService;
    private readonly IRegisterCodeApiService _registerCodeService;

    public StudentExamController(IStudentExamApiService studentExamApiService, IRegisterCodeApiService registerCodeService)
    {
        _studentExamApiService = studentExamApiService;
        _registerCodeService = registerCodeService;
    }

    [HttpGet("{classroomId}/{registerCode}")]
    public async Task<IActionResult> GetStudentExamScore(Guid classroomId, string registerCode)
    {
        var isCodeActive = await _registerCodeService.IsRegisterCodeActiveAsync(registerCode);
        if (!isCodeActive)
        {
            return Unauthorized("Geçersiz veya süresi dolmuş registerCode");
        }
        var result = await _studentExamApiService.GetStudentExamsByClassroomIdAsync(classroomId);
        if (result == null || !result.Data.Any())
        {
            return NotFound("Bu ClassroomId'ye ait sınav sonuçları bulunamadı.");
        }
        return Ok(result.Data);
    }
}


