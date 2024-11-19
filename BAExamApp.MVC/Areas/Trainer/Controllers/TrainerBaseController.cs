using BAExamApp.MVC.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace BAExamApp.MVC.Areas.Trainer.Controllers;

[Area("Trainer")]
[Authorize(Roles = "Trainer,Admin")]
public class TrainerBaseController : BaseController
{
}
