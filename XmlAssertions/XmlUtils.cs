using System.Xml;

namespace XmlAssertions
{
    internal class XmlUtils
    {
        public static XmlAttributeSimplified SimplifyXmlAttribute(XmlAttribute attr)
        {
            return new XmlAttributeSimplified(attr.Name, attr.Value);
        } 
    }
}