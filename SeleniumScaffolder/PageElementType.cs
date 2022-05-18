using System.Collections.Generic;

namespace SeleniumScaffolder
{
    public class PageElementType
    {
        public string Name { get; set; }

        public HashSet<string> Abbreviations { get; set; }

        public string Interactions { get; set; }

        public string WebElements { get; set; }
    }
}
