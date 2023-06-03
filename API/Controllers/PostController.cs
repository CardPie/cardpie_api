using API.Dtos;
using API.Services;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class PostController : BaseController
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [SwaggerOperation("Create new post")]
    [HttpPost]
    public async Task<ApiResponse<DetailPostDto>> Create(CreatePostDto createPostDto)
    {
        return await _postService.CreatePost(createPostDto);
    }
    
    [SwaggerOperation("Update post")]
    [HttpPut("{id:guid}")]
    public async Task<ApiResponse<DetailPostDto>> Update(Guid id, UpdatePostDto updatePostDto)
    {
        return await _postService.UpdatePost(id, updatePostDto);
    }
    
    [SwaggerOperation("Get detail post")]
    [HttpGet("{id:guid}")]
    public async Task<ApiResponse<DetailPostDto>> Detail(Guid id)
    {
        return await _postService.GetDetail(id);
    }
    
    [SwaggerOperation("Get list post of folder")]
    [HttpGet]
    public async Task<ApiResponses<PostDto>> Get(PostQueryDto queryDto)
    {
        return await _postService.GetPostOfFolder(queryDto);
    }
    
    [SwaggerOperation("Delete post")]
    [HttpDelete("{id:guid}")]
    public async Task<ApiResponse> Delete(Guid id)
    {
        return await _postService.DeletePost(id);
    }
}