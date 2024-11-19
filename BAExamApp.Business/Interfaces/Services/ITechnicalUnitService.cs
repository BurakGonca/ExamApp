using BAExamApp.Dtos.TecnicalUnits;

namespace BAExamApp.Business.Interfaces.Services;

public interface ITechnicalUnitService
{
    /// <summary>
    /// Teknik birim eklemek için kullanılır
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>TechnicalUnitDto Data Result dönüş yapar.</returns>
    Task<IDataResult<TechnicalUnitDto>> AddAsync(TechnicalUnitCreateDto entity);
    /// <summary>
    /// Tüm teknik birimleri listelemek için kullanılırç
    /// </summary>
    /// <returns>List tipinde TechnicalUnitListDto Data result döner </returns>
    Task<IDataResult<List<TechnicalUnitListDto>>> GetAllAsync();
    /// <summary>
    /// Belirtilen Id'ye Teknik birimi çeker.
    /// </summary>
    /// <param name="id">Teknik birim Id'si</param>
    /// <returns></returns>
    Task<IDataResult<TechnicalUnitDto>> GetByIdAsync(Guid id);
    Task<IDataResult<TechnicalUnitDto>> UpdateAsync(TechnicalUnitUpdateDto technicalUnitUpdateDto);
    Task<IResult> DeleteAsync(Guid id);
}