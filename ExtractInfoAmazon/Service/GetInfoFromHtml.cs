using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ExtractInfoAmazon.Interface;
using ExtractInfoAmazon.Model.Dto;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ExtractInfoAmazon.Service
{
    public class GetInfoFromHtml(IReviewCount review, IPhotoParsing getPhotoLink) : IGetInfoFromHtml
    {
        private string GetElement(string selector, IHtmlDocument doc)
        {
            string text = doc.QuerySelector(selector)?.TextContent ?? "";
            return text;
        }
        private string GetBrand(IHtmlDocument doc, IEnumerable<KeyValuePair<string, string>> en)
        {
            var element = doc.QuerySelectorAll("div[id=bylineInfo_feature_div] a[id=bylineInfo]")
                .Where(a => a?.TextContent?.Contains("Marca:") ?? false)
                .Select(a => a.TextContent)
                .FirstOrDefault();
            //var element = GetElement("", doc);
            Console.WriteLine(string.IsNullOrWhiteSpace(element));
            if (!string.IsNullOrWhiteSpace(element))
                return element.Replace("Marca:", "").Trim(' ');
            var first = en.FirstOrDefault(a => a.Key == "Marca");
            return first.Value;
        }
        private (string name, string url) GetSeller(IHtmlDocument doc)
        {
            var sel = doc.QuerySelectorAll("#bylineInfo")
                .FirstOrDefault(a =>
                {
                    return a.Attributes.Any(a => a.Name == "href" && a.Value.StartsWith("/stores/"));
                });
            string name = sel?.TextContent;
            string href = sel?.Attributes.FirstOrDefault(a => a.Name == "href").Value;
            if (href is not null)
                href = $"https://www.amazon.es{href}";
            return (name, href);
        }
        private decimal? GetPrice(IHtmlDocument doc)
        {

            string priceText = doc.QuerySelector("#corePrice_feature_div .a-price span[class='a-offscreen']")?.TextContent ?? "";
            priceText = ClearStr(priceText);
            decimal? price = ParseDecimal(priceText);
            if (price == null)
            {
                priceText = doc.QuerySelector("div#corePriceDisplay_desktop_feature_div span.a-offscreen")?.TextContent ?? "";
                priceText = ClearStr(priceText);
                price = ParseDecimal(priceText);

            }

            if (price == null)
            {
                priceText = doc.QuerySelector("#tmmSwatches .a-color-price")?.TextContent ?? "";
                priceText = ClearStr(priceText);
                price = ParseDecimal(priceText);

            }
            return price;
        }
        private string ClearStr(string input)
        {
            // Використовуємо регулярний вираз для відбору лише цифр, ком та крапок
            string pattern = @"[^0-9,\.]";
            string result = Regex.Replace(input, pattern, "");

            return result;
        }
        private decimal? ParseDecimal(string input)
        {
            NumberFormatInfo formatInfo = new NumberFormatInfo();
            formatInfo.CurrencyDecimalSeparator = ",";

            if (decimal.TryParse(input, NumberStyles.Any, formatInfo, out decimal result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetSpecification(IHtmlDocument doc)
        {
            var en = doc.QuerySelectorAll(".prodDetTable");
            var ls = new List<KeyValuePair<string, string>>();
            foreach (var item in en)
            {
                var res = ParseTableWithItems(item);
                foreach (var element in res)
                {
                    yield return element;
                }
            }
        }

        IEnumerable<KeyValuePair<string, string>> ParseTableWithItems(IElement document)
        {

            var rows = document.QuerySelectorAll("tbody tr");

            var data = new List<KeyValuePair<string, string>>();

            foreach (var row in rows)
            {
                var key = ClearElement(row.QuerySelector("th"))?.TextContent.Trim();
                var value = ClearElement(row.QuerySelector("td"))?.TextContent.Trim();

                if (!string.IsNullOrWhiteSpace(key))
                {
                    yield return new KeyValuePair<string, string>(key, value);
                }
            }
        }
        private IElement ClearElement(IElement element)
        {
            if (element is null) return element;
            foreach (var item in element.QuerySelectorAll("script"))
            {
                item.Remove();
            }
            foreach (var item in element.QuerySelectorAll("style"))
            {
                item.Remove();
            }
            return element;
        }
        private string GetReviewCount(string asin)
        {
            return review.GetReviewCount(asin);
        }
        private double? GetRating(IHtmlDocument doc)
        {
            string text = doc.QuerySelector("span[data-hook='rating-out-of-text']")?.TextContent;
            string obj = Regex.Match(text ?? "", "(?<number>[\\d,]+) de 5")?.Groups["number"]?.Value;
            var numberFormat = new NumberFormatInfo()
            {
                NumberDecimalSeparator = ",",
            };
            if (double.TryParse(obj, numberFormat, out var res))
            {
                return res;
            }
            return null;
        }
        private IEnumerable<string> GetPhoto(IHtmlDocument doc)
        {
            string html = doc.ToHtml();
            return Regex.Matches(html, "\"hiRes\":\"(?<link>[^\"]+)\"")
                 .Select(a => a.Groups["link"].Value)
                 .ToList();
        }
        //private IEnumerable<string> GetPhoto(IHtmlDocument doc)
        //{
        //    var ls = doc.QuerySelectorAll(".imgTagWrapper img");
        //    foreach (var item in ls)
        //    {
        //        var attr = item.Attributes;
        //        var element = attr?.GetNamedItem("data-old-hires");
        //        if (element is not null)
        //        {
        //            yield return element.Value;
        //            continue;
        //        }
        //        string uri = attr?.GetNamedItem("src")?.Value;
        //        if (uri is not null)
        //        {
        //            yield return uri;
        //        }
        //    }

        //}
        private string ClearHtml(string text)
        {
            return text?.Replace(";", "")
                       ?.Replace(":", "")
                       ?.Replace("\r", "")
                       ?.Replace("\n", "") ?? "";
        }
        public AmazonSaveModel GetInfo(string html, string asin)
        {
            var obj = new AmazonSaveModel();
            var document = new HtmlParser().ParseDocument(html);
            var specification = GetSpecification(document).ToList();
           // obj.Photo = GetPhoto(document).ToList();
            obj.Characteristics = string.Join(";",
                specification.Select(a => $"{ClearHtml(a.Key)}:{ClearHtml(a.Value)}"));
            obj.Brand = GetBrand(document, specification);
            obj.Name = GetElement("#centerCol #productTitle", document).Replace("  ", " ")
                .Trim(' ');
            obj.Price = GetPrice(document);
            var res = GetSeller(document);
            obj.Seller = res.name?.Replace("Visita la tienda de ", "");
            obj.SellerUrl = res.url;
            obj.ReviewCount = GetReviewCount(asin);
            obj.Rating = GetRating(document);
            obj.Photo = getPhotoLink.GetPhotoFromHtml(document).ToList();
            obj.Asin = asin;
            return obj;
        }
    }
}
