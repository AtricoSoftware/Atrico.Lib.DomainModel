using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.DomainModel.Tests.Annotations;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.NUnitAttributes;

namespace Atrico.Lib.DomainModel.Tests
{
	[TestFixture]
	public class ValueObjectTests : TestFixtureBase
	{
		private class Address : ValueObject<Address>
		{
			[UsedImplicitly] private readonly string _address1;
			[UsedImplicitly] private readonly string _city;
			[UsedImplicitly] private readonly string _state;

			public Address(string address1, string city, string state)
			{
				_address1 = address1;
				_city = city;
				_state = state;
			}
		}

		private class ExpandedAddress : Address
		{
			[UsedImplicitly] private readonly string _address2;

			public ExpandedAddress(string address1, string address2, string city, string state)
				: base(address1, city, state)
			{
				_address2 = address2;
			}
		}

		[Test]
		public void AddressEqualsWorksWithIdenticalAddresses()
		{
			var address = new Address("Address1", "Austin", "TX");
			var address2 = new Address("Address1", "Austin", "TX");

			Assert.That(Value.Of(address.Equals(address2)).Is().True());
		}

		[Test]
		public void AddressEqualsWorksWithNonIdenticalAddresses()
		{
			var address = new Address("Address1", "Austin", "TX");
			var address2 = new Address("Address2", "Austin", "TX");

			Assert.That(Value.Of(address.Equals(address2)).Is().False());
		}

		[Test]
		public void AddressEqualsWorksWithNulls()
		{
			var address = new Address(null, "Austin", "TX");
			var address2 = new Address("Address2", "Austin", "TX");

			Assert.That(Value.Of(address.Equals(address2)).Is().False());
		}

		[Test]
		public void AddressEqualsWorksWithNullsOnOtherObject()
		{
			var address = new Address("Address2", "Austin", "TX");
			var address2 = new Address("Address2", null, "TX");

			Assert.That(Value.Of(address.Equals(address2)).Is().False());
		}

		[Test]
		public void AddressEqualsIsReflexive()
		{
			var address = new Address("Address1", "Austin", "TX");

			Assert.That(Value.Of(address.Equals(address)).Is().True());
		}

		[Test]
		public void AddressEqualsIsSymmetric()
		{
			var address = new Address("Address1", "Austin", "TX");
			var address2 = new Address("Address2", "Austin", "TX");

			Assert.That(Value.Of(address.Equals(address2)).Is().False());
            Assert.That(Value.Of(address2.Equals(address)).Is().False());
		}

		[Test]
		public void AddressEqualsIsTransitive()
		{
			var address = new Address("Address1", "Austin", "TX");
			var address2 = new Address("Address1", "Austin", "TX");
			var address3 = new Address("Address1", "Austin", "TX");

			Assert.That(Value.Of(address.Equals(address2)).Is().True());
            Assert.That(Value.Of(address2.Equals(address3)).Is().True());
			Assert.That(Value.Of(address.Equals(address3)).Is().True());
		}

		[Test]
		public void AddressOperatorsWork()
		{
			var address = new Address("Address1", "Austin", "TX");
			var address2 = new Address("Address1", "Austin", "TX");
			var address3 = new Address("Address2", "Austin", "TX");

			Assert.That(Value.Of(address == address2).Is().True());
            Assert.That(Value.Of(address2 != address3).Is().True());
		}

		[Test]
		public void AddressOperatorsWorkWithNulls()
		{
			var address = new Address("Address1", null, "TX");
			var address2 = new Address("Address1", null, "TX");
			var address3 = new Address("Address2", null, "TX");

			Assert.That(Value.Of(address == address2).Is().True());
            Assert.That(Value.Of(address2 != address3).Is().True());
		}

		[Test]
		public void DerivedTypesDoNotEqualBaseTypes()
		{
			var address = new Address("Address1", "Austin", "TX");
			var address2 = new ExpandedAddress("Address1", "Apt 123", "Austin", "TX");

			Assert.That(Value.Of(address.Equals(address2)).Is().False());
			Assert.That(Value.Of(address == address2).Is().False());
		}

		[Test]
		public void DerivedTypesEqualUsingBaseFields()
		{
			var address = new ExpandedAddress("Address1", "Apt 123", "Austin", "TX");
			var address2 = new ExpandedAddress("Address2", "Apt 123", "Austin", "TX");

			Assert.That(Value.Of(address.Equals(address2)).Is().False());
			Assert.That(Value.Of(address == address2).Is().False());
		}

		[Test]
		public void EqualValueObjectsHaveSameHashCode()
		{
			var address = new Address("Address1", "Austin", "TX");
			var address2 = new Address("Address1", "Austin", "TX");

            Assert.That(Value.Of(address2.GetHashCode()).Is().EqualTo(address.GetHashCode()));
		}

		[Test]
		public void TransposedValuesGiveDifferentHashCodes()
		{
			var address = new Address(null, "Austin", "TX");
			var address2 = new Address("TX", "Austin", null);

            Assert.That(Value.Of(address2.GetHashCode()).Is().Not().EqualTo(address.GetHashCode()));
		}

		[Test]
		public void UnequalValueObjectsHaveDifferentHashCodes()
		{
			var address = new Address("Address1", "Austin", "TX");
			var address2 = new Address("Address2", "Austin", "TX");

            Assert.That(Value.Of(address2.GetHashCode()).Is().Not().EqualTo(address.GetHashCode()));
		}

		[Test]
		public void TransposedValuesOfFieldNamesGivesDifferentHashCodes()
		{
			var address = new Address("_city", null, null);
			var address2 = new Address(null, "_address1", null);

            Assert.That(Value.Of(address2.GetHashCode()).Is().Not().EqualTo(address.GetHashCode()));
		}

		[Test]
		public void DerivedTypesHashCodesBehaveCorrectly()
		{
			var address = new ExpandedAddress("Address99999", "Apt 123", "New Orleans", "LA");
			var address2 = new ExpandedAddress("Address1", "Apt 123", "Austin", "TX");

            Assert.That(Value.Of(address2.GetHashCode()).Is().Not().EqualTo(address.GetHashCode()));
		}
	}
}
