using ExtractInfoAmazon.Model;

namespace ExtractInfoAmazon.Interface.Db
{
    public interface IWriteToStorage
    {
        public void SaveToStorage(StorageInfo storageInfo);
        public bool ExistFileInStorage(string file);
    }
}
