using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlAssertions
{
    internal class XmlUtils
    {
        public static XmlAttributeSimplified SimplifyXmlAttribute(XmlAttribute attr)
        {
            return new XmlAttributeSimplified(attr.Name, attr.Value);
        }

        public static List<XmlNode> ExtractChildNodes(XmlNode parentNode)
        {
            return parentNode.ChildNodes.Cast<XmlNode>().ToList();
        }
    }
}