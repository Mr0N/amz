using ExtractInfoAmazon;
using ExtractInfoFromPage.Interface;
using ExtractInfoFromPage.Service.Driver;
using ExtractInfoFromPage.Service;
using Interfaces;
using OpenQA.Selenium;
using ExtractInfoAmazon.Service;
using ExtractInfoAmazon.Model;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IWebDriver>(a => a.GetService<IFactoryWebDriver>().Create());
builder.Services.AddSingleton<IGetLinkFromHtml, GetLinkFromHtml>();
builder.Services.AddSingleton<IGetLinkFromUri, GetLinkFromUri>();
builder.Services.AddSingleton<IGetProgresion, GetProgresion>();
builder.Services.AddSingleton<IGetInfo, GetInfo>();
builder.Services.AddSingleton<IFactoryWebDriver, FactoryWebDriver>();
builder.Services.AddSingleton<IGetInfoFromEbay, GetInfoFromFromAmazon>();
builder.Services.AddDbContext<StorageDbContext>(a =>
{
    a.UseNpgsql(builder.Configuration.GetConnectionString("amazon"));
});
builder.Services.AddCustomService();
var app = builder.Build();
app.Run();

