using BAExamApp.MVC.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace BAExamApp.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminBaseController : BaseController
{
}
