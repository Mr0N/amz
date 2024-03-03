using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using ExtractInfoAmazon.Interface;
using ExtractInfoAmazon.Model.Dto;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ExtractInfoAmazon.Service
{
    public class PhotoParsing : IPhotoParsing
    {
        public IEnumerable<string> GetPhotoFromHtml(IHtmlDocument doc)
        {
           string html = doc.DocumentElement.OuterHtml;
            string json = Regex.Match(html, "'colorImages': { 'initial': (?<json>.+)}").Groups["json"].Value;
            var obj = JsonConvert.DeserializeObject<IEnumerable<ImageJson>>(json);
            return obj?.Select(a =>
            {
                if (a.hiRes is not null)
                    return a.hiRes;
                if (a.large is not null)
                    return a.large;
                return null;
            })?.Where(a => a is not null) ?? [];
          
        }
    }
}
