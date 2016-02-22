using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlAssertions.Checks
{
    internal class AttributeCheck
    {
        private readonly AssertContext _assertContext;

        public AttributeCheck(AssertContext assertContext)
        {
            _assertContext = assertContext;
        }

        public void AssertAttributesCollection(IEnumerable<XmlAttributeSimplified> expected)
        {
            AssertAttributeNames(expected);
            AssertAttributeValues(expected);
        }

        public void AssertAttributeExists(string attributeName)
        {
            var attribute = GetAttributeByName(attributeName);
            var attributeFound = attribute != null;
            if (!attributeFound)
            {
                var exceptionMessage = string.Format("Expected attribute [{0}] was not found", attributeName);
                _assertContext.ThrowErrorMessage(exceptionMessage);
            }
        }

        public void AssertAttributeExists(string attributeName, string expectedAttributeValue)
        {
            AssertAttributeExists(attributeName);

            var attribute = GetAttributeByName(attributeName);
            var properValue = _assertContext.StringComparer.Equals(attribute.Value, expectedAttributeValue);
            if (!properValue)
            {
                var exceptionMessage = string.Format("Expected attribute [{0}] ", attributeName) +
                                       string.Format("with value [{0}], ", expectedAttributeValue) +
                                       string.Format("but found [{0}]", attribute.Value);
                _assertContext.ThrowErrorMessage(exceptionMessage);
            }
        }

        private XmlAttributeSimplified GetAttributeByName(string attributeName)
        {
            return _assertContext.NodeAttributesSimplified.FirstOrDefault(
                attr => _assertContext.StringComparer.Equals(attr.Name, attributeName));
        }

        private void AssertAttributeValues(IEnumerable<XmlAttributeSimplified> expected)
        {
            foreach (var attributeSimplified in expected)
            {
                AssertAttributeExists(attributeSimplified.Name, attributeSimplified.Value);
            }
        }

        private void AssertAttributeNames(IEnumerable<XmlAttributeSimplified> expected)
        {
            var expectedAttrNames = expected.Select(a => a.Name);
            var redundantAttrs = _assertContext.NodeAttributeNames.Except(expectedAttrNames,
                _assertContext.StringComparer);
            var lackingAttrs = expectedAttrNames.Except(_assertContext.NodeAttributeNames, _assertContext.StringComparer);
            var attributeKeysAreOkay = !redundantAttrs.Any() && !lackingAttrs.Any();
            if (!attributeKeysAreOkay)
            {
                var message = GetExceptionMessageForAttributesCollections(
                    redundantAttrs, lackingAttrs
                    );
                _assertContext.ThrowErrorMessage(message);
            }
        }

        private string GetExceptionMessageForAttributesCollections(
            IEnumerable<string> redundantAttrs,
            IEnumerable<string> lackingAttrs)
        {
            var sb = new StringBuilder();
            sb.Append("Attributes collection does not match expected state, ");
            if (redundantAttrs.Any())
            {
                sb.Append(string.Format("redundant attributes found: [{0}]", string.Join(", ", redundantAttrs)));
            }
            if (lackingAttrs.Any())
            {
                sb.Append(string.Format("lacking attributes: [{0}]", string.Join(", ", lackingAttrs)));
            }
            return sb.ToString();
        }
    }
}
