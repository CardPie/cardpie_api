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

        try
        {
            using (var workBook = new XLWorkbook(formStream))
            {
                var worksheet = workBook.Worksheet(1);

                // Get deck name from the first row
                deck.Name = worksheet.Cell(1, 1).Value.ToString().Trim();

                // Get deck description from the second row
                deck.Description = worksheet.Cell(2, 1).Value.ToString().Trim();
                
                // Get deck IsPublic value from the third row
                deck.IsPublic = bool.Parse(worksheet.Cell(3, 1).Value.ToString().Trim());

                // Read card data starting from the third row
                var nonEmptyDataRows = worksheet.RowsUsed().Skip(3);
                foreach (var dataRow in nonEmptyDataRows)
                {
                    deck.Cards.Add(new ImportCardReaderDto()
                    {
                        FrontContent = dataRow.Cell(1).Value.ToString().Trim(),
                        FrontDescription = dataRow.Cell(2).Value.ToString()?.Trim(),
                        BackContent = dataRow.Cell(3).Value.ToString().Trim()
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