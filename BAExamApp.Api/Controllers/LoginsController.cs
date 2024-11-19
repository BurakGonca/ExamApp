using BAExamApp.Business.ApiServices.Interfaces;
using BAExamApp.Core.Utilities.Results.Concrete;
using BAExamApp.Dtos.ApiDtos.LoginDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BAExamApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginsController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IRegisterCodeApiService _registerCodeService;

    public LoginsController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IRegisterCodeApiService registerCodeService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _registerCodeService = registerCodeService;
    }

    [HttpGet("Login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return NotFound("Kullanıcı bulunamadı.");

        var role = (await _userManager.GetRolesAsync(user))[0];
        if (role != "ApiUser") return Unauthorized("Giriş izniniz yok.");

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (!result.Succeeded) return BadRequest("Email veya şifre hatalı.");
        else
        {
            var value = await _registerCodeService.GenerateCodeOnLoginAsync(user.Id);
            if (value is null) return BadRequest("Kod üretilemedi.");

            return Ok(new LoginResultWithRegisterCodeDto { Code = value.Data, Message = value.Message });
        }
    }
}