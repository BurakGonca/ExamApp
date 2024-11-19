using BAExamApp.Dtos.StudentAnswers;

namespace BAExamApp.Business.Interfaces.Services;

public interface IStudentAnswerService
{
    /// <summary>
    /// Öğrencinin vermiş olduğu cevapları database liste olarak kaydetmek için kullanılırç
    /// </summary>
    /// <param name="studentAnswerCreateDtos">List<AnswerOfStudentDto></param>
    /// <returns>DataResult tipinde öğrencinin cevapları.</returns>
    Task<IDataResult<List<StudentAnswerDto>>> AddRangeAsync(List<StudentAnswerCreateDto> studentAnswerCreateDtos);

    /// <summary>
    /// Öğrencinin vermiş olduğu cevabı database kaydetmek için kullanılır.
    /// </summary>
    /// <param name="studentAnswerCreateDto">List<AnswerOfStudentDto></param>
    /// <returns>DataResult</returns>
    Task<IDataResult<StudentAnswerDto>> AddAsync(StudentAnswerCreateDto studentAnswerCreateDto);

    /// <summary>
    /// Öğrencinin sınavdaki sorusunun Id'sine göre verdiği cevapları çekmek için kullanılır.
    /// </summary>
    /// <param name="id">Öğrencinin Sınav Soru Id'si</param>
    /// <returns>IDataResul tipinde SutudentAnswerDto Listesi</returns>
    Task<IDataResult<List<StudentAnswerDto>>> GetByStudentQuestionId(Guid id);

}
