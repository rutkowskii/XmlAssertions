using System.Collections.Generic;
using System.Linq;
using System.Xml;
using XmlAssertions.Checks;

// ReSharper disable UseStringInterpolation

namespace XmlAssertions
{
    internal class XmlAssertable : IXmlAssertable
    {
        private readonly AssertContext _assertContext;
        private readonly AttributeCheck _attributeCheck;
        private readonly NameCheck _nameCheck;
        private readonly TextCheck _textCheck;
        private readonly ChildrenNumberCheck _childrenNumberCheck;

        public XmlAssertable(AssertContext assertContext)
        {
            _assertContext = assertContext;
            _attributeCheck = new AttributeCheck(_assertContext);
            _nameCheck = new NameCheck(_assertContext);
            _textCheck = new TextCheck(_assertContext);
            _childrenNumberCheck = new ChildrenNumberCheck(_assertContext);
        }        

        public void BeEqualTo(string expected)
        {
            BeEqualTo(expected.ToXmlElement());
        }

        public void BeEqualTo(XmlNode expected)
        {
           BeEqualTo(expected.Simplify());
        }

        public void BeEqualTo(XmlNodeSimplified expected)
        {
            BeEqualShallowTo(expected);
            _childrenNumberCheck.AssertChildrenNumber(expected);

            var childrenActual = _assertContext.XmlNode.Children.ToList();
            var childrenExpected = expected.Children.ToList();
            for (var i = 0; i < childrenActual.Count; i++)
            {
                var childAssertable = CreateChildAssertable(childrenActual, i);
                childAssertable.BeEqualTo(childrenExpected[i]);
            }
        }

        private XmlAssertable CreateChildAssertable(IList<XmlNodeSimplified> childrenActual, int i)
        {
            var childPath = _assertContext.BuildChildContext(childrenActual[i], i);
            return new XmlAssertable(childPath);
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
            BeEqualShallowTo(expected.Simplify());
        }

        public void BeEqualShallowTo(string expected)
        {
            BeEqualShallowTo(expected.ToXmlElement());
        }

        private void BeEqualShallowTo(XmlNodeSimplified expected)
        {
            _nameCheck.AssertName(expected.Name);
            _attributeCheck.AssertAttributesCollection(expected.Attributes);
            _textCheck.AssertText(expected);
        }

        public IXmlAssertable CheckLetterCase()
        {
            return new XmlAssertable(AssertContextBuilder.CaseSensitive.Build(_assertContext));
        }

        public IXmlAssertable IgnoreCase()
        {
            return new XmlAssertable(AssertContextBuilder.CaseInsensitive.Build(_assertContext));
        }
    }
}