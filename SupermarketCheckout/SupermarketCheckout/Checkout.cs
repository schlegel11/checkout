using System;
using System.Collections.Generic;
using System.Text;
using SupermarketCheckout.Entities;
using SupermarketCheckout.Utils;

namespace SupermarketCheckout
{
    public class Checkout
    {
        public DiscountCollection DiscountCollection { get; set; }
        private Dictionary<Item, int> items = new Dictionary<Item, int>();
        private static readonly Discount NoDiscount = new Discount {Name = "NoDiscount", Price = 0, Quantity = -1};

        public void AddItem(int amount, Item item)
        {
            Checks.CheckArgumentNotNull(item, "Item can't be null.");
            Checks.CheckArgument(amount > 0, "Amount must be > 0.");

            if (items.TryGetValue(item, out var currentAmount))
            {
                items[item] = currentAmount + amount;
            }
            else
            {
                items.Add(item, amount);
            }
        }

        public CheckoutBill PayItems()
        {
            var checkoutBill = new CheckoutBill();
            foreach (var (item, amount) in items)
            {
                var discount = GetDiscount(item);
                var (price, appliedDiscounts) = CalculatePrice(amount, item, discount);

                var checkoutItem = new CheckoutItem
                {
                    Amount = amount, Discount = discount, Item = item, Price = price,
                    AppliedDiscounts = appliedDiscounts
                };
                checkoutBill.Items.Add(checkoutItem);
            }

            return checkoutBill;
        }

        private Discount GetDiscount(Item item)
        {
            if (DiscountCollection.IsValid(DateTime.Now))
            {
                return DiscountCollection.GetOrDefault(item, NoDiscount);
            }

            //Log discount is not valid.
            Console.WriteLine($"DiscountCollection is invalid with date range {DiscountCollection.ValidityRange}.");

            return NoDiscount;
        }

        private (decimal price, int appliedDiscounts) CalculatePrice(int amount, Item item, Discount discount)
        {
            if (discount == NoDiscount)
            {
                var regularItemPrice = amount * item.Price;

                return (regularItemPrice, 0);
            }
            else
            {
                var appliedDiscounts = amount / discount.Quantity;
                var leftItemsWithoutDiscount = amount % discount.Quantity;
                var discountPrice = appliedDiscounts * discount.Price;
                var regularItemPrice = leftItemsWithoutDiscount * item.Price;

                return (regularItemPrice + discountPrice, appliedDiscounts);
            }
        }
    }
}