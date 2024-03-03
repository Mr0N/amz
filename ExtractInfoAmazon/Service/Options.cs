using AngleSharp.Html.Dom;
using ExtractInfoAmazon.Interface;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ExtractInfoAmazon.Service
{

    public class JsonInfoDto
    {
        public string size_name { get; set; }
        public string ASIN { get; set; }
    }

    
    public class Options(string html) : IOptions
    {

        public bool CheckIsOptions()
        {
            
            return this.GetAsinOptions().Any();
        }

        public IEnumerable<string> GetAsinOptions()
        {
            string obj = Regex.Match(html, "\"asinVariationValues\"[ ]+:[ ]+(?<var>.+)}").Groups["var"].Value;
            if (!string.IsNullOrWhiteSpace(obj))
            {
                obj += "}";
            }
            else return [];
            var res = JsonConvert.DeserializeObject<Dictionary<string, JsonInfoDto>>(obj);
            return res.Select(a=>a.Value?.ASIN);
        }

    }
}
