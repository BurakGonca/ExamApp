using BAExamApp.Business.ApiServices.Interfaces;
using BAExamApp.Business.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAExamApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExamsController : ControllerBase
{
    private readonly IExamApiService _examApiService;
    private readonly IRegisterCodeApiService _registerCodeApiService;

    public ExamsController(IExamApiService examApiService, IRegisterCodeApiService registerCodeApiService)
    {
        _examApiService = examApiService;
        _registerCodeApiService = registerCodeApiService;
    }

    [HttpGet("GetAllData")]
    public async Task<IActionResult> GetAllData(string registerCode)
    {
        if (await _registerCodeApiService.IsRegisterCodeActiveAsync(registerCode))
        {

            var values = await _examApiService.GetAllDataWithRegisterCodeAsync();
            return Ok(new { Values = values.Data, Message = values.Message });
        }
        else return Unauthorized("Kullanma izniniz yok");
    }
}
