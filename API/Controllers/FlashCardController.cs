using API.Dtos;
using API.Services;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class FlashCardController : BaseController
{
    private readonly IFlashCardService _flashCardService;

    public FlashCardController(IFlashCardService flashCardService)
    {
        _flashCardService = flashCardService;
    }
    
    
    [SwaggerOperation("Get list flashcard")]
    [HttpGet]
    public async Task<ApiResponses<FlashCardDto>> GetFlashCard([FromQuery]FlashCardQueryDto queryDto)
    {
        return await _flashCardService.GetAllFlashCard(queryDto);
    }
    
    [SwaggerOperation("Get detail flashcard")]
    [HttpGet("{id:guid}")]
    public async Task<ApiResponse<DetailFlashCardDto>> GetDetailFlashCard(Guid id)
    {
        return await _flashCardService.DetailFlashCard(id);
    }
    
    [SwaggerOperation("Create flashcard")]
    [HttpPost]
    public async Task<ApiResponse<DetailFlashCardDto>> CreateFlashCard(CreateFlashCardDto createFlashCardDto)
    {
        return await _flashCardService.CreateCard(createFlashCardDto);
    }
}