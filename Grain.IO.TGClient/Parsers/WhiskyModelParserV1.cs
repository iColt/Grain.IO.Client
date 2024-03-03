using Grain.IO.TGClient.Common;
using Grain.IO.TGClient.Models;
using HtmlAgilityPack;

namespace Grain.IO.TGClient.Parsers;

public class WhiskyModelParserV1
{
    private const string WHISKY_TABLE_ELEMENT_NAME = "//table[contains(@class, 'whiskytable') and contains(@class, 'table-clickable')"
                                                    +" and contains(@class, 'compositor') and contains(@class, 'table-sortable')]/tbody";

    public static IList<WhiskyModel> Parse() {
        // Load HTML content from the URL
        HtmlWeb web = new();
        HtmlDocument doc = web.Load(Constants.TestWhiskyBaseURL);

        // Select the table with the specified class name
        HtmlNode table = doc.DocumentNode.SelectSingleNode(WHISKY_TABLE_ELEMENT_NAME);

        // Parse table rows
        List<WhiskyModel> whiskyList = new();
        foreach (HtmlNode row in table.SelectNodes(".//tr"))
        {
            HtmlNodeCollection cells = row.SelectNodes(".//td");
            if (cells != null && cells.Count >= 4)
            {
                //TODO: parse needed values
                WhiskyModel whisky = new(
                    cells[0].InnerText.Trim(),
                    cells[1].InnerText.Trim(),
                    cells[2].InnerText.Trim(),
                    cells[3].InnerText.Trim()
                );
                whiskyList.Add( whisky );
            }
        }

        // Display parsed data
        //foreach (var whisky in whiskyList)
        //{
        //    Console.WriteLine($"Name: {whisky.Name}, Distillery: {whisky.Distillery}, Age: {whisky.Age}, ABV: {whisky.ABV}");
        //}

        return whiskyList;
    }

}
