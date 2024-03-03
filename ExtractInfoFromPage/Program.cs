using ExtractInfoFromPage.Interface;
using ExtractInfoFromPage.Service;
using ExtractInfoFromPage.Service.Driver;
using Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IWebDriver>(a=>a.GetService<IFactoryWebDriver>().Create());
builder.Services.AddSingleton<IGetLinkFromHtml, GetLinkFromHtml>();
builder.Services.AddSingleton<IGetLinkFromUri, GetLinkFromUri>();
builder.Services.AddSingleton<IGetProgresion, GetProgresion>();
builder.Services.AddSingleton<IGetInfo, GetInfo>();
builder.Services.AddSingleton<IFactoryWebDriver, FactoryWebDriver>();
builder.Services.AddSingleton<IGetInfoFromEbay, GetInfoFromFromAmazon>();

builder.Services.AddHostedService<MainBackgroundService>();
var app = builder.Build();

app.Run();
