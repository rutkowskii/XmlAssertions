using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
// ReSharper disable UseStringInterpolation

namespace XmlAssertions
{
    public class XmlAssertable : IXmlAssertable
    {
        private readonly XmlNode _xmlNode;
        private readonly XmlPath _myPath;
        private StringComparer _stringComparer;

        public XmlAssertable(XmlNode xmlNode, XmlPath myPath)
        {
            _xmlNode = xmlNode;
            _myPath = myPath;
            _stringComparer = StringComparer.InvariantCultureIgnoreCase;
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

            var childrenActual = _xmlNode.ChildNodes.Cast<XmlNode>().ToList();
            var childrenExpected = expected.ChildNodes.Cast<XmlNode>().ToList();
            for (int i = 0; i < childrenActual.Count; i++)
            {
                var childAssertable = new XmlAssertable(childrenActual[i], _myPath.Append(childrenActual[i].Name, i));
                childAssertable.BeEqualTo(childrenExpected[i]);
            }
        }

        private void AssertChildNumber(XmlNode expected)
        {
            var childrenActual = _xmlNode.ChildNodes.Cast<XmlNode>().ToList();
            var childrenExpected = expected.ChildNodes.Cast<XmlNode>().ToList();
            var equalChildrenNumber = childrenActual.Count == childrenExpected.Count;
            if (!equalChildrenNumber)
            {
                ThrowErrorMessage(string.Format("Number of children is " +
                                           "incorrent, expected [{0}], but was [{1}]", childrenExpected.Count, childrenActual.Count));
            }
        }

        public void BeEqualShallowTo(XmlNode expected)
        {
            HaveName(expected.Name);
            HaveAttributes(expected.Attributes);
            HaveText(expected);
        }

        private void HaveText(XmlNode expectedNode)
        {
            if (expectedNode.NodeType != XmlNodeType.Text) return;

            var expectedText = expectedNode.InnerText;
            if (_xmlNode.NodeType != XmlNodeType.Text)
            {
                ThrowErrorMessage(string.Format("Expected text, but found [{0}]", _xmlNode.NodeType));
            }
            var actualText = _xmlNode.InnerText;
            var eq = _stringComparer.Equals(actualText, expectedText);
            if (!eq)
            {
                ThrowErrorMessage(string.Format("Expected text [{0}], but found [{1}]", expectedText, actualText));
            }
        }

        //todo namespace

        public void HaveName(string expectedName)
        {
            var actualName = _xmlNode.Name;
            var everythingOk = _stringComparer.Equals(actualName, expectedName);
            if (!everythingOk)
            {
                ThrowErrorMessage(String.Format("Expected xml node with name [{0}], but found [{1}]", expectedName,
                    actualName));
            }
        }

        public void HaveAttributes(XmlAttributeCollection expected)
        {
            HaveAttributes(ExtractExpectedAttributes(expected));
        }

        private IEnumerable<XmlAttributeSimplified> ExtractExpectedAttributes(XmlAttributeCollection expected)
        {
            return expected == null 
                ? Enumerable.Empty<XmlAttributeSimplified>() 
                : expected.Cast<XmlAttribute>().Select(SimplifyXmlAttribute);
        }

        public void HaveAttributes(IEnumerable<XmlAttributeSimplified> expected)
        {
            AssertAttributeNames(expected);
            AssertAttributeValues(expected);
        }

        private void AssertAttributeValues(IEnumerable<XmlAttributeSimplified> expected)
        {
            foreach (var attributeSimplified in expected)
            {
                HaveAttribute(attributeSimplified.Name, attributeSimplified.Value);
            }
        }

        private void AssertAttributeNames(IEnumerable<XmlAttributeSimplified> expected)
        {
            var expectedAttrNames = expected.Select(a => a.Name);
            var redundantAttrs = NodeAttributeNames.Except(expectedAttrNames, _stringComparer);
            var lackingAttrs = expectedAttrNames.Except(NodeAttributeNames, _stringComparer);
            var attributeKeysAreOkay = !redundantAttrs.Any() && !lackingAttrs.Any();
            if (!attributeKeysAreOkay)
            {
                var message = GetExceptionMessageForAttributesCollections(
                    redundantAttrs, lackingAttrs
                    );
                ThrowErrorMessage(message);
            }
        }

        private string GetExceptionMessageForAttributesCollections(
            IEnumerable<string> redundantAttrs, 
            IEnumerable<string> lackingAttrs)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Attributes collection does not match expected state, ");
            if (redundantAttrs.Any())
            {
                sb.Append(String.Format("redudant attributes found: [{0}]", string.Join(", ", redundantAttrs)));
            }
            if (lackingAttrs.Any())
            {
                sb.Append(String.Format("lacking attributes: [{0}]", string.Join(", ", lackingAttrs))); 
            }
            return sb.ToString();
        }

        public void HaveAttribute(string attributeName)
        {
            var attribute = GetAttributeByName(attributeName);
            var attributeFound = attribute != null;
            if (!attributeFound)
            {
                ThrowErrorMessage(string.Format("Expected attribute [{0}] was not found", attributeName));
            }
        }

        public void HaveAttribute(string attributeName, string expectedAttributeValue)
        {
            HaveAttribute(attributeName);

            var attribute = GetAttributeByName(attributeName);
            var properValue = _stringComparer.Equals(attribute.Value , expectedAttributeValue);
            if (!properValue)
            {
                ThrowErrorMessage(string.Format("Expected attribute [{0}] ", attributeName) +
                             string.Format("with value [{0}], ", expectedAttributeValue) +
                             string.Format("but found [{0}]", attribute.Value)); 
            }
        }

        private void ThrowErrorMessage(string message)
        {
            XmlExc.Throw(_myPath, message);
        }

        private XmlAttribute GetAttributeByName(string attributeName)
        {
            return NodeAttributes.FirstOrDefault(
                attr => _stringComparer.Equals(attr.Name, attributeName));
        }

        public XmlAssertable BeCaseSensitive()
        {
            SetStringComparer(false);
            return this;
        }

        public XmlAssertable BeCaseInsensitive()
        {
            SetStringComparer(true);
            return this;
        }

        private void SetStringComparer(bool ignoreCase)
        {
            _stringComparer = ignoreCase
                ? StringComparer.InvariantCultureIgnoreCase
                : StringComparer.InvariantCulture;
        }

        private IEnumerable<XmlAttribute> NodeAttributes
        {
            get
            {
                return _xmlNode.Attributes != null 
                    ? _xmlNode.Attributes.Cast<XmlAttribute>() 
                    : Enumerable.Empty<XmlAttribute>();
            }
        } 

        private IEnumerable<XmlAttributeSimplified> NodeAttributesSimplified
        {
            get {  return NodeAttributes.Select(SimplifyXmlAttribute); }
        }

        private XmlAttributeSimplified SimplifyXmlAttribute(XmlAttribute attr)
        {
            return new XmlAttributeSimplified(attr.Name, attr.Value);
        }

        private IEnumerable<string> NodeAttributeNames
        {
            get { return NodeAttributesSimplified.Select(attr => attr.Name); }
        } 
    }
}