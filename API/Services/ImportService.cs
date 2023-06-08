using API.Dtos;
using API.Helpers;
using AppCore.Extensions;
using AppCore.Models;
using MainData;
using MainData.Entities;
using MainData.Repositories;

namespace API.Services;

public interface IImportService : IBaseService
{
    public Task<ApiResponse> ImportDeck(IFormFile formFile);
}

public class ImportService : BaseService, IImportService
{
    public ImportService(MainUnitOfWork mainUnitOfWork, IHttpContextAccessor httpContextAccessor, IMapperRepository mapperRepository) : base(mainUnitOfWork, httpContextAccessor, mapperRepository)
    {
    }

    public async Task<ApiResponse> ImportDeck(IFormFile formFile)
    {
        if (formFile == null)
            throw new ApiException("Not recognize file");
        string[] extensions = { ".xlsx", ".xls" };
        if (!extensions.Contains(Path.GetExtension(formFile.FileName)))
            throw new ApiException("Not supported file extension");
        
        var deckReader = ExcelReader.DeckReader(formFile.OpenReadStream());

        var deck = deckReader.ProjectTo<ImportDeckReaderDto, Deck>();
        var deckId = Guid.NewGuid();
        deck.Id = deckId;
        
        var cards = deckReader.Cards.ProjectTo<ImportCardReaderDto, FlashCard>();

        foreach (var card in cards)
        {
            card.DeckId = deckId;
        }

        if (!await MainUnitOfWork.DeckRepository.InsertAsync(deck, AccountId, CurrentDate))
            throw new ApiException("Import deck fail", StatusCode.SERVER_ERROR);
        
        if (!await MainUnitOfWork.FlashCardRepository.InsertAsync(cards, AccountId, CurrentDate))
            throw new ApiException("Import cards fail", StatusCode.SERVER_ERROR);

        return ApiResponse.Success();
    }
}