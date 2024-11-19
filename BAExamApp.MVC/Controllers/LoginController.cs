using BAExamApp.MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace BAExamApp.MVC.Controllers;

public class LoginController : BaseController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ISendMailService _sendMailService;
    private readonly IStudentService _studentService;

    public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ISendMailService sendMailService, IStudentService studentService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _sendMailService = sendMailService;
        _studentService = studentService;
    }


    public async Task<IActionResult> Verify(LoginVM model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
        {
            NotifyError("Email veya şifre hatalı");
            return Json(new { success = false });
        }

        var checkPass = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (!checkPass.Succeeded)
        {
            NotifyError("Email veya şifre hatalı");
            return Json(new { success = false });
        }

        int verificationCode = await _sendMailService.SendEmailVerificationCode(model.Email);
        TempData["VerificationCode"] = verificationCode;
        return Json(new { success = true });
    }

    public async Task<IActionResult> Index()
    {
        //if (TempData["Login"] != null)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var userRole = await _userManager.GetRolesAsync(user);
        //    Json(new { success = true });
        //    return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });
        //}
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Index(LoginVM model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
        {
            NotifyError("Email veya şifre hatalı");
            return View(model);
        }

        var checkPass = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (!checkPass.Succeeded)
        {
            NotifyError("Email veya şifre hatalı");
            return View(model);
        }

        if (User.IsInRole("Student"))
        {
            bool isGraduated = await _studentService.IsGraduatedAsync(user?.Id);

            if (isGraduated)
            {
                NotifyError("Mezun olduğunuz için artık sisteme giriş yapamazsınız.Tebrikler!");
                return View(model);
            }
        }
        var userRole = await _userManager.GetRolesAsync(user);
        if (userRole is null)
        {
            NotifyError("Kullanıcıya ait rol bulunamadı");
            return View(model);
        }
        TempData["Login"] = "ok";
        Json(new { success = true });
        return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });

        /* to do: iki adımlı doğrulama yorum satırına alındı. proje canlıya alındığı zaman geri açılacak
        var user = await _usermanager.findbyemailasync(model.email);

        if (user.ısınrole("student"))
        {
            bool isgraduated = await _studentservice.ısgraduatedasync(user?.ıd);
           
            if (isgraduated)
            {
                notifyerror("mezun olduğunuz için artık sisteme giriş yapamazsınız.tebrikler!");
                return view(model);
            }
        }

        if (!modelstate.ısvalid)
            return view(model);

        int savedverificationcode;
        savedverificationcode = (int)tempdata["verificationcode"];
        tempdata.keep("verificationcode");

        if (savedverificationcode == model.verificationcode)
        {
            var userrole = await _usermanager.getrolesasync(user);
            if (userrole is null)
            {
                notifyerror("kullanıcıya ait rol bulunamadı");
                return view(model);
            }
            tempdata["login"] = "ok";
            return json(new { success = true });
        }
        if (savedverificationcode != model.verificationcode)
        {
            notifyerror("geçersiz kod girişi yaptınız.");
        }
        return view(model);
        */

    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> AccessDenied(AccessDeniedVM? accessDeniedVM)
    {
        var user = await _userManager.GetUserAsync(User);
        var userRole = await _userManager.GetRolesAsync(user);

        if (accessDeniedVM != null)
        {
            if (accessDeniedVM.AreaName == null)
            {
                accessDeniedVM.AreaName = userRole[0].ToString();
            }
            return View(accessDeniedVM);
        }
        else
        {
            accessDeniedVM = new AccessDeniedVM();
            accessDeniedVM.AreaName = userRole[0].ToString();
            return View(accessDeniedVM);
        }

    }
}