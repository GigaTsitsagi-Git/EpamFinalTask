using Core.Driver;
using TechTalk.SpecFlow;
using WebPages;
using FluentAssertions;

namespace Tests.BDD.StepDefinition
{
	[Binding]
	public sealed class LoginSteps
	{
		[Given(@"I run on (.*)")]
		public static void GivenIRunOnBrowser(string browser) { }

		private LoginPage _loginPage = null!;

		[Given(@"I open the login page")]
		public void GivenIOpenTheLoginPage()
		{
			_loginPage = new LoginPage(DriverManager.Driver).Open();
		}

		[When(@"I enter valid username and password")]
		public void WhenIEnterValidUsernameAndPassword()
		{
			EnsureLoginPage();
			_loginPage.EnterUsernameAndPassword();
		}

		[When(@"I click the login button")]
		public void WhenIClickTheLoginButton()
		{
			EnsureLoginPage();
			_loginPage.ClickLoginButton();
		}

		[Then(@"I should see the home page")]
		public void ThenIShouldSeeTheHomePage()
		{
			var indexPage = new IndexPage(DriverManager.Driver);
			indexPage.AppLogoExists().Should().BeTrue();
        }

		private void EnsureLoginPage()
		{
			if (_loginPage == null)
			{
				_loginPage = new LoginPage(DriverManager.Driver);
			}
		}
	}
}

