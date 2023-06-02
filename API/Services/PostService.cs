using System.Linq.Expressions;
using API.Dtos;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;

namespace API.Services;

public interface IPostService : IBaseService
{
    Task<ApiResponses<PostDto>> GetPostOfFolder(PostQueryDto queryDto);
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
}