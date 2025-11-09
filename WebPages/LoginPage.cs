using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebPages
{
    public class LoginPage
    {
        public static string Url { get; } = "https://www.saucedemo.com";

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        //locators
        private readonly By _usernameInput = By.CssSelector("input[data-test='username']");
        private readonly By _passwordInput = By.CssSelector("input[data-test='password']");
        private readonly By _loginButton = By.CssSelector("input[data-test='login-button']");
        private readonly By ErrorMessage = By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentException(nameof(driver));
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        }

        public LoginPage Open()
        {
            _driver.Navigate().GoToUrl(Url);
            return this;
        }

        // Method to clear text boxes by sending backspace keys instead of using Clear() method
        // clear does not work reliably with rapit execution of tests, so I used this workaround
        // selenium may think that the element is already cleared and go on to the next step too quickly
        public LoginPage ClearTextBox(By textBox)
        {
            var element = _wait.Until(driver => driver.FindElement(textBox));

            var value = element.GetAttribute("value");
            while (!string.IsNullOrEmpty(value))
            {
                element.SendKeys(Keys.Backspace);
                value = element.GetAttribute("value");
            }
            return this;
        }

        // Methods to perform login actions (enter username/password, click login, or full login)
        public IndexPage LogIn(string username, string password)
        {
            EnterUsername(username).EnterPassword(password).ClickLoginButton();

            if(IsErrorMessageDisplayed())
            {
                throw new InvalidOperationException(GetErrorMessage());
            }
            return new IndexPage(_driver);
        }

        public LoginPage EnterUsernameAndPassword(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            return this;
        }

        public LoginPage EnterUsername(string username)
        {
            _wait.Until(driver => driver.FindElement(_usernameInput)).SendKeys(username);
            return this;
        }
        public LoginPage ClearUsername()
        {
            ClearTextBox(_usernameInput);
            return this;
        }
        public LoginPage EnterPassword(string password)
        {
            _wait.Until(driver => driver.FindElement(_passwordInput)).SendKeys(password);
            return this;
        }

        public LoginPage ClearPassword()
        {
            ClearTextBox(_passwordInput);
            return this;
        }
        public LoginPage ClickLoginButton()
        {
            _wait.Until(driver => driver.FindElement(_loginButton)).Click();
            return this;
        }

        // Methods related to errors
        public string GetErrorMessage()
        {
            return _wait.Until(driver =>
            {
                var element = driver.FindElement(ErrorMessage);
                return element.Displayed ? element : null;
            }).Text;
        }

        private bool IsErrorMessageDisplayed()
        {
            try
            {
                return _wait.Until(driver => driver.FindElement(ErrorMessage).Displayed);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}