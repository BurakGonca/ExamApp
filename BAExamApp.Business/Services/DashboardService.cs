using BAExamApp.Business.Interfaces.Services;
using BAExamApp.Core.Enums;
using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.Dtos.Dashboard;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.StudentClassrooms;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services;
public class DashboardService : IDashboardService
{
    private readonly IQuestionService _questionService;
    private readonly IStudentRepository _studentRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly IExamRepository _examRepository;
    private readonly IExamEvaluatorRepository _examEvaluatorRepository;
    private readonly IStudentExamRepository _studentExamRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly ITrainerService _trainerService;
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public DashboardService(IQuestionService questionService, IStudentRepository studentRepository, ITrainerRepository trainerRepository, IExamRepository examRepository, IStudentExamRepository studentExamRepository, IClassroomRepository classroomRepository, ITrainerService trainerService, IMapper mapper, IQuestionRepository questionRepository)
    {
        _questionService = questionService;
        _studentRepository = studentRepository;
        _trainerRepository = trainerRepository;
        _examRepository = examRepository;
        _studentExamRepository = studentExamRepository;
        _trainerService = trainerService;
        _classroomRepository = classroomRepository;

        _mapper = mapper;
        _questionRepository = questionRepository;
    }
    /// <summary>
    /// GetAllByAwaitedQuestionAsync is an asynchronous method that retrieves a list of questions in the 'Awaited' state.
    /// </summary>
    /// <returns>The method returns a list of QuestionListDto objects wrapped in a IDataResult object.</returns>
    public async Task<IDataResult<List<QuestionListDto>>> GetAllByAwaitedQuestionAsync()
    {
        return await _questionService.GetAllByStateAsync(Entities.Enums.State.Awaited);
    }
    /// <summary>
    /// GetAllByAprrovedQuestionAsync is an asynchronous method that retrieves a list of questions in the 'Aprroved' state.
    /// </summary>
    /// <returns>The method returns a list of QuestionListDto objects wrapped in a IDataResult object.</returns>
    public async Task<IDataResult<List<QuestionListDto>>> GetAllByAprrovedQuestionAsync()
    {
        return await _questionService.GetAllByStateAsync(Entities.Enums.State.Approved);
    }
    /// <summary>
    /// GetAllByReviewedQuestionAsync is an asynchronous method that retrieves a list of questions in the 'Reviewed' state.
    /// </summary>
    /// <returns>The method returns a list of QuestionListDto objects wrapped in a IDataResult object.</returns>
    public async Task<IDataResult<List<QuestionListDto>>> GetAllByReviewedQuestionAsync()
    {
        return await _questionService.GetAllByStateAsync(Entities.Enums.State.Reviewed);
    }
    /// <summary>
    /// GetDashboardOverview is an asynchronous method that retrieves an overview of the dashboard.
    /// It counts the number of students, trainers, and exams from their respective repositories and assigns these counts to the DashboardOverviewDTO object.
    /// It fetches all student exams from the _studentExamRepository, calculates the average score, and assigns this average to the SuccessRate property of the DashboardOverviewDTO object.
    /// </summary>
    /// <returns>The method returns a SuccessDataResult object containing the DashboardOverviewDTO object and a success message.</returns>
    public async Task<IDataResult<DasboardOverviewDTO>> GetDashboardOverview()
    {
        var dashboardOverview = new DasboardOverviewDTO();

        dashboardOverview.StudentCount = _studentRepository.Count();
        dashboardOverview.TrainerCount = _trainerRepository.Count();
        dashboardOverview.ExamCount = _examRepository.Count();

        var studentExams = await _studentExamRepository.GetAllAsync();
        var scores = studentExams.Where(x => x.Score != null && x.Exam.IsTest == false && x.Exam.IsCanceled == false).Select(x => x.Score.Value);

        dashboardOverview.SuccessRate = Math.Round(scores.Count() > 0 ? scores.Average() : 0, 2);

        return new SuccessDataResult<DasboardOverviewDTO>(dashboardOverview, Messages.FoundSuccess);
    }
    /// <summary>
    /// GetEventsAsync is an asynchronous method that retrieves a list of DashboardEventDto objects.
    /// If a year and month are provided, it fetches the events for that specific month of the year.
    /// If no year and month are provided, it fetches the events for the current month of the current year.
    /// </summary>
    /// <returns>The method returns a SuccessDataResult object containing the list of DashboardEventDto objects and a success message.</returns>
    public async Task<IDataResult<List<DashboardEventDto>>> GetEventsAsync(int? year, int? month)
    {
        if (year.HasValue && month.HasValue)
        {
            var events = await _examRepository.GetAllAsync(x => x.ExamDateTime.Month == month && x.ExamDateTime.Year == year && x.Status != Status.Deleted);

            return new SuccessDataResult<List<DashboardEventDto>>(_mapper.Map<List<DashboardEventDto>>(events), Messages.FoundSuccess);
        }
        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;

        var exams = await _examRepository.GetAllAsync(x => x.ExamDateTime.Month == currentMonth && x.ExamDateTime.Year == currentYear && x.Status != Status.Deleted);
        return new SuccessDataResult<List<DashboardEventDto>>(_mapper.Map<List<DashboardEventDto>>(exams), Messages.FoundSuccess);
    }


