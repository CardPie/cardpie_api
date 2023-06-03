using API.Dtos;
using API.Services;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class InteractionController : BaseController
{
    private readonly IInteractionService _interactionService;

    public InteractionController(IInteractionService interactionService)
    {
        _interactionService = interactionService;
    }
    
    [SwaggerOperation("Create new interaction")]
    [HttpPost]
    public async Task<ApiResponse<DetailInteractionDto>> Create(CreateInteractionDto createInteractionDto)
    {
        return await _interactionService.CreateInteraction(createInteractionDto);
    }
    
    [SwaggerOperation("Update interaction")]
    [HttpPut("{id:guid}")]
    public async Task<ApiResponse<DetailInteractionDto>> Update(Guid id, UpdateInteractionDto updateInteractionDto)
    {
        return await _interactionService.UpdateInteraction(id, updateInteractionDto);
    }
    
    [SwaggerOperation("Get detail interaction")]
    [HttpGet("{id:guid}")]
    public async Task<ApiResponse<DetailInteractionDto>> Detail(Guid id)
    {
        return await _interactionService.DetailInteraction(id);
    }
    
    [SwaggerOperation("Delete interaction")]
    [HttpDelete("{id:guid}")]
    public async Task<ApiResponse> Delete(Guid id)
    {
        return await _interactionService.DeleteInteraction(id);
    }
}