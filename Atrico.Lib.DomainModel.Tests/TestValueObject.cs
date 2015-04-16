using System.Diagnostics;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.DomainModel.Tests.Annotations;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.NUnitAttributes;

namespace Atrico.Lib.DomainModel.Tests
{
    [TestFixture]
    public class TestValueObject : TestFixtureBase
    {
        private const int _pivot = 10;

        #region Equals values

        [Test]
        public void TestEquals([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new Helpers.TestValueObject(_pivot);
            var obj2 = new Helpers.TestValueObject(val);
            var expected = _pivot.Equals(val);
            Debug.WriteLine("{0} equals {1} = {2}", _pivot, val, expected);

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestOperatorEquals([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new Helpers.TestValueObject(_pivot);
            var obj2 = new Helpers.TestValueObject(val);
            var expected = _pivot == val;
            Debug.WriteLine("{0} == {1} = {2}", _pivot, val, expected);

            // Act
            var result = obj1 == obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestOperatorNotEquals([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new Helpers.TestValueObject(_pivot);
            var obj2 = new Helpers.TestValueObject(val);
            var expected = _pivot != val;
            Debug.WriteLine("{0} != {1} = {2}", _pivot, val, expected);

            // Act
            var result = obj1 != obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        #endregion

        #region Equals with null

        [Test]
        public void TestEqualsWithNull()
        {
            // Arrange
            var val = RandomValues.Integer();
            var obj1 = new Helpers.TestValueObject(val);
            const bool expected = false; // Null never equals
            Debug.WriteLine("{0} equals NULL = {1}", val, expected);

            // Act
            var result = obj1.Equals(null);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestOperatorEqualsWithNull()
        {
            // Arrange
            var val = RandomValues.Integer();
            var obj1 = new Helpers.TestValueObject(val);
            const bool expected = false; // Null never equals
            Debug.WriteLine("{0} == NULL = {1}", val, expected);

            // Act
            var result = obj1 == null;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestOperatorNotEqualsWithNull()
        {
            // Arrange
            var val = RandomValues.Integer();
            var obj1 = new Helpers.TestValueObject(val);
            const bool expected = true; // Null never equals
            Debug.WriteLine("{0} != NULL = {1}", val, expected);

            // Act
            var result = obj1 != null;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        #endregion

        #region Equals with derived type

        [Test]
        public void TestEqualsWithOtherType([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new Helpers.TestValueObject(_pivot);
            var obj2 = new Helpers.TestValueObjectDerived(val);
            const bool expected = false; // Different type never equals
            Debug.WriteLine("{0} equals {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestOperatorEqualsWithOtherType([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new Helpers.TestValueObject(_pivot);
            var obj2 = new Helpers.TestValueObjectDerived(val);
            const bool expected = false; // Different type never equals
            Debug.WriteLine("{0} == {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1 == obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestOperatorNotEqualsWithOtherType([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new Helpers.TestValueObject(_pivot);
            var obj2 = new Helpers.TestValueObjectDerived(val);
            const bool expected = true; // Different type never equals
            Debug.WriteLine("{0} != {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1 != obj2;

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        #endregion
    }
}