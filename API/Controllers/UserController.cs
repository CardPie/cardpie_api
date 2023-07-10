using API.Dtos;
using API.Services;
using AppCore.Models;
using MainData.Entities;
using MainData.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[Authorize(new[] { UserRole.Admin })]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [SwaggerOperation("Get users")]
    [HttpGet]
    public async Task<ApiResponses<UserAdminDto>> Get([FromQuery]UserQuery queryDto)
    {
      return await _userService.GetUsers(queryDto);
    }

    [SwaggerOperation("Active premium for user")]
    [HttpPost("active")]
    public async Task<ApiResponse> ActivePremium(ActiveDto activeDto)
    {
      return await _userService.ActivePremium(activeDto);
    }
}
