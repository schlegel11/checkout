using System;
using System.Collections.Generic;

namespace SupermarketCheckout
{
    public class Checkout
    {
        public DiscountCollection DiscountCollection { get; set; }
        private Dictionary<Item, int> items = new Dictionary<Item, int>();
        private static readonly Discount NoDiscount = new Discount {Name = "NoDiscount", Price = 0, Quantity = -1};

        public void AddItem(int amount, Item item)
        {
            if (items.TryGetValue(item, out var currentAmount))
            {
                items[item] = currentAmount + amount;
            }
            else
            {
                items.Add(item, amount);
            }
        }

        public void CheckoutItems()
        {
            Console.Out.WriteLine("--- Items ---");
            decimal total = 0;
            foreach (var keyValuePair in items)
            {
                var item = keyValuePair.Key;
                var amount = keyValuePair.Value;

                var discount = GetDiscount(item);
                var priceInfo = CalculatePrice(amount, item, discount);
                total += priceInfo.price;

                Console.Out.WriteLine(
                    $"Name: {item.Name} | Amount: {amount} | Unit price: {item.Price} | Price: {priceInfo.price} | Applied discount: {(discount == NoDiscount ? "No discounts" : priceInfo.appliedDiscounts + " X " + discount.Name)}");
            }

            Console.Out.WriteLine("--- * ---");
            Console.Out.WriteLine($"Total: {total}");
        }

        private Discount GetDiscount(Item item)
        {
            if (DiscountCollection.IsValid(DateTime.Now))
            {
                return DiscountCollection.GetOrDefault(item, NoDiscount);
            }
            else
            {
                //Log discount is not valid.
            }

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