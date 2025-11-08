using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebPages
{
    public class IndexPage
    {
        public static string Url { get; } = "https://www.saucedemo.com/inventory.html";

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        //locators
        private readonly By _primaryHeader = By.CssSelector("div[data-test='primary-header']");
        private readonly By _appLogo = By.CssSelector(".app_logo");

        public IndexPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentException(nameof(driver));
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        }

        public IndexPage Open()
        {
            _driver.Navigate().GoToUrl(Url);
            return this;
        }

        public bool AppLogoExists()
        {
            var primaryHeader = _wait.Until(driver =>
            {
                var element = driver.FindElement(_primaryHeader);
                return element.Displayed ? element : null;
            });

            var appLogo = _wait.Until(driver =>
            {
                var element = primaryHeader.FindElement(_appLogo);
                return element.Displayed ? element : null;
            });
            return appLogo.Displayed;
        }
    }
}
