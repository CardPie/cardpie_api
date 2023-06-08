using API.Dtos;
using AppCore.Extensions;
using AppCore.Models;
using ClosedXML.Excel;
using Microsoft.Extensions.Logging.Console;

namespace API.Helpers;

public static class ExcelReader
{
    // NEW READER
    public static ImportDeckReaderDto DeckReader(Stream formStream)
    {
        var deck = new ImportDeckReaderDto();
        var deep = 0;

        try
        {
            using (var workBook = new XLWorkbook(formStream))
            {
                var nonEmptyDataRows = workBook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    deep = dataRow.RowNumber();
                    if (dataRow.RowNumber() < 3) continue;
                    deck.Cards.Add(new ImportCardReaderDto()
                    {
                        FrontContent = dataRow.Cell(1).Value.ToString().RemoveSpace(),
                        FrontDescription = dataRow.Cell(3).Value.ToString()?.Trim(),
                        BackContent = dataRow.Cell(4).Value.ToString().Trim()
                    });
                }
            }

            return deck;
        }
        catch (Exception exception)
        {
            throw new ApiException(exception.Message);
        }
    }

}