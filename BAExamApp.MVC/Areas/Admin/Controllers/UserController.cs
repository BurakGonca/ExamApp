using AutoMapper;
using BAExamApp.Dtos.Trainers;
using BAExamApp.Dtos.Users;
using BAExamApp.MVC.Areas.Admin.Models.TrainerVMs;
using BAExamApp.MVC.Areas.Admin.Models.UsersVMs;
using Microsoft.AspNetCore.Identity;

namespace BAExamApp.MVC.Areas.Admin.Controllers;
public class UserController : AdminBaseController
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly ITrainerService _trainerService;
    private readonly IAccountService _accountService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IMapper _mapper;

    public UserController(IMapper mapper, IUserService userService, IRoleService roleService, ITrainerService trainerService, IAccountService accountService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _mapper = mapper;
        _userService = userService;
        _roleService = roleService;
        _trainerService = trainerService;
        _accountService = accountService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? Role = "Trainer")
    {
        List<TrainerListDto> trainers = new();
        var users = await _userManager.GetUsersInRoleAsync(Role);
        foreach (var user in users)
        {
            var trainer = await _trainerService.GetByIdentityIdAsync(user.Id);
            if (trainer.IsSuccess)
           {
                var trainerMap = _mapper.Map<TrainerListDto>(trainer.Data);
               trainers.Add(trainerMap);
           }

        }
        var sortedTrainers = trainers.OrderBy(t => t.FirstName);
        var trainerList = _mapper.Map<IEnumerable<AdminTrainerListVM>>(sortedTrainers);
        TempData["Role"] = Role;
        //var trainerList = _mapper.Map<List<AdminTrainerListVM>>(trainers);
        return View(trainerList);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserRoles(List<UserListVM> model)
    {
        var result = await _roleService.UpdateUserRole(_mapper.Map<List<UserRoleAssingDto>>(model), "1");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserRole(AdminUserRoleUpdateVM adminUserRoleUpdateVM)
    {
        var result = await _roleService.ChangeUserRole(_mapper.Map<UserRoleUpdateDto>(adminUserRoleUpdateVM));
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> LoginAsTrainer(Guid trainerId)
    {
        var selectedTrainer = await _trainerService.GetByIdAsync(trainerId);
        var infoOfTrainer = await _userManager.FindByIdAsync(selectedTrainer.Data.IdentityId);
        var adminId = (await _userManager.FindByNameAsync(User.Identity!.Name!))!.Id;
        HttpContext.Session.SetString("changeSession", "true");
        HttpContext.Session.SetString("adminId", adminId);
        await _signInManager.SignInAsync(infoOfTrainer!, isPersistent: false);
        return RedirectToAction("Index", "Home", new { area = "Trainer" });

    }
}
