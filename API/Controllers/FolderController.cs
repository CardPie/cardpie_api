using API.Dtos;
using API.Services;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class FolderController : BaseController
{
    private readonly IFolderService _folderService;

    public FolderController(IFolderService folderService)
    {
        _folderService = folderService;
    }

    [SwaggerOperation("Get list folder")]
    [HttpGet]
    public async Task<ApiResponses<FolderDto>> GetDecks([FromQuery]FolderQuery folderQuery)
    {
        return await _folderService.GetFolders(folderQuery);
    }
}