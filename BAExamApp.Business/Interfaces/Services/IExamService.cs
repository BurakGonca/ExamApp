using BAExamApp.Dtos.Exams;

namespace BAExamApp.Business.Interfaces.Services;

public interface IExamService
{
    Task<IDataResult<ExamDto>> GetByIdAsync(Guid id);
    /// <summary>
    /// Eğer sınav mevcutsa mevcut bütün sınavları liste olarak döner.
    /// Sınav listesi şunları içerir; Sınav ismi, Sınav süresi ve Oluşturma Tarihi    
    /// <returns>ExamListDto</returns>
    Task<IDataResult<List<ExamListDto>>> GetAllAsync();
    /// <summary>
    /// Verilen id ile eşleşen sınav verisini getirir
    /// </summary>
    /// <param name="id">Exam Id</param>
    /// <returns>DataResult<ExamDetailDto></returns>
    Task<IDataResult<ExamDetailDto>> GetDetailsByIdAsync(Guid id);
    Task<IDataResult<List<ExamListDto>>> GetByIdentityIdAsync(string id);
    /// <summary>
    /// ExamCreateDto parametresi alır. Belirli ClassromId ve ExamRuleId değerine sahip olan tüm sınavları döndürür. Eğer bu id'lere sahip bir Exam oluştuysa uyarı verir ve eklemez.
    /// Eğer daha önceden eklenmiş olan ClassroomId ve ExamRuleId yoksa Exam ekler.
    /// </summary>
    /// <param name="examCreateDto">ExamCreateDto</param>
    /// <returns>DataResult<ExamDto></returns>
    Task<IDataResult<ExamDto>> AddAsync(ExamCreateDto examCreateDto);
    /// <summary>
    /// Bütün sınavların bulunduğu sınav listesinden seçilen sınavın silinmesi
    /// </summary>
    /// <param name="examId">Sınavların id'si</param>
    /// <returns>IResult döndürür</returns>
    Task<IResult> DeleteAsync(Guid id);
    /// <summary>
    /// Bütün sınavların bulunduğu sınav listesinden seçilen sınavın statusu deleted olarak değiştir
    /// </summary>
    /// <param name="examId">Sınavların id'si</param>
    /// <returns>IResult döndürür</returns>
    Task<IResult> SoftDeleteAsync(Guid id);
    /// <summary>
    /// Bütün sınavların bulunduğu sınav listesinden bitmiş sınavlar arasından seçilen sınavın iscanceled durumunu true olarak değiştir. Bitmiş sınavları iptal eder.
    /// </summary>
    /// <param name="examId">Sınavların id'si</param>
    /// <returns>IResult döndürür</returns>
    Task<IResult> IsCanceledAsync(Guid id);
    /// <summary>
    /// Sınavı oluşturan kişinin email adresini alma.
    /// </summary>
    /// <param name="id">Sınavın id'si</param>
    /// <returns>IResult döndürür</returns>
    Task<IDataResult<string>> CreateByIdAsync(Guid id);
    /// <summary>
    /// ExamUpdateDto parametresi alır. Var olan bir sınavı Id'sine göre veritabanından çeker ve yeni veriyi üzerine yazar.
    /// </summary>
    /// <param name="examUpdateDto"></param>
    /// <returns>DataResult<ExamDto></returns>
    Task<IDataResult<ExamDto>> UpdateAsync(ExamUpdateDto examUpdateDto);
    /// <summary>
    /// Kullanıcıdan alınan exam status ile eşleşen sınav verisini getirir
    /// </summary>
    /// <returns>DataResult<ExamDetailDto></returns>
    Task<IDataResult<List<ExamListDto>>> GetExamsByStatusAsync(string status);
    
    /// <summary>
    /// Tüm sınıfların bulunduğu sınıf listesinden seçilen sınıfın tüm sınavlarını getirir
    /// </summary>
    /// <param name="classroomId">Seçilen sınıfın id'si</param>
    /// <returns></returns>
    Task<IDataResult<List<ExamListDto>>> GetExamsByClassIdAsync(Guid classroomId);


    /// <summary>
    /// Kullanıcının yaptığı filtreleme seçimlerine göre sınav listesinin filtreli halini döndürüyor.
    /// </summary>
    /// <param name="selectedClassroomId"></param>
    /// <param name="selectedRulenameId"></param>
    /// <param name="datetimePickerStart"></param>
    /// <param name="datetimePickerEnd"></param>
    /// <param name="isActiveExams"></param>
    /// <returns></returns>
    Task<IDataResult<List<ExamListDto>>> GetExamsByFiltered(string selectedClassroomId, string selectedRulenameId, string datetimePickerStart, string datetimePickerEnd,bool isActiveExams);

    Task<IDataResult<List<ExamListDto>>> GetExamsByFilteredByTrainer(string selectedClassroomId, string selectedRulenameId, string datetimePickerStart, string datetimePickerEnd, bool isActiveExams, string UserIdentityId);

    /// <summary>
    /// Sınav listesinde Id'ye sınavlarının ortalamasını ve sınava katılım durumlarını döndürür.
    /// </summary>
    /// <param name="examId">Sınavların Id'lerini tutan listedir.</param>
    /// <returns></returns>
    Task<IDataResult<List<ExamListDto>>> GetExamsByExamIdAsync(List<Guid> examId);

    ///// <summary>
    ///// Bu metot, sınavın Id'sine göre, o sınava girecek olan öğrencilere sınav linkinin gönderilmesini sağlar.
    ///// </summary>
    ///// <param name="examId"></param>
    ///// <returns></returns>
    Task<IDataResult<List<ExamListDto>>> GetStudentsInfosByExamIdAsync(Guid examId, string link);



}