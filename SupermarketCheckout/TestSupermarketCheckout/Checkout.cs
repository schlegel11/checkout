using System;
using System.Linq;
using NUnit.Framework;
using SupermarketCheckout;
using SupermarketCheckout.Entities;

namespace TestSupermarketCheckout
{
    public class Checkout
    {
        private SupermarketCheckout.Checkout checkout;
        private SupermarketCheckout.DiscountCollection discountCollection;

        [SetUp]
        public void Setup()
        {
            discountCollection = new SupermarketCheckout.DiscountCollection
            {
                ValidityRange =
                    new SupermarketCheckout.Utils.DateTimeRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1))
            };

            checkout = new SupermarketCheckout.Checkout {DiscountCollection = discountCollection};
        }

        [Test]
        public void TestAddItemPositive()
        {
            var apple = new Item {Name = "Apple", Price = 30};
            checkout.AddItem(2, apple);
            Assert.AreEqual(apple, checkout.PayItems().Items.First().Item);
        }

        [Test]
        public void TestAddItemNegative()
        {
            var apple = new Item {Name = "Apple", Price = 30};
            Assert.Throws(typeof(ArgumentException), () => checkout.AddItem(0, apple));
            Assert.Throws(typeof(ArgumentException), () => checkout.AddItem(-1, apple));
            Assert.Throws(typeof(ArgumentNullException), () => checkout.AddItem(1, null));
        }

        [Test]
        public void TestPayItemsPositive()
        {
            var apple = new Item {Name = "Apple", Price = 30};
            var banana = new Item {Name = "Banana", Price = 50};
            var peach = new Item {Name = "Peach", Price = 60};

            checkout.AddItem(2, apple);
            checkout.AddItem(7, banana);
            checkout.AddItem(4, peach);
            var checkoutBill = checkout.PayItems();

            var paidApple = new CheckoutItem
            {
                Amount = 2, Discount = SupermarketCheckout.Checkout.NoDiscount, Item = apple, Price = 60,
                AppliedDiscounts = 0
            };
            var paidBanana = new CheckoutItem
            {
                Amount = 7, Discount = SupermarketCheckout.Checkout.NoDiscount, Item = banana, Price = 350,
                AppliedDiscounts = 0
            };
            var paidPeach = new CheckoutItem
            {
                Amount = 4, Discount = SupermarketCheckout.Checkout.NoDiscount, Item = peach, Price = 240,
                AppliedDiscounts = 0
            };

            Assert.True(checkoutBill.Items.Contains(paidApple));
            Assert.True(checkoutBill.Items.Contains(paidBanana));
            Assert.True(checkoutBill.Items.Contains(paidPeach));
            Assert.AreEqual(650, checkoutBill.Total);
        }

        [Test]
        public void TestPayItemsWithDiscountPositive()
        {
            var apple = new Item {Name = "Apple", Price = 30};
            var banana = new Item {Name = "Banana", Price = 50};
            var peach = new Item {Name = "Peach", Price = 60};

            checkout.AddItem(2, apple);
            checkout.AddItem(7, banana);
            checkout.AddItem(4, peach);

            var discountApple = new Discount {Name = "2 for 45", Quantity = 2, Price = 45};
            var discountBanana = new Discount {Name = "3 for 130", Quantity = 3, Price = 130};
            var discountPeach = new Discount {Name = "5 for 40", Quantity = 5, Price = 40};

            discountCollection.Add(apple, discountApple);
            discountCollection.Add(banana, discountBanana);
            discountCollection.Add(peach, discountPeach);
            var checkoutBill = checkout.PayItems();

            var paidApple = new CheckoutItem
            {
                Amount = 2, Discount = discountApple, Item = apple, Price = 45,
                AppliedDiscounts = 1
            };

            var paidBanana = new CheckoutItem
            {
                Amount = 7, Discount = discountBanana, Item = banana, Price = 310,
                AppliedDiscounts = 2
            };
            var paidPeach = new CheckoutItem
            {
                Amount = 4, Discount = discountPeach, Item = peach, Price = 240,
                AppliedDiscounts = 0
            };

            Assert.True(checkoutBill.Items.Contains(paidApple));
            Assert.True(checkoutBill.Items.Contains(paidBanana));
            Assert.True(checkoutBill.Items.Contains(paidPeach));
            Assert.AreEqual(595, checkoutBill.Total);
        }

        [Test]
        public void TestPayItemsWithDiscountNegative()
        {
            var apple = new Item {Name = "Apple", Price = 30};
            var banana = new Item {Name = "Banana", Price = 50};
            var peach = new Item {Name = "Peach", Price = 60};

            checkout.AddItem(2, apple);
            checkout.AddItem(7, banana);
            checkout.AddItem(4, peach);

            var invalidDiscountCollection = new SupermarketCheckout.DiscountCollection
            {
                ValidityRange =
                    new SupermarketCheckout.Utils.DateTimeRange(new DateTime(DateTime.Now.Year - 1, 6, 10),
                        new DateTime(DateTime.Now.Year - 1, 6, 16))
            };

            var discountApple = new Discount {Name = "2 for 45", Quantity = 2, Price = 45};
            var discountBanana = new Discount {Name = "3 for 130", Quantity = 3, Price = 130};
            var discountPeach = new Discount {Name = "5 for 40", Quantity = 5, Price = 40};

            invalidDiscountCollection.Add(apple, discountApple);
            invalidDiscountCollection.Add(banana, discountBanana);
            invalidDiscountCollection.Add(peach, discountPeach);

            checkout.DiscountCollection = invalidDiscountCollection;
            var checkoutBill = checkout.PayItems();

            var paidApple = new CheckoutItem
            {
                Amount = 2, Discount = SupermarketCheckout.Checkout.NoDiscount, Item = apple, Price = 60,
                AppliedDiscounts = 0
            };
            var paidBanana = new CheckoutItem
            {
                Amount = 7, Discount = SupermarketCheckout.Checkout.NoDiscount, Item = banana, Price = 350,
                AppliedDiscounts = 0
            };
            var paidPeach = new CheckoutItem
            {
                Amount = 4, Discount = SupermarketCheckout.Checkout.NoDiscount, Item = peach, Price = 240,
                AppliedDiscounts = 0
            };

            Assert.True(checkoutBill.Items.Contains(paidApple));
            Assert.True(checkoutBill.Items.Contains(paidBanana));
            Assert.True(checkoutBill.Items.Contains(paidPeach));
            Assert.AreEqual(650, checkoutBill.Total);
        }
    }
}