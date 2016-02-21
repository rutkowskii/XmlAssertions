using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlAssertions.Checks
{
    internal class TextCheck
    {
        private AssertContext _assertContext;

        public TextCheck(AssertContext assertContext)
        {
            _assertContext = assertContext;
        }

        public void AssertText(XmlNode expectedNode)
        {
            if (expectedNode.NodeType != XmlNodeType.Text) return;

            var expectedText = expectedNode.InnerText;
            if (_assertContext.XmlNode.NodeType != XmlNodeType.Text)
            {
                var exceptionMsg = string.Format("Expected text, but found [{0}]", 
                    _assertContext.XmlNode.NodeType);
                _assertContext.ThrowErrorMessage(exceptionMsg);
            }
            var actualText = _assertContext.XmlNode.InnerText;
            var eq = _assertContext.StringComparer.Equals(actualText, expectedText);
            if (!eq)
            {
                var exceptionMsg = string.Format("Expected text [{0}], but found [{1}]", 
                    expectedText, actualText);
                _assertContext.ThrowErrorMessage(exceptionMsg);
            }
        }
    }
}
