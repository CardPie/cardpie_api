using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ISavedDeckService : IBaseService
{
    Task<ApiResponses<SavedDeckDto>> GetOwnSavedDeck(SavedDeckQueryDto queryDto);

    Task<ApiResponse<DetailSavedDeckDto>> CreateSavedDeck(CreateSavedDeckDto dto);

    Task<ApiResponse<DetailSavedDeckDto>> DetailSavedDeck(Guid id);
    
    Task<ApiResponse<DetailSavedDeckDto>> UpdateSavedDeck(Guid id, UpdateSavedDeckDto updateSavedDeckDto);
    
    Task<ApiResponse> DeleteSavedDeck(Guid id);
}

public class SavedDeckService : BaseService, ISavedDeckService
{
    public SavedDeckService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor,
        IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }

    public async Task<ApiResponses<SavedDeckDto>> GetOwnSavedDeck(SavedDeckQueryDto queryDto)
    {
        var savedDecks = MainUnitOfWork.SavedDeckRepository.GetQuery()
            .Where(d => d.CreatorId == AccountId && !d.DeletedAt.HasValue)
            .Join(MainUnitOfWork.DeckRepository.GetQuery(),
                x => x.DeckId,
                y => y.Id,
                (savedDeck, deck) => new SavedDeckDto
                {
                    Id = savedDeck!.Id,
                    DeckId = deck!.Id,
                    Deck = deck.ProjectTo<Deck, DeckDto>(),
                    CreatorId = savedDeck.CreatorId ?? Guid.Empty,
                    Note = savedDeck.Note,
                    CreatedAt = savedDeck.CreatedAt,
                    EditedAt = savedDeck.UpdatedAt
                });
        
        var isDescending = queryDto.OrderBy.Split(' ').Last().ToLowerInvariant()
            .StartsWith("desc");
        
        var sortField = queryDto.OrderBy.Split(' ').First();
        
        // Sort
        if (!string.IsNullOrEmpty(sortField))
        {
            try
            {
                savedDecks = savedDecks.OrderBy(sortField, isDescending);
            }
            catch
            {
                throw new ApiException("Làm đel gì có field đó cho m sort", StatusCode.BAD_REQUEST);
            } 
        }

        var totalCount = savedDecks.Count();
        
        savedDecks = savedDecks
            .Skip(queryDto.Skip())
            .Take(queryDto.PageSize);

        var items = savedDecks.ToList();
        
        return ApiResponses<SavedDeckDto>.Success(
            items,
            totalCount,
            queryDto.PageSize,
            queryDto.Skip(),
            (int)Math.Ceiling(totalCount/ (double)queryDto.PageSize)
        );
        
    }

    public async Task<ApiResponse<DetailSavedDeckDto>> CreateSavedDeck(CreateSavedDeckDto dto)
    {
        var savedDeck = dto.ProjectTo<CreateSavedDeckDto, SavedDeck>();
        savedDeck.Id = Guid.NewGuid();

        var deck = await MainUnitOfWork.DeckRepository.FindOneAsync(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue
        });

        if (deck == null)
            throw new ApiException("Deck not found", StatusCode.NOT_FOUND);
        
        if (deck.CreatorId == AccountId)
            throw new ApiException("It's already your deck", StatusCode.BAD_REQUEST);

        if (!await MainUnitOfWork.SavedDeckRepository.InsertAsync(savedDeck, AccountId, CurrentDate))
            throw new ApiException("Can't save", StatusCode.SERVER_ERROR);

        return await DetailSavedDeck(savedDeck.Id);
    }

    public async Task<ApiResponse<DetailSavedDeckDto>> DetailSavedDeck(Guid id)
    {
        /*var saveDeck = await MainUnitOfWork.SavedDeckRepository.FindOneAsync<DetailSavedDeckDto>(
            new Expression<Func<SavedDeck, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.CreatorId == AccountId
            });*/
        
        var savedDeck = MainUnitOfWork.SavedDeckRepository.GetQuery()
            .Where(d => d.Id == id && !d.DeletedAt.HasValue)
            .Join(MainUnitOfWork.DeckRepository.GetQuery(),
                x => x.DeckId,
                y => y.Id,
                (savedDeck, deck) => new DetailSavedDeckDto
                {
                    Id = savedDeck!.Id,
                    DeckId = deck!.Id,
                    Deck = deck.ProjectTo<Deck, DeckDto>(),
                    CreatorId = savedDeck.CreatorId ?? Guid.Empty,
                    Note = savedDeck.Note,
                    CreatedAt = savedDeck.CreatedAt,
                    EditedAt = savedDeck.UpdatedAt
                }).FirstOrDefault();

        if (savedDeck == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        savedDeck = await _mapperRepository.MapCreator(savedDeck);

        return ApiResponse<DetailSavedDeckDto>.Success(savedDeck);
    }

    public async Task<ApiResponse<DetailSavedDeckDto>> UpdateSavedDeck(Guid id, UpdateSavedDeckDto updateSavedDeckDto)
    {
        var saveDeck = await MainUnitOfWork.SavedDeckRepository.FindOneAsync(new Expression<Func<SavedDeck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == id
        });
        
        if (saveDeck == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);
        
        if(saveDeck.CreatorId != AccountId)
            throw new ApiException("Can't update other's data", StatusCode.BAD_REQUEST);

        saveDeck.Note = updateSavedDeckDto.Note ?? saveDeck.Note;

        if (!await MainUnitOfWork.SavedDeckRepository.UpdateAsync(saveDeck, AccountId, CurrentDate))
            throw new ApiException("Update fail", StatusCode.SERVER_ERROR);

        return await DetailSavedDeck(saveDeck.Id);
    }

    public async Task<ApiResponse> DeleteSavedDeck(Guid id)
    {
        var saveDeck = await MainUnitOfWork.SavedDeckRepository.FindOneAsync(new Expression<Func<SavedDeck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == id
        });
        
        if (saveDeck == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);
        
        if(saveDeck.CreatorId != AccountId)
            throw new ApiException("Can't delete other's data", StatusCode.BAD_REQUEST);
        
        if(!await MainUnitOfWork.SavedDeckRepository.DeleteAsync(saveDeck, AccountId, CurrentDate))
            throw new ApiException("Delete fail", StatusCode.SERVER_ERROR);

        return ApiResponse.Success();
    }
}