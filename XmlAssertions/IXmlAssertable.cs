using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace XmlAssertions
{
    public interface IXmlAssertable
    {
        void BeEqualTo(string expected);
        void BeEqualTo(XmlNode expected);
        void HaveAttribute(string attributeName);
        void HaveAttribute(string attributeName, string attributeValue);
        void HaveName(string expectedName);
        void BeEqualShallowTo(XmlNode expected);
    }
}