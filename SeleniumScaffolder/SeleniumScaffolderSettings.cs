using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SeleniumScaffolder
{
    public class SeleniumScaffolderSettings
    {
        public SeleniumScaffolderSettings()
        {
        }

        public SeleniumScaffolderSettings(string ymlContents)
        {
            var deserializedSettings = DeserializeYmlFile(ymlContents);

            YmlContents = ymlContents;
            PageElementTypes = deserializedSettings.PageElementTypes;
            PageElementLocatorTypes = deserializedSettings.PageElementLocatorTypes;
            PageElementGroupings = deserializedSettings.PageElementGroupings;
        }

        public string YmlContents { get; set; }

        public List<PageElementType> PageElementTypes { get; set; }

        public List<PageElementLocatorType> PageElementLocatorTypes { get; set; }

        public List<PageElementGrouping> PageElementGroupings { get; set; }

        public static SeleniumScaffolderSettings DeserializeYmlFile(string ymlContents)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<SeleniumScaffolderSettings>(ymlContents);
        }
    }
}
