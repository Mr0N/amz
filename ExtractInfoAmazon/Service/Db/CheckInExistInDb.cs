using ExtractInfoAmazon.Interface.Db;
using ExtractInfoAmazon.Model;

namespace ExtractInfoAmazon.Service.Db
{
    public class CheckInExistInDb(StorageDbContext db) : ICheckInExistInDb
    {
        public bool ExistInDb(string asin)
        {
            return db.info.Any(a=>a.Asin == asin);
        }
    }
}
