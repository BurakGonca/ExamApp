using BAExamApp.MVC.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace BAExamApp.MVC.Areas.CandidateAdmin.Controllers;
[Area("CandidateAdmin")]
[Authorize(Roles = "CandidateAdmin")]
public class CandidateAdminBaseController : BaseController
{ }



