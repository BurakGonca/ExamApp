using BAExamApp.MVC.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace BAExamApp.MVC.Areas.Student.Controllers;

[Area("Student")]
[Authorize(Roles = "Student")]
public class StudentBaseController : BaseController
{
}
