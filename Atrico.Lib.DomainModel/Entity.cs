using System;

namespace Atrico.Lib.DomainModel
{
	/// <summary>
	///     This is a trivial class that is used to make sure that Equals and GetHashCode
	///     are properly overloaded with the correct semantics.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public abstract class Entity<T, TKey> where T : Entity<T, TKey>, IEquatable<T>
	{
		private readonly TKey _id;

		/// <summary>
		/// Constructor
		/// </summary>
		protected Entity()
			: this(default(TKey))
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		protected Entity(TKey id)
		{
			_id = id;
		}

		public bool Equals(T other)
		{
			return Equals(other as object);
		}

		public override bool Equals(object obj)
		{
			var other = obj as T;

			if (other == null) return false;

			//to handle the case of comparing two new objects

			var otherIsTransient = Equals(other._id, default(TKey));
			var thisIsTransient = Equals(_id, default(TKey));

			if (otherIsTransient && thisIsTransient) return ReferenceEquals(other, this);

			return other._id.Equals(_id);
		}

		public override int GetHashCode()
		{
			var thisIsTransient = Equals(_id, default(TKey));

			return !thisIsTransient ? _id.GetHashCode() : default(TKey).GetHashCode();
		}

		public static bool operator ==(Entity<T, TKey> x, Entity<T, TKey> y)
		{
			return Equals(x, y);
		}

		public static bool operator !=(Entity<T, TKey> x, Entity<T, TKey> y)
		{
			return !(x == y);
		}
	}

	/// <summary>
	/// Entity type with guid for entity id
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Entity<T> : Entity<T, Guid> where T : Entity<T, Guid>, IEquatable<T>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		protected Entity()
			: base(Guid.NewGuid())
		{
		}
	}
}