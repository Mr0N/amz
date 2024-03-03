using ExtractInfoFromPage.Interface;
using Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Specialized;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExtractInfoFromPage.Service
{
    public class GetLinkFromUri(
        IGetLinkFromHtml getLink,
        IGetProgresion progresion,
        IGetInfo getInfo) : IGetLinkFromUri
    {

        private IEnumerable<(int begin, int end)> GetRange(string url, int beginMoney, int endMoney)
        {
            var en = progresion.GetInfoProgresion(beginMoney, endMoney);
            foreach (var item in en)
            {
                checked
                {
                    yield return (item.Key * 100, item.Value * 100);
                }
            }
        }
        public IEnumerable<string> GetLinks(string url, int beginMoney, int endMoney)
        {
            foreach (var element in GetRange(url, beginMoney, endMoney))
            {

                string uriInfo = ChangeRangeFilterMoneyInUri(url, element.begin, element.end);
                var en = GetLinkFromRangeMoney(uriInfo);
                foreach (var item in en)
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<string> GetLinkFromRangeMoney(string url)
        {
            for (int i = 1; i < 8; i++)
            {

                string pageUri = ChangePageInUri(url, i);
                string htmlContent = getInfo.GetRequest(pageUri);
                var ls = getLink.GetUrlsFromHtml(htmlContent).ToList();
                if (ls.Count == 0) break;
                foreach (var item in ls)
                {
                    yield return item;
                }
            }
            ///return [];
        }

        private string ChangeRangeFilterMoneyInUri(string url, int begin, int end)
        {
            var builder = new UriBuilder(new Uri(url));
            var dict = HttpUtility.ParseQueryString(builder.Query);
            dict.Remove("rh");
            dict.Set("rh", $"p_36:{begin}-{end}");
            var result = Convert(dict);
            string query = string.Join("&", result.Select(a => $"{a.key}={a.value}"));
            builder.Query = query;
            return builder.ToString();
        }
        private IEnumerable<(string key, string value)> Convert(NameValueCollection collection)
        {
            foreach (var element in collection.AllKeys)
            {
                foreach (var value in collection.GetValues(element))
                {
                    yield return (element, value);
                }
            }
        }
        private string ChangePageInUri(string url, int page)
        {
            var builder = new UriBuilder(new Uri(url));
            var dict = HttpUtility.ParseQueryString(builder.Query);
            dict.Remove("ref");
            dict.Remove("page");
            dict.Set("page", page.ToString());
            dict.Set("ref", $"sr_pg_{page}");
            var result = Convert(dict);
            string query = string.Join("&", result.Select(a => $"{a.key}={a.value}"));
            builder.Query = query;
            return builder.ToString();
        }


    }
}
