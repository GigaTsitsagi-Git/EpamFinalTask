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
        public static IWebDriver Create(BrowserType browsertype)
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
            var options = new ChromeOptions { PageLoadStrategy = PageLoadStrategy.Eager };
            foreach (var arg in SharedArguments) options.AddArgument(arg);
            Log.Debug("ChromeDriver created successfully");
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions { PageLoadStrategy = PageLoadStrategy.Eager };
            foreach (var arg in SharedArguments) options.AddArgument(arg);
            Log.Debug("FirefoxDriver created successfully");
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions { PageLoadStrategy = PageLoadStrategy.Eager };
            foreach (var arg in SharedArguments) options.AddArgument(arg);
            Log.Debug("EdgeDriver created successfully");
            return new EdgeDriver(options);
        }

        private static IWebDriver CreateSafariDriver()
        {
            var options = new SafariOptions() { PageLoadStrategy = PageLoadStrategy.Eager };
            var driver = new SafariDriver(options);
            Log.Debug("Safari created successfully");
            return driver;
        }

        private static IWebDriver CreateInternetExplorerDriver()
        {
            var options = new InternetExplorerOptions() { PageLoadStrategy = PageLoadStrategy.Eager };
            var driver = new InternetExplorerDriver(options);
            Log.Debug("Internet Explorer created successfully");
            return driver;
        }

        // Shared browser arguments
        private static IEnumerable<string> SharedArguments =>
        [
            "--disable-infobars",
            "--disable-extensions",
            "--disable-notifications",
        ];
    }
}
