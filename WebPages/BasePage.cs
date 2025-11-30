using Core.Strategy;
using Core.Strategy.FindBy;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;
        protected readonly ElementFinder ElementFinder;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentException(nameof(driver));
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            ElementFinder = new ElementFinder(new FindByMixed());
        }

        protected IWebElement WaitForVisible(By locator)
        {
            return Wait.Until(driver =>
            {
                var element = ElementFinder.Find(driver, locator);
                return element.Displayed ? element : null;
            });
        }

        protected void Click(By locator)
        {
            WaitForVisible(locator).Click();
        }

        protected void Type(By locator, string text)
        {
            WaitForVisible(locator).SendKeys(text);
        }

        protected string GetText(By locator)
        {
            return WaitForVisible(locator).Text;
        }
    }
}
