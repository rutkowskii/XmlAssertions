using System;

namespace XmlAssertions
{
    public class XmlAttributeSimplified : IEquatable<XmlAttributeSimplified> // todo case sensitivity / insensitivity. 
    {
        public XmlAttributeSimplified(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }

        #region eq

        public bool Equals(XmlAttributeSimplified other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Value, other.Value); //todo case sensitivity 
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((XmlAttributeSimplified) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }

        #endregion eq
    }
}