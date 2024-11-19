using BAExamApp.Business.Services;
using BAExamApp.Dtos.Classrooms;
using BAExamApp.Dtos.ExamRules;

namespace BAExamApp.Business.Interfaces.Services;

public interface IExamRuleService
{

    /// <summary>
    /// Id parametresine göre sınav kuralını getirir.
    /// </summary>
    /// <param name="id">Seçilen ExamRule id sine ait sınav kuralını getirir</param>
    /// <returns>Eğer id parametresine sahip bir sınav kuralı yoksa notfound mesajı döner, bulunursa da success mesajı döner</returns>
    Task<IDataResult<ExamRuleDto>> GetByIdAsync(Guid id);



    /// <summary>
    /// Filtreyi doldurmak için tüm sınavların kurallarını isim ve id'sine göre getirir.
    /// </summary>
    /// <returns>Liste tipinde ExamRuleFilterDto döndürür</returns>
    Task<IDataResult<List<ExamRuleFilterDto>>> GetAllExamRulesByFilter();


    Task<IDataResult<List<ExamRuleListDto>>> GetAllAsync();
    /// <summary>
    /// Seçilen sınav türüne göre sınav kurallarını getirir. 
    /// </summary>
    /// <param name="examType"> Seçilen sınav türü </param>
    /// <returns>Eğer ilgili sınav türüne ait sınav kuralı varsa sonuçlarla birlikte Success Result ve mesajını döner.</returns>
    Task<IDataResult<List<ExamRuleListDto>>> GetExamRulesByExamTypeAsync(string examType);
    /// <summary>
    /// Sınav Kuralı eklenirken önce verilen ismin kontrolünü sağlar. Daha sonra Sınav Kuralı ve ve Sınav Kuralı Konuları ile map yaparak ekleme işlemini sağlar. 
    /// </summary>
    /// <param name="examRuleCreateDto"> Eklecenek olan model</param>
    /// <returns>Eğer aynı isimde Sınav Kuralı va rise Error Result ve mesajını döner ve daha sonra ekleme işleminin Error Result veya Success Result değerlerinin ve mesajlarını döner.</returns>
    Task<IDataResult<ExamRuleDetailsDto>> GetDetailsByIdAsync(Guid id);
    /// <summary>
    /// Id parametresine göre sınav kuralını siler
    /// </summary>
    /// <param name="id">Silme işlemi yapılacak sınav kuralının id'sidir</param>
    /// <returns>Eğer id parametresine sahip bir sınav kuralı yoksa notfound mesajı döner, varsa silme işlemini yapıp sınav kuralı silme başarılı notify mesaj döner</returns>
    Task<IDataResult<ExamRuleDto>> AddAsync(ExamRuleCreateDto examRuleCreateDto);
    /// <summary>
    /// Argüman olarak gönderilen <see cref="ExamRuleUpdateDto"/> databasede varsa güncelleme işlemi gerçekleştirilir.
    /// </summary>
    /// <param name="examRuleUpdateDto">Güncellenecek bilgileri içeren sınav kuralı nesnesi</param>
    /// <returns>
    /// <para>İşlem başarılı olursa güncellenen nesneyi geri döndürülür.</para>
    /// <para>İşlem başarısız olursa hata mesajı geri döndürülür.</para>
    /// </returns>
    Task<IDataResult<ExamRuleDto>> UpdateAsync(ExamRuleUpdateDto examRuleUpdateDto);
    /// <summary>
    /// Id parametresine göre sınav kural detayını getirir.ExamRule ile ExamRuleDetailsDto yu mapler.
    /// </summary>
    /// <param name="id">Seçilen ExamRule id sine ait sınav kuralını getirir</param>
    /// <returns>Eğer id parametresine sahip bir sınav kuralı yoksa notfound mesajı döner, bulunursa da success mesajı döner</returns>
    Task<IResult> DeleteAsync(Guid id);

    /// <summary>
    /// Belirtilen kuralın herhangi bir sınavda kullanılıp kullanılmadığını kontrol eder.
    /// </summary>
    /// <param name="ruleId">Parametre olarak gönderilen kural id ile işlwm yapılır.</param>
    /// <returns>Kuralın herhangi bir sınavda kullanılıp kullanılmadığı durumunu gösterir, kullanılıyor ise true, kullanılmıyor ise false döner.</returns>
    Task<bool> IsRuleUsedInExamsAsync(Guid ruleId);

    /// <summary>
    /// Kuralın statüsü aktifse eğer pasife, pasifse eğer aktife alınır.
    /// </summary>
    /// <param name="ruleId">Parametre olarak gönderilen kural id ile işlwm yapılır.</param>
    /// <returns>Eğer id parametresine sahip bir sınav kuralı yoksa notfound mesajı döner, eğer bulunup statü değişikliği gerçekleşirse success mesajı döner.</returns>
    Task<IResult> ChangeRuleStatusAsync(Guid ruleId);

}