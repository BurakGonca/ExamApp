using AutoMapper;
using BAExamApp.MVC.Areas.Student.Models.ClassroomVMs;
using BAExamApp.MVC.Areas.Student.Models.StudentClassroomVMs;

namespace BAExamApp.MVC.Areas.Student.Controllers;

public class ClassroomController : StudentBaseController
{
    private readonly IMapper _mapper;
    private readonly IStudentService _studentService;
    private readonly IStudentClassroomService _studentClassroomService;
    private readonly IClassroomService _classroomService;

    public ClassroomController(IMapper mapper, IStudentClassroomService studentClassroomService, IStudentService studentService, IClassroomService classroomService)
    {
        _mapper = mapper;
        _studentClassroomService = studentClassroomService;
        _studentService = studentService;
        _classroomService = classroomService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var studentResult = await _studentService.GetByIdentityIdAsync(UserIdentityId);

        if (studentResult.IsSuccess)
        {
            var studentClassroomResult = await _studentClassroomService.GetAllByStudentIdForStudentAsync(studentResult.Data.Id);
            if (studentClassroomResult.IsSuccess)
            {
                return View(_mapper.Map<IEnumerable<StudentStudentClassroomListVM>>(studentClassroomResult.Data));
            }
            NotifyErrorLocalized(studentClassroomResult.Message);
        }
        NotifyErrorLocalized(studentResult.Message);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var classroomResponse = await _classroomService.GetDetailsByIdAsync(id);
        if (classroomResponse.IsSuccess)
        {
            return View(_mapper.Map<StudentClassroomDetailsVM>(classroomResponse.Data));
        }

        NotifyErrorLocalized(classroomResponse.Message);
        return RedirectToAction(nameof(Index));
    }
}