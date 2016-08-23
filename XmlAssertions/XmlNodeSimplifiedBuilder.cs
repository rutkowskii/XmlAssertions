using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlAssertions
{
    internal class XmlNodeSimplifiedBuilder
    {
        public XmlNodeSimplified Build(XmlNode node)
        {
            var result = new XmlNodeSimplified
            {
                Name = node.Name,
                Attributes = GetAttributes(node).ToArray(),
                Children = node.ChildNodes.Cast<XmlNode>().Select(Build).ToArray(),
                NodeType = node.NodeType,
                InnerText = node.InnerText
            };
            return result;
        }

        private IEnumerable<XmlAttributeSimplified> GetAttributes(XmlNode node)
        {
            var attributes = node.Attributes != null
                ? node.Attributes.Cast<XmlAttribute>()
                : Enumerable.Empty<XmlAttribute>();
            return attributes.Select(XmlUtils.SimplifyXmlAttribute);
        }
    }
}