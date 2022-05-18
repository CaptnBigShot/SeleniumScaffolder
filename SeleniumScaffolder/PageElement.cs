namespace SeleniumScaffolder
{
    public class PageElement
    {
        public PageElement(string name, PageElementType type, PageElementLocator locator)
        {
            Name = name;
            Type = type;
            Locator = locator;
        }

        public string Name { get; set; }

        public PageElementType Type { get; set; }

        public PageElementLocator Locator { get; set; }

        public string VariableName =>
            Name.Length > 1 ? char.ToLower(Name[0]) + Name[1..] : Name;

        public string WebElements =>
            ReplaceVariablePlaceholders(Type.WebElements);

        public string Interactions =>
            ReplaceVariablePlaceholders(Type.Interactions);

        public string ReplaceVariablePlaceholders(string stringWithVariablePlaceholders)
        {
            return stringWithVariablePlaceholders
                .Replace("{ElementName}", Name)
                .Replace("{ElementVariableName}", VariableName)
                .Replace("{FindsByExpression}", Locator.ByExpression)
                .Replace("{FindsBy}", Locator.FullByString);
        }
    }
}
