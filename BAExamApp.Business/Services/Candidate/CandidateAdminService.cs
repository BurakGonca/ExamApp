using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Candidate.CandidateAdmins;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BAExamApp.Business.Services.Candidate;
public class CandidateAdminService : ICandidateAdminService
{
    private readonly ICandidateAdminRepository _candidateAdminRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public CandidateAdminService(ICandidateAdminRepository candidateAdminRepository, ICandidateRepository candidateRepository, IAccountService accountService, IMapper mapper)
    {
        _candidateAdminRepository = candidateAdminRepository;
        _candidateRepository = candidateRepository;
        _accountService = accountService;
        _mapper = mapper;
    }
    /// <summary>
    /// Aday yönetici ekleme işlemi.
    /// </summary>
    /// <param name="candidateAdminCreateDto"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateAdminDto>> AddAsync(CandidateAdminCreateDto candidateAdminCreateDto)
    {
        if (await _accountService.AnyAsync(x => x.Email == candidateAdminCreateDto.Email))
        {
            return new ErrorDataResult<CandidateAdminDto>(Messages.EmailDuplicate);
        }

        IdentityUser identityUser = new()
        {
            Email = candidateAdminCreateDto.Email,
            UserName = candidateAdminCreateDto.Email,
            EmailConfirmed = true, // TODO: Email confirmation yapılırsa burası değiştirilmeli.
        };

        DataResult<CandidateAdminDto> result = new ErrorDataResult<CandidateAdminDto>();

        var strategy = await _candidateAdminRepository.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using var transactionScope = await _candidateAdminRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.CreateUserAsync(identityUser, Roles.CandidateAdmin);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<CandidateAdminDto>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }

                CandidateCandidateAdmin candidateAdmin = _mapper.Map<CandidateCandidateAdmin>(candidateAdminCreateDto);
                candidateAdmin.IdentityId = identityUser.Id;

                await _candidateAdminRepository.AddAsync(candidateAdmin);
                await _candidateAdminRepository.SaveChangesAsync();

                result = new SuccessDataResult<CandidateAdminDto>(_mapper.Map<CandidateAdminDto>(candidateAdmin), Messages.AddSuccess);
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<CandidateAdminDto>($"{Messages.AddFail} - {ex.Message}");
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });

        return result;
    }
    /// <summary>
    /// Aday yönetici silme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResult> DeleteAsync(Guid id)
    {
        var candidateAdmin = await _candidateAdminRepository.GetByIdAsync(id);

        if (candidateAdmin == null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        var deleteIdentityResult = await _accountService.DeleteUserAsync(candidateAdmin.IdentityId!);

        if (!deleteIdentityResult.Succeeded)
        {
            return new ErrorResult(deleteIdentityResult.ToString());
        }

        await _candidateAdminRepository.DeleteAsync(candidateAdmin);
        await _candidateAdminRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }
    /// <summary>
    /// Tüm aday yöneticiler çağırma işlemi
    /// </summary>
    /// <returns></returns>
    public async Task<IDataResult<List<CandidateAdminListDto>>> GetAllAsync()
    {
        var candidateAdmins = await _candidateAdminRepository.GetAllAsync();

        return new SuccessDataResult<List<CandidateAdminListDto>>(_mapper.Map<List<CandidateAdminListDto>>(candidateAdmins), Messages.ListedSuccess);
    }
    /// <summary>
    /// Aday yöneticiyi id ile çağırma işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateAdminDto>> GetByIdAsync(Guid id)
    {
        var candidateAdmin = await _candidateAdminRepository.GetByIdAsync(id);

        if (candidateAdmin is null)
        {
            return new ErrorDataResult<CandidateAdminDto>(Messages.UserNotFound);
        }

        return new SuccessDataResult<CandidateAdminDto>(_mapper.Map<CandidateAdminDto>(candidateAdmin), Messages.ListedSuccess);
    }
    /// <summary>
    /// Aday yöneticiyi identityId ile çağırma işlemi.
    /// </summary>
    /// <param name="identityId"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateAdminDto>> GetByIdentityIdAsync(string identityId)
    {
        var candidateAdmin = await _candidateAdminRepository.GetAsync(x => x.IdentityId == identityId);

        if (candidateAdmin is null)
        {
            return new ErrorDataResult<CandidateAdminDto>(Messages.UserNotFound);
        }

        return new SuccessDataResult<CandidateAdminDto>(_mapper.Map<CandidateAdminDto>(candidateAdmin), Messages.ListedSuccess);
    }
    /// <summary>
    /// Aday yöneticiye ait detayları getirme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateAdminDetailsDto>> GetDetailsByIdAsync(Guid id)
    {
        var candidateAdmin = await _candidateAdminRepository.GetByIdAsync(id);

        if (candidateAdmin is null)
        {
            return new ErrorDataResult<CandidateAdminDetailsDto>(Messages.UserNotFound);
        }

        return new SuccessDataResult<CandidateAdminDetailsDto>(_mapper.Map<CandidateAdminDetailsDto>(candidateAdmin), Messages.FoundSuccess);
    }
    /// <summary>
    /// Aday yönetici bilgilerini güncelleme işlemi.
    /// </summary>
    /// <param name="candidateAdminUpdateDto"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateAdminDto>> UpdateAsync(CandidateAdminUpdateDto candidateAdminUpdateDto)
    {
        var strategy = await _candidateAdminRepository.CreateExecutionStrategy();

        var result = default(IDataResult<CandidateAdminDto>);

        await strategy.ExecuteAsync(async () =>
        {
            using var transactionScope = await _candidateAdminRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var candidateAdmin = await _candidateAdminRepository.GetByIdAsync(candidateAdminUpdateDto.Id);

                if (candidateAdmin is null)
                {
                    result = new ErrorDataResult<CandidateAdminDto>(Messages.UserNotFound);
                    transactionScope.Rollback();
                    return;
                }

                var updatedAdmin = _mapper.Map(candidateAdminUpdateDto, candidateAdmin);

                await _candidateAdminRepository.UpdateAsync(updatedAdmin);

                var identityUser = await _accountService.GetUserByEmailAsync(updatedAdmin.Email);
                if (identityUser != null)
                {
                    identityUser.Email = updatedAdmin.Email;
                    identityUser.UserName = updatedAdmin.Email;

                    var identityResult = await _accountService.UpdateUserAsync(identityUser);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorDataResult<CandidateAdminDto>(identityResult.ToString());
                        transactionScope.Rollback();
                        return;
                    }
                }
                await _candidateAdminRepository.SaveChangesAsync();

                result = new SuccessDataResult<CandidateAdminDto>(_mapper.Map<CandidateAdminDto>(updatedAdmin), Messages.UpdateSuccess);
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<CandidateAdminDto>($"{Messages.UpdateFail} - {ex.Message}");
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });

        return result;
    }

    public async Task<IResult> AllowCandidateToRetakeExamAsync(Guid candidateId)
    {
        // Adayı ICandidateRepository kullanarak bul
        var candidate = await _candidateRepository.GetByIdAsync(candidateId);

        if (candidate == null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        // Adayın sınava tekrar girme iznini güncelle
        candidate.IsRetakeAllowed = !candidate.IsRetakeAllowed;

        await _candidateRepository.UpdateAsync(candidate);
        await _candidateRepository.SaveChangesAsync();

        return new SuccessResult(Messages.UpdateSuccess);
    }
}
