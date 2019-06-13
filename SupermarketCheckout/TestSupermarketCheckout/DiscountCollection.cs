using System;
using NUnit.Framework;
using SupermarketCheckout.Entities;

namespace TestSupermarketCheckout
{
    public class DiscountCollection
    {
        private SupermarketCheckout.DiscountCollection discountCollection;

        [SetUp]
        public void Setup()
        {
            discountCollection = new SupermarketCheckout.DiscountCollection();
            discountCollection.ValidityRange =
                new SupermarketCheckout.Utils.DateTimeRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
        }

        [Test]
        public void TestAddPositive()
        {
            var item = new Item();
            var discount = new Discount();
            discountCollection.Add(item, discount);
            Assert.AreEqual(discount, discountCollection.GetOrDefault(item, null));
        }

        [Test]
        public void TestAddNegative()
        {
            var item = new Item();
            var discount = new Discount();
            Assert.Throws(typeof(ArgumentNullException), () => discountCollection.Add(null, discount));
            Assert.Throws(typeof(ArgumentNullException), () => discountCollection.Add(item, null));
        }

        [Test]
        public void TestGetOrDefaultPositive()
        {
            var item = new Item();
            var discount = new Discount();
            discountCollection.Add(item, discount);
            Assert.AreEqual(discount, discountCollection.GetOrDefault(item, null));
            Assert.Null(discountCollection.GetOrDefault(new Item {Name = "OtherName"}, null));
        }

        [Test]
        public void TestGetOrDefaultNegative()
        {
            Assert.Throws(typeof(ArgumentNullException), () => discountCollection.GetOrDefault(null, new Discount()));
        }

        [Test]
        public void TestIsValidPositive()
        {
            Assert.True(discountCollection.IsValid(DateTime.Now));
        }

        [Test]
        public void TestIsValidNegative()
        {
            Assert.False(discountCollection.IsValid(DateTime.Now.AddDays(-2)));
            Assert.False(discountCollection.IsValid(DateTime.Now.AddDays(2)));
        }
    }
}