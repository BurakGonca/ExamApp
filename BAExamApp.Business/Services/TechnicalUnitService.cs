using BAExamApp.Dtos.TecnicalUnits;

namespace BAExamApp.Business.Services;

public class TechnicalUnitService : ITechnicalUnitService
{
    private readonly ITechnicalUnitRepository _tecnicalUnitRepository;
    private readonly IMapper _mapper;
    public TechnicalUnitService(ITechnicalUnitRepository tecnicalUnitRepository, IMapper mapper)
    {
        _tecnicalUnitRepository = tecnicalUnitRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<TechnicalUnitDto>> AddAsync(TechnicalUnitCreateDto createDto)
    {
        var hasTechnicalUnit = await _tecnicalUnitRepository.AnyAsync(x => x.Name.ToLower().Equals(createDto.Name.Trim().ToLower()));
        if (hasTechnicalUnit)
        {
            return new ErrorDataResult<TechnicalUnitDto>(Messages.AddFailAlreadyExists);
        }

        var technicalUnit = _mapper.Map<TechnicalUnit>(createDto);
        await _tecnicalUnitRepository.AddAsync(technicalUnit);
        await _tecnicalUnitRepository.SaveChangesAsync();

        return new SuccessDataResult<TechnicalUnitDto>(_mapper.Map<TechnicalUnitDto>(technicalUnit), Messages.AddSuccess);
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var technicalUnit = await _tecnicalUnitRepository.GetByIdAsync(id);
        if (technicalUnit is null)
            return new ErrorResult(Messages.TechnicalUnitNotFound);

        await _tecnicalUnitRepository.DeleteAsync(technicalUnit);
        await _tecnicalUnitRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IDataResult<List<TechnicalUnitListDto>>> GetAllAsync()
    {
        var technicalUnits = await _tecnicalUnitRepository.GetAllAsync(false);
        return new SuccessDataResult<List<TechnicalUnitListDto>>(_mapper.Map<List<TechnicalUnitListDto>>(technicalUnits), Messages.ListedSuccess);
    }

    public async Task<IDataResult<TechnicalUnitDto>> GetByIdAsync(Guid id)
    {
        var technicalUnit = await _tecnicalUnitRepository.GetAsync(x => x.Id == id);
        if (technicalUnit is null) return new ErrorDataResult<TechnicalUnitDto>(Messages.TechnicalUnitNotFound);
        return new SuccessDataResult<TechnicalUnitDto>(_mapper.Map<TechnicalUnitDto>(technicalUnit), Messages.TechnicalUnitFoundSuccess);
    }

    public async Task<IDataResult<TechnicalUnitDto>> UpdateAsync(TechnicalUnitUpdateDto technicalUnitUpdateDto)
    {
        var technicalUnit = await _tecnicalUnitRepository.GetByIdAsync(technicalUnitUpdateDto.Id);
        if (technicalUnit is null)
        {
            return new ErrorDataResult<TechnicalUnitDto>(Messages.TechnicalUnitNotFound);
        }
        var updatedTechnicalUnit = _mapper.Map(technicalUnitUpdateDto, technicalUnit);
        await _tecnicalUnitRepository.UpdateAsync(updatedTechnicalUnit);
        await _tecnicalUnitRepository.SaveChangesAsync();

        return new SuccessDataResult<TechnicalUnitDto>(_mapper.Map<TechnicalUnitDto>(updatedTechnicalUnit), Messages.UpdateSuccess);
    }
}