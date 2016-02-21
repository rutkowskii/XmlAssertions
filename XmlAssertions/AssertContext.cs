using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlAssertions
{
    public class AssertContext
    {
        public XmlPath MyPath { get; set; }
        public XmlNode XmlNode { get; set; }
        public StringComparer StringComparer { get; set; }

        public void SetStringComparer(bool ignoreCase)
        {
            StringComparer = ignoreCase
                ? StringComparer.InvariantCultureIgnoreCase
                : StringComparer.InvariantCulture;
        }

        public void ThrowErrorMessage(string message)
        {
            XmlExc.Throw(MyPath, message);
        }

        public IEnumerable<XmlAttribute> NodeAttributes
        {
            get
            {
                return XmlNode.Attributes != null
                    ? XmlNode.Attributes.Cast<XmlAttribute>()
                    : Enumerable.Empty<XmlAttribute>();
            }
        }

        public IEnumerable<XmlAttributeSimplified> NodeAttributesSimplified
        {
            get { return NodeAttributes.Select(XmlUtils.SimplifyXmlAttribute); }
        }

        public IEnumerable<string> NodeAttributeNames
        {
            get { return NodeAttributesSimplified.Select(attr => attr.Name); }
        }

    }
}