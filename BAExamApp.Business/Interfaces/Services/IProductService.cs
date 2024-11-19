using BAExamApp.Dtos.Products;

namespace BAExamApp.Business.Interfaces.Services;

public interface IProductService
{
    /// <summary>
    /// Verilen id ile eşleşen eğitim verisini getirir
    /// </summary>
    /// <param name="id">Product Id</param>
    /// <returns>Gönderilen id ile eşleşen eğitim nesnesini döndürür.</returns>
    Task<IDataResult<ProductDto>> GetByIdAsync(Guid id);
    Task<IDataResult<List<ProductListDto>>> GetAllAsync();
    /// <summary>
    /// Belirtilen Konu Id'sine göre eğitim listesini getirir.
    /// </summary>
    /// <param name="subjectId">Eğitimleri filtrelenecek Konu Id'si.</param>
    /// <returns>İşlem sonucu ve eğitim listesi DTO verisini içerir.</returns>
    Task<IDataResult<List<ProductListDto>>> GetAllBySubjectIdAsync(Guid id);
    /// <summary>
    /// Belirtilen Teknik Birim Id'sine göre eğitim listesini getirir.
    /// </summary>
    /// <param name="technicalUnitId">Eğitimleri filtrelenecek Teknik Birim Id'si.</param>
    /// <returns>İşlem sonucu ve eğitim listesi DTO verisini içerir.</returns>
    Task<IDataResult<List<ProductListDto>>> GetAllByTechnicalUnitIdAsync(Guid id);
    /// <summary>
    /// Verilen id ile eşleşen eğitim verisini getirir
    /// </summary>
    /// <param name="id">Product Id</param>
    /// <returns>DataResult<ProductDetailDto></returns>
    Task<IDataResult<ProductDetailDto>> GetDetailsByIdAsync(Guid id);
    Task<IDataResult<ProductDto>> AddAsync(ProductCreateDto productCreateDto);
    /// <summary>
    /// Gönderilen ProductUpdateDto nesnesi üzerinden databasedeki aynı id ye ait veriyi günceller.
    /// </summary>
    /// <param name="productUpdateDto">Güncellenecek bilgileri içeren eğitim modeli</param>
    /// <returns>Güncellenen nesneyi geri döndürür.</returns>
    Task<IDataResult<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
    Task<IResult> DeleteAsync(Guid id);
    Task<bool> IsClassroomUsedProductAsync(Guid productId);
    Task<bool> IsTrainerUsedProductAsync(Guid productId);
    Task<bool> IsExamRuleUsedProductAsync(Guid productId);
}