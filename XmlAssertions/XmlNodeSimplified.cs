using System.Xml;

namespace XmlAssertions
{
    public class XmlNodeSimplified
    {
        public XmlNodeSimplified[] Children { get; set; }
        public string Name { get; set; }
        public XmlAttributeSimplified[] Attributes { get; set; }
        public XmlNodeType NodeType { get; set; }
        public string InnerText { get; set; }
    }
}