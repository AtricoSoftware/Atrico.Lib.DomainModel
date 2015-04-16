using System;

namespace Atrico.Lib.DomainModel
{
    public abstract class ComparableValueObject<T> : ValueObject<T>, IComparable, IComparable<T> where T : ValueObject<T>
    {
        public int CompareTo(object obj)
        {
            return CompareTo(obj as T);
        }

        public int CompareTo(T other)
        {
            // Null is smallest
            if (ReferenceEquals(other, null)) return -1;
            // If types are different, compare by type
            var comp = String.Compare(GetType().FullName, other.GetType().FullName, StringComparison.Ordinal);
            return comp != 0 ? comp : CompareToImpl(other);
        }

        protected abstract int CompareToImpl(T other);
    }
}