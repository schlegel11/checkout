using System;
using NUnit.Framework;

namespace TestSupermarketCheckout
{
    public class DateTimeRange
    {
        [Test]
        public void TestConstructionPositive()
        {
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(1);
            var dateTimeRange = new SupermarketCheckout.Utils.DateTimeRange(startDate, endDate);

            Assert.AreEqual(dateTimeRange.Start, startDate);
            Assert.AreEqual(dateTimeRange.End, endDate);
        }

        [Test]
        public void TestConstructionNegative()
        {
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(1);

            Assert.Throws(typeof(ArgumentException),
                () => new SupermarketCheckout.Utils.DateTimeRange(endDate, startDate));
        }

        [Test]
        public void TestIsInRangePositive()
        {
            var dateTimeRange =
                new SupermarketCheckout.Utils.DateTimeRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            Assert.True(dateTimeRange.IsInRange(DateTime.Now));
        }

        [Test]
        public void TestIsInRangeNegative()
        {
            var dateTimeRange =
                new SupermarketCheckout.Utils.DateTimeRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            Assert.False(dateTimeRange.IsInRange(DateTime.Now.AddDays(-2)));
            Assert.False(dateTimeRange.IsInRange(DateTime.Now.AddDays(2)));
        }
    }
}