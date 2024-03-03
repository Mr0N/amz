using ExtractInfoFromPage.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System.Diagnostics;

namespace ExtractInfoFromPage.Service
{
    public class GetInfoFromFromAmazon : IGetInfoFromEbay
    {
        private IWebDriver _driver;
        IFactoryWebDriver _factory;

        public IWebDriver driver => _driver;

        public GetInfoFromFromAmazon(IWebDriver driver, IFactoryWebDriver factory)
        {
            _driver = driver;
            _factory = factory;
        }
        private void GoToUrl(string url)
        {
            Console.WriteLine(new string('-', 23));
            while (true)
            {
                try
                {
                    _driver.Navigate().GoToUrl(url);
                    _driver.ExecuteJavaScript("document.querySelectorAll(\".imageThumbnail\").forEach(a=>a.click());");
                    _driver.ExecuteJavaScript("document.querySelectorAll(\".regularAltImageViewLayout li\").forEach(a=>a.click());");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception :{ex.ToString()}");
                    RestartChrome();
                }
            }
        }
        private void RestartChrome()
        {
            CloseChrome();
            Thread.Sleep(5000);
            _driver = _factory.Create();
        }
        private void CloseChrome()
        {
            foreach (var process in Process.GetProcessesByName("chromedriver"))
            {
                process.Kill();
            }
            foreach (var process in Process.GetProcessesByName("chrome"))
            {
                process.Kill();
            }
            Thread.Sleep(1000);
        }
        public string GetInfo(string url)
        {
            GoToUrl(url);
            int count = 0;
            while (true)
            {
                string html = null;
                while (true)
                {
                    try
                    {
                        html = _driver.PageSource;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        RestartChrome();
                    }
                }
                if (html.Contains("checkCaptchaRendered"))
                {
                    Console.WriteLine("checkCaptcha");
                    count++;
                    Thread.Sleep(500);
                    if (count > 100)
                    {
                        GoToUrl(url);
                        count = 0;
                    }
                    continue;
                }
                return html;
            }
        }
    }
}
