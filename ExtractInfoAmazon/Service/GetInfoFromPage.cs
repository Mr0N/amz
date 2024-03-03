using ExtractInfoAmazon.Interface;
using ExtractInfoAmazon.Model.Dto;
using ExtractInfoFromPage.Interface;

namespace ExtractInfoAmazon.Service
{
    public class GetInfoFromPage(IGetInfo getInfo, IGetInfoFromHtml parsHtml) : IGetInfoFromPage
    {
        public IEnumerable<AmazonSaveModel> GetInfo(string asin)
        {
            string link = $"https://www.amazon.es/dp/{asin}";
            string response = getInfo.GetRequest(link);
            yield return parsHtml.GetInfo(response, asin);
            var options = new Options(response);
            var ls = options.GetAsinOptions().ToList();
            foreach (var item in ls)
            {
                if (item == asin)
                    continue;
                string requestOptionLink = $"https://www.amazon.es/dp/{item}";
                string responseOptionLink = getInfo.GetRequest(requestOptionLink);
                yield return parsHtml.GetInfo(responseOptionLink, item);
            }
            yield break;
        }

    }
}
