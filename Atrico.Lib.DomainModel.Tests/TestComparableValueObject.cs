using System;
using System.Diagnostics;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.NUnitAttributes;

namespace Atrico.Lib.DomainModel.Tests
{
    [TestFixture]
    public class TestComparableValueObject : TestFixtureBase
    {
        private class TestObject : ComparableValueObject<TestObject>
        {
            private readonly int _value;

            public TestObject(int value)
            {
                _value = value;
            }

            protected override int GetHashCodeImpl()
            {
                return _value.GetHashCode();
            }

            protected override bool EqualsImpl(TestObject other)
            {
                return _value.Equals(other._value);
            }

            protected override int CompareToImpl(TestObject other)
            {
                return _value.CompareTo(other._value);
            }

            public override string ToString()
            {
                return string.Format("{0}({1})", GetType().Name, _value);
            }
        }

        private const int _pivot = 10;

        #region Compare values

        [Test]
        public void TestCompare([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObject(val);
            var expected = _pivot.CompareTo(val);
            Debug.WriteLine("{0} compareto {1} = {2}", _pivot, val, expected);

            // Act
            var result = obj1.CompareTo(obj2);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestLessThan([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObject(val);
            var expected = _pivot < val;
            Debug.WriteLine("{0} < {1} = {2}", _pivot, val, expected);

            // Act
            var result = obj1 < obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestGreaterThan([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObject(val);
            var expected = _pivot > val;
            Debug.WriteLine("{0} > {1} = {2}", _pivot, val, expected);

            // Act
            var result = obj1 > obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestLessThanEquals([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObject(val);
            var expected = _pivot <= val;
            Debug.WriteLine("{0} <= {1} = {2}", _pivot, val, expected);

            // Act
            var result = obj1 <= obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestGreaterThanEquals([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObject(val);
            var expected = _pivot >= val;
            Debug.WriteLine("{0} >= {1} = {2}", _pivot, val, expected);

            // Act
            var result = obj1 >= obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        #endregion

        #region Compare with null

        [Test]
        public void TestCompareWithNull()
        {
            // Arrange
            var val = RandomValues.Integer();
            var obj1 = new TestObject(val);
            const int expected = 1; // Null is lowest
            Debug.WriteLine("{0} compareto NULL = {1}", val, expected);

            // Act
            var result = obj1.CompareTo(null);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestLessThanWithNull()
        {
            // Arrange
            var val = RandomValues.Integer();
            var obj1 = new TestObject(val);
            const bool expected = false; // Null is lowest
            Debug.WriteLine("{0} < NULL = {1}", val, expected);

            // Act
            var result = obj1 < null;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestGreaterThanWithNull()
        {
            // Arrange
            var val = RandomValues.Integer();
            var obj1 = new TestObject(val);
            const bool expected = true; // Null is lowest
            Debug.WriteLine("{0} > NULL = {1}", val, expected);

            // Act
            var result = obj1 > null;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestLessThanEqualWithNull()
        {
            // Arrange
            var val = RandomValues.Integer();
            var obj1 = new TestObject(val);
            const bool expected = false; // Null is lowest
            Debug.WriteLine("{0} <= NULL = {1}", val, expected);

            // Act
            var result = obj1 <= null;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestGreaterThanEqualWithNull()
        {
            // Arrange
            var val = RandomValues.Integer();
            var obj1 = new TestObject(val);
            const bool expected = true; // Null is lowest
            Debug.WriteLine("{0} >= NULL = {1}", val, expected);

            // Act
            var result = obj1 >= null;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        #endregion

        #region Compare with derived type

        private class TestObjectDerived : TestObject
        {
            public TestObjectDerived(int value)
                : base(value)
            {
            }
        }

        [Test]
        public void TestCompareWithOtherType([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObjectDerived(val);
            var expected = String.Compare(obj1.GetType().FullName, obj2.GetType().FullName, StringComparison.Ordinal);
            Debug.WriteLine("{0} compareto {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1.CompareTo(obj2);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestLessThanOtherType([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObjectDerived(val);
            var expected = String.Compare(obj1.GetType().FullName, obj2.GetType().FullName, StringComparison.Ordinal) < 0;
            Debug.WriteLine("{0} < {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1 < obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestGreaterThanOtherType([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObjectDerived(val);
            var expected = String.Compare(obj1.GetType().FullName, obj2.GetType().FullName, StringComparison.Ordinal) > 0;
            Debug.WriteLine("{0} > {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1 > obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestLessThanEqualOtherType([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObjectDerived(val);
            var expected = String.Compare(obj1.GetType().FullName, obj2.GetType().FullName, StringComparison.Ordinal) <= 0;
            Debug.WriteLine("{0} <= {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1 <= obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestGreaterThanEqualOtherType([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestObject(_pivot);
            var obj2 = new TestObjectDerived(val);
            var expected = String.Compare(obj1.GetType().FullName, obj2.GetType().FullName, StringComparison.Ordinal) >= 0;
            Debug.WriteLine("{0} >= {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1 >= obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        #endregion
    }
}