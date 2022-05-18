namespace SeleniumScaffolder
{
    public class PageElementLocator
    {
        public PageElementLocator(PageElementLocatorType type, string byExpression)
        {
            Type = type;
            ByExpression = byExpression;
        }

        public PageElementLocatorType Type { get; set; }

        public string ByExpression { get; set; }

        public string FullByString =>
            @$"{Type.ByType}.{Type.Method}(""{ByExpression}"")";
    }
}
