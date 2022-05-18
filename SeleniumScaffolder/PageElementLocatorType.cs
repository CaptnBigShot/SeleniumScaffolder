using System.Collections.Generic;

namespace SeleniumScaffolder
{
    public class PageElementLocatorType
    {
        public string ByType { get; set; }

        public string Method { get; set; }

        public HashSet<string> Abbreviations { get; set; }
    }
}
