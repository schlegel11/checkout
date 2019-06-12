using System;
using System.Collections.Generic;

namespace SupermarketCheckout
{
    public class DiscountCollection
    {
        private readonly IDictionary<Item, Discount> discountItems = new Dictionary<Item, Discount>();
        public DateTimeRange ValidityRange { get; set; }

        public void Add(Item item, Discount discount)
        {
            discountItems.Add(item, discount);
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