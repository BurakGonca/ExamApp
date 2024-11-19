using AutoMapper;
using BAExamApp.MVC.Areas.Trainer.Models.ExamVMs;
using BAExamApp.MVC.Areas.Trainer.Models.StudentExamVMs;
using BAExamApp.MVC.Areas.Trainer.Models.StudentVMs;

namespace BAExamApp.MVC.Areas.Trainer.Controllers;
public class StudentController : TrainerBaseController
{
    private readonly IStudentService _studentService;
    private readonly IExamService _examService;
    private readonly IStudentExamService _studentExamService;
    private readonly ITrainerClassroomService _trainerClassroomService;
    private readonly IStudentClassroomService _studentClassroomService;
    private readonly IClassroomService _classroomService;
    private readonly IMapper _mapper;

    public StudentController(IStudentService studentService, IExamService examService, IStudentExamService examStudentService, ITrainerClassroomService trainerClassroomService, IStudentClassroomService studentClassroomService,IClassroomService classroomService ,IMapper mapper)
    {
        _studentService = studentService;
        _examService = examService;
        _studentExamService = examStudentService;
        _trainerClassroomService = trainerClassroomService;
        _studentClassroomService = studentClassroomService;
        _classroomService = classroomService;
        _mapper = mapper;
    }

    public async Task<IActionResult> StudentExamScoreList(Guid studentId)
    {        
        var studentExamsResult = await _studentExamService.GetAllExamsWithDetailsByIdForTrainerAsync(studentId);
        var studentExamList = _mapper.Map<IEnumerable<StudentExamsForTrainerVM>>(studentExamsResult.Data);
        return View(studentExamList);
    }


    /// <summary>
    /// The method aims to list the attendance and excuse information for the exams that the student could not attend.
    /// </summary>
    /// <param name="studentId">The unique identifier of the student.</param>
    /// <returns>An asynchronous task that represents the result of the operation. The result is of type IActionResult.</returns>  bunun türkçesini açıkla
    public async Task<IActionResult> StudentExamStatusList(Guid studentId)
    {
        var studentExamsResult = await _studentExamService.GetAllByStudentIdAsync(studentId);
        var studentExams = studentExamsResult.Data;
        var unfinishedExams = studentExams.Where(x => x.IsFinished == false).ToList();
        var studentExamList = _mapper.Map<IEnumerable<StudentExamStatusForTrainerVM>>(unfinishedExams);
        return View(studentExamList);
    }


    //public IActionResult GetAllStudentsWhoChangeClasses()
    //{
    //    var studentsWhoChangedClasses = _studentService.GetAllStudentsWhoChangeClasses();
    //    return View(studentsWhoChangedClasses);
    //}

    //public async Task<IActionResult> GetOldExamsOfStudentsWhoChangedClasses(Guid id)
    //{
    //    Expression<Func<StudentExam, object>>[] expressions = { examStudent => examStudent.Exam, examStudent => examStudent.Student };
    //    var examStudent = _examStudentService.GetExamStudentWithParameters(expressions).Where(x => x.Student.Id.Equals(id));
    //    var test1 = examStudent.FirstOrDefault().StudentId;
    //    var test2 = examStudent.FirstOrDefault().ExamId;

    //    return View(examStudent);
    //}
    //[HttpPost]
    //public async Task<IActionResult> GetOldExamsOfStudentsWhoChangedClasses(Guid examId, Guid studentId)
    //{
    //    Expression<Func<StudentExam, object>>[] expressions = { examStudent => examStudent.Exam, examStudent => examStudent.Student };
    //    var examStudent = _examStudentService.GetExamStudentWithParameters(expressions).Where(x => x.Student.Id.Equals(studentId) && x.Exam.Id.Equals(examId)).FirstOrDefault();
    //    return RedirectToAction("MakeExamUntaken",examStudent);
    //}
    //public async Task<IActionResult> MakeExamUntaken(StudentExam examStudent)
    //{
    //    return View(examStudent);
    //}
}
