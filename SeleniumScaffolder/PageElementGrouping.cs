using System.Collections.Generic;

namespace SeleniumScaffolder
{
    public class PageElementGrouping
    {
        public string Name { get; set; }

        public List<PageElementGroupingElementType> PageElementGroupingElementTypes { get; set; }

        public string Output { get; set; }

        public string GroupedPageElementsOutput { get; set; }

        public string ProcessedOutput { get; set; }

        public List<PageElement> PageElements { get; set; } = new List<PageElement>();

        public Dictionary<string, PageElementGroupingElementType> PageElementGroupingElementTypesDictionary()
        {
            var dictionary = new Dictionary<string, PageElementGroupingElementType>();

            foreach (var pageElementGroupingElementType in PageElementGroupingElementTypes)
            {
                dictionary[pageElementGroupingElementType.Name] = pageElementGroupingElementType;
            }

            return dictionary;
        }

        private string GenerateGroupedPageElementsOutput(List<PageElement> pageElements)
        {
            var groupingElementTypesDictionary = PageElementGroupingElementTypesDictionary();
            var groupedPageElementsOutput = new List<string>();

            foreach (var pageElement in pageElements)
            {
                PageElementGroupingElementType groupingElementType; 

                if (groupingElementTypesDictionary.ContainsKey(pageElement.Type.Name))
                {
                    groupingElementType = groupingElementTypesDictionary[pageElement.Type.Name];
                }
                else if (groupingElementTypesDictionary.ContainsKey("*"))
                {
                    groupingElementType = groupingElementTypesDictionary["*"];
                }
                else
                {
                    continue;
                }

                PageElements.Add(pageElement);

                var pageElementGroupingOutput = pageElement.ReplaceVariablePlaceholders(groupingElementType.Output);

                groupedPageElementsOutput.Add(pageElementGroupingOutput);
            }

            return string.Join("\n", groupedPageElementsOutput);
        }

        public void GeneratePageElementGrouping(List<PageElement> pageElements)
        {
            GroupedPageElementsOutput = GenerateGroupedPageElementsOutput(pageElements);
            ProcessedOutput = ReplaceVariablePlaceholders(Output);
        }

        public string ReplaceVariablePlaceholders(string stringWithVariablePlaceholders)
        {
            return stringWithVariablePlaceholders
                .Replace("{GroupedPageElementsOutput}", GroupedPageElementsOutput);
        }
    }
}
