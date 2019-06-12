using System;
using System.Collections.Generic;
using SupermarketCheckout.Utils;

namespace SupermarketCheckout
{
    public class DiscountCollection
    {
        private readonly IDictionary<Item, Discount> discountItems = new Dictionary<Item, Discount>();
        public DateTimeRange ValidityRange { get; set; }

        public void Add(Item item, Discount discount)
        {
            Checks.CheckArgumentNotNull(item, "Item can't be null.");
            Checks.CheckArgumentNotNull(discount, "Discount can't be null.");

            discountItems[item] = discount;
        }

        public Discount GetOrDefault(Item item, Discount defaultDiscount)
        {
            return discountItems.TryGetValue(item, out var discount)
                ? discount
                : defaultDiscount;
        }

        public bool IsValid(DateTime dateTime)
        {
            return ValidityRange.IsInRange(dateTime);
        }
    }
}