using BAExamApp.Core.Enums;
using BAExamApp.Dtos.ApiUsers;
using BAExamApp.Entities.ApiEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services
{
    public class ApiUserService : IApiUserService
    {
        private readonly IMapper _mapper;
        private readonly IApiUserRepository _apiUserRepository;
        private readonly IAccountService _accountService;
        private readonly UserManager<IdentityUser> _userManager;
        public ApiUserService(IApiUserRepository userRepository, IMapper mapper, IAccountService accountService, UserManager<IdentityUser> userManager)
        {
            _apiUserRepository = userRepository;
            _mapper = mapper;
            _accountService = accountService;
            _userManager = userManager;
        }

        /// <summary>
        /// Yeni bir api kullanıcısını asenkron olarak ekler.
        /// </summary>
        /// <param name="createApiUserDto">Oluşturulacak kullanıcının bilgilerini içeren DTO.</param>
        /// <returns>Oluşturulan API kullanıcısı DTO'sunu içeren veri sonucu.</returns>
        public async Task<IDataResult<ApiUserDto>> AddAsync(CreateApiUserDto createApiUserDto)
        {
            if (await _accountService.AnyAsync(x => x.Email == createApiUserDto.Email))
            {
                return new ErrorDataResult<ApiUserDto>(Messages.EmailDuplicate);
            }
            IdentityUser identityUser = new()
            {
                Email = createApiUserDto.Email,
                UserName = createApiUserDto.Email,
                EmailConfirmed = true, // TODO: Email confirmation yapılırsa burası değiştirilmeli.
            };

            DataResult<ApiUserDto> result = new ErrorDataResult<ApiUserDto>();

            var strategy = await _apiUserRepository.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await _apiUserRepository.BeginTransactionAsync().ConfigureAwait(false);
                try
                {
                    var identityResult = await _accountService.CreateUserAsync(identityUser, Roles.ApiUser);
                    if (!identityResult.Succeeded)
                    {
                        result = new ErrorDataResult<ApiUserDto>(identityResult.ToString());
                        transactionScope.Rollback();
                        return;
                    }

                    ApiUser apiUser = _mapper.Map<ApiUser>(createApiUserDto);
                    apiUser.IdentityId = identityUser.Id;

                    await _apiUserRepository.AddAsync(apiUser);
                    await _apiUserRepository.SaveChangesAsync();

                    result = new SuccessDataResult<ApiUserDto>(_mapper.Map<ApiUserDto>(apiUser), Messages.AddSuccess);
                    transactionScope.Commit();
                }
                catch (Exception ex)
                {
                    result = new ErrorDataResult<ApiUserDto>($"{Messages.AddFail} - {ex.Message}");
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
        /// API kullanıcısını asenkron olarak siler.
        /// </summary>
        /// <param name="id">Silinecek kullanıcının ID'si.</param>
        /// <returns>Silme işleminin başarı veya başarısızlığını belirten sonuç.</returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var user = await _apiUserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            var identityDeleteResult = await _accountService.DeleteUserAsync(user.IdentityId!);
            if (!identityDeleteResult.Succeeded)
            {
                return new ErrorResult(identityDeleteResult.ToString());
            }

            await _apiUserRepository.DeleteAsync(user);
            await _apiUserRepository.SaveChangesAsync();
            return new SuccessResult(Messages.DeleteSuccess);
        }

        /// <summary>
        /// Tüm API kullanıcılarını asenkron olarak getirir.
        /// </summary>
        /// <returns>API kullanıcısı DTO'larından oluşan bir listeyi içeren veri sonucu.</returns>
        public async Task<IDataResult<List<ApiUserListDto>>> GetAllAsync()
        {
            var apiUsers = await _apiUserRepository.GetAllAsync();
            return new SuccessDataResult<List<ApiUserListDto>>(_mapper.Map<List<ApiUserListDto>>(apiUsers), Messages.ListedSuccess);
        }

        /// <summary>
        /// ID'ye göre bir API kullanıcısını asenkron olarak getirir.
        /// </summary>
        /// <param name="id">Getirilecek kullanıcının ID'si.</param>
        /// <returns>API kullanıcısı DTO'sunu içeren veri sonucu.</returns>
        public async Task<IDataResult<ApiUserDto>> GetByIdAsync(Guid id)
        {
            var user = await _apiUserRepository.GetByIdAsync(id);
            if (user is null)
            {
                return new ErrorDataResult<ApiUserDto>(Messages.UserNotFound);
            }

            return new SuccessDataResult<ApiUserDto>(_mapper.Map<ApiUserDto>(user), Messages.FoundSuccess);
        }

        /// <summary>
        /// Bir API kullanıcısını asenkron olarak günceller.
        /// </summary>
        /// <param name="updateApiUserDto">Kullanıcının güncellenmiş bilgilerini içeren DTO.</param>
        /// <returns>Güncellenmiş API kullanıcısı DTO'sunu içeren veri sonucu.</returns>
        public async Task<IDataResult<ApiUserDto>> UpdateAsync(UpdateApiUserDto updateApiUserDto)
        {
            var apiUser = await _apiUserRepository.GetByIdAsync(updateApiUserDto.Id);
            var user = await _userManager.FindByIdAsync(apiUser.IdentityId);
            if (user is null)
            {
                return new ErrorDataResult<ApiUserDto>(Messages.UserNotFound);
            }


            user.Email = updateApiUserDto.Email;
            user.UserName = updateApiUserDto.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // return new ErrorResult(result.ToString());
            }

            if (apiUser is null) return new ErrorDataResult<ApiUserDto>(Messages.UserNotFound);

            var updatedApiUser = _mapper.Map(updateApiUserDto, apiUser);

            await _apiUserRepository.UpdateAsync(updatedApiUser);
            await _apiUserRepository.SaveChangesAsync();

            return new SuccessDataResult<ApiUserDto>(_mapper.Map<ApiUserDto>(updatedApiUser), Messages.UpdateSuccess);
        }
    }
}