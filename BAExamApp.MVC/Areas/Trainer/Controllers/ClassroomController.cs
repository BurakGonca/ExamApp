using AutoMapper;
using BAExamApp.MVC.Areas.Trainer.Models.ClassroomVMs;

namespace BAExamApp.MVC.Areas.Trainer.Controllers;

public class ClassroomController : TrainerBaseController
{
    private readonly ITrainerService _trainerService;
    private readonly IClassroomService _classroomService;
    private readonly IMapper _mapper;
    private readonly IStudentExamService _studentExamService;
    private readonly IExamService _examService;

    public ClassroomController(ITrainerService trainerService, IClassroomService classroomService, IMapper mapper, IExamService examService, IExamAnalysisService examAnalysisService, IStudentExamService studentExamService)
    {
        _trainerService = trainerService;
        _classroomService = classroomService;
        _mapper = mapper;
        _studentExamService = studentExamService;
        _examService = examService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var classroomResult = await _trainerService.GetClassroomsByIdentityId(UserIdentityId);

        var classrooms = _mapper.Map<IEnumerable<TrainerClassroomListVM>>(classroomResult.Data);

        return View(classrooms);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var classroomDetailResult = await _classroomService.GetDetailsByIdAsync(id);

        if (!classroomDetailResult.IsSuccess)
            return NotFound();

        var classroomDetails = _mapper.Map<TrainerClassroomDetailsVM>(classroomDetailResult.Data);

        return View(classroomDetails);
    }

    public async Task<IActionResult> ExamAnalysis(Guid examId, string classroomName)
    {
        var user = (await _trainerService.GetByIdentityIdAsync(UserIdentityId)).Data;

        var trainerId = user.Id;
         
        var examAnalysis = await _studentExamService.AnalysisExamPerformanceByTrainerAsync(trainerId, examId);

        var model = new TrainerClassroomExamAnalysisVM
        {
            SubtopicPerformances = examAnalysis
        };

        var exam = await _examService.GetByIdAsync(examId);
        ViewBag.ExamName = exam.Data.Name;

        ViewBag.ClassroomName = classroomName;


        return View(model);
    }
}