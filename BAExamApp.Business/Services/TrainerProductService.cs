using BAExamApp.Dtos.TrainerProducts;

namespace BAExamApp.Business.Services;
public class TrainerProductService : ITrainerProductService
{
    private readonly ITrainerProductRepository _trainerProductRepository;
    private readonly IMapper _mapper;
    private readonly ITrainerRepository _trainerRepository;

    public TrainerProductService(ITrainerProductRepository trainerProductRepository, IMapper mapper,ITrainerRepository trainerRepository)
    {
        _trainerProductRepository = trainerProductRepository;
        _mapper = mapper;
        _trainerRepository = trainerRepository;
    }

    public async Task<IDataResult<List<TrainerProductListDto>>> GetAllTrainersByProductIdAsync(Guid productId)
    {
        var trainers = await _trainerProductRepository.GetAllAsync(x => x.ProductId == productId);
        
        var mappedTrainerProductList = _mapper.Map<List<TrainerProductListDto>>(trainers);

        return new SuccessDataResult<List<TrainerProductListDto>>(mappedTrainerProductList, Messages.ListedSuccess);
    }

    public async Task<IResult> AddTrainersToProductAsync(ProductAddTrainerDto productAddTrainerDto)
    {
        var trainerProduct = await _trainerProductRepository.GetAllAsync(x => x.ProductId == productAddTrainerDto.ProductId);

        var toBeDeletedTrainers = trainerProduct.Where(x => !productAddTrainerDto.SelectedTrainersIds.Contains(x.TrainerId)).ToList();

        var newTrainers = productAddTrainerDto.SelectedTrainersIds.Where(trainerId => !trainerProduct.Any(tc => tc.TrainerId == trainerId)).Select(trainerId => new TrainerProduct
        {
            ProductId = productAddTrainerDto.ProductId,
            TrainerId = trainerId
        })
    .ToList();

        if (toBeDeletedTrainers != null)
        {
            await _trainerProductRepository.DeleteRangeAsync(toBeDeletedTrainers);
        }
        if (trainerProduct != null)

        {
            await _trainerProductRepository.AddRangeAsync(newTrainers);
        }
        await _trainerProductRepository.SaveChangesAsync();
        return new SuccessResult(Messages.UpdateSuccess);
    }
}
