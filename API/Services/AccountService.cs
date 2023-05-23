using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;

namespace API.Services;

public interface IAccountService : IBaseService
{
    Task<ApiResponse<UserDto>> GetAccountInformation();
    Task<ApiResponse<UserDto>> UpdateInformation(UpdateUserDto updateUserDto);
}

public class AccountService : BaseService, IAccountService
{
    public AccountService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }
    public async Task<ApiResponse<UserDto>> GetAccountInformation()
    {
        var account = await MainUnitOfWork.UserRepository.FindOneAsync<UserDto>(new Expression<Func<User, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == AccountId
        });

        if (account == null)
            throw new ApiException("Not found the information", StatusCode.NOT_FOUND);

        // Get number of saved deck
        var saveDecks = await MainUnitOfWork.SavedDeckRepository.FindAsync(new Expression<Func<SavedDeck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.CreatorId == AccountId
        },null);

        account.SavedDeck = saveDecks.Count();
        
        // Get number of created decks
        var createdDeck = await MainUnitOfWork.DeckRepository.FindAsync(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.CreatorId == AccountId
        }, null);

        account.CreatedDeck = createdDeck.Count();
        
        // Get number studied cards
        var studySessions = await MainUnitOfWork.StudySessionRepository.FindAsync(
            new Expression<Func<StudySession, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.UserId == AccountId
            }, null);

       account.StudiedCard = studySessions
            .SelectMany(s => s!.CardsStudied?.Split(',') ?? Array.Empty<string>())
            .Count();

       account = await _mapperRepository.MapCreator(account);

       return ApiResponse<UserDto>.Success(account);
    }

    public async Task<ApiResponse<UserDto>> UpdateInformation(UpdateUserDto updateUserDto)
    {
        var account = await MainUnitOfWork.UserRepository.FindOneAsync(new Expression<Func<User, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == AccountId
        });

        if (account == null)
            throw new ApiException("Not found the information", StatusCode.NOT_FOUND);

        account.Fullname = updateUserDto.Fullname ?? account.Fullname;
        account.Email = updateUserDto.Email ?? account.Email;
        account.PhoneNumber = updateUserDto.PhoneNumber ?? account.PhoneNumber;
        account.Address = updateUserDto.Address ?? account.Address;
        account.Avatar = updateUserDto.Avatar ?? updateUserDto.Avatar;
        
        if(!await MainUnitOfWork.UserRepository.UpdateAsync(account, AccountId, CurrentDate))
            throw new ApiException("Can't update the information", StatusCode.SERVER_ERROR);

        var response = account.ProjectTo<User, UserDto>();
        
        return ApiResponse<UserDto>.Success(response);
    }
}