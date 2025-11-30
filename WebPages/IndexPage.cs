using OpenQA.Selenium;

namespace WebPages
{
    public class IndexPage : BasePage
    {
        public static string Url { get; } = "https://www.saucedemo.com/inventory.html";

        private readonly By _primaryHeader = By.CssSelector("div[data-test='primary-header']");
        private readonly By _appLogo = By.CssSelector(".app_logo");

        public IndexPage(IWebDriver driver) : base(driver) { }

        public IndexPage Open()
        {
            Driver.Navigate().GoToUrl(Url);
            return this;
        }

        public bool AppLogoExists()
        {
            WaitForVisible(_primaryHeader);
            return WaitForVisible(_appLogo).Displayed;
        }
    }
}
