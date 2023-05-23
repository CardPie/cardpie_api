using MainData.Entities;
using MainData.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[Controller]")]
[Authorize(new[] { UserRole.Admin, UserRole.Member })]
public class BaseController : ControllerBase
{
    
}