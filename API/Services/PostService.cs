using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;

namespace API.Services;

public interface IPostService : IBaseService
{
    Task<ApiResponses<PostDto>> GetPostOfFolder(PostQueryDto queryDto);
    Task<ApiResponse<DetailPostDto>> GetDetail(Guid id);
    Task<ApiResponse<DetailPostDto>> UpdatePost(Guid id, UpdatePostDto updatePostDto);
    Task<ApiResponse<DetailPostDto>> CreatePost(CreatePostDto createPostDto);
    Task<ApiResponse> DeletePost(Guid id);
}

public class PostService : BaseService, IPostService
{
    public PostService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }
    
    public async Task<ApiResponses<PostDto>> GetPostOfFolder(PostQueryDto queryDto)
    {
        var post = await MainUnitOfWork.PostRepository.FindResultAsync<PostDto>(new Expression<Func<Post, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => queryDto.FolderId == null || x.FolderId == queryDto.FolderId
        }, queryDto.OrderBy, queryDto.Skip(), queryDto.PageSize);

        post.Items = await _mapperRepository.MapCreator(post.Items.ToList());

        return ApiResponses<PostDto>.Success(
            post.Items,
            post.TotalCount,
            queryDto.Skip(),
            (int)Math.Ceiling(post.TotalCount / (double)queryDto.PageSize)
        );
    }

    public async Task<ApiResponse<DetailPostDto>> GetDetail(Guid id)
    {
        var post = await MainUnitOfWork.PostRepository.FindOneAsync<DetailPostDto>(new Expression<Func<Post, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == id
        });

        if (post == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        var interactions = await MainUnitOfWork.InteractionRepository.FindAsync<InteractionDto>(
            new Expression<Func<Interaction, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.PostId == id
            }, null);

        interactions = await _mapperRepository.MapCreator(interactions.ToList());
        
        post.InteractionDtos = interactions;

        post = await _mapperRepository.MapCreator(post);

        return ApiResponse<DetailPostDto>.Success(post);
    }

    public async Task<ApiResponse<DetailPostDto>> UpdatePost(Guid id, UpdatePostDto updatePostDto)
    {
        var post = await MainUnitOfWork.PostRepository.FindOneAsync(new Expression<Func<Post, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == id
        });

        if (post == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);
        
        if (post.CreatorId == AccountId)
            throw new ApiException("Can't delete other's post", StatusCode.BAD_REQUEST);

        post.Type = updatePostDto.Type ?? post.Type;
        post.Content = updatePostDto.Content ?? post.Content;

        if (!await MainUnitOfWork.PostRepository.UpdateAsync(post, AccountId, CurrentDate))
            throw new ApiException("Can't update", StatusCode.SERVER_ERROR);

        return await GetDetail(id);
    }

    public async Task<ApiResponse<DetailPostDto>> CreatePost(CreatePostDto createPostDto)
    {
        var post = createPostDto.ProjectTo<CreatePostDto, Post>();

        post.Id = Guid.NewGuid();
        post.Status = PostStatus.Newly;

        if (!await MainUnitOfWork.PostRepository.InsertAsync(post, AccountId, CurrentDate))
            throw new ApiException("Can't create new", StatusCode.SERVER_ERROR);

        return await GetDetail(post.Id);
    }

    public async Task<ApiResponse> DeletePost(Guid id)
    {
        var post = await MainUnitOfWork.PostRepository.FindOneAsync(new Expression<Func<Post, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == id
        });

        if (post == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        if (post.CreatorId == AccountId)
            throw new ApiException("Can't delete other's post", StatusCode.BAD_REQUEST);

        if (!await MainUnitOfWork.PostRepository.DeleteAsync(post, AccountId, CurrentDate))
            throw new ApiException("Can't not delete", StatusCode.SERVER_ERROR);
        
        return ApiResponse.Success();
    }
}