using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;

namespace API.Services;

public interface IInteractionService : IBaseService
{
    public Task<ApiResponse<DetailInteractionDto>> CreateInteraction(CreateInteractionDto createInteractionDto);
    public Task<ApiResponse<DetailInteractionDto>> DetailInteraction(Guid id);
    public Task<ApiResponse> DeleteInteraction(Guid id);
    public Task<ApiResponse<DetailInteractionDto>> UpdateInteraction(Guid id, UpdateInteractionDto updateInteractionDto);
}

public class InteractionService : BaseService, IInteractionService
{
    public InteractionService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }
    
    public async Task<ApiResponse<DetailInteractionDto>> CreateInteraction(CreateInteractionDto createInteractionDto)
    {
        var interaction = createInteractionDto.ProjectTo<CreateInteractionDto, Interaction>();
        interaction.Id = Guid.NewGuid();

        if (!await MainUnitOfWork.InteractionRepository.InsertAsync(interaction, AccountId, CurrentDate))
            throw new ApiException("Can't create new interaction", StatusCode.SERVER_ERROR);

        return await DetailInteraction(interaction.Id);
    }

    public async Task<ApiResponse<DetailInteractionDto>> DetailInteraction(Guid id)
    {
        var interaction = await MainUnitOfWork.InteractionRepository.FindOneAsync<DetailInteractionDto>(
            new Expression<Func<Interaction, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.Id == id
            });

        if (interaction == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        var post = await MainUnitOfWork.PostRepository.FindOneAsync<PostDto>(new Expression<Func<Post, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.Id == interaction.PostId
        });

        post = await _mapperRepository.MapCreator(post);
        
        interaction.PostDto = post ?? new PostDto();

        interaction = await _mapperRepository.MapCreator(interaction);

        return ApiResponse<DetailInteractionDto>.Success(interaction);
    }

    public async Task<ApiResponse> DeleteInteraction(Guid id)
    {
        var interaction = await MainUnitOfWork.InteractionRepository.FindOneAsync(
            new Expression<Func<Interaction, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.Id == id
            });

        if (interaction == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);
        
        if (interaction.CreatorId == AccountId)
            throw new ApiException("Can't delete other's interaction", StatusCode.BAD_REQUEST);
        
        if (!await MainUnitOfWork.InteractionRepository.DeleteAsync(interaction, AccountId, CurrentDate))
            throw new ApiException("Can't delete", StatusCode.SERVER_ERROR);

        return ApiResponse.Success();
    }

    public async Task<ApiResponse<DetailInteractionDto>> UpdateInteraction(Guid id, UpdateInteractionDto updateInteractionDto)
    {
        var interaction = await MainUnitOfWork.InteractionRepository.FindOneAsync(
            new Expression<Func<Interaction, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.Id == id
            });

        if (interaction == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);
        
        if (interaction.CreatorId == AccountId)
            throw new ApiException("Can't update other's interaction", StatusCode.BAD_REQUEST);

        interaction.Content = updateInteractionDto.Content ?? interaction.Content;

        if (!await MainUnitOfWork.InteractionRepository.UpdateAsync(interaction, AccountId, CurrentDate))
            throw new ApiException("Can't update", StatusCode.SERVER_ERROR);

        return await DetailInteraction(id);
    }
}