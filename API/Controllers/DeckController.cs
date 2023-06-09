using API.Dtos;
using API.Services;
using AppCore.Models;
using MainData.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class DeckController : BaseController
{
    private readonly IDeckService _deckService;

    public DeckController(IDeckService deckService)
    {
        _deckService = deckService;
    }

    [SwaggerOperation("Get recently seen decks")]
    [HttpGet("recently-seen")]
    public async Task<ApiResponses<DeckDto>> GetRecentlySeenDecks([FromQuery]DeckQueryDto deckQueryDto)
    {
        return await _deckService.GetRecentlySeenDecks(deckQueryDto);
    }
    
    [SwaggerOperation("Get list deck")]
    [HttpGet]
    public async Task<ApiResponses<DeckDto>> GetDecks([FromQuery]DeckQueryDto deckQueryDto)
    {
        return await _deckService.GetDecks(deckQueryDto);
    }
    
    [SwaggerOperation("Get deck of current logged in account")]
    [HttpGet("own")]
    public async Task<ApiResponses<DeckDto>> GetOwnDecks([FromQuery]DeckQueryDto deckQueryDto)
    {
        return await _deckService.GetOwnDeck(deckQueryDto);
    }
    
    [SwaggerOperation("Get detail deck")]
    [HttpGet("{deckId:guid}")]
    public async Task<ApiResponse<DetailDeckDto>> GetDeckDetail(Guid deckId)
    {
        return await _deckService.GetDetailDeck(deckId);
    }
    
    [SwaggerOperation("Create new deck")]
    [HttpPost]
    public async Task<ApiResponse<DetailDeckDto>> CreateDeck(CreateDeckDto createDeckDto)
    {
        return await _deckService.CreateDeck(createDeckDto);
    }
    
    [SwaggerOperation("Update deck")]
    [HttpPut("{id:guid}")]
    public async Task<ApiResponse<DetailDeckDto>> UpdateDeck(Guid id,UpdateDeckDto updateDeckDto)
    {
        return await _deckService.UpdateDeck(id, updateDeckDto);
    }
    
    [SwaggerOperation("Get recommend decks for current logged in account")]
    [HttpGet("recommend")]
    public async Task<ApiResponses<DeckDto>> GetRecommendDeck([FromQuery]DeckQueryDto deckQueryDto)
    {
        return await _deckService.GetRecommendDecks(deckQueryDto);
    }
    
    [SwaggerOperation("Get saved decks for current logged in account")]
    [HttpGet("saved")]
    public async Task<ApiResponses<DeckDto>> GetSavedDeck([FromQuery]DeckQueryDto deckQueryDto)
    {
        return await _deckService.GetOwnSavedDeck(deckQueryDto);
    }
    
    [SwaggerOperation("Update view for decks")]
    [HttpGet("{id:guid}/view")]
    [AllowAnonymous]
    public async Task<ApiResponse> UpdateViewDeck(Guid id)
    {
        return await _deckService.UpdateDeckView(id);
    }
    
    [SwaggerOperation("Delete deck")]
    [HttpDelete("{id:guid}")]
    public async Task<ApiResponse> DeleteDeck(Guid id)
    {
        return await _deckService.DeleteDeck(id);
    }
}