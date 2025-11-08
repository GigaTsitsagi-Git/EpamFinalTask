using Core.Driver;
using Core.Enums;
using WebPages;

namespace Tests
{
    [TestClass]
    public sealed class LoginPageTests
    {
        private LoginPage? _loginPage;

        private readonly string _username = "standard_user";
        private readonly string _password = "secret_sauce";

        private const string _invalidUsername = "Epic sadface: Username is required"; // can create separate object for error messages struct
        private const string _invalidPassword = "Epic sadface: Password is required";

        public TestContext TestContext { get; set; } = null!;

        public void StartTest(BrowserType browser)
        {
            DriverManager.Start(browser);
            _loginPage = new LoginPage(DriverManager.Driver);
        }

        [TestMethod]
        [DataRow(BrowserType.Edge)]
        [DataRow(BrowserType.Firefox)]
        public void Login_EmptyUsername_ShowsUsernameRequiredError(BrowserType browser)
        {
            StartTest(browser);

            _loginPage!.Open()
                .EnterUsername(_username)
                .EnterPassword(_password)
                .ClearUsername()
                .ClearPassword()
                .ClickLoginButton();

            Assert.AreEqual(_loginPage.GetErrorMessage(), _invalidUsername); // can create separate object for error messages
        }


        [TestMethod]
        [DataRow(BrowserType.Edge)]
        [DataRow(BrowserType.Firefox)]
        public void Login_EmptyPassword_ShowsUsernameRequiredError(BrowserType browser)
        {
            StartTest(browser);

            _loginPage!.Open()
                .EnterUsername("123")
                .EnterPassword(_password)
                .ClearPassword()
                .ClickLoginButton();

            Assert.AreEqual(_loginPage.GetErrorMessage(), _invalidPassword);

        }

        [TestMethod]
        [DataRow(BrowserType.Edge)]
        [DataRow(BrowserType.Firefox)]
        public void Login_ValidCredentials_SuccessfulLogin(BrowserType browser)
        {
            StartTest(browser);

            Assert.IsTrue(_loginPage!.Open().LogIn(_username, _password).AppLogoExists());
        }


        [TestCleanup]
        public void TestCleanup()
        {
            DriverManager.Quit();
        }
    }   
}
