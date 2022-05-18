using System.Collections.Generic;
using System.Linq;

namespace SeleniumScaffolder
{
    public class PageElementScaffolder
    {
        public PageElementScaffolder(SeleniumScaffolderSettings settings)
        {
            Settings = settings;
            PageElementTypeDictionary = CreatePageElementTypeDictionary();
            PageElementLocatorTypeDictionary = CreatePageElementLocatorTypeDictionary();
        }

        public SeleniumScaffolderSettings Settings { get; set; }

        public Dictionary<string, PageElementType> PageElementTypeDictionary { get; set; }

        public Dictionary<string, PageElementLocatorType> PageElementLocatorTypeDictionary { get; set; }

        private Dictionary<string, PageElementType> CreatePageElementTypeDictionary()
        {
            var dictionary = new Dictionary<string, PageElementType>();

            foreach (var pageElementType in Settings.PageElementTypes)
            {
                foreach (var abbreviation in pageElementType.Abbreviations)
                {
                    dictionary[abbreviation] = pageElementType;
                }
            }

            return dictionary;
        }

        private Dictionary<string, PageElementLocatorType> CreatePageElementLocatorTypeDictionary()
        {
            var dictionary = new Dictionary<string, PageElementLocatorType>();

            foreach (var pageElementLocatorType in Settings.PageElementLocatorTypes)
            {
                foreach (var abbreviation in pageElementLocatorType.Abbreviations)
                {
                    dictionary[abbreviation] = pageElementLocatorType;
                }
            }

            return dictionary;
        }

        public PageElementType MapToPageElementType(string pageElementTypeAbbreviation)
        {
            return PageElementTypeDictionary.GetValueOrDefault(
                pageElementTypeAbbreviation.Trim().ToLower(), Settings.PageElementTypes[0]);
        }

        public PageElementLocatorType MapToPageElementLocatorType(string pageElementLocatorTypeAbbreviation)
        {
            return PageElementLocatorTypeDictionary.GetValueOrDefault(
                pageElementLocatorTypeAbbreviation.Trim().ToLower(), Settings.PageElementLocatorTypes[0]);
        }

        #region Bulk Import From String

        private static string[] ParseLinesFromString(string str)
        {
            return str.Split('\n');
        }

        private static string[] ParseFieldsFromLine(string line)
        {
            return line.Split('|');
        }

        private PageElement CreatePageElement(string name, string pageElementTypeStr, string locatorByMethodStr,
            string locatorByExpression)
        {
            var pageElementType = MapToPageElementType(pageElementTypeStr);
            var pageElementLocatorType = MapToPageElementLocatorType(locatorByMethodStr);
            var pageElementLocator = new PageElementLocator(pageElementLocatorType, locatorByExpression);
            var pageElement = new PageElement(name, pageElementType, pageElementLocator);

            return pageElement;
        }

        private PageElement ReadLineToPageElement(string pageElementStr)
        {
            var fields = ParseFieldsFromLine(pageElementStr);
            var name = fields[0].RemoveWhitespace();
            var pageElementTypeStr = "";
            var locatorByMethodStr = "";
            var locatorByExpression = "";

            if (fields.Length > 1)
                pageElementTypeStr = fields[1].RemoveWhitespace();

            if (fields.Length > 2)
                locatorByMethodStr = fields[2].RemoveWhitespace();

            if (fields.Length > 3)
                locatorByExpression = fields[3].Trim();

            var pageElement = CreatePageElement(name, pageElementTypeStr, locatorByMethodStr, locatorByExpression);

            return pageElement;
        }

        public List<PageElement> ReadStringToPageElements(string pageElementsStr)
        {
            var pageElements = new List<PageElement>();
            var lines = ParseLinesFromString(pageElementsStr);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                pageElements.Add(ReadLineToPageElement(line));
            }

            return pageElements;
        }

        #endregion

        public List<PageElementGrouping> GeneratePageElementGroupings(List<PageElement> pageElements)
        {
            var generatedGroupings = new List<PageElementGrouping>();

            foreach (var grouping in Settings.PageElementGroupings)
            {
                grouping.GeneratePageElementGrouping(pageElements);

                if (grouping.PageElements.Any())
                    generatedGroupings.Add(grouping);
            }

            return generatedGroupings;
        }
    }
}
