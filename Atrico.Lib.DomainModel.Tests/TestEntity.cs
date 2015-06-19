using System.Diagnostics;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.Testing.TestAttributes.NUnit;

namespace Atrico.Lib.DomainModel.Tests
{
    [TestFixture]
    public class TestEntity : DomainModelTestFixtureBase
    {
        private const int _pivot = 10;

        [Test]
        public void TestEquals([Values(1, 10, 100)] int val)
        {
            // Arrange
            var obj1 = new TestEntity(_pivot);
            var obj2 = new TestEntity(val);
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
            var obj1 = new TestEntity(val);
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
            var obj1 = new TestEntity(_pivot);
            var obj2 = new TestEntityDerived(val);
            const bool expected = false; // Different type never equals
            Debug.WriteLine("{0} equals {1} = {2}", obj1, obj2, expected);

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.That(Value.Of(result).Is().EqualTo(expected));
        }
    }
}
