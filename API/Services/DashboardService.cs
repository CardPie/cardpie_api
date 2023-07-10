using API.Dtos;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;
using Task = DocumentFormat.OpenXml.Office2021.DocumentTasks.Task;

namespace API.Services;

public interface IDashboardService : IBaseService
{
  public Task<ApiResponse<DashboardDto>> GetInformation();
}

public class DashboardService : BaseService, IDashboardService
{
  public DashboardService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
  {
  }

  public async Task<ApiResponse<DashboardDto>> GetInformation()
  {
    var userDataset = MainUnitOfWork.UserRepository.GetQuery().Where(x => !x!.DeletedAt.HasValue);

    var totalUser = userDataset.Count();

    var totalPremiumUser = userDataset.Count(x => x!.AccountType == AccountType.Premium);

    var totalNewUser =
      userDataset.Count(x => x!.CreatedAt.Month == DateTime.Now.Month && x.CreatedAt.Year == DateTime.Now.Year);

    var totalMonthlyPremium = userDataset.Count(x =>
      x!.AccountType == AccountType.Premium && x.TypeOfPremium == TypeOfPremium.Monthly) * 40000; //Code smell, fixed the price

    var totalYearlyPremium = userDataset.Count(x =>
      x!.AccountType == AccountType.Premium && x.TypeOfPremium == TypeOfPremium.Yearly) * 200000; //Code smell, fixed the price

    var totalIncome = totalMonthlyPremium + totalYearlyPremium;

    var dashboard = new DashboardDto
    {
      TotalIncome = totalIncome,
      TotalUser = totalUser,
      TotalNewUser = totalNewUser,
      TotalPremiumAccount = totalPremiumUser,
    };

    await System.Threading.Tasks.Task.CompletedTask;
    return ApiResponse<DashboardDto>.Success(dashboard);
  }
}
