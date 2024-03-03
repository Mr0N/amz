namespace ExtractInfoAmazon.Interface.Db
{
    public interface ICheckInExistInDb
    {
        public bool ExistInDb(string asin);
    }
}