    /// <summary>
    /// GetEventsAsync is an asynchronous method used to retrieve a list of DashboardEventDto objects based on TrainerId.
    /// If a year and month are provided, it fetches the events for that specific month of the year.
    /// If no year and month are provided, it fetches the events for the current month of the current year.
    /// </summary>
    /// <returns>The method returns a SuccessDataResult object containing the list of DashboardEventDto objects and a success message.</returns>
    public async Task<IDataResult<List<DashboardEventDto>>> GetEventsWithSpesificTrainerIdAsync(int? year, int? month, Guid trainerId)
    {
        // Exam - Trainer ilişkili ara tablonun verileri yoktur. Bundan dolayı trainerId classrom üzerinden alındı.
        if (year.HasValue && month.HasValue)
        {
            var events = await _examRepository.GetAllAsync(
            x => x.ExamDateTime.Month == month &&
            x.ExamDateTime.Year == year &&
            x.Status != Status.Deleted &&
            x.ExamClassrooms.Any(ec => ec.Classroom.TrainerClassrooms.Any(tc => tc.TrainerId == trainerId)));

            return new SuccessDataResult<List<DashboardEventDto>>(_mapper.Map<List<DashboardEventDto>>(events), Messages.FoundSuccess);
        }

        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;

        var exams = await _examRepository.GetAllAsync(
            x => x.ExamDateTime.Month == currentMonth &&
            x.ExamDateTime.Year == currentYear &&
            x.Status != Status.Deleted &&
            x.ExamClassrooms.Any(ec => ec.Classroom.TrainerClassrooms.Any(tc => tc.TrainerId == trainerId)));

        return new SuccessDataResult<List<DashboardEventDto>>(_mapper.Map<List<DashboardEventDto>>(exams), Messages.FoundSuccess);
    }
    /// <summary>
    /// GetTopRatedStudentAsync is an asynchronous method that retrieves a list of the top rated students.
    /// It fetches all active students who have not graduated from the _studentRepository.
    /// For each student, it calculates the average score from all their exams and gets their latest classroom.
    /// It then creates a new DashboardTopRatedStudentDto object for the student and adds it to the list.
    /// The list is sorted in descending order by the students' average scores.
    /// </summary>
    /// <returns>The method returns a SuccessDataResult object containing the top 10 students from the list and a success message.</returns>
    public async Task<IDataResult<List<DashboardTopRatedStudentDto>>> GetTopRatedStudentAsync()
    {
        List<DashboardTopRatedStudentDto> topRatedStudents = new List<DashboardTopRatedStudentDto>();

        IEnumerable<Student> students = await _studentRepository.GetAllDataAsync();

        foreach (var student in students)
        {
            var scores = student.StudentExams.Where(x => x.Score != null && x.Exam.IsTest == false && x.Exam.IsCanceled == false).Select(x => x.Score.Value);

            var studentClassrooms = student.StudentClassrooms;



            if (studentClassrooms.Count() <= 0)
            {
                continue;
            }

            var studentLatestClassroom = student.StudentClassrooms.OrderByDescending(x => x.ModifiedDate).First();

            topRatedStudents.Add(new DashboardTopRatedStudentDto
            {
                StudentFullName = $"{student.FirstName} {student.LastName}",
                LatestClassroom = studentLatestClassroom.Classroom.Name,
                Score = Math.Round(scores.Count() > 0 ? scores.Average() : 0, 2),
                Id = student.Id,
                StudentExamsCount = scores.Count(),
                Status = student.Status               
            });
        }
        topRatedStudents.Sort((x, y) => y.Score.CompareTo(x.Score));

        return new SuccessDataResult<List<DashboardTopRatedStudentDto>>(topRatedStudents.Take(10).ToList(), Messages.FoundSuccess);
    }


