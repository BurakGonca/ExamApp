using BAExamApp.Core.Utilities.Results.Concrete;
using BAExamApp.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
public class HomeController : CandidateAdminBaseController
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}
