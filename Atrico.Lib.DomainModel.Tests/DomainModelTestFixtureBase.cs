using System;
using Atrico.Lib.Testing;

namespace Atrico.Lib.DomainModel.Tests
{
    public abstract class DomainModelTestFixtureBase : TestFixtureBase
    {
        public class TestKey : ValueObject<TestKey>
        {
            private readonly int _value;

            public TestKey(int value)
            {
                _value = value;
            }

            protected override int GetHashCodeImpl()
            {
                return _value.GetHashCode();
            }

            protected override bool EqualsImpl(TestKey other)
            {
                return _value.Equals(other._value);
            }

            public override string ToString()
            {
                return String.Format("Key({0})", _value);
            }
        }

        public class TestEntity : Entity<TestEntity, TestKey>
        {
            private readonly string _text = new RandomValueGenerator().String();

            public TestEntity(int key)
                : base(new TestKey(key))
            {
            }

            public override string ToString()
            {
                return String.Format("{0}({1},'{2}')", GetType().Name, EntityKey, _text);
            }
        }

        public class TestEntityDerived : TestEntity
        {
            public TestEntityDerived(int value)
                : base(value)
            {
            }
        }

        public class TestComparableKey : ComparableValueObject<TestComparableKey>
        {
            private readonly int _value;

            public TestComparableKey(int value)
            {
                _value = value;
            }

            protected override int GetHashCodeImpl()
            {
                return _value.GetHashCode();
            }

            protected override bool EqualsImpl(TestComparableKey other)
            {
                return _value.Equals(other._value);
            }

            protected override int CompareToImpl(TestComparableKey other)
            {
                return _value.CompareTo(other._value);
            }

            public override string ToString()
            {
                return String.Format("CKey({0})", _value);
            }
        }

        public class TestComparableEntity : ComparableEntity<TestComparableEntity, TestComparableKey>
        {
            private readonly string _text = new RandomValueGenerator().String();

            public TestComparableEntity(int key)
                : base(new TestComparableKey(key))
            {
            }

            public override string ToString()
            {
                return String.Format("{0}({1},'{2}')", GetType().Name, EntityKey, _text);
            }
        }

        public class TestComparableEntityDerived : TestComparableEntity
        {
            public TestComparableEntityDerived(int key)
                : base(key)
            {
            }
        }

        public class TestValueObject : ValueObject<TestValueObject>
        {
            private readonly int _value;

            public TestValueObject(int value)
            {
                _value = value;
            }

            protected override int GetHashCodeImpl()
            {
                return _value.GetHashCode();
            }

            protected override bool EqualsImpl(TestValueObject other)
            {
                return _value.Equals(other._value);
            }

            public override string ToString()
            {
                return String.Format("{0}({1})", GetType().Name, _value);
            }
        }

        public class TestValueObjectDerived : TestValueObject
        {
            public TestValueObjectDerived(int value)
                : base(value)
            {
            }
        }

        public class TestComparableValueObject : ComparableValueObject<TestComparableValueObject>
        {
            private readonly int _value;

            public TestComparableValueObject(int value)
            {
                _value = value;
            }

            protected override int GetHashCodeImpl()
            {
                return _value.GetHashCode();
            }

            protected override bool EqualsImpl(TestComparableValueObject other)
            {
                return _value.Equals(other._value);
            }

            protected override int CompareToImpl(TestComparableValueObject other)
            {
                return _value.CompareTo(other._value);
            }

            public override string ToString()
            {
                return String.Format("{0}({1})", GetType().Name, _value);
            }
        }

        public class TestComparableValueObjectDerived : TestComparableValueObject
        {
            public TestComparableValueObjectDerived(int value)
                : base(value)
            {
            }
        }
    }
}