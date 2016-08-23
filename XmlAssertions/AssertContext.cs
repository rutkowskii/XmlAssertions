using System;
using System.Collections.Generic;
using System.Linq;
using XmlAssertions.Exceptions;

namespace XmlAssertions
{
    internal class AssertContext
    {
        public IEnumerable<XmlAttributeSimplified> NodeAttributesSimplified
        {
            get { return XmlNode.Attributes; }
        }

        public IEnumerable<string> NodeAttributeNames
        {
            get { return NodeAttributesSimplified.Select(attr => attr.Name); }
        }

        public XmlPath MyPath { get; private set; }
        public XmlNodeSimplified XmlNode { get; private set; }
        public StringComparer StringComparer { get; private set; }


        public AssertContext(XmlPath myPath, XmlNodeSimplified xmlNode, StringComparer stringComparer)
        {
            MyPath = myPath;
            XmlNode = xmlNode;
            StringComparer = stringComparer;
        }
        
        public void ThrowErrorMessage(string message)
        {
            XmlExc.Throw(MyPath, message);
        }

        public AssertContext BuildChildContext(XmlNodeSimplified childNode, int childNodeIndex)
        {
            var childPath = MyPath.Append(childNode.Name, childNodeIndex);
            return new AssertContext(childPath, childNode, StringComparer);
        }
    }
}