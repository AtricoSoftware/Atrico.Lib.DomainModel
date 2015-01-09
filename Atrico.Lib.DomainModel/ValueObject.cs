using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Atrico.Lib.DomainModel
{
	public abstract class ValueObject<T> : IEquatable<T>
		where T : ValueObject<T>
	{
		private readonly Lazy<object[]> _values;
		private readonly Lazy<int> _hashCode;

		/// <summary>
		/// Constructor
		/// </summary>
		protected ValueObject()
		{
			_values = new Lazy<object[]>(GetValues);
			_hashCode = new Lazy<int>(CreateHashCode);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			var other = obj as T;

			return Equals(other);
		}

		public override int GetHashCode()
		{
			return _hashCode.Value;
		}

		public virtual bool Equals(T other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}

			var t = GetType();
			var otherType = other.GetType();

			if (t != otherType)
			{
				return false;
			}

			return !_values.Value.Where((t1, idx) => !t1.Equals(other._values.Value[idx])).Any();
		}

		public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
		{
			return ReferenceEquals(x, null) ? ReferenceEquals(y, null) : x.Equals(y);
		}

		public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
		{
			return !(x == y);
		}

		private object[] GetValues()
		{
			var t = GetType();

			var fields = new List<FieldInfo>();

			// ReSharper disable once PossibleNullReferenceException (In first instance, t is derived from ValueType)
			while (t.BaseType != typeof (object))
			{
				fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

				t = t.BaseType;
			}

			return fields.Select(field => field.GetValue(this) ?? new Null()).ToArray();
		}

		private int CreateHashCode()
		{
			const int startValue = 17;
			const int multiplier = 59;

			return _values.Value.Aggregate(startValue, (current, value) => current * multiplier + value.GetHashCode());
		}

		private class Null
		{
			public override int GetHashCode()
			{
				return 0;
			}

			public override bool Equals(object obj)
			{
				return obj is Null;
			}
		}
	}
}
