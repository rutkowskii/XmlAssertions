namespace XmlAssertions
{
    public static class XmlExc
    {
        public static void Throw(string message)
        {
            throw new XmlAssertionException(message);
        }
    }
}