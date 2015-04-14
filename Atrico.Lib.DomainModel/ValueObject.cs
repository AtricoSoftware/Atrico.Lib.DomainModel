using System;

namespace Atrico.Lib.DomainModel
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public override int GetHashCode()
        {
            return GetHashCodeImpl();
        }

        public virtual bool Equals(T other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return EqualsImpl(other);
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            return ReferenceEquals(x, null) ? ReferenceEquals(y, null) : x.Equals(y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }

        protected abstract int GetHashCodeImpl();
        protected abstract bool EqualsImpl(T other);
    }
}
