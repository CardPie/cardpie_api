using API.Dtos;
using API.Services;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class SavedDeckController : BaseController
{
    private readonly ISavedDeckService _savedDeckService;

    public SavedDeckController(ISavedDeckService savedDeckService)
    {
        _savedDeckService = savedDeckService;
    }

    [SwaggerOperation("Get list saved deck of current account")]
    [HttpGet]
    public async Task<ApiResponses<SavedDeckDto>> GetSavedDecks([FromQuery]SavedDeckQueryDto savedDeckQueryDto)
    {
        return await _savedDeckService.GetOwnSavedDeck(savedDeckQueryDto);
    }
    
    [SwaggerOperation("Get detail saved deck")]
    [HttpGet("{id:guid}")]
    public async Task<ApiResponse<DetailSavedDeckDto>> GetDetailSavedDecks(Guid id)
    {
        return await _savedDeckService.DetailSavedDeck(id);
    }
    
    [SwaggerOperation("Create saved deck")]
    [HttpPost]
    public async Task<ApiResponse<DetailSavedDeckDto>> CreateSavedDecks(CreateSavedDeckDto createSavedDeckDto)
    {
        return await _savedDeckService.CreateSavedDeck(createSavedDeckDto);
    }
    
    [SwaggerOperation("Update saved deck")]
    [HttpPut("{id:guid}")]
    public async Task<ApiResponse<DetailSavedDeckDto>> UpdateSavedDecks(Guid id, [FromBody]UpdateSavedDeckDto updateSavedDeckDto)
    {
        return await _savedDeckService.UpdateSavedDeck(id, updateSavedDeckDto);
    }
    
    [SwaggerOperation("Delete saved deck")]
    [HttpDelete("{id:guid}")]
    public async Task<ApiResponse> DeleteSavedDecks(Guid id)
    {
        return await _savedDeckService.DeleteSavedDeck(id);
    }
}