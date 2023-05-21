using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;

namespace API.Services;

public interface IFlashCardService : IBaseService
{
    Task<ApiResponses<FlashCardDto>> GetAllFlashCard(FlashCardQueryDto queryDto);
    Task<ApiResponse<DetailFlashCardDto>> CreateCard(CreateFlashCardDto createFlashCardDto);
    Task<ApiResponse<DetailFlashCardDto>> DetailFlashCard(Guid id);
}

public class FlashCardService : BaseService, IFlashCardService
{
    public FlashCardService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }

    public async Task<ApiResponses<FlashCardDto>> GetAllFlashCard(FlashCardQueryDto queryDto)
    {
        var flashCardDtos = await MainUnitOfWork.FlashCardRepository.FindResultAsync<FlashCardDto>(new Expression<Func<FlashCard, bool>>[]
        {
          x => !x.DeletedAt.HasValue  
        }, queryDto.OrderBy, queryDto.Skip(), queryDto.PageSize);

        flashCardDtos.Items = await _mapperRepository.MapCreator(flashCardDtos.Items.ToList());

        return ApiResponses<FlashCardDto>.Success(
            flashCardDtos.Items, 
            flashCardDtos.TotalCount, 
            queryDto.PageSize,
            queryDto.Skip(),
            (int)Math.Ceiling(flashCardDtos.TotalCount/ (double)queryDto.PageSize));
    }

    public async Task<ApiResponse<DetailFlashCardDto>> CreateCard(CreateFlashCardDto createFlashCardDto)
    {
        var flashcard = createFlashCardDto.ProjectTo<CreateFlashCardDto, FlashCard>();
        flashcard.Id = Guid.NewGuid();
        
        if (!await MainUnitOfWork.FlashCardRepository.InsertAsync(flashcard, AccountId, CurrentDate))
            throw new ApiException("Can't save flashcard", StatusCode.SERVER_ERROR);

        return await DetailFlashCard(flashcard.Id);
    }

    public async Task<ApiResponse<DetailFlashCardDto>> DetailFlashCard(Guid id)
    {
        var flashcardDto = await MainUnitOfWork.FlashCardRepository.FindOneAsync<DetailFlashCardDto>(new Expression<Func<FlashCard, bool>>[]
        {
            x  => !x.DeletedAt.HasValue,
            x => x.Id == id
        });

        if (flashcardDto == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);
        
        flashcardDto = await _mapperRepository.MapCreator(flashcardDto);

        return ApiResponse<DetailFlashCardDto>.Success(flashcardDto);
    }
}