    /// <summary>
    /// Retrieves the top-rated active students based on their exam scores. 
    /// Filters out inactive students and those in closed or passive classrooms.
    /// Calculates the average scores and returns the top 10 students sorted by their scores.
    /// </summary>
    /// <returns>A list of top-rated active students with their details.</returns>
    public async Task<IDataResult<List<DashboardTopRatedStudentDto>>> GetTopRatedActiveStudentAsync()
    {
        List<DashboardTopRatedStudentDto> topRatedStudents = new List<DashboardTopRatedStudentDto>();

        IEnumerable<Student> students = await _studentRepository.GetAllDataAsync();

        foreach (var student in students)
        {
            var scores = student.StudentExams.Where(x => x.Score != null && x.Exam.IsTest == false && x.Exam.IsCanceled == false).Select(x => x.Score.Value);

            var studentClassrooms = student.StudentClassrooms;

            if (studentClassrooms.Count() <= 0 || studentClassrooms.Any(sc => sc.Classroom.ClosedDate < DateTime.Now || sc.Status == Status.Passive))
            {
                continue;
            }

            var studentLatestClassroom = student.StudentClassrooms.OrderByDescending(x => x.ModifiedDate).First();

            topRatedStudents.Add(new DashboardTopRatedStudentDto
            {
                StudentFullName = $"{student.FirstName} {student.LastName}",
                LatestClassroom = studentLatestClassroom.Classroom.Name,
                Score = Math.Round(scores.Count() > 0 ? scores.Average() : 0, 2),
                Id = student.Id,
                StudentExamsCount = scores.Count(),
                Status = student.Status,              

            });
        }
        topRatedStudents.Sort((x, y) => y.Score.CompareTo(x.Score));

        return new SuccessDataResult<List<DashboardTopRatedStudentDto>>(topRatedStudents.Take(10).ToList(), Messages.FoundSuccess);
    }

    /// <summary>
    /// Gets the top 10 top-rated students for a specified trainer.
    /// </summary>
    /// <param name="TrainerId"></param>
    /// <returns>The method returns a SuccessDataResult object containing the top 10 students from the list and a success message.</returns>
    public async Task<IDataResult<List<DashboardTopRatedStudentDto>>> GetTopRatedStudentForTrainerAsync(string TrainerId)
    {
        List<DashboardTopRatedStudentDto> topRatedStudents = new List<DashboardTopRatedStudentDto>();

        var classroomResult = await _trainerService.GetClassroomsByIdentityId(TrainerId);

        var classrooms = classroomResult.Data;

        if (classrooms.Count > 0)
        {
            var allStudentList = await _studentRepository.GetAllDataAsync();
            var trainerStudentList = allStudentList.Where(s => s.StudentClassrooms.Any(sc => classrooms.Select(c => c.Id).Contains(sc.ClassroomId)));

            foreach (var student in trainerStudentList)
            {
                var scores = student.StudentExams.Where(x => x.Score != null).Select(x => x.Score.Value);

                var studentClassrooms = student.StudentClassrooms;

                if (studentClassrooms.Count() <= 0 || studentClassrooms.Any(sc => sc.Classroom.ClosedDate < DateTime.Now || sc.Status == Status.Passive) || student.Status != Status.Active)
                {
                    continue;
                }

                var studentLatestClassroom = student.StudentClassrooms.OrderByDescending(x => x.ModifiedDate).First();

                topRatedStudents.Add(new DashboardTopRatedStudentDto
                {
                    StudentFullName = $"{student.FirstName} {student.LastName}",
                    LatestClassroom = studentLatestClassroom.Classroom.Name,
                    Score = Math.Round(scores.Count() > 0 ? scores.Average() : 0, 2),
                    Id = student.Id,
                    StudentExamsCount = scores.Count(),
                    Status = student.Status                    
                });
            }
            topRatedStudents.Sort((x, y) => y.Score.CompareTo(x.Score));

            return new SuccessDataResult<List<DashboardTopRatedStudentDto>>(topRatedStudents.Take(10).ToList(), Messages.FoundSuccess);
        }
        return new SuccessDataResult<List<DashboardTopRatedStudentDto>>(topRatedStudents.ToList(), Messages.ClassroomNotFound);
    }
       

    /// <summary>
    /// GetWaitedRevisedApprovedQuestionByTrainerIdAsync metodu trainerId'ye ait onaylanan, onay bekleyen ve revize istenen soru listesini asenkron şekilde getirir.
    /// </summary>
    /// <param name="trainerIdentityId"></param>
    /// <returns><List<DashboardTrainerQuestionDto>> dönüş değerine sahiptir.</returns>
    public async Task<IDataResult<List<DashboardTrainerQuestionDto>>> GetWaitedRevisedApprovedQuestionByTrainerIdAsync(string trainerIdentityId)
    {
        var questions = await _questionRepository.GetAllAsync(question =>
            question.CreatedBy == trainerIdentityId &&
            (question.State == State.Awaited || question.State == State.Approved || question.State == State.Reviewed) &&
            question.Status != Status.Deleted);

        return new SuccessDataResult<List<DashboardTrainerQuestionDto>>(_mapper.Map<List<DashboardTrainerQuestionDto>>(questions), Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<QuestionListDto>>> GetQuestionsBySubjectAsync(Guid subjectId)
    {
        var questions = await _questionRepository.GetAllAsync(q => q.SubjectId == subjectId) ?? new List<Question>();
        return new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(questions), Messages.ListedSuccess);

    }
}
