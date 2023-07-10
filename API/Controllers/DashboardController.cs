using API.Dtos;
using API.Services;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class DashboardController : BaseController
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
      _dashboardService = dashboardService;
    }

    [SwaggerOperation("Get base information")]
    [HttpGet]
    public async Task<ApiResponse<DashboardDto>> GetInformation()
    {
      return await _dashboardService.GetInformation();
    }
}
