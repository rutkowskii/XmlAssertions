using System;
using System.Collections.Generic;
using System.Linq;
using XmlAssertions.Exceptions;

namespace XmlAssertions
{
    internal class AssertContext
    {
        public XmlPath MyPath { get; set; }
        public XmlNodeSimplified XmlNode { get; set; }
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

        public IEnumerable<XmlAttributeSimplified> NodeAttributesSimplified
        {
            get { return XmlNode.Attributes; }
        }

        public IEnumerable<string> NodeAttributeNames
        {
            get { return NodeAttributesSimplified.Select(attr => attr.Name); }
        }

    }
}