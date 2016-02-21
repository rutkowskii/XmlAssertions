using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlAssertions.Checks
{
    internal class NameCheck
    {
        private AssertContext _assertContext;

        public NameCheck(AssertContext assertContext)
        {
            _assertContext = assertContext;
        }

        //todo namespace

        public void AssertName(string expectedName)
        {
            var actualName = _assertContext.XmlNode.Name;
            var everythingOk = _assertContext.StringComparer.Equals(actualName, expectedName);
            if (everythingOk)
            {
                return;
            }
            var exceptionMessage = string.Format("Expected xml node with name [{0}], but found [{1}]", 
                expectedName, actualName);
            _assertContext.ThrowErrorMessage(exceptionMessage);
        }
    }
}
