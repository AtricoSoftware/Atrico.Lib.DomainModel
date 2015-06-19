using Atrico.Lib.Common;

namespace Atrico.Lib.DomainModel
{
    public abstract class ValueObject<T> : EquatableObject<T>
        where T : ValueObject<T>
    {
        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            return ReferenceEquals(x, null) ? ReferenceEquals(y, null) : x.Equals(y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }
    }
}
