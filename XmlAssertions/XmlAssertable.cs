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
        private StringComparer _stringComparer;

        public  XmlAssertable(XmlNode xmlNode)
        {
            _xmlNode = xmlNode;
            _stringComparer = StringComparer.InvariantCultureIgnoreCase;
        }

        public void BeEqualTo(string expected)
        {
            throw new System.NotImplementedException();
        }

        public void BeEqualTo(XmlNode expected)
        {
            BeEqualShallowTo(expected);
        }
        public void BeEqualShallowTo(XmlNode expected)
        {
            HaveName(expected.Name);
            HaveAttributes(expected.Attributes);
        }

        //todo namespace

        public void HaveName(string expectedName)
        {
            var actualName = _xmlNode.Name;
            var everythingOk = _stringComparer.Equals(actualName, expectedName);
            if (!everythingOk)
            {
                XmlExc.Throw(String.Format("Expected xml node with name [{0}], but found [{1}]", expectedName,
                    actualName));
            }
        }

        public void HaveAttributes(XmlAttributeCollection expected)
        {
            HaveAttributes(expected.Cast<XmlAttribute>().Select(SimplifyXmlAttribute));
        }

        public void HaveAttributes(IEnumerable<XmlAttributeSimplified> expected)
        {
            var actual = NodeAttributesSimplified;
            var actualAttrNames = actual.Select(a => a.Name);
            var redundantAttrs = NodeAttributeNames.Except(actualAttrNames, _stringComparer);
            var lackingAttrs = actualAttrNames.Except(NodeAttributeNames, _stringComparer);
            var everythingOk = !redundantAttrs.Any() && !lackingAttrs.Any();
            if (!everythingOk)
            {
                var message = GetExceptionMessageForAttributesCollections(
                    redundantAttrs, lackingAttrs
                    );
                XmlExc.Throw(message);
            }
        }

        private string GetExceptionMessageForAttributesCollections(
            IEnumerable<string> redundantAttrs, 
            IEnumerable<string> lackingAttrs)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Attributes collection does not match expected state. ");
            if (redundantAttrs.Any())
            {
                sb.Append(String.Format("Redudant attributes found: [{0}]. ", string.Join(", ", redundantAttrs)));
            }
            if (lackingAttrs.Any())
            {
                sb.Append(String.Format("Lacking attributes: [{0}]. ", string.Join(", ", lackingAttrs))); 
                //todo string format, bo w robocie mamy stare visuale. 
            }
            return sb.ToString();
        }

        public void HaveAttribute(string attributeName)
        {
            var attribute = GetAttributeByName(attributeName);
            var attributeFound = attribute != null;
            if (!attributeFound)
            {
                XmlExc.Throw(string.Format("Expected attribute [{0}] was not found", attributeName));
            }
        }

        public void HaveAttribute(string attributeName, string expectedAttributeValue)
        {
            HaveAttribute(attributeName);

            var attribute = GetAttributeByName(attributeName);
            var properValue = _stringComparer.Equals(attribute.Value , expectedAttributeValue);
            if (!properValue)
            {
                XmlExc.Throw(string.Format("Expected attribute [{0}] ", attributeName) +
                             string.Format("with value [{0}], ", expectedAttributeValue) +
                             string.Format("but found value [{0}]", attribute.Value)); 
            }
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

        private IEnumerable<XmlAttribute> NodeAttributes => _xmlNode.Attributes.Cast<XmlAttribute>();

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