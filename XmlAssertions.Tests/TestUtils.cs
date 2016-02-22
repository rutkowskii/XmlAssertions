using System.Xml;

namespace XmlAssertions.Tests
{
    public static class TestUtils
    {
        public static XmlElement ToXmlElement(this string input)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(input);
            return xmlDoc.DocumentElement;
        }

        public static XmlAssertable ToXmlAssertable(this string input)
        {
            var xmlElement = new XmlNodeSimplifiedBuilder().Build(ToXmlElement(input));

            return new XmlAssertable(xmlElement, new XmlPath(xmlElement.Name));
        }
    }
}