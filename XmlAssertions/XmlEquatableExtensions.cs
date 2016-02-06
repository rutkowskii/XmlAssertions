using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XmlAssertions
{
    public static class XmlEquatableExtensions
    {
        public static IXmlAssertable XmlShould(this XmlNode node)
        {
            return new XmlAssertable(node);
        }
    }
}
