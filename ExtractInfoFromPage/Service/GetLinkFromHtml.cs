using ExtractInfoFromPage.Interface;
using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;

namespace ExtractInfoFromPage.Service
{
    public class GetLinkFromHtml : IGetLinkFromHtml
    {
        public IEnumerable<string> GetUrlsFromHtml(string html)
        {
            var doc = new HtmlParser().ParseDocument(html);
            var select = doc.QuerySelectorAll("div[class='s-result-list sg-row']");
            foreach (var item in select)
            {
                item.Remove();
            }
            var res = doc.QuerySelectorAll("div[class='sg-col-4-of-24 sg-col-4-of-12 s-result-item s-asin sg-col-4-of-16 sg-col s-widget-spacing-small sg-col-4-of-20'] " +
                "a[class='a-link-normal s-underline-text s-underline-link-text s-link-style a-text-normal']");
            var obj  =  res.SelectMany(a => a.Attributes)
                .Where(a => a.Name?.ToLower() == "href")
                .Select(a=>a.Value)
                .Where(a=>a is not null)
                .Select(a=>$"https://www.amazon.es{a}")
                .ToList();
            return obj;
           
        }
    }
}
