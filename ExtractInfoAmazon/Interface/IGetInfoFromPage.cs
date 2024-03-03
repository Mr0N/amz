using ExtractInfoAmazon.Model.Dto;

namespace ExtractInfoAmazon.Interface
{

    public interface IGetInfoFromPage
    {
        public IEnumerable<AmazonSaveModel> GetInfo(string asin);
    }
}
