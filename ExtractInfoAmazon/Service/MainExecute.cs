using ExtractInfoAmazon.Interface;
using ExtractInfoAmazon.Interface.Db;
using ExtractInfoAmazon.Model;
using ExtractInfoAmazon.Model.Dto;
using ExtractInfoFromPage.Interface;
using OpenQA.Selenium;
using System.Security.Cryptography;
using System.Text;

namespace ExtractInfoAmazon.Service
{
    public class MainExecute(IGetInfoFromPage getInfo,
                            IAllAsin allAsin,
                            IImages image,
                            IWriteToStorage storage,
                            ICheckInExistInDb checkExist,
                            IGetInfo get) : IMainExecute
    {
        public void Execute()
        {
            get.GetRequest("https://www.google.com/");
            Console.WriteLine("Click to console");
            Console.ReadKey();
            var asin = allAsin.GetAllAsin();
            foreach (var item in asin)
            {
                try
                {

                    if (checkExist.ExistInDb(item))
                    {
                        Console.WriteLine("continue");
                        continue;
                    }
                    var info = getInfo.GetInfo(item).ToList();
                    SaveToDb(info);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex?.ToString());
                }
            }

            // Console.WriteLine(obj);
            Console.ReadKey();
        }

        private void SaveToDb(IEnumerable<AmazonSaveModel> info)
        {
            foreach (var item in info)
            {

                var objImage = image.GetImages(item.Photo);
                storage.SaveToStorage(ConvertObj(item, objImage));
            }
        }
        private StorageInfo ConvertObj(AmazonSaveModel save, IEnumerable<ImagesObj> images)
        {
            var obj = new StorageInfo();
            obj.Name = save.Name;
            obj.Asin = save.Asin;
            obj.Brand = save.Brand;
            obj.Characteristics = save.Characteristics;
            obj.Price = save.Price;
            obj.Rating = save.Rating;
            obj.Asin = save.Asin;
            obj.Seller = save.Seller;
            obj.SellerUrl = save.SellerUrl;
            obj.ReviewCount = save.ReviewCount;
            obj.Files = images.Select(a => new FilesInfo(a.name, a.file))
                              .ToList();
            return obj;
        }
    }
}
