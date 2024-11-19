using BAExamApp.Core.Enums;
using BAExamApp.Dtos.ExamRules;
using BAExamApp.Entities.Enums;
using System.Runtime.CompilerServices;

namespace BAExamApp.Business.Services;

public class ExamRuleService : IExamRuleService
{
    private readonly IExamRuleRepository _examRuleRepository;
    private readonly IExamRepository _examRepository;
    private readonly IMapper _mapper;

    private readonly IAdminService _adminService;

    public ExamRuleService(IExamRuleRepository examRuleRepository, IMapper mapper, IAdminService adminService, IExamRepository examRepository)
    {
        _examRuleRepository = examRuleRepository;
        _mapper = mapper;
        _adminService = adminService;
        _examRepository = examRepository;
    }

    public async Task<IDataResult<ExamRuleDto>> GetByIdAsync(Guid id)
    {
        var examRule = await _examRuleRepository.GetByIdAsync(id);

        if (examRule is null)
        {
            return new ErrorDataResult<ExamRuleDto>(Messages.ExamRuleNotFound);
        }

        return new SuccessDataResult<ExamRuleDto>(_mapper.Map<ExamRuleDto>(examRule), Messages.FoundSuccess);
    }

    public async Task<IDataResult<List<ExamRuleListDto>>> GetAllAsync()
    {
        var examRules = await _examRuleRepository.GetAllAsync();

        var examRuleListDtos = _mapper.Map<List<ExamRuleListDto>>(examRules);
        var IdList = examRules.Select(x => x.CreatedBy).ToList();
        for (int i = 0; i < IdList.Count; i++)
        {
            var admin = await _adminService.GetByIdentityIdAsync(IdList[i]);
            var firstname = admin.Data.FirstName;
            var lastname = admin.Data.LastName;
            var createdBy = char.ToUpper(firstname[0]) + firstname.Substring(1).ToLower() + " " +
                            char.ToUpper(lastname[0]) + lastname.Substring(1).ToLower();
            examRuleListDtos[i].CreatedByPerson = createdBy;
        }

        return new SuccessDataResult<List<ExamRuleListDto>>(examRuleListDtos, Messages.ListedSuccess);
    }

    public async Task<IDataResult<ExamRuleDetailsDto>> GetDetailsByIdAsync(Guid id)
    {
        var examRule = await _examRuleRepository.GetByIdAsync(id);

        if (examRule is null)
        {
            return new ErrorDataResult<ExamRuleDetailsDto>(Messages.ExamRuleNotFound);
        }

        return new SuccessDataResult<ExamRuleDetailsDto>(_mapper.Map<ExamRuleDetailsDto>(examRule), Messages.FoundSuccess);
    }

    public async Task<IDataResult<ExamRuleDto>> AddAsync(ExamRuleCreateDto examRuleCreateDto)
    {
        var hasExamRule = await _examRuleRepository.AnyAsync(examRule => examRule.Name.ToLower() == examRuleCreateDto.Name.Trim().ToLower());

        if (hasExamRule)
        {
            return new ErrorDataResult<ExamRuleDto>(Messages.AddFailAlreadyExists);
        }

        var examRule = _mapper.Map<ExamRule>(examRuleCreateDto);

        try
        {
            await _examRuleRepository.AddAsync(examRule);
        }
        catch (Exception)
        {
            return new ErrorDataResult<ExamRuleDto>(Messages.DuplicateExamRule);
        }
        await _examRuleRepository.SaveChangesAsync();

        return new SuccessDataResult<ExamRuleDto>(_mapper.Map<ExamRuleDto>(examRule), Messages.AddSuccess);
    }

    public async Task<IDataResult<ExamRuleDto>> UpdateAsync(ExamRuleUpdateDto examRuleUpdateDto)
    {
        var examRule = await _examRuleRepository.GetByIdAsync(examRuleUpdateDto.Id);

        if (examRule is null)
        {
            return new ErrorDataResult<ExamRuleDto>(Messages.ExamRuleNotFound);
        }

        var updatedExamRule = _mapper.Map(examRuleUpdateDto, examRule);

        try
        {
            await _examRuleRepository.UpdateAsync(updatedExamRule);

        }
        catch (Exception)
        {
            return new ErrorDataResult<ExamRuleDto>(Messages.UpdateFail);
        }

        try
        {
            await _examRuleRepository.SaveChangesAsync();
        }
        catch (Exception)
        {

            return new ErrorDataResult<ExamRuleDto>(Messages.DuplicateExamRule);
        }

        return new SuccessDataResult<ExamRuleDto>(_mapper.Map<ExamRuleDto>(updatedExamRule), Messages.UpdateSuccess);
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var examRule = await _examRuleRepository.GetByIdAsync(id);

        if (examRule is null)
        {
            return new ErrorResult(Messages.ExamRuleNotFound);
        }

        examRule.ExamRuleSubtopics.ToList().ForEach(x => x.Status = Core.Enums.Status.Deleted);

        await _examRuleRepository.DeleteAsync(examRule);
        await _examRuleRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }


    public async Task<IDataResult<List<ExamRuleListDto>>> GetExamRulesByExamTypeAsync(string examType)
    {
        if (examType == "1")
        {
            var examRules = await _examRuleRepository.GetAllAsync(x => x.ExamRuleSubtopics);

            var filteredRules = examRules
                .Where(x => x.Status != Status.Passive) // Classic türünü filtreleme kaldırıldı
                .ToList();

            return new SuccessDataResult<List<ExamRuleListDto>>(_mapper.Map<List<ExamRuleListDto>>(filteredRules), Messages.ListedSuccess);
        }

        var rules = await _examRuleRepository.GetAllAsync(x => x.Status != Status.Passive);

        return new SuccessDataResult<List<ExamRuleListDto>>(_mapper.Map<List<ExamRuleListDto>>(rules), Messages.ListedSuccess);
    }


    /// <summary>
    /// Filtreyi doldurmak için tüm sınavların kurallarını isim ve id'sine göre getirir.
    /// </summary>
    /// <returns>Liste tipinde ExamRuleFilterDto döndürür</returns>
    public async Task<IDataResult<List<ExamRuleFilterDto>>> GetAllExamRulesByFilter()
    {
        var examRules = await _examRuleRepository.GetAllAsync();

        var filteredRules = examRules.Where(x => x.Status != Status.Passive).ToList();

        if (filteredRules.Any())
        {
            return new SuccessDataResult<List<ExamRuleFilterDto>>(_mapper.Map<List<ExamRuleFilterDto>>(filteredRules), Messages.ListedSuccess);

        }
        else
        {

            return new ErrorDataResult<List<ExamRuleFilterDto>>(Messages.ListNotFound);

        }

    }

    public async Task<bool> IsRuleUsedInExamsAsync(Guid ruleId)
    {
        var examsUsingRule = await _examRepository.AnyAsync(e => e.ExamRuleId == ruleId);
        return examsUsingRule;
    }

    public async Task<IResult> ChangeRuleStatusAsync(Guid ruleId)
    {
        var examRule = await _examRuleRepository.GetByIdAsync(ruleId);

        if (examRule is null)
        {
            return new ErrorResult(Messages.ExamRuleNotFound);
        }

        examRule.Status = examRule.Status == Status.Active ? Status.Passive : Status.Active;

        try
        {
            await _examRuleRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new ErrorResult(Messages.ChangeStatusFail);
        }

        return new SuccessResult(Messages.ChangeStatusSuccess);
    }
}
