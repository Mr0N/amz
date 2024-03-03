namespace ExtractInfoFromPage.Interface
{
    public interface IGetLinkFromHtml
    {
        public IEnumerable<string> GetUrlsFromHtml(string html);
    }
}
