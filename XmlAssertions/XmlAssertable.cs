using System.Collections.Generic;
using System.Linq;
using System.Xml;
using XmlAssertions.Checks;

// ReSharper disable UseStringInterpolation

namespace XmlAssertions
{
    public class XmlAssertable : IXmlAssertable
    {
        private readonly AssertContext _assertContext;
        private readonly AttributeCheck _attributeCheck;
        private readonly NameCheck _nameCheck;
        private readonly TextCheck _textCheck;

        public XmlAssertable(XmlNode xmlNode, XmlPath myPath)
        {
            _assertContext = new AssertContext
            {
                MyPath = myPath,
                XmlNode = xmlNode
            };
            _assertContext.SetStringComparer(true);
            _attributeCheck = new AttributeCheck(_assertContext);
            _nameCheck = new NameCheck(_assertContext);
            _textCheck = new TextCheck(_assertContext);
        }


        public void BeEqualTo(string expected)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(expected);
            BeEqualTo(xmlDoc.DocumentElement);
        }

        public void BeEqualTo(XmlNode expected)
        {
            BeEqualShallowTo(expected);
            AssertChildNumber(expected);

            var childrenActual = ExtractChildNodes(_assertContext.XmlNode);
            var childrenExpected = ExtractChildNodes(expected);
            for (var i = 0; i < childrenActual.Count; i++)
            {
                var childAssertable = new XmlAssertable(childrenActual[i],
                    _assertContext.MyPath.Append(childrenActual[i].Name, i));
                childAssertable.BeEqualTo(childrenExpected[i]);
            }
        }

        public void HaveAttribute(string attributeName)
        {
            _attributeCheck.HaveAttribute(attributeName);
        }

        public void HaveAttribute(string attributeName, string attributeValue)
        {
            _attributeCheck.HaveAttribute(attributeName, attributeValue);
        }

        public void HaveName(string expectedName)
        {
            _nameCheck.HaveName(expectedName);
        }

        public void BeEqualShallowTo(XmlNode expected)
        {
            _nameCheck.HaveName(expected.Name);
            _attributeCheck.HaveAttributes(expected.Attributes);
            _textCheck.HaveText(expected);
        }


        private static List<XmlNode> ExtractChildNodes(XmlNode parentNode)
        {
            return parentNode.ChildNodes.Cast<XmlNode>().ToList();
        }

        private void AssertChildNumber(XmlNode expected)
        {
            var childrenActual = ExtractChildNodes(_assertContext.XmlNode);
            var childrenExpected = ExtractChildNodes(expected);
            var equalChildrenNumber = childrenActual.Count == childrenExpected.Count;
            if (!equalChildrenNumber)
            {
                _assertContext.ThrowErrorMessage(string.Format("Number of children is " +
                                                               "incorrent, expected [{0}], but was [{1}]",
                    childrenExpected.Count,
                    childrenActual.Count));
            }
        }

        public XmlAssertable BeCaseSensitive()
        {
            _assertContext.SetStringComparer(false);
            return this;
        }

        public XmlAssertable BeCaseInsensitive()
        {
            _assertContext.SetStringComparer(true);
            return this;
        }
    }
}