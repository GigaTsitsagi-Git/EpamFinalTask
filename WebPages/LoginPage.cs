using OpenQA.Selenium;

namespace WebPages
{
    public class LoginPage : BasePage
    {
        public static string Url { get; } = "https://www.saucedemo.com";

        private readonly By _usernameInput = By.CssSelector("input[data-test='username']");
        private readonly By _passwordInput = By.CssSelector("input[data-test='password']");
        private readonly By _loginButton = By.CssSelector("input[data-test='login-button']");
        private readonly By _errorMessage = By.CssSelector("[data-test='error']");
        private readonly By _loginCredentials = By.CssSelector("div[data-test='login-credentials']");
        private readonly By _loginPassword = By.CssSelector("div[data-test='login-password']");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public LoginPage Open()
        {
            Driver.Navigate().GoToUrl(Url);
            return this;
        }

        public string GetUsername => GetText(_loginCredentials).Split('\n')[1];
        public string GetPassword => GetText(_loginPassword).Split('\n').Last().Trim();

        public LoginPage EnterUsername(string username)
        {
            Type(_usernameInput, username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            Type(_passwordInput, password);
            return this;
        }

        public LoginPage ClickLoginButton()
        {
            Click(_loginButton);
            return this;
        }

        public IndexPage LogIn()
        {

            EnterUsername(GetUsername).EnterPassword(GetPassword).ClickLoginButton();

            if (IsErrorMessageDisplayed())
            {
                throw new InvalidOperationException(GetErrorMessage());
            }
            return new IndexPage(Driver);
        }

        // Method to clear text boxes by sending backspace keys instead of using Clear() method 
        // clear does not work reliably with rapit execution of tests, so I used this workaround 
        // selenium may think that the element is already cleared and go on to the next step too quickly
        public LoginPage ClearTextBox(By locator) 
        { 
            var element = WaitForVisible(locator);
            var value = element.GetAttribute("value");
            while (!string.IsNullOrEmpty(value)) 
            { 
                element.SendKeys(Keys.Backspace);
                value = element.GetAttribute("value");
            } 
            return this; 
        }

        public LoginPage ClearUsername() { ClearTextBox(_usernameInput); return this; }

        public LoginPage ClearPassword() { ClearTextBox(_passwordInput); return this; }

        public bool IsErrorMessageDisplayed()
        {
            try
            {
                return WaitForVisible(_errorMessage).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public string GetErrorMessage() => GetText(_errorMessage);
    }
}