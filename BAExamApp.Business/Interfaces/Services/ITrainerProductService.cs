using BAExamApp.Dtos.TrainerProducts;

namespace BAExamApp.Business.Interfaces.Services;
public interface ITrainerProductService
{
    /// <summary>
    /// Verilen eğitime sahip bütün eğitmenleri geri getirir
    /// </summary>
    /// <param name="productId"></param>
    /// <returns>IDataResult<List<Trainer>></returns>
    Task<IDataResult<List<TrainerProductListDto>>> GetAllTrainersByProductIdAsync(Guid productId);
    Task<IResult> AddTrainersToProductAsync(ProductAddTrainerDto productAddTrainerDto);
}
