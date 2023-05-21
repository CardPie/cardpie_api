using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;

namespace API.Services;

public interface IAccountService : IBaseService
{
    Task<ApiResponse<UserDto>> GetAccountInformation();
    Task<ApiResponse<UserDto>> UpdateInformation(UpdateUserDto updateUserDto);
}

public class AccountService : BaseService, IAccountService
{
    public AccountService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }
    public async Task<ApiResponse<UserDto>> GetAccountInformation()
    {
        var account = await MainUnitOfWork.UserRepository.FindOneAsync<UserDto>(new Expression<Func<User, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == AccountId
        });

        if (account == null)
            throw new ApiException("Not found the information", StatusCode.NOT_FOUND);

        return ApiResponse<UserDto>.Success(account);
    }

    public async Task<ApiResponse<UserDto>> UpdateInformation(UpdateUserDto updateUserDto)
    {
        var account = await MainUnitOfWork.UserRepository.FindOneAsync(new Expression<Func<User, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == AccountId
        });

        if (account == null)
            throw new ApiException("Not found the information", StatusCode.NOT_FOUND);

        account.Fullname = updateUserDto.Fullname ?? account.Fullname;
        account.Email = updateUserDto.Email ?? account.Email;
        account.PhoneNumber = updateUserDto.PhoneNumber ?? account.PhoneNumber;
        account.Address = updateUserDto.Address ?? account.Address;
        account.Avatar = updateUserDto.Avatar ?? updateUserDto.Avatar;
        
        if(!await MainUnitOfWork.UserRepository.UpdateAsync(account, AccountId, CurrentDate))
            throw new ApiException("Can't update the information", StatusCode.SERVER_ERROR);

        var response = account.ProjectTo<User, UserDto>();
        
        return ApiResponse<UserDto>.Success(response);
    }
}