using AutoMapper;
using BAExamApp.Dtos.ApiUsers;
using BAExamApp.MVC.Areas.Admin.Models.ApiUserVMs;
using X.PagedList;

namespace BAExamApp.MVC.Areas.Admin.Controllers
{
    public class ApiUserController : AdminBaseController
    {
        private readonly IApiUserService _apiUserService;
        private readonly IMapper _mapper;

        public ApiUserController(IApiUserService apiUserService, IMapper mapper)
        {
            _apiUserService = apiUserService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string typeOfApiUser, int? page, int pageSize = 10)
        {
            int pageNumber = page ?? 1;
            var apiUserResult = await _apiUserService.GetAllAsync();
            var apiUserList = _mapper.Map<List<AdminApiUserListVM>>(apiUserResult.Data).OrderBy(x => x.FirstName).ToList();

            if (!string.IsNullOrEmpty(typeOfApiUser))
                apiUserList = await Search(typeOfApiUser);

            var pagedList = apiUserList.ToPagedList(pageNumber, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.typeOfApiUser = typeOfApiUser;

            return View(pagedList);

        }



        public async Task<List<AdminApiUserListVM>> Search(string typeOfApiUser)
        {
            var apiUserResult = await _apiUserService.GetAllAsync();
            var apiUserList = _mapper.Map<List<AdminApiUserListVM>>(apiUserResult.Data);

            var searchList = apiUserList
                .Where(s => s.FirstName.IndexOf(typeOfApiUser, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(o => o.FirstName)
                .ToList();

            return searchList;
        }





        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminApiUserCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var createApiUserDto = _mapper.Map<CreateApiUserDto>(model);
            var result = await _apiUserService.AddAsync(createApiUserDto);

            if (!result.IsSuccess)
            {
                NotifyErrorLocalized(result.Message);
                return View(model);
            }

            NotifySuccess($"{model.FirstName} {model.LastName} başarıyla eklendi.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _apiUserService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                NotifyErrorLocalized(result.Message);
                return RedirectToAction(nameof(Index));
            }

            var model = _mapper.Map<AdminApiUserUpdateVM>(result.Data);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AdminApiUserUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updateApiUserDto = _mapper.Map<UpdateApiUserDto>(model);


            var result = await _apiUserService.UpdateAsync(updateApiUserDto);

            if (!result.IsSuccess)
            {
                NotifyErrorLocalized(result.Message);
            }
            else
            {
                NotifySuccessLocalized(result.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiUserDeleteResponse = await _apiUserService.DeleteAsync(id);

            if (apiUserDeleteResponse.IsSuccess)
                NotifySuccess(apiUserDeleteResponse.Message);
            else
                NotifyError(apiUserDeleteResponse.Message);
            return Json(apiUserDeleteResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _apiUserService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                var apiUsersDetailsVM = _mapper.Map<AdminApiUserDetailsVM>(result.Data);
                return View(apiUsersDetailsVM);
            }

            NotifyError(result.Message);
            return RedirectToAction(nameof(Index));
        }
        public async Task<AdminApiUserUpdateVM> GetApiUser(Guid apiUserId)
        {
            var getApiUserResult = await _apiUserService.GetByIdAsync(apiUserId);
            var apiUserDto = getApiUserResult.Data;
            var apiUserUpdateVM = _mapper.Map<AdminApiUserUpdateVM>(apiUserDto);
            return apiUserUpdateVM;
        }
    }
}