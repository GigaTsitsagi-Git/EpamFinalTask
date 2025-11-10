using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class BrowserData
    {
        public static IEnumerable<object[]> Provider()
        {
            yield return new object[] { BrowserType.Edge };
            yield return new object[] { BrowserType.Firefox };
        }
    }
}
