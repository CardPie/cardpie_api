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
            Email = "oanhnmh150996@gmail.com",
            Address = "Thu Dau Mot, Binh Duong",
            Fullname = "Nguyen Mai Hoang Oanh",
            Username = "Oanh",
            Password = SecurityExtension.HashPassword<User>("Oanh123", salt),
            Role = UserRole.Member,
            Salt = salt,
            Status = UserStatus.Active,
            PhoneNumber = "0928498293",
        };

        await _unitOfWork.UserRepository.InsertAsync(user, Guid.Empty, DateTime.UtcNow);
        return Ok();
    }
}