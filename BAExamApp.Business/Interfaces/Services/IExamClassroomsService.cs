using BAExamApp.Dtos.ExamClassrooms;

namespace BAExamApp.Business.Interfaces.Services;
public interface IExamClassroomsService
{
    Task<IDataResult<ExamClassroomsDto>> GetByIdAsync(Guid id);
    /// <summary>
    /// Belirtilen ID'ye sahip ExamClassrooms kaydını getirir.
    /// </summary>
    /// <param name="id">Getirilecek ExamClassrooms kaydının unique identifier'ı (Guid).</param>
    /// <returns>Spesifik ID'ye sahip ExamClassroomsDto nesnesi içeren bir IDataResult.</returns>
    Task<IDataResult<List<ExamClassroomsListDto>>> GetAllAsync();
    /// <summary>
    /// Tüm ExamClassrooms kayıtlarını getirir.
    /// </summary>
    /// <returns>Tüm ExamClassrooms kayıtlarını içeren bir liste döndüren IDataResult.</returns>
    Task<IDataResult<ExamClassroomsDto>> AddAsync(ExamClassroomsAddDto examClassroomsAddDto);
    /// <summary>
    /// Yeni bir ExamClassrooms kaydı ekler.
    /// </summary>
    /// <param name="examClassroomsAddDto">Eklenecek yeni ExamClassrooms bilgilerini içeren DTO.</param>
    /// <returns>Eklenen ExamClassroomsDto nesnesi içeren bir IDataResult.</returns>
    Task<IDataResult<ExamClassroomsDto>> UpdateAsync(ExamClassroomsUpdateDto examClassroomsUpdateDto);
    /// <summary>
    /// Var olan bir ExamClassrooms kaydını günceller.
    /// </summary>
    /// <param name="examClassroomsUpdateDto">Güncellenecek ExamClassrooms bilgilerini içeren DTO.</param>
    /// <returns>Güncellenen ExamClassroomsDto nesnesi içeren bir IDataResult.</returns>
    Task<IDataResult<ExamClassroomsDto>> DeleteAsync(Guid id);
    /// <summary>
    /// Belirtilen ID'ye sahip ExamClassrooms kaydını siler.
    /// </summary>
    /// <param name="id">Silinecek ExamClassrooms kaydının unique identifier'ı (Guid).</param>
    /// <returns>Silinen ExamClassroomsDto nesnesi içeren bir IDataResult.</returns>
}
