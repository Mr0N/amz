using ExtractInfoAmazon.Interface;
using ExtractInfoAmazon.Model.Dto;

namespace ExtractInfoAmazon.Service
{
    public class InfoOfProduct : IInfoOfProduct
    {
        public IEnumerable<AmazonSaveModel> GetProduct(string asin)
        {
            return new List<AmazonSaveModel>();
        }
    }
}
