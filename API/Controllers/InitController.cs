using AppCore.Extensions;
using MainData;
using MainData.Entities;
using MainData.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class InitController : BaseController
{
    
    private readonly MainUnitOfWork _unitOfWork;

    public InitController(MainUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var salt = SecurityExtension.GenerateSalt();
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "doannh@gmail.com",
            Address = "Thu Duc, TP Ho Chi Minh",
            Fullname = "Nguyen Huu Doan",
            Username = "covarom",
            Password = SecurityExtension.HashPassword<User>("covarom", salt),
            Role = UserRole.Admin,
            Salt = salt,
            Status = UserStatus.Active,
            PhoneNumber = "0922398293",
        };

        await _unitOfWork.UserRepository.InsertAsync(user, Guid.Empty, DateTime.UtcNow);
        return Ok();
    }
}