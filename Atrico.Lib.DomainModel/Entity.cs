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

        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public bool Equals(T other)
        {
            return !ReferenceEquals(other, null) && GetType() == other.GetType() && EntityKey.Equals(other.EntityKey);
        }

        public override int GetHashCode()
        {
            return EntityKey.GetHashCode();
        }
    }
}