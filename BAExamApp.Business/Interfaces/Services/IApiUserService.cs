using BAExamApp.Dtos.ApiUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services
{
    public interface IApiUserService
    {
        Task<IDataResult<List<ApiUserListDto>>> GetAllAsync();
        Task<IDataResult<ApiUserDto>> AddAsync(CreateApiUserDto createApiUserDto);
        Task<IDataResult<ApiUserDto>> UpdateAsync(UpdateApiUserDto updateApiUserDto);
        Task<IDataResult<ApiUserDto>> GetByIdAsync(Guid id);
        Task<IResult> DeleteAsync(Guid id);
    }
}