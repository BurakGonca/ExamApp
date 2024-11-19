using BAExamApp.Dtos.ExamRuleSubtopics;
using BAExamApp.Dtos.Questions;
using BAExamApp.Dtos.StudentQuestions;

namespace BAExamApp.Business.Interfaces.Services;
public interface IStudentQuestionService
{
    Task<IDataResult<StudentQuestionDto>> GetByIdAsync(Guid id);
    Task<IDataResult<List<StudentQuestionListDto>>> GetByStudentExamIdAsync(Guid id);
    Task<IDataResult<StudentQuestionDetailsDto>> GetByStudentExamIdAndQuestionOrderAsync(Guid id, int questionOrder);
    Task<IDataResult<List<StudentQuestionDto>>> AddRangeAsync(List<StudentQuestionCreateDto> studentQuestionsCreateDto);
    Task<IDataResult<List<StudentQuestionDto>>> AddRangeForExamIdAsync(Guid id);
    /// <summary>
    /// Gönderilen sınav kurallarına göre genel soru havuzundan istenen soru seviyesi ve soru tipine göre istenen miktarda rastegele soru seçilerek bir soru listesi oluşturur.
    /// </summary>
    /// <param name="examRuleSubtopic">ExamRuleSubtopic</param>
    /// <returns>DataResult<QuestionForStudentListDto></returns>
    Task<IDataResult<List<QuestionListDto>>> CreateQuestionPoolForExamRuleSubtopicsAsync(List<ExamRuleSubtopicDto> examRuleSubtopics);
    Task<IDataResult<StudentQuestionDto>> UpdateAsync(StudentQuestionUpdateDto studentQuestionUpdateDto);
    Task ScoringOfTheExam(Guid studentQuestionID);
    Task<IDataResult<string>> TotalGivenTimeAsync(IEnumerable<ExamRuleSubtopicDto> subtopics);
}
