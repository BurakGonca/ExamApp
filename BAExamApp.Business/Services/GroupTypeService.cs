using BAExamApp.Dtos.GroupTypes;

namespace BAExamApp.Business.Services;

public class GroupTypeService : IGroupTypeService
{
    private readonly IGroupTypeRepository _groupTypeRepository;
    private readonly IMapper _mapper;
    public GroupTypeService(IGroupTypeRepository groupTypeRepository, IMapper mapper)
    {
        _groupTypeRepository = groupTypeRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<GroupTypeDto>> GetByIdAsync(Guid id)
    {
        var groupType = await _groupTypeRepository.GetByIdAsync(id);

        if (groupType is null)
        {
            return new ErrorDataResult<GroupTypeDto>(Messages.GroupTypeNotFound);
        }

        return new SuccessDataResult<GroupTypeDto>(_mapper.Map<GroupTypeDto>(groupType), Messages.FoundSuccess);
    }

    public async Task<IDataResult<List<GroupTypeListDto>>> GetAllAsync()
    {
        var questions = await _groupTypeRepository.GetAllAsync(false);

        return new SuccessDataResult<List<GroupTypeListDto>>(_mapper.Map<List<GroupTypeListDto>>(questions), Messages.ListedSuccess);
    }

    public async Task<IDataResult<GroupTypeDto>> AddAsync(GroupTypeCreateDto groupTypeCreateDto)
    {
        var hasGroupType = await _groupTypeRepository.AnyAsync(x => x.Name.ToLower().Equals(groupTypeCreateDto.Name.ToLower()));

        if (hasGroupType)
        {
            return new ErrorDataResult<GroupTypeDto>(Messages.AddFailAlreadyExists);
        }

        var groupType = _mapper.Map<GroupType>(groupTypeCreateDto);

        await _groupTypeRepository.AddAsync(groupType);
        await _groupTypeRepository.SaveChangesAsync();

        return new SuccessDataResult<GroupTypeDto>(_mapper.Map<GroupTypeDto>(groupType), Messages.AddSuccess);
    }

    public async Task<IDataResult<GroupTypeDto>> UpdateAsync(GroupTypeUpdateDto groupTypeUpdateDto)
    {
        var groupType = await _groupTypeRepository.GetByIdAsync(groupTypeUpdateDto.Id);

        if (groupType is null)
        {
            return new ErrorDataResult<GroupTypeDto>(Messages.GroupTypeNotFound);
        }

        var updatedGroupType = _mapper.Map(groupTypeUpdateDto, groupType);

        await _groupTypeRepository.UpdateAsync(updatedGroupType);
        await _groupTypeRepository.SaveChangesAsync();

        return new SuccessDataResult<GroupTypeDto>(_mapper.Map<GroupTypeDto>(updatedGroupType), Messages.UpdateSuccess);
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var groupType = await _groupTypeRepository.GetByIdAsync(id);

        if (groupType is null)
        {
            return new ErrorResult(Messages.GroupTypeNotFound);
        }

        await _groupTypeRepository.DeleteAsync(groupType);
        await _groupTypeRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }
}