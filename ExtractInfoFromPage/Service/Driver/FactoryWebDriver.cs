using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Diagnostics;
using ExtractInfoFromPage.Interface;

namespace ExtractInfoFromPage.Service.Driver
{
    public class FactoryWebDriver : IFactoryWebDriver
    {
        public IWebDriver Create()
        {
            string directory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            //string path = Path.Combine(directory, "1.crx");
            var chromeOptions = new ChromeOptions();
            //   chromeOptions.AddArguments("--disable-extensions-except=path_to_extension_folder");
            //  chromeOptions.AddArguments("--load-extension=path_to_extension_folder");
            //   chromeOptions.AddEx
           // chromeOptions.AddExtension(path);
            chromeOptions.BinaryLocation = Path.Combine(directory, "chrome\\chrome.exe");

            var userDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "profile chrome");
            chromeOptions.AddArgument($"--user-data-dir={userDataDir}");

            // chromeOptions.AddArgument($"--user-data-dir={userDataDir}");
            var serv = ChromeDriverService.CreateDefaultService(Path.Combine(directory, "\\chrome\\"), Path.Combine(directory, "chromedriver.exe"));

            var driver = new ChromeDriver(serv, chromeOptions);

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(5);

            return driver;
        }

        public FactoryWebDriver()
        {

        }
    }
}
