using ExtractInfoAmazon.Interface.Db;
using ExtractInfoAmazon.Model;
using ExtractInfoAmazon.Model.Db;
using System.Diagnostics;
using System.Reflection;

namespace ExtractInfoAmazon.Service.Db
{
    public class WriteToStorage(StorageDbContext dbContext) : IWriteToStorage
    {

        const string storagePhotoPatch = "Photo";
        string dirExeFile =
            Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        public bool ExistFileInStorage(string file)
        {
            string saveFile = Path.Combine(dirExeFile, storagePhotoPatch);
            return File.Exists(Path.Combine(saveFile, file));
        }

        public void SaveToStorage(StorageInfo storageInfo)
        {
            string saveFile = Path.Combine(dirExeFile, storagePhotoPatch);
            Directory.CreateDirectory(saveFile);
            var ls = new List<string>();
            foreach (var item in storageInfo.Files)
            {
                if (!this.ExistFileInStorage(item.nameFile))
                {
                    using var write = File.OpenWrite(Path.Combine(saveFile, item.nameFile));
                    item.stream.Position = 0;
                    item.stream.CopyTo(write);
                }
                ls.Add(item.nameFile);
            }
            Console.WriteLine(storageInfo);
            dbContext.info.Add(ConvertToDbType(storageInfo, ls));
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();

        }
        private SaveInfoAmazon ConvertToDbType(StorageInfo storage,List<string> files)
        {
            var obj = new SaveInfoAmazon();
            obj.Asin = storage.Asin;
            obj.Brand = storage.Brand;
            obj.Characteristics = storage.Characteristics;
            obj.Price = storage.Price;
            obj.Rating = storage.Rating;
            obj.ReviewCount = storage.ReviewCount;
            obj.Seller = storage.Seller;
            obj.SellerUrl = storage.SellerUrl;
            obj.Name = storage.Name;
            obj.Files = files.Select(a => new FilesInfoAmazon() { Name = a })
                             .ToList();
            return obj;
        }
    }

}
