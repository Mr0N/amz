using ExtractInfoAmazon.Interface;
using ExtractInfoAmazon.Interface.Db;
using ExtractInfoAmazon.Model;
using ExtractInfoAmazon.Service;
using ExtractInfoAmazon.Service.Background;
using ExtractInfoAmazon.Service.Db;
using Microsoft.EntityFrameworkCore;

namespace ExtractInfoAmazon
{
    public static class Services
    {
        public static void AddCustomService(this IServiceCollection sr)
        {
            sr.AddHostedService<MainBackgroundService>();
            sr.AddTransient<IGetInfoFromPage, GetInfoFromPage>();
            sr.AddTransient<IGetInfoFromHtml, GetInfoFromHtml>();
            sr.AddTransient<IPhotoParsing, PhotoParsing>();
            sr.AddTransient<IReviewCount, ReviewCount>();
            sr.AddTransient<IMainExecute, MainExecute>();
            sr.AddHttpClient();
            sr.AddTransient<IImages, Images>();
            sr.AddTransient<IAllAsin, AllAsin>();
            sr.AddTransient<IWriteToStorage, WriteToStorage>();
            sr.AddTransient<ICheckInExistInDb, CheckInExistInDb>();
           
        }
    }
}
