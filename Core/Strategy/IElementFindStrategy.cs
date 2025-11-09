using OpenQA.Selenium;

namespace Core.Strategy
{
    public interface IElementFindStrategy
    {
        IWebElement FindElement(IWebDriver driver, By locator);
    }
}
