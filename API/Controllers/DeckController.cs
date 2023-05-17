using API.Dtos;
using API.Services;
using AppCore.Models;
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

    [SwaggerOperation("Get list deck")]
    [HttpGet]
    public async Task<ApiResponses<DeckDto>> GetDecks([FromQuery]DeckQueryDto deckQueryDto)
    {
        return await _deckService.GetDecks(deckQueryDto);
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
}