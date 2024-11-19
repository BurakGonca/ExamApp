using BAExamApp.Dtos.ApiDtos.StudentExamApiDtos;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.StudentExams;

namespace BAExamApp.Business.Interfaces.Services;
public interface IStudentExamService
{
    Task<IDataResult<StudentExamDto>> GetByIdAsync(Guid id);
    /// <summary>
    ///  ExamStudent tablosundaki studentta ait geçmiş sınav verilerini getirir.
    /// </summary>
    /// <param name="Guid Id">Guid Id</param>
    /// <returns>List<ExamStudentListDto></returns>
    Task<IDataResult<List<StudentExamListDto>>> GetAllByStudentIdAsync(Guid id);
    /// <summary>
    ///  ExamStudent tablosundaki studentta ait geçmiş sınav verilerini getirir.
    /// </summary>
    /// <param name="Guid Id">Guid Id</param>
    /// <returns>List<ExamStudentListDto></returns>
    Task<IDataResult<List<StudentExamListDto>>> GetAllByExamIdAsync(Guid id);
    /// ExamStudentEvaluateDto nesnesi kullanılarak öğrenciye ait sınavi günceller. Güncellenmiş olan sınava öğrenciyle ilgili bilgileri içeren ExamStudentEvaluateDto nesnesi ile birlikte IDataResult arayüzünden türetilen bir sonuç döndürür. 
    /// </summary>
    /// <param name="examStudentEvaluateDto">ExamStudentEvaluateDto</param>
    /// <returns>DataResult<ExamStudentEvaluateDto></returns>
    Task<IDataResult<StudentExamDto>> UpdateAsync(StudentExamUpdateDto studentExamUpdateDto);

    /// <summary>
    /// Admin sorgusuyla ogrenciye ait olan tüm sınavları ve öğrencinin bilgilerisini getirir.
    /// </summary>
    /// <param name=" Guid id"></param>
    /// <returns>DataResult<List<StudentExamsDetailsDto>></returns>
    Task<IDataResult<List<StudentExamsDetailsDto>>> GetAllExamsWithDetailsByIdAsync(Guid id);

    /// <summary>
    /// Trainer sorgusuyla ogrenciye ait olan ogrencının sınava girerek (Sıfır dahil) not aldıgı sınavları ve öğrencinin bilgilerisini getirir.
    /// </summary>
    /// <param name="Guid id"></param>
    /// <returns>DataResult<List<StudentExamsDetailsDto>></returns>
    Task<IDataResult<List<StudentExamsDetailsDto>>> GetAllExamsWithDetailsByIdForTrainerAsync(Guid id);

    ///<summary>
    ///ExamStudent tablosundaki studentta ait geçmiş sınav ve sınıf verilerini  getirir.
    /// </summary>
    /// <param name="Guid Id">Guid Id</param>
    /// <returns>List<StudentExamsAdminDto></returns>
    Task<IDataResult<List<StudentExamsAdminDto>>> GetAllExamsByStudentIdAsync(Guid id);
    Task<IDataResult<ExamStrudentQuestionDetailsDto>> GetExamStrudentQuestionDetailsByIdAsync(Guid id);
    /// <summary>
    /// ClassroomId'ye göre öğrenci notlarının getirir(Api)
    /// </summary>
    /// <param name="classroomId"></param>
    /// <returns></returns>

    /// <summary>
    /// Öğrencinin mail adresini tutar.
    /// </summary>
    /// <param name="studenntExamId"></param>
    /// <returns></returns>
    Task<string> GetStudentEmailByStudentExamAsync(Guid studentExamId);

    /// <summary>
    /// Seçilen sınava giren öğrenciler arasından, eğitmenin kendi öğrencilerinin sınav performanslarını sınavın içerdiği konulara göre hesaplar
    /// </summary>
    /// <param name="trainerId"></param>
    /// <param name="examId">Ortalama performansı hesaplanmak istenen sınav id'si</param>
    /// <returns>Konu başına ortalama performansları tutan kütüphane</returns>
    Task<IDictionary<string, double>> AnalysisExamPerformanceByTrainerAsync(Guid trainerId, Guid examId);

    Task<List<StudentExam>> GetStudentExamsByExamIdAsync(Guid examId);

    /// <summary>
    /// Sınav süresi dolan, sınavını tamamlamış ve daha önce sınav notlarının liste halindeki maili gitmemiş öğrencilere mail gönderme işlemi
    /// </summary>
    Task CheckAndSendExamResultsAsync();

    Task<IResult>ConfirmStudentExcuse(string studentExamId, string studentId);

}
