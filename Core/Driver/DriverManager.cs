using Core.Enums;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Driver
{
    public class DriverManager
    {
        [ThreadStatic]
        private static IWebDriver? _driver;

        public static IWebDriver Driver => _driver ?? throw new NullReferenceException("Driver not initialized");

        public static void Start(BrowserType browserType)
        {
            if (_driver != null)
                return;

            _driver ??= WebDriverFactory.Create(browserType);

        }

        public static void Quit()
        {
            _driver?.Quit();
            _driver = null;
        }
    }
}
