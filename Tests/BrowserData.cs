using Core.Enums;

namespace Tests
{
    public static class BrowserData
    {
        public static IEnumerable<object[]> Browser()
        {
            yield return new object[] { BrowserType.Edge };
            yield return new object[] { BrowserType.Firefox };
        }
    }
}
