using OpenQA.Selenium;

namespace ExtractInfoFromPage.Interface
{
    public interface IFactoryWebDriver
    {
        public IWebDriver Create();
    }
}