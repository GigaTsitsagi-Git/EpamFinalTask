using OpenQA.Selenium;

namespace Core.Strategy.FindBy
{
    public class FindByMixed : IElementFindStrategy
    {
        public IWebElement FindElement(IWebDriver driver, By locator)
            => driver.FindElement(locator);
    }
}
