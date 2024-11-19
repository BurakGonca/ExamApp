using BAExamApp.Dtos.Subjects;

namespace BAExamApp.Business.Interfaces.Services;

public interface ISubjectService
{
    /// <summary>
    /// Konu Listesinin tamamını getirir.
    /// </summary>
    /// <returns>SubjectListDto döner</returns>
    Task<IDataResult<List<SubjectListDto>>> GetAllAsync();

    /// <summary>
    /// Eğitim içeriği ve eğitimin konusu aynı anda eşsiz olması sağlanmıştı.Sağlanmadığı durumda hata mesajı SubjectAlreadyExist döner(if kontrolu biraz karmaşık olabilir bir eğitimin aynı isimde konusu olamaz fakat farklı eğitimler aynı konulara sahip olabilir)
    /// </summary>
    /// <param name="subjectCreateDto"></param>
    /// <returns>İşlemler hatasız gerçekleştirildiğinde konu ve eğitim içeri data basede eşleşir konu yoksa oluşturulur ve addSucces mesajı döner</returns>
    Task<IDataResult<SubjectDto>> AddAsync(SubjectCreateDto entity);

    /// <summary>
    /// Verilen Id'ye göre bağlı olan konuları SubjectLDto tipinde döner.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Başarısız ise Error Result ve mesajını, başarılı ise Success Result mesajını döner</returns>
    Task<IDataResult<SubjectDto>> GetByIdAsync(Guid id);

    /// <summary>
    /// Verilen Product Id'ye göre bağlı olan konuları SubjectListDto tipinde döner.
    /// </summary>
    /// <param name="productId"> Seçilen eğitim Id değeri</param>
    /// <returns>Başarısız ise Error Result ve mesajını, başarılı ise Success Result mesajını döner </returns>
    Task<IDataResult<List<SubjectListDto>>> GetAllByProductIdAsync(Guid productId);
    
    /// <summary>
    /// Verilen Id'ye göre Konu silme işlemi yapar.
    /// </summary>
    /// <param name="id">Subjet Id</param>
    /// <returns>IResult tipinde dönüş yapar.</returns>
    Task<IResult> DeleteAsync(Guid id);
   
    /// <summary>
    /// Gönderilen Tentity olarak güncelleme işlemini yapar ver SubjectDto döndürür.
    /// </summary>
    /// <param name="entity">Güncellenen konu</param>
    /// <returns>Başarısız ise Error Result ve mesajını, başarılı ise Success Result mesajını döner</returns>
    Task<IDataResult<SubjectDto>> UpdateAsync(SubjectUpdateDto entity);

    /// <summary>
    /// Verilen Product Id listesine göre bağlı olan konuları SubjectListDto tipinde döner.
    /// </summary>
    /// <param name="productIds"> Seçilen eğitim Id değerleri</param>
    /// <returns>Başarısız ise Error Result ve mesajını, başarılı ise Success Result mesajını döner </returns>
    Task<IDataResult<List<SubjectListDto>>> GetAllByListProductIdsAsync(List<Guid> productIds);

    /// <summary>
    /// Verilen id ile eşleşen konunun verilerini getirir.
    /// </summary>
    /// <param name="id">Subject Id</param>
    /// <returns>SubjectDetailDto tipinde IDataResult dönüş yapar</returns>
    Task<IDataResult<SubjectDetailDto>> GetDetailsByIdAsync(Guid id);

    /// <summary>
    /// Belirli bir konu altında kullanılan bir sorunun olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="subjectId">Konu kimliği</param>
    /// <returns>Belirli bir konu altında kullanılan bir soru varsa true, yoksa false döndürür.</returns>
    Task<bool> IsQuestionUsedInSubjectAsync(Guid subjectId);

    /// <summary>
    /// Belirli bir konu altında kullanılan bir sorunun olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="subjectId">Konu kimliği</param>
    /// <returns>Belirli bir konu altında kullanılan bir soru varsa true, yoksa false döndürür.</returns>
    Task<bool> IsSubtopicUsedInSubjectAsync(Guid subjectId);

    /// <summary>
    /// Belirli bir konu altında kullanılan bir sorunun olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="subjectId">Konu kimliği</param>
    /// <returns>Belirli bir konu altında kullanılan bir soru varsa true, yoksa false döndürür.</returns>
    Task<bool> IsProductSubjectUsedInSubjectAsync(Guid subjectId);

    /// <summary>
    /// Verilen konu kimliğine göre konunun durumunu değiştirir.
    /// </summary>
    /// <param name="subjectId">Konu kimliği</param>
    /// <returns>IResult tipinde dönüş yapar.</returns>
    Task<IResult> ChangeSubjectStatusAsync(Guid subjectId);
}

