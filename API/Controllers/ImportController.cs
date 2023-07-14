using API.Dtos;
using API.Services;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class ImportController : BaseController
{
    private readonly IImportService _importService;

    public ImportController(IImportService importService)
    {
        _importService = importService;
    }

    [SwaggerOperation("Import deck")]
    [HttpPost("deck")]
    public async Task<ApiResponse> ImportDeck(IFormFile formFile)
    {
        return await _importService.ImportDeck(formFile);
    }
    
    [SwaggerOperation("Import user")]
    [HttpPost("user")]
    public async Task<ApiResponse> ImportUser(IFormFile formFile)
    {
        return await _importService.ImportUser(formFile);
    }
}