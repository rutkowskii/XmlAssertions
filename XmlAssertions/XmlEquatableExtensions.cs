using System.Xml;

namespace XmlAssertions
{
    public static class XmlEquatableExtensions
    {
        public static IXmlAssertable XmlShould(this string nodeStr)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(nodeStr);
            return XmlShould(xmlDoc.DocumentElement);
        }

        public static IXmlAssertable XmlShould(this XmlNode node)
        {
            var nodeSimplified = new XmlNodeSimplifiedBuilder().Build(node);
            var xmlPath = new XmlPath(node.Name);
            var rootAssertContext = AssertContextBuilder.Default.Build(xmlPath, nodeSimplified);
            return new XmlAssertable(rootAssertContext);
        }
    }
}
