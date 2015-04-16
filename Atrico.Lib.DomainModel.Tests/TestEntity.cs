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
    public class TestEntity : TestFixtureBase
    {
        private const int _pivot = 10;

        [Test]
        public void TestEquals([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new Helpers.TestEntity(_pivot);
            var obj2 = new Helpers.TestEntity(val);
            var expected = _pivot.Equals(val);
            Debug.WriteLine("{0} equals {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }


        [Test]
        public void TestEqualsWithNull()
        {
            // Arrange
            var val = RandomValues.Integer();
            var obj1 = new Helpers.TestEntity(val);
            const bool expected = false; // Null never equals
            Debug.WriteLine("{0} equals NULL = {1}", obj1, expected);

            // Act
            var result = obj1.Equals(null);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }

        [Test]
        public void TestEqualsWithOtherType([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new Helpers.TestEntity(_pivot);
            var obj2 = new Helpers.TestEntityDerived(val);
            const bool expected = false; // Different type never equals
            Debug.WriteLine("{0} equals {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }
    }
}