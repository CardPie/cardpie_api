using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IDeckService : IBaseService
{
    public Task<ApiResponses<DeckDto>> GetDecks(DeckQueryDto deckQueryDto);
    public Task<ApiResponse<DetailDeckDto>> GetDetailDeck(Guid deckId);
    public Task<ApiResponse<DetailDeckDto>> CreateDeck(CreateDeckDto createDeckDto);
    public Task<ApiResponse<DetailDeckDto>> UpdateDeck(Guid id, UpdateDeckDto updateDeckDto);
    public Task<ApiResponses<DeckDto>> GetOwnDeck(DeckQueryDto deckQueryDto);
    public Task<ApiResponses<DeckDto>> GetRecommendDecks(DeckQueryDto deckQueryDto);
    public Task<ApiResponses<DeckDto>> GetRecentlySeenDecks(DeckQueryDto deckQueryDto);
    public Task<ApiResponse> UpdateDeckView(Guid id);
    public Task<ApiResponse> DeleteDeck(Guid id);
}

public class DeckService : BaseService, IDeckService
{
    public DeckService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor,
        IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }

    public async Task<ApiResponses<DeckDto>> GetDecks(DeckQueryDto deckQueryDto)
    {
        var decks = await MainUnitOfWork.DeckRepository.FindResultAsync<DeckDto>(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.IsPublic,
            x => string.IsNullOrEmpty(deckQueryDto.DeckName) || x.Name.ToLower()
                .Contains(deckQueryDto.DeckName.Trim().ToLower()),
        }, deckQueryDto.OrderBy, deckQueryDto.Skip(), deckQueryDto.PageSize);

        decks.Items = await _mapperRepository.MapCreator(decks.Items.ToList());

        var cards = MainUnitOfWork.FlashCardRepository.GetQuery();
        
        foreach (var deck in decks.Items)
        {
            var cardCount = cards.Where(c => c.DeckId == deck.Id)?.Count() ?? 0;
            deck.TotalCard = cardCount;
        }

        return ApiResponses<DeckDto>.Success(
            decks.Items,
            decks.TotalCount,
            deckQueryDto.PageSize,
            deckQueryDto.Skip(),
            (int)Math.Ceiling(decks.TotalCount / (double)deckQueryDto.PageSize)
        );
    }

    public async Task<ApiResponse<DetailDeckDto>> GetDetailDeck(Guid deckId)
    {
        var deckDto = await MainUnitOfWork.DeckRepository.FindOneAsync<DetailDeckDto>(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == deckId,
            x => x.IsPublic
        });

        if (deckDto == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        var flashCards = await MainUnitOfWork.FlashCardRepository.FindAsync<FlashCardDto>(
            new Expression<Func<FlashCard, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.DeckId == deckDto.Id
            }, null);

        deckDto.ListFlashCards = flashCards;
        deckDto.TotalCard = flashCards.Count();

        var studySessions = await MainUnitOfWork.StudySessionRepository.FindAsync<StudySessionDto>(
            new Expression<Func<StudySession, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.DeckId == deckDto.Id,
                x => x.UserId == AccountId,
            }, null);

        deckDto.ListStudySessions = studySessions;

        var folderDto = await MainUnitOfWork.FolderRepository.FindOneAsync(new Expression<Func<Folder, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == deckDto.FolderId,
        });

        if (folderDto != null)
        {
            deckDto.FolderName = folderDto.FolderName;
        }

        return ApiResponse<DetailDeckDto>.Success(deckDto);
    }

    public async Task<ApiResponse<DetailDeckDto>> CreateDeck(CreateDeckDto createDeckDto)
    {
        var deck = createDeckDto.ProjectTo<CreateDeckDto, Deck>();
        deck.Id = Guid.NewGuid();
        deck.FolderId = createDeckDto.FolderId;
        deck.UserId = AccountId;

        if (!(await MainUnitOfWork.DeckRepository.InsertAsync(deck, AccountId, CurrentDate)))
            throw new ApiException("Save fail", StatusCode.SERVER_ERROR);

        var flashCards = createDeckDto.ListFlashCards.ProjectTo<CreateFlashCardWithDeckDto, FlashCard>();

        flashCards.ForEach(x => x.DeckId = deck.Id);

        flashCards.ForEach(item => item.DeckId = deck.Id);

        if (!(await MainUnitOfWork.FlashCardRepository.InsertAsync(flashCards, AccountId, CurrentDate)))
            throw new ApiException("Save fail", StatusCode.SERVER_ERROR);

        return await GetDetailDeck(deck.Id);
    }

    public async Task<ApiResponse<DetailDeckDto>> UpdateDeck(Guid id, UpdateDeckDto updateDeckDto)
    {
        var deckDto = await MainUnitOfWork.DeckRepository.FindOneAsync(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == id,
            x => x.IsPublic || x.CreatorId == AccountId
        });

        if (deckDto == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        if (deckDto.CreatorId != AccountId)
            throw new ApiException("Can't update other's deck", StatusCode.BAD_REQUEST);

        deckDto.IsPublic = updateDeckDto.IsPublic ?? deckDto.IsPublic;
        deckDto.Color = updateDeckDto.Color ?? deckDto.Color;
        deckDto.Order = updateDeckDto.Order ?? deckDto.Order;
        deckDto.FolderId = updateDeckDto.FolderId ?? deckDto.FolderId;
        deckDto.Description = updateDeckDto.Description ?? deckDto.Description;
        deckDto.Name = updateDeckDto.Name ?? deckDto.Name;
        deckDto.LearningLength = updateDeckDto.LearningLength ?? deckDto.LearningLength;
        deckDto.RecallStrength = updateDeckDto.RecallStrength ?? deckDto.RecallStrength;
        deckDto.IsDailyRemind = updateDeckDto.IsDailyRemind ?? deckDto.IsDailyRemind;
        deckDto.ReminderTime = updateDeckDto.ReminderTime ?? deckDto.ReminderTime;
        deckDto.WeeklyReminderDays = updateDeckDto.WeeklyReminderDays ?? deckDto.WeeklyReminderDays;
        deckDto.SpacedRepetitionStrategyLevel =
            updateDeckDto.SpacedRepetitionStrategyLevel ?? deckDto.SpacedRepetitionStrategyLevel;

        if (!await MainUnitOfWork.DeckRepository.UpdateAsync(deckDto, AccountId, CurrentDate))
            throw new ApiException("Update fail", StatusCode.SERVER_ERROR);

        return await GetDetailDeck(id);
    }

    public async Task<ApiResponses<DeckDto>> GetOwnDeck(DeckQueryDto deckQueryDto)
    {
        var decks = await MainUnitOfWork.DeckRepository.FindResultAsync<DeckDto>(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.CreatorId == AccountId
        }, deckQueryDto.OrderBy, deckQueryDto.Skip(), deckQueryDto.PageSize);

        decks.Items = await _mapperRepository.MapCreator(decks.Items.ToList());

        return ApiResponses<DeckDto>.Success(
            decks.Items,
            decks.TotalCount,
            deckQueryDto.PageSize,
            deckQueryDto.Skip(),
            (int)Math.Ceiling(decks.TotalCount / (double)deckQueryDto.PageSize)
        );
    }

    public async Task<ApiResponses<DeckDto>> GetRecommendDecks(DeckQueryDto deckQueryDto)
    {
        var studySessions = await MainUnitOfWork.StudySessionRepository.FindAsync(
            new Expression<Func<StudySession, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.UserId == AccountId
            }, null);

        var deckIds = studySessions.Select(s => s.DeckId).ToList();

        var recommendedDecks = MainUnitOfWork.DeckRepository.GetQuery().Where(x => !x.DeletedAt.HasValue);

        if (deckIds.Any())
        {
            recommendedDecks = recommendedDecks.Where(d => deckIds.Contains(d.Id));
        }

        var allRecommendedDecks = await recommendedDecks.ToListAsync();

        /*// Retrieve all the tags associated with the recommended decks
        var recommendedTags = allRecommendedDecks
            .SelectMany(d => d.Tags)
            .Distinct()
            .ToList();

        // Get the top tags based on the number of occurrences in study sessions
        var topTags = recommendedTags
            .GroupBy(t => t)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .Take(5) // Change the number as desired
            .ToList();*/

        // var filteredDecks = allRecommendedDecks
        //     .Where(d => d.Tags.Any(t => topTags.Contains(t)))
        //     .OrderByDescending(d => d.View)
        //     .Take(5)
        //     .ToList();

        var deckDtos = allRecommendedDecks.ProjectTo<Deck, DeckDto>();
        deckDtos = await _mapperRepository.MapCreator(deckDtos);

        var cards = MainUnitOfWork.FlashCardRepository.GetQuery().Where(x => !x.DeletedAt.HasValue);
        foreach (var deck in deckDtos)
        {
            var cardCount = cards.Where(c => c.DeckId == deck.Id)?.Count() ?? 0;
            deck.TotalCard = cardCount;
        }

        return ApiResponses<DeckDto>.Success(
            deckDtos,
            deckDtos.Count,
            deckQueryDto.PageSize,
            deckQueryDto.Skip(),
            (int)Math.Ceiling(deckDtos.Count / (double)deckQueryDto.PageSize)
        );
    }

    public async Task<ApiResponses<DeckDto>> GetRecentlySeenDecks(DeckQueryDto deckQueryDto)
    {
        var studySessions = await MainUnitOfWork.StudySessionRepository.FindAsync(
            new Expression<Func<StudySession, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.UserId == AccountId
            }, deckQueryDto.OrderBy);
        
        var deckIds = studySessions.Select(s => s.DeckId).ToList();

        var recentlySeenDecks = MainUnitOfWork.DeckRepository.GetQuery();

        if (deckIds.Any())
        {
            recentlySeenDecks = recentlySeenDecks.Where(d => deckIds.Contains(d.Id));
        }

        //var allRecentlySeenDecks = await recentlySeenDecks.ToListAsync();
        // Count items
        var totalCount = recentlySeenDecks.Count();
        recentlySeenDecks = recentlySeenDecks
            .Skip(deckQueryDto.Skip())
            .Take(deckQueryDto.PageSize);

        var decks = (await recentlySeenDecks.ToListAsync()).ProjectTo<Deck, DeckDto>();

        decks = await _mapperRepository.MapCreator(decks);

        // Return data
        return ApiResponses<DeckDto>.Success(
            decks, 
            totalCount, 
            deckQueryDto.PageSize, 
            deckQueryDto.Skip(), 
            (int)Math.Ceiling(totalCount/ (double)deckQueryDto.PageSize));
    }

    public async Task<ApiResponse> UpdateDeckView(Guid id)
    {
        var deck = await MainUnitOfWork.DeckRepository.FindOneAsync(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == id
        });
        
        if (deck == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        deck.View++;
        if (!await MainUnitOfWork.DeckRepository.UpdateAsync(deck, AccountId ?? Guid.Empty, CurrentDate))
            throw new ApiException();
        
        return ApiResponse.Success();
    }

    public async Task<ApiResponse> DeleteDeck(Guid id)
    {
        var deck = await MainUnitOfWork.DeckRepository.FindOneAsync(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == id
        });
        
        if (deck == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        if (!await MainUnitOfWork.DeckRepository.DeleteAsync(deck, AccountId, CurrentDate))
            throw new ApiException("Delete fail", StatusCode.SERVER_ERROR);
        
        return ApiResponse.Success();
    }
}