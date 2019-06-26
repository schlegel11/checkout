using System;
using System.Collections.Generic;
using SupermarketCheckout.Entities;
using SupermarketCheckout.Utils;

namespace SupermarketCheckout
{
    /// <summary>
    ///     Class which represents a checkout where items can be added and paid.
    /// </summary>
    public class Checkout
    {
        /// <summary>
        ///     Constant which represents a not available discount.
        /// </summary>
        public static readonly Discount NoDiscount = new Discount {Name = "NoDiscount", Price = 0, Quantity = -1};

        private readonly Dictionary<Item, int> items = new Dictionary<Item, int>();

        /// <summary>
        ///     The <see cref="SupermarketCheckout.DiscountCollection" /> for setup discounts in a specific <see cref="DateTime" />
        ///     interval.
        /// </summary>
        public DiscountCollection DiscountCollection { get; set; }

        /// <summary>
        ///     Add items to checkout.
        /// </summary>
        /// <param name="amount">The item amount.</param>
        /// <param name="item">The item for checkout.</param>
        public void AddItem(int amount, Item item)
        {
            Checks.CheckArgumentNotNull(item, "Item can't be null.");
            Checks.CheckArgument(amount > 0, "Amount must be > 0.");

            if (items.TryGetValue(item, out var currentAmount))
                items[item] = currentAmount + amount;
            else
                items.Add(item, amount);
        }

        /// <summary>
        ///     Pay the added items (with discounts) (<see cref="AddItem" />) and get a <see cref="CheckoutBill" />.
        /// </summary>
        /// <returns>The <see cref="CheckoutBill" />.</returns>
        public CheckoutBill PayItems()
        {
            var checkoutBill = new CheckoutBill();
            foreach (var (item, amount) in items)
            {
                var discount = GetDiscount(item);
                var (price, appliedDiscounts) = CalculatePrice(amount, item, discount);

                var checkoutItem = new CheckoutItem(item, discount)
                {
                    Amount = amount, Price = price,
                    AppliedDiscounts = appliedDiscounts
                };
                checkoutBill.Items.Add(checkoutItem);
            }

            return checkoutBill;
        }

        private Discount GetDiscount(Item item)
        {
            if (DiscountCollection.IsValid(DateTime.Now)) return DiscountCollection.GetOrDefault(item, NoDiscount);

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