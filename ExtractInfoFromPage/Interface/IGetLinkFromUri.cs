namespace ExtractInfoFromPage.Interface
{
    public interface IGetLinkFromUri
    {
        public IEnumerable<string> GetLinks(string url, int beginMoney, int endMoney);
    }
}
