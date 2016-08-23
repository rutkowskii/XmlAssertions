namespace XmlAssertions.Exceptions
{
    internal static class XmlExc
    {
        public static void Throw(XmlPath path, string message)
        {
            var effectiveMessage = WrapMessage(path.ToString(), message);
            throw new XmlAssertionException(effectiveMessage);
        }

        public static string WrapMessage(string path, string message)
        {
            return string.Format("Node {0}; {1}", path, message);
        }
    }
}