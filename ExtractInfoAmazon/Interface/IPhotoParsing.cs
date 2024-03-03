using AngleSharp.Html.Dom;

namespace ExtractInfoAmazon.Interface
{
    public interface IPhotoParsing
    {
        public IEnumerable<string> GetPhotoFromHtml(IHtmlDocument doc);
    }
}
