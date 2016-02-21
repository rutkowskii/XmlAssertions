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
        private readonly ChildrenNumberCheck _childrenNumberCheck;

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
            _childrenNumberCheck = new ChildrenNumberCheck(_assertContext);
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
            _childrenNumberCheck.AssertChildrenNumber(expected);

            var childrenActual = XmlUtils.ExtractChildNodes(_assertContext.XmlNode);
            var childrenExpected = XmlUtils.ExtractChildNodes(expected);
            for (var i = 0; i < childrenActual.Count; i++)
            {
                var childAssertable = new XmlAssertable(childrenActual[i],
                    _assertContext.MyPath.Append(childrenActual[i].Name, i));
                childAssertable.BeEqualTo(childrenExpected[i]);
            }
        }

        public void HaveAttribute(string attributeName)
        {
            _attributeCheck.AssertAttributeExists(attributeName);
        }

        public void HaveAttribute(string attributeName, string attributeValue)
        {
            _attributeCheck.AssertAttributeExists(attributeName, attributeValue);
        }

        public void HaveName(string expectedName)
        {
            _nameCheck.AssertName(expectedName);
        }

        public void BeEqualShallowTo(XmlNode expected)
        {
            _nameCheck.AssertName(expected.Name);
            _attributeCheck.AssertAttributesCollection(expected.Attributes);
            _textCheck.AssertText(expected);
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