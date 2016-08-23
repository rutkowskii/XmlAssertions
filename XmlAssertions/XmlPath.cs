using System.Collections.Generic;
using System.Text;

namespace XmlAssertions
{
    //todo should we switch to XPath?

    internal class XmlPath
    {
        private readonly List<XmlPathMember> _members;

        public XmlPath(string rootname)
        {
            _members = new List<XmlPathMember>();
            _members.Add(new XmlPathMember
            {
                Name = rootname
            });
        }

        public XmlPath(List<XmlPathMember> members)
        {
            _members = members;
        }

        public XmlPath AppendWith(string name, int index)
        {
            AddToMembersCollection(name, index);
            return this;
        }

        public XmlPath Append(string name, int index)
        {
            var listCopied = new List<XmlPathMember>(_members);
            var result = new XmlPath(listCopied);
            return result.AppendWith(name, index);
        }

        private void AddToMembersCollection(string name, int index)
        {
            _members.Add(new XmlPathMember
            {
                Name = name,
                Index = index
            });
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("/");
            foreach (var pathMember in _members)
            {
                sb.Append("/" + string.Format("{0}[{1}]", pathMember.Name, pathMember.Index));
            }
            return sb.ToString();
        }
    }

    //todo culture when comparing strings. 
}