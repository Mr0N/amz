using ExtractInfoAmazon.Interface.Db;

namespace ExtractInfoAmazon.Service.Db
{
    public class AllAsin : IAllAsin
    {
        public IEnumerable<string> GetAllAsin()
        {
            return File.ReadAllLines("asin.txt");
        }
    }
}
