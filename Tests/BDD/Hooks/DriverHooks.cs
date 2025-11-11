using Core.Driver;
using Core.Enums;
using TechTalk.SpecFlow;

namespace Tests.BDD.Hooks
{
	[Binding]
	public sealed class DriverHooks
	{
		private readonly ScenarioContext _scenarioContext;

		public DriverHooks(ScenarioContext scenarioContext)
		{
			_scenarioContext = scenarioContext;
		}

		[BeforeScenario]
		public void BeforeScenario()
		{
			var browser = ResolveBrowserType();
			DriverManager.Start(browser);
		}

		[AfterScenario]
		public void AfterScenario()
		{
			DriverManager.Quit();
		}

		private BrowserType ResolveBrowserType()
		{
			var arguments = _scenarioContext.ScenarioInfo.Arguments;
			object? valueObj = null;
			if (arguments != null && arguments.Contains("browser"))
			{
				valueObj = arguments["browser"];
				if (valueObj is string browserValue &&
				    Enum.TryParse<BrowserType>(browserValue, ignoreCase: true, out var fromArgument))
				{
					return fromArgument;
				}
			}

			return BrowserType.Edge;
		}
	}
}
