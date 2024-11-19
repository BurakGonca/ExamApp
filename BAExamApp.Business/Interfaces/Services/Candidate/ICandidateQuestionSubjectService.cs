using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services.Candidate;
public interface ICandidateQuestionSubjectService
{
    /// <summary>
    /// Konu Listesinin tamamını getirir.
    /// </summary>
    /// <returns>SubjectListDto döner</returns>
    Task<IDataResult<List<CandidateQuestionSubjectListDto>>> GetAllAsync();

    /// <summary>
    /// Eğitim içeriği ve eğitimin konusu aynı anda eşsiz olması sağlanmıştı.Sağlanmadığı durumda hata mesajı SubjectAlreadyExist döner(if kontrolu biraz karmaşık olabilir bir eğitimin aynı isimde konusu olamaz fakat farklı eğitimler aynı konulara sahip olabilir)
    /// </summary>
    /// <param name="subjectCreateDto"></param>
    /// <returns>İşlemler hatasız gerçekleştirildiğinde konu ve eğitim içeri data basede eşleşir konu yoksa oluşturulur ve addSucces mesajı döner</returns>
    Task<IDataResult<CandidateQuestionSubjectDto>> AddAsync(CandidateQuestionSubjectCreateDto entity);

    /// <summary>
    /// Verilen Id'ye göre bağlı olan konuları SubjectLDto tipinde döner.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Başarısız ise Error Result ve mesajını, başarılı ise Success Result mesajını döner</returns>
    Task<IDataResult<CandidateQuestionSubjectDto>> GetByIdAsync(Guid id);


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
    Task<IDataResult<CandidateQuestionSubjectDto>> UpdateAsync(CandidateQuestionSubjectUpdateDto entity);


    /// <summary>
    /// Verilen id ile eşleşen konunun verilerini getirir.
    /// </summary>
    /// <param name="id">Subject Id</param>
    /// <returns>SubjectDetailDto tipinde IDataResult dönüş yapar</returns>
    Task<IDataResult<CandidateQuestionSubjectDetailDto>> GetDetailsByIdAsync(Guid id);

    /// <summary>
    /// Belirli bir konu altında kullanılan bir sorunun olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="subjectId">Konu kimliği</param>
    /// <returns>Belirli bir konu altında kullanılan bir soru varsa true, yoksa false döndürür.</returns>
    Task<bool> IsQuestionUsedInSubjectAsync(Guid subjectId);


    /// <summary>
    /// Verilen konu kimliğine göre konunun durumunu değiştirir.
    /// </summary>
    /// <param name="subjectId">Konu kimliği</param>
    /// <returns>IResult tipinde dönüş yapar.</returns>


    /// <summary>
    /// Aktivasyon durumunu değiştirmek için belirli bir konuyu ve bu konuya bağlı soruları ve yanıtlarını 'Aktif' olarak ayarlar.
    /// </summary>
    /// <param name="id">Aktifleştirilecek konu ID'si.</param>
    /// <returns>Aktif hale getirilen konu ve ilişkili veriler ile birlikte başarılı veya hatalı sonuç döner.</returns>
    Task<IDataResult<CandidateQuestionSubjectDto>> ActivateSubjectAndRelatedQuestionsWithAnswersAsync(Guid id);


    /// <summary>
    /// Pasif durumunu değiştirmek için belirli bir konuyu ve bu konuya bağlı soruları ve yanıtlarını 'Pasif' olarak ayarlar.
    /// </summary>
    /// <param name="id">Pasifleştirilecek konu ID'si.</param>
    /// <returns>Pasif hale getirilen konu ve ilişkili veriler ile birlikte başarılı veya hatalı sonuç döner.</returns>
    Task<IDataResult<CandidateQuestionSubjectDto>> InactivateSubjectAndRelatedQuestionsWithAnswersAsync(Guid id);

    Task<List<List<string>>> GetAllQuestionsCountsBySubjectAndType();
}
