using Core.Enums;
using OpenQA.Selenium;
using Serilog;

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
            {
                Log.Warning("Driver already initialized. Skipping driver start.");
                return;
            }

            Log.Information("Starting WebDriver for browser: {BrowserType}", browserType);
            _driver ??= WebDriverFactory.Create(browserType);
            Log.Information("WebDriver started successfully for browser: {BrowserType}", browserType);
        }

        public static void Quit()
        {
            if (_driver == null)
            {
                Log.Warning("Attempted to quit driver, but driver is null.");
                return;
            }

            Log.Information("Quitting WebDriver");
            _driver.Quit();
            _driver = null;
            Log.Information("WebDriver quit successfully");
        }
    }
}
