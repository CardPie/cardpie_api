using API.Dtos;
using API.Services;
using AppCore.Models;
using MainData.Entities;
using MainData.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Login api")]
    public async Task<ApiResponse<AuthDto>> SignIn(AccountCredentialLoginDto accountCredentialLoginDto)
    {
        return await _authenticationService.SignIn(accountCredentialLoginDto);
    }
    
    [Authorize(new[] { UserRole.Admin, UserRole.Member})]
    [HttpPost("sign-out")]
    public async Task<ApiResponse> Logout()
    {
        return await _authenticationService.RevokeToken();
    }
    
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [SwaggerOperation("Refresh token")]
    public async Task<ApiResponse<AuthDto>> SignIn(AuthRefreshDto authRefreshDto)
    {
        return await _authenticationService.RefreshToken(authRefreshDto);
    }
}   