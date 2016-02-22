using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlAssertions
{
    internal static class XmlUtils
    {
        public static XmlAttributeSimplified SimplifyXmlAttribute(XmlAttribute attr)
        {
            return new XmlAttributeSimplified(attr.Name, attr.Value);
        }

        public static XmlElement ToXmlElement(this string input)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(input);
            return xmlDoc.DocumentElement;
        }

        public static List<XmlNode> ExtractChildNodes(XmlNode parentNode)
        {
            return parentNode.ChildNodes.Cast<XmlNode>().ToList();
        }

        public static XmlNodeSimplified Simplify(this XmlNode node)
        {
            return new XmlNodeSimplifiedBuilder().Build(node);
        }
    }
}