using System.Linq.Expressions;
using API.Dtos;
using AppCore.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using MainData;
using MainData.Entities;
using MainData.Repositories;

namespace API.Services;

public interface IUserService : IBaseService
{
  public Task<ApiResponses<UserAdminDto>> GetUsers(UserQuery userQuery);
  public Task<ApiResponse> ActivePremium(ActiveDto activeDto);
}

public class UserService : BaseService, IUserService
{
  public UserService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
  {
  }

  public async Task<ApiResponses<UserAdminDto>> GetUsers(UserQuery userQuery)
  {
    var keyword = userQuery.Keyword?.ToLower().Trim();
    var users = await MainUnitOfWork.UserRepository.FindResultAsync<UserAdminDto>(new Expression<Func<User, bool>>[]
    {
        x => !x.DeletedAt.HasValue,
        x => string.IsNullOrEmpty(keyword) || (x.Fullname!.ToLower().Contains(keyword) || x.Email!.Contains(keyword) ||
                                               x.PhoneNumber!.Contains(keyword))
    }, userQuery.OrderBy, userQuery.Skip(), userQuery.PageSize);

    users.Items = await _mapperRepository.MapCreator(users.Items.ToList());

    return ApiResponses<UserAdminDto>.Success(
      users.Items,
      users.TotalCount,
      userQuery.PageSize,
      userQuery.Skip(),
      (int)Math.Ceiling(users.TotalCount / (double)userQuery.PageSize));
  }

  public async Task<ApiResponse> ActivePremium(ActiveDto activeDto)
  {
    var account = await MainUnitOfWork.UserRepository.FindOneAsync(activeDto.UserId);

    if (account == null)
      throw new ApiException("Not found this account", StatusCode.NOT_FOUND);

    if (account.AccountType == AccountType.Premium)
      throw new ApiException("This account is already premium", StatusCode.BAD_REQUEST);

    account.AccountType = AccountType.Premium;
    account.ActivePremiumDate = CurrentDate;
    account.TypeOfPremium = activeDto.TypeOfPremium;

    if (!await MainUnitOfWork.UserRepository.UpdateAsync(account, AccountId, CurrentDate))
      throw new ApiException(MessageKey.ServerError, StatusCode.SERVER_ERROR);

    return ApiResponse.Success();
  }
}
