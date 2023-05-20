using System.Linq.Expressions;
using API.Dtos;
using AppCore.Models;
using MainData;
using MainData.Entities;

namespace API.Services;

public interface IFolderService : IBaseService
{
    public Task<ApiResponses<FolderDto>> GetFolders(FolderQuery folderQuery);
}

public class FolderService : BaseService, IFolderService
{
    public FolderService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor) : base(mainUnitOfWork, httpContextAccessor)
    {
    }

    public async Task<ApiResponses<FolderDto>> GetFolders(FolderQuery folderQuery)
    {
        var folderDtos = await MainUnitOfWork.FolderRepository.FindResultAsync<FolderDto>(new Expression<Func<Folder, bool>>[]
        {
            x => !x.DeletedAt.HasValue
        }, folderQuery.OrderBy, folderQuery.Skip(), folderQuery.PageSize);

        return ApiResponses<FolderDto>.Success(
            folderDtos.Items,
            folderDtos.TotalCount,
            folderQuery.PageSize,
            folderQuery.Skip(),
            (int)Math.Ceiling(folderDtos.TotalCount/ (double)folderQuery.PageSize)
        );
    }
}