using BAExamApp.Dtos.Dashboard;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.StudentExams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services;
public interface IDashboardService
{
    /// <summary>
    /// GetDashboardOverview is an asynchronous method that retrieves an overview of the dashboard.
    /// It counts the number of students, trainers, and exams from their respective repositories and assigns these counts to the DashboardOverviewDTO object.
    /// It fetches all student exams from the _studentExamRepository, calculates the average score, and assigns this average to the SuccessRate property of the DashboardOverviewDTO object.
    /// </summary>
    /// <returns>The method returns a SuccessDataResult object containing the DashboardOverviewDTO object and a success message.</returns>
    Task<IDataResult<DasboardOverviewDTO>> GetDashboardOverview();
    /// <summary>
    /// GetEventsAsync is an asynchronous method that retrieves a list of DashboardEventDto objects.
    /// If a year and month are provided, it fetches the events for that specific month of the year.
    /// If no year and month are provided, it fetches the events for the current month of the current year.
    /// </summary>
    /// <returns>The method returns a SuccessDataResult object containing the list of DashboardEventDto objects and a success message.</returns>
    Task<IDataResult<List<DashboardEventDto>>> GetEventsAsync(int? year, int? month);
    /// <summary>
    /// GetEventsAsync is an asynchronous method used to retrieve a list of DashboardEventDto objects based on TrainerId.
    /// If a year and month are provided, it fetches the events for that specific month of the year.
    /// If no year and month are provided, it fetches the events for the current month of the current year.
    /// </summary>
    /// <returns>The method returns a SuccessDataResult object containing the list of DashboardEventDto objects and a success message.</returns>
    Task<IDataResult<List<DashboardEventDto>>> GetEventsWithSpesificTrainerIdAsync(int? year, int? month, Guid trainerId);
    /// <summary>
    /// GetAllByAwaitedQuestionAsync is an asynchronous method that retrieves a list of questions in the 'Awaited' state.
    /// </summary>
    /// <returns>The method returns a list of QuestionListDto objects wrapped in a IDataResult object.</returns>
    Task<IDataResult<List<QuestionListDto>>> GetAllByAwaitedQuestionAsync();

    /// <summary>
    /// GetAllByAprrovedQuestionAsync is an asynchronous method that retrieves a list of questions in the 'Aprroved' state.
    /// </summary>
    /// <returns>The method returns a list of QuestionListDto objects wrapped in a IDataResult object.</returns>
    Task<IDataResult<List<QuestionListDto>>> GetAllByAprrovedQuestionAsync();

    /// <summary>
    /// GetAllByReviewedQuestionAsync is an asynchronous method that retrieves a list of questions in the 'Reviewed' state.
    /// </summary>
    /// <returns>The method returns a list of QuestionListDto objects wrapped in a IDataResult object.</returns>
    Task<IDataResult<List<QuestionListDto>>> GetAllByReviewedQuestionAsync();
    /// <summary>
    /// GetTopRatedStudentAsync is an asynchronous method that retrieves a list of the top rated students.
    /// It fetches all active students who have not graduated from the _studentRepository.
    /// For each student, it calculates the average score from all their exams and gets their latest classroom.
    /// It then creates a new DashboardTopRatedStudentDto object for the student and adds it to the list.
    /// The list is sorted in descending order by the students' average scores.
    /// </summary>
    /// <returns>The method returns a SuccessDataResult object containing the top 10 students from the list and a success message.</returns>
    Task<IDataResult<List<DashboardTopRatedStudentDto>>> GetTopRatedStudentAsync();

    /// <summary>
    /// Gets the top 10 top-rated students for a specified trainer.
    /// </summary>
    /// <param name="TrainerId"></param>
    /// <returns>The method returns a SuccessDataResult object containing the top 10 students from the list and a success message.</returns>
    Task<IDataResult<List<DashboardTopRatedStudentDto>>> GetTopRatedStudentForTrainerAsync(string TrainerId);

    /// <summary>
    /// GetWaitedRevisedApprovedQuestionByTrainerIdAsync metodu trainerId'ye ait onaylanan, onay bekleyen ve revize istenen soru listesini asenkron şekilde getirir.
    /// </summary>
    /// <param name="trainerIdentityId"></param>
    /// <returns><List<DashboardTrainerQuestionDto>> dönüş değerine sahiptir.</returns>
    Task<IDataResult<List<DashboardTrainerQuestionDto>>> GetWaitedRevisedApprovedQuestionByTrainerIdAsync(string trainerIdentityId);

    /// <summary>
    /// Retrieves the top-rated active students based on their exam scores. 
    /// Filters out inactive students and those in closed or passive classrooms.
    /// Calculates the average scores and returns the top 10 students sorted by their scores.
    /// </summary>
    /// <returns>A list of top-rated active students with their details.</returns>
    Task<IDataResult<List<DashboardTopRatedStudentDto>>> GetTopRatedActiveStudentAsync();
    /// <summary>
    /// Retrieves the top-rated active students based on their exam scores. 
    /// Filters out inactive students and those in closed or passive classrooms.
    /// Calculates the average scores and returns the top 10 students sorted by their scores.
    /// </summary>
    /// <returns>A list of top-rated active students with their details.</returns>
    Task<IDataResult<List<QuestionListDto>>> GetQuestionsBySubjectAsync(Guid subjectId);
}
