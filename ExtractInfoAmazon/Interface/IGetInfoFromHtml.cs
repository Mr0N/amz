using ExtractInfoAmazon.Model.Dto;

namespace ExtractInfoAmazon.Interface
{
    public interface IGetInfoFromHtml
    {
        public AmazonSaveModel GetInfo(string html,string asin);
    }
}
