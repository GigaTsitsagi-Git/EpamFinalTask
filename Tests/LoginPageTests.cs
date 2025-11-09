using Core.Driver;
using Core.Enums;
using Serilog;
using WebPages;

namespace Tests
{
    [TestClass]
    public sealed class LoginPageTests
    {
        private LoginPage? _loginPage;

        private readonly string _username = "standard_user";
        private readonly string _password = "secret_sauce";

        public TestContext TestContext { get; set; } = null!;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Test class initialization completed");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Log.Information("Starting test: {TestName}", TestContext.TestName);
        }

        public void StartTest(BrowserType browser)
        {
            Log.Information("Initializing test with browser: {Browser}", browser);
            DriverManager.Start(browser);
            _loginPage = new LoginPage(DriverManager.Driver);
            Log.Debug("LoginPage initialized");
        }

        [TestMethod]
        [DataRow(BrowserType.Edge)]
        [DataRow(BrowserType.Firefox)]
        public void Login_EmptyUsername_ShowsUsernameRequiredError(BrowserType browser)
        {
            Log.Information("Executing test: Login_EmptyUsername_ShowsUsernameRequiredError with browser: {Browser}", browser);
            StartTest(browser);

            Log.Debug("Opening login page and entering credentials");
            _loginPage!.Open()
                .EnterUsernameAndPassword(_username, _password)
                .ClearUsername()
                .ClearPassword()
                .ClickLoginButton();

            var errorMessage = _loginPage.GetErrorMessage();
            Log.Information("Error message received: {ErrorMessage}", errorMessage);
            Assert.AreEqual(errorMessage, Error.UsernameRequired);
            Log.Information("Test passed: Username required error displayed correctly");
        }


        [TestMethod]
        [DataRow(BrowserType.Edge)]
        [DataRow(BrowserType.Firefox)]
        public void Login_EmptyPassword_ShowsUsernameRequiredError(BrowserType browser)
        {
            Log.Information("Executing test: Login_EmptyPassword_ShowsUsernameRequiredError with browser: {Browser}", browser);
            StartTest(browser);

            Log.Debug("Opening login page, entering username, clearing password");
            _loginPage!.Open()
                .EnterUsernameAndPassword(_username, _password)
                .ClearPassword()
                .ClickLoginButton();

            var errorMessage = _loginPage.GetErrorMessage();
            Log.Information("Error message received: {ErrorMessage}", errorMessage);
            Assert.AreEqual(errorMessage, Error.PasswordRequired);
            Log.Information("Test passed: Password required error displayed correctly");
        }

        [TestMethod]
        [DataRow(BrowserType.Edge)]
        [DataRow(BrowserType.Firefox)]
        public void Login_ValidCredentials_SuccessfulLogin(BrowserType browser)
        {
            Log.Information("Executing test: Login_ValidCredentials_SuccessfulLogin with browser: {Browser}", browser);
            StartTest(browser);

            Log.Debug("Attempting login with valid credentials");
            var result = _loginPage!.Open().LogIn(_username, _password).AppLogoExists();
            Log.Information("Login result: {Result}, App logo exists: {AppLogoExists}", result ? "Success" : "Failed", result);
            Assert.IsTrue(result);
            Log.Information("Test passed: Successful login verified");
        }


        [TestCleanup]
        public void TestCleanup()
        {
            Log.Information("Cleaning up test: {TestName}", TestContext.TestName);
            try
            {
                DriverManager.Quit();
                Log.Information("Test cleanup completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during test cleanup");
                throw;
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Log.Information("Closing logger and disposing resources");
            Log.CloseAndFlush();
        }
    }   
}
