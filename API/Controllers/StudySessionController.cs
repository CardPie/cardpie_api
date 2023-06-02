using API.Dtos;
using API.Services;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class StudySessionController : BaseController
{
    private readonly IStudySessionService _studySessionService;

    public StudySessionController(IStudySessionService studySessionService)
    {
        _studySessionService = studySessionService;
    }
    
    [SwaggerOperation("Create new folder")]
    [HttpPost]
    public async Task<ApiResponse<StudySessionDetailDto>> CreatedStudySession(CreateStudySessionDto studySession)
    {
        return await _studySessionService.CreateStudySession(studySession);
    }
    
    [SwaggerOperation("Get own study session")]
    [HttpGet("own")]
    public async Task<ApiResponses<StudySessionDto>> GetOwnStudySession([FromQuery]StudySessionQueryDto queryDto)
    {
        return await _studySessionService.GetOwnStudySession(queryDto);
    }
    
    [SwaggerOperation("Get detail study session")]
    [HttpGet("{id:guid}")]
    public async Task<ApiResponse<StudySessionDetailDto>> GetDetailStudySession(Guid id)
    {
        return await _studySessionService.GetDetailStudySession(id);
    }
    
    [SwaggerOperation("Update study session")]
    [HttpPut("{id:guid}")]
    public async Task<ApiResponse<StudySessionDetailDto>> UpdateStudySession(Guid id, UpdateStudySessionDto updateStudySessionDto)
    {
        return await _studySessionService.UpdateStudySession(id, updateStudySessionDto);
    }
    
    [SwaggerOperation("Delete study session")]
    [HttpDelete("{id:guid}")]
    public async Task<ApiResponse> DeleteStudySession(Guid id)
    {
        return await _studySessionService.DeleteStudySession(id);
    }
}