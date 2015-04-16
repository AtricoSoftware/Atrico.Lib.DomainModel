using System;

namespace Atrico.Lib.DomainModel
{
    /// <summary>
    ///     This is a trivial class that is used to make sure that Equals and GetHashCode
    ///     are properly overloaded with the correct semantics.
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <typeparam name="TKey">Type of unique key/identifier</typeparam>
    public abstract class Entity<T, TKey> : IEquatable<T> where T : Entity<T, TKey> where TKey : ValueObject<TKey>
    {
        protected TKey EntityKey { get; private set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        protected Entity(TKey entityKey)
        {
            EntityKey = entityKey;
        }

        public bool Equals(T other)
        {
            return Equals(other as object);
        }

        public override bool Equals(object obj)
        {
            var other = obj as T;

            if (other == null)
            {
                return false;
            }
            if (!(GetType() == other.GetType())) return false;

            // To handle the case of comparing two new objects
            var otherIsTransient = ReferenceEquals(other.EntityKey, null);
            var thisIsTransient = ReferenceEquals(EntityKey, null);

            if (otherIsTransient && thisIsTransient)
            {
                return ReferenceEquals(other, this);
            }
            if (otherIsTransient || thisIsTransient)
            {
                return false;
            }

            return other.EntityKey.Equals(EntityKey);
        }

        public override int GetHashCode()
        {
            var thisIsTransient = Equals(EntityKey, null);

            return !thisIsTransient ? EntityKey.GetHashCode() : 0;
        }
    }
}