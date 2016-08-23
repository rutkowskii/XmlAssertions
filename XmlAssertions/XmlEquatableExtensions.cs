using System.Xml;

namespace XmlAssertions
{
    public static class XmlEquatableExtensions
    {
        public static IXmlAssertable XmlShould(this XmlNode node)
        {
            var nodeSimplified = new XmlNodeSimplifiedBuilder().Build(node);
            return new XmlAssertable(nodeSimplified, new XmlPath(node.Name));
        }

        public static IXmlAssertable XmlShould(this string nodeStr)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(nodeStr);
            return XmlShould(xmlDoc.DocumentElement);
        }
    }
}
