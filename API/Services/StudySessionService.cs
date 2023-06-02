using System.Linq.Expressions;
using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;
using System.Text.Json;

namespace API.Services;

public interface IStudySessionService : IBaseService
{
    public Task<ApiResponses<StudySessionDto>> GetOwnStudySession(StudySessionQueryDto queryDto);
    public Task<ApiResponse<StudySessionDetailDto>> CreateStudySession(CreateStudySessionDto createStudySessionDto);
    public Task<ApiResponse<StudySessionDetailDto>> GetDetailStudySession(Guid id);
    public Task<ApiResponse<StudySessionDetailDto>> UpdateStudySession(Guid id, UpdateStudySessionDto updateStudySessionDto);
    public Task<ApiResponse> DeleteStudySession(Guid id);
    
}

public class StudySessionService : BaseService, IStudySessionService
{
    public StudySessionService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }

    public async Task<ApiResponses<StudySessionDto>> GetOwnStudySession(StudySessionQueryDto queryDto)
    {
        var studySessionDto = await MainUnitOfWork.StudySessionRepository
            .FindResultAsync<StudySessionDto>(new Expression<Func<StudySession, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.CreatorId == AccountId
            }, queryDto.OrderBy, queryDto.Skip(), queryDto.PageSize);

        studySessionDto.Items = await _mapperRepository.MapCreator(studySessionDto.Items.ToList());

        return ApiResponses<StudySessionDto>.Success(
            studySessionDto.Items,
            studySessionDto.TotalCount,
            queryDto.PageSize,
            queryDto.Skip(),
            (int)Math.Ceiling(studySessionDto.TotalCount / (double)queryDto.PageSize)
        );
    }

    public async Task<ApiResponse<StudySessionDetailDto>> CreateStudySession(CreateStudySessionDto createStudySessionDto)
    {
        var studySession = createStudySessionDto.ProjectTo<CreateStudySessionDto, StudySession>();

        studySession.Id = Guid.NewGuid();
        studySession.UserId = AccountId ?? Guid.Empty;
        studySession.StartTime = CurrentDate;

        // Serialize the ListFlashCards into a JSON string
        var cardsStudiedJson = JsonSerializer.Serialize(createStudySessionDto.ListFlashCards);

        studySession.CardsStudied = cardsStudiedJson;

        if (!await MainUnitOfWork.StudySessionRepository.InsertAsync(studySession, AccountId, CurrentDate))
            throw new ApiException("Can't save", StatusCode.SERVER_ERROR);

        return await GetDetailStudySession(studySession.Id);
    }

    public async Task<ApiResponse<StudySessionDetailDto>> GetDetailStudySession(Guid id)
    {
        var detailStudySession = await MainUnitOfWork.StudySessionRepository.FindOneAsync(new Expression<Func<StudySession, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.CreatorId == AccountId
        });

        if (detailStudySession == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);

        var detailStudySessionDto = detailStudySession.ProjectTo<StudySession, StudySessionDetailDto>();

        detailStudySessionDto.DeckDto = await MainUnitOfWork.DeckRepository.FindOneAsync<DeckDto>(
            new Expression<Func<Deck, bool>>[]
            {
                x => !x.DeletedAt.HasValue,
                x => x.Id == detailStudySession.DeckId
            }) ?? new DeckDto();

        detailStudySessionDto.ListFlashCards = JsonSerializer.Deserialize<List<FlashCardInSession>>(detailStudySession.CardsStudied);

        detailStudySessionDto = await _mapperRepository.MapCreator(detailStudySessionDto);
        
        return ApiResponse<StudySessionDetailDto>.Success(detailStudySessionDto);
    }

    public async Task<ApiResponse<StudySessionDetailDto>> UpdateStudySession(Guid id, UpdateStudySessionDto updateStudySessionDto)
    {
        var detailStudySession = await MainUnitOfWork.StudySessionRepository.FindOneAsync(new Expression<Func<StudySession, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.CreatorId == AccountId
        });

        if (detailStudySession == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);
        
        if (detailStudySession.CreatorId != AccountId)
            throw new ApiException("Can't not update other's session", StatusCode.BAD_REQUEST);

        detailStudySession.CardsStudied =
            JsonSerializer.Serialize(updateStudySessionDto.ListFlashCards);
        detailStudySession.CorrectCount = updateStudySessionDto.CorrectCount ?? detailStudySession.CorrectCount;
        detailStudySession.IncorrectCount = updateStudySessionDto.IncorrectCount ?? detailStudySession.IncorrectCount;
        detailStudySession.IsCompleted = updateStudySessionDto.IsCompleted ?? detailStudySession.IsCompleted;

        if (detailStudySession.IsCompleted)
            detailStudySession.EndTime = CurrentDate;
        
        detailStudySession.CurrentCardIndex = updateStudySessionDto.CurrentCardIndex ?? detailStudySession.CurrentCardIndex;

        if(!await MainUnitOfWork.StudySessionRepository.UpdateAsync(detailStudySession, AccountId, CurrentDate))
            throw new ApiException("Can't update", StatusCode.SERVER_ERROR);

        return await GetDetailStudySession(detailStudySession.Id);
    }

    public async Task<ApiResponse> DeleteStudySession(Guid id)
    {
        var detailStudySession = await MainUnitOfWork.StudySessionRepository.FindOneAsync(new Expression<Func<StudySession, bool>>[]
        {
            x => !x.DeletedAt.HasValue,
            x => x.CreatorId == AccountId
        });

        if (detailStudySession == null)
            throw new ApiException("Not found", StatusCode.NOT_FOUND);
        
        if (detailStudySession.CreatorId != AccountId)
            throw new ApiException("Can't not delete other's session", StatusCode.BAD_REQUEST);

        if (!await MainUnitOfWork.StudySessionRepository.DeleteAsync(detailStudySession, AccountId, CurrentDate))
            throw new ApiException("Can't delete", StatusCode.SERVER_ERROR);

        return ApiResponse.Success();
    }
}