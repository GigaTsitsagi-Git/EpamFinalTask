using Core.Strategy;
using Core.Strategy.FindBy;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebPages
{
    public class IndexPage
    {
        public static string Url { get; } = "https://www.saucedemo.com/inventory.html";

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly ElementFinder _elementFinder;


        //locators
        private readonly By _primaryHeader = By.CssSelector("div[data-test='primary-header']");
        private readonly By _appLogo = By.CssSelector(".app_logo");
        private readonly By _inventoryContainer = By.CssSelector("#inventory_Container");
        private readonly By _inventoryList = By.CssSelector("div[data-test='inventory-list']");

        public IndexPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentException(nameof(driver));
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            _elementFinder = new ElementFinder(new FindByMixed());
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
                var element = _elementFinder.Find(driver, _primaryHeader);
                return element.Displayed ? element : null;
            });

            var appLogo = _wait.Until(driver =>
            {
                var element = _elementFinder.Find(driver, _appLogo);
                return element.Displayed ? element : null;
            });
            return appLogo.Displayed;
        }
    }
}
