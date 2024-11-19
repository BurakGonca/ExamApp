using BAExamApp.Dtos.StudentExams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services;
public interface IExamAnalysisService
{
    Task<StudentExamResultDto> AnalysisStudentPerformanceAsync(Guid studentId, Guid examId);
    Task<IDictionary<string, double>> AnalysisExamPerformanceAsync(Guid examId);
    StudentExamResultDto CalculateOverallPerformance(StudentExam studentExam);

}