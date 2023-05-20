using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;

namespace API.Services;

public interface IDeckService : IBaseService
{
    public Task<ApiResponses<DeckDto>> GetDecks(DeckQueryDto deckQueryDto);
    public Task<ApiResponse<DetailDeckDto>> GetDetailDeck(Guid deckId);
    public Task<ApiResponse<DetailDeckDto>> CreateDeck(CreateDeckDto createDeckDto);
}

public class DeckService : BaseService, IDeckService
{
    public DeckService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor) : base(mainUnitOfWork, httpContextAccessor)
    {
    }

    public async Task<ApiResponses<DeckDto>> GetDecks(DeckQueryDto deckQueryDto)
    {
        var decks = await MainUnitOfWork.DeckRepository.FindResultAsync<DeckDto>(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.IsPublic
        }, deckQueryDto.OrderBy, deckQueryDto.Skip(), deckQueryDto.PageSize);

        return ApiResponses<DeckDto>.Success(
            decks.Items,
            decks.TotalCount,
            deckQueryDto.PageSize,
            deckQueryDto.Skip(),
            (int)Math.Ceiling(decks.TotalCount/ (double)deckQueryDto.PageSize)
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
        
        var flashCards = await MainUnitOfWork.FlashCardRepository.FindAsync<FlashCardDto>(new Expression<Func<FlashCard, bool>>[]
        {
            x => !x.DeletedAt.HasValue, 
            x => x.DeckId == deckDto.Id
        }, null);

        deckDto.ListFlashCards = flashCards;
        
        var studySessions = await MainUnitOfWork.StudySessionRepository.FindAsync<StudySessionDto>(new Expression<Func<StudySession, bool>>[]
        {
            x => !x.DeletedAt.HasValue, 
            x => x.DeckId == deckDto.Id,
            x => x.UserId == AccountId,
        }, null);

        deckDto.ListStudySessions = studySessions;

        return ApiResponse<DetailDeckDto>.Success(deckDto);
    }

    public async Task<ApiResponse<DetailDeckDto>> CreateDeck(CreateDeckDto createDeckDto)
    {
        var deck = createDeckDto.ProjectTo<CreateDeckDto, Deck>();
        deck.Id = Guid.NewGuid();
        deck.UserId = AccountId;

        if (!(await MainUnitOfWork.DeckRepository.InsertAsync(deck, AccountId, CurrentDate)))
            throw new ApiException("Save fail", StatusCode.SERVER_ERROR);
        
        var flashCards = createDeckDto.ListFlashCards.ProjectTo<CreateFlashCardDto, FlashCard>();
        
        flashCards.ForEach(item => item.DeckId = deck.Id);
        
        if (!(await MainUnitOfWork.FlashCardRepository.InsertAsync(flashCards, AccountId, CurrentDate)))
            throw new ApiException("Save fail", StatusCode.SERVER_ERROR);

        return await GetDetailDeck(deck.Id);
    }
}