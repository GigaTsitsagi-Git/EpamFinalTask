using OpenQA.Selenium;

namespace Core.Strategy
{
    public class ElementFinder
    {
        private readonly IElementFindStrategy _strategy;

        public ElementFinder(IElementFindStrategy strategy)
        {
            _strategy = strategy;
        }

        public IWebElement Find(IWebDriver driver, By locator)
            => _strategy.FindElement(driver, locator);
    }
}
