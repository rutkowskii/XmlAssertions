using System;

namespace XmlAssertions
{
    internal class AssertContextBuilder
    {
        private readonly StringComparer _stringComparer;

        private AssertContextBuilder(StringComparer stringComparer)
        {
            _stringComparer = stringComparer;
        }

        public AssertContext Build(XmlPath path, XmlNodeSimplified node)
        {
            return new AssertContext(path, node, _stringComparer);
        }

        public AssertContext Build(AssertContext context)
        {
            return new AssertContext(context.MyPath, context.XmlNode, _stringComparer);
        }

        public static AssertContextBuilder Default
        {
            get { return CaseSensitive; }
        }

        public static AssertContextBuilder CaseSensitive
        {
            get { return new AssertContextBuilder(StringComparer.InvariantCulture); }
        }

        public static AssertContextBuilder CaseInsensitive
        {
            get { return new AssertContextBuilder(StringComparer.InvariantCultureIgnoreCase); }
        }
    }
}