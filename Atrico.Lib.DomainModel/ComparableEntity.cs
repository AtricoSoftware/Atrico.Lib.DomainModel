using System;

namespace Atrico.Lib.DomainModel
{
    public abstract class ComparableEntity<T, TKey> : Entity<T, TKey>, IComparable, IComparable<T>
        where T : ComparableEntity<T, TKey>
        where TKey : ComparableValueObject<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        protected ComparableEntity(TKey entityKey)
            : base(entityKey)
        {
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as T);
        }

        public int CompareTo(T other)
        {
            // Null is smallest
            if (ReferenceEquals(other, null)) return 1;
            // If types are different, compare by type
            var comp = String.Compare(GetType().FullName, other.GetType().FullName, StringComparison.Ordinal);
            return comp != 0 ? comp : EntityKey.CompareTo(other.EntityKey);
        }

        public static bool operator <(ComparableEntity<T, TKey> x, ComparableEntity<T, TKey> y)
        {
            return x.CompareTo(y) < 0;
        }

        public static bool operator >(ComparableEntity<T, TKey> x, ComparableEntity<T, TKey> y)
        {
            return x.CompareTo(y) > 0;
        }

        public static bool operator <=(ComparableEntity<T, TKey> x, ComparableEntity<T, TKey> y)
        {
            return x.CompareTo(y) <= 0;
        }

        public static bool operator >=(ComparableEntity<T, TKey> x, ComparableEntity<T, TKey> y)
        {
            return x.CompareTo(y) >= 0;
        }
    }
}