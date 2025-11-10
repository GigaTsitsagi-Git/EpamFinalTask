using Core.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using Serilog;

namespace Core.Driver
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(BrowserType browsertype) // search option for improvement
        {
            Log.Debug("Creating WebDriver for browser type: {BrowserType}", browsertype);
            
            IWebDriver driver = browsertype switch
            {
                BrowserType.Chrome => CreateChromeDriver(),
                BrowserType.Firefox => CreateFirefoxDriver(),
                BrowserType.Edge => CreateEdgeDriver(),
                BrowserType.Safari=> CreateSafariDriver(),
                BrowserType.InternetExplorer => CreateInternetExplorerDriver(),
                _ => throw new ArgumentException($"Unsupported browser: {browsertype}")
            };
            
            driver.Manage().Window.Maximize();
            Log.Information("WebDriver created and window maximized for browser: {BrowserType}", browsertype);

            return driver;
        }

        private static IWebDriver CreateChromeDriver()
        {
            Log.Debug("Creating ChromeDriver with normal page load strategy");
            var options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            var driver = new ChromeDriver(options);
            Log.Debug("ChromeDriver created successfully");
            return driver;
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            Log.Debug("Creating FirefoxDriver with normal page load strategy");
            var options = new FirefoxOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            var driver = new FirefoxDriver(options);
            Log.Debug("FirefoxDriver created successfully");
            return driver;
        }

        private static IWebDriver CreateEdgeDriver()
        {
            Log.Debug("Creating EdgeDriver with normal page load strategy");
            var options = new EdgeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            var driver = new EdgeDriver(options);
            Log.Debug("EdgeDriver created successfully");
            return driver;
        }

        private static IWebDriver CreateSafariDriver()
        {
            Log.Debug("Creating EdgeDriver with normal page load strategy");
            var options = new SafariOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            var driver = new SafariDriver(options);
            Log.Debug("EdgeDriver created successfully");
            return driver;
        }

        private static IWebDriver CreateInternetExplorerDriver()
        {
            Log.Debug("Creating EdgeDriver with normal page load strategy");
            var options = new InternetExplorerOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            var driver = new InternetExplorerDriver(options);
            Log.Debug("EdgeDriver created successfully");
            return driver;
        }
    }
}
