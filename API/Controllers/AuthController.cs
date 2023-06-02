using API.Dtos;
using API.Services;
using AppCore.Models;
using MainData.Entities;
using MainData.Middlewares;
using Microsoft.AspNetCore.Authentication;
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
    
    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerOperation("Register account")]
    public async Task<ApiResponse> Register(RegisterDto registerDto)
    {
        return await _authenticationService.Register(registerDto);
    }
    
    [HttpGet("sign-in-google")]
    [AllowAnonymous]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(GoogleLoginCallback)),
            Items =
            {
                { "LoginProvider", "Google" }
            }
        };

        return Challenge(properties, "Google");
    }

    [HttpGet("google/callback")]
    [AllowAnonymous]
    public IActionResult GoogleLoginCallback()
    {
        return Ok("Google login successful!");
    }
}   