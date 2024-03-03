using AngleSharp.Html.Parser;
using ExtractInfoAmazon.Interface;
using ExtractInfoFromPage.Interface;
using System.Text.RegularExpressions;

namespace ExtractInfoAmazon.Service
{
    public class ReviewCount(IGetInfo getInfo) : IReviewCount
    {
        public string GetReviewCount(string asin)
        {
            string link = $"https://www.amazon.es/product-reviews/{asin}";
            string html = getInfo.GetRequest(link);
            var doc = new HtmlParser().ParseDocument(html);
            string text = ClearStr(doc.QuerySelector("#filter-info-section > div")?.TextContent);
            //return reviewCount;
            return Regex.Match(text ?? "", "valoraciones totales, (?<count>[0-9]+) con reseñas")?.Groups["count"]?.Value;
        }
        private string ClearStr(string text)
        {
            return Regex.Replace(text ?? "", "[\n ]{2,}", "");
        }
    }
}
