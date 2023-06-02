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
    public async Task<ApiResponses<FolderDto>> GetFolders([FromQuery]FolderQuery folderQuery)
    {
        return await _folderService.GetFolders(folderQuery);
    }
    
    [SwaggerOperation("Get folder of the current account")]
    [HttpGet("own-folder")]
    public async Task<ApiResponses<FolderDto>> GetFolderOfCurrentAccount([FromQuery]FolderQuery folderQuery)
    {
        return await _folderService.GetFolderOfAccount(folderQuery);
    }
    
    [SwaggerOperation("Get detail of folder")]
    [HttpGet("{id:guid}")]
    public async Task<ApiResponse<DetailFolderDto>> GetDetailFolder(Guid id)
    {
        return await _folderService.GetDetail(id);
    }
    
     
    [SwaggerOperation("Create new folder")]
    [HttpPost]
    public async Task<ApiResponse<DetailFolderDto>> CreatedFolder(CreateFolder folder)
    {
        return await _folderService.CreateFolder(folder);
    }
    
    [SwaggerOperation("Update folder information")]
    [HttpPut("{id:guid}")]
    public async Task<ApiResponse<DetailFolderDto>> UpdateFolder(Guid id, UpdateFolder folder)
    {
        return await _folderService.UpdateFolder(id, folder);
    }
    
    [SwaggerOperation("Delete folder")]
    [HttpDelete("{id:guid}")]
    public async Task<ApiResponse> DeleteFolder(Guid id)
    {
        return await _folderService.DeleteFolder(id);
    }
}