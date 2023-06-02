using API.Dtos;
using AppCore.Models;
using MainData;
using MainData.Repositories;

namespace API.Services;

public interface IStudySessionService : IBaseService
{
    //public Task<ApiResponses<StudySessionDto>> GetOwnSe
}

public class StudySessionService : BaseService, IStudySessionService
{
    public StudySessionService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }
}