using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Strategy.FindBy
{
    public class FindByMixed : IElementFindStrategy
    {
        public IWebElement FindElement(IWebDriver driver, By locator)
            => driver.FindElement(locator);
    }
}
