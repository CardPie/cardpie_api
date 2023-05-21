using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;

namespace API.Services;

public interface IFolderService : IBaseService
{
    public Task<ApiResponses<FolderDto>> GetFolders(FolderQuery folderQuery);

    Task<ApiResponses<FolderDto>> GetFolderOfAccount(FolderQuery folderQuery);
    
    Task<ApiResponse<DetailFolderDto>> GetDetail(Guid id);
    
    Task<ApiResponse<DetailFolderDto>> CreateFolder(CreateFolder createFolder);
    
    Task<ApiResponse<DetailFolderDto>> UpdateFolder(Guid id, UpdateFolder updateFolder);

}

public class FolderService : BaseService, IFolderService
{
    public FolderService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }
    public async Task<ApiResponses<FolderDto>> GetFolders(FolderQuery folderQuery)
    {
        var folderDtos = await MainUnitOfWork.FolderRepository.FindResultAsync<FolderDto>(new Expression<Func<Folder, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => (x.IsPublic != null && x.IsPublic == true) || x.CreatorId == AccountId
        }, folderQuery.OrderBy, folderQuery.Skip(), folderQuery.PageSize);

        folderDtos.Items = await _mapperRepository.MapCreator(folderDtos.Items.ToList());
        
        return ApiResponses<FolderDto>.Success(
            folderDtos.Items,
            folderDtos.TotalCount,
            folderQuery.PageSize,
            folderQuery.Skip(),
            (int)Math.Ceiling(folderDtos.TotalCount/ (double)folderQuery.PageSize)
        );
    }
    
    public async Task<ApiResponses<FolderDto>> GetFolderOfAccount(FolderQuery folderQuery)
    {
        var folderDtos = await MainUnitOfWork.FolderRepository.FindResultAsync<FolderDto>(new Expression<Func<Folder, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.CreatorId == AccountId
        }, folderQuery.OrderBy, folderQuery.Skip(), folderQuery.PageSize);

        folderDtos.Items = await _mapperRepository.MapCreator(folderDtos.Items.ToList());
        
        return ApiResponses<FolderDto>.Success(
            folderDtos.Items,
            folderDtos.TotalCount,
            folderQuery.PageSize,
            folderQuery.Skip(),
            (int)Math.Ceiling(folderDtos.TotalCount/ (double)folderQuery.PageSize)
        );
    }

    public async Task<ApiResponse<DetailFolderDto>> GetDetail(Guid id)
    {
        var folderDto = await MainUnitOfWork.FolderRepository.FindOneAsync<DetailFolderDto>(
            new Expression<Func<Folder, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.IsPublic != null && x.IsPublic == true,
                x => x.Id == id
            });

        if (folderDto == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        var deckDtos = await MainUnitOfWork.DeckRepository.FindAsync<DeckDto>(new Expression<Func<Deck, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.FolderId == folderDto.Id
        }, null);

        folderDto.ListDeck = deckDtos;

        folderDto = await _mapperRepository.MapCreator(folderDto);

        return ApiResponse<DetailFolderDto>.Success(folderDto);
    }

    public async Task<ApiResponse<DetailFolderDto>> CreateFolder(CreateFolder createFolder)
    {
        var folder = createFolder.ProjectTo<CreateFolder, Folder>();
        folder.Id = Guid.NewGuid();
        
        if (!await MainUnitOfWork.FolderRepository.InsertAsync(folder, AccountId, CurrentDate))
            throw new ApiException("Error while saving the folder", StatusCode.SERVER_ERROR);

        return await GetDetail(folder.Id);
    }

    public async Task<ApiResponse<DetailFolderDto>> UpdateFolder(Guid id, UpdateFolder updateFolder)
    {
        var folderDto = await MainUnitOfWork.FolderRepository.FindOneAsync(
            new Expression<Func<Folder, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.Id == id
            });

        if (folderDto == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        folderDto.FolderName = updateFolder.FolderName ?? folderDto.FolderName;
        folderDto.IsPublic = updateFolder.IsPublic ?? folderDto.IsPublic;

        if (!await MainUnitOfWork.FolderRepository.UpdateAsync(folderDto, AccountId, CurrentDate))
            throw new ApiException("Error while updating the folder", StatusCode.SERVER_ERROR);

        return await GetDetail(folderDto.Id);
    }
    
}