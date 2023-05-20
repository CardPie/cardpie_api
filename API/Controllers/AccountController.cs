using API.Dtos;
using API.Services;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet("information")]
    [SwaggerOperation("Get current account information")]
    public async Task<ApiResponse<UserDto>> GetAccountInformation()
    {
        return await _accountService.GetAccountInformation();
    }
    
    [HttpPut("information")]
    [SwaggerOperation("Update current account information")]
    public async Task<ApiResponse<UserDto>> UpdateAccountInformation(UpdateUserDto updateUserDto)
    {
        return await _accountService.UpdateInformation(updateUserDto);
    }
}