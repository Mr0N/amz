using ExtractInfoAmazon.Model.Dto;

namespace ExtractInfoAmazon.Interface
{
    public interface IInfoOfProduct
    {
        public IEnumerable<AmazonSaveModel> GetProduct(string asin);
    }
}
