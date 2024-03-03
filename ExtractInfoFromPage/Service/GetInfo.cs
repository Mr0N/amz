using ExtractInfoFromPage.Interface;

namespace ExtractInfoFromPage.Service
{
    public class GetInfo(IGetInfoFromEbay getInfo) : IGetInfo
    {
        public string GetRequest(string url)
        {
            return getInfo.GetInfo(url);
        }
    }
}
