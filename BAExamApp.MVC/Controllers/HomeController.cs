namespace BAExamApp.MVC.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Login");
    }
}