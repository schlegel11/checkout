using System;
using System.Collections.Generic;
using SupermarketCheckout.Entities;
using SupermarketCheckout.Utils;

namespace SupermarketCheckout
{
    /// <summary>
    ///     Class which represents a collection of <see cref="Discount" />s valid in a specific <see cref="DateTimeRange" />.
    /// </summary>
    public class DiscountCollection
    {
        private readonly IDictionary<Item, Discount> discountItems = new Dictionary<Item, Discount>();

        /// <summary>
        ///     The <see cref="DateTimeRange" /> in which the <see cref="Add" />ed <see cref="Discount" />s are valid.
        /// </summary>
        public DateTimeRange ValidityRange { get; set; }

        /// <summary>
        ///     Add a <see cref="Discount" /> which is mapped to a specific <see cref="Item" />.
        /// </summary>
        /// <param name="item">The <see cref="Item" />.</param>
        /// <param name="discount">The mapped <see cref="Discount" />.</param>
        public void Add(Item item, Discount discount)
        {
            Checks.CheckArgumentNotNull(item, "Item can't be null.");
            Checks.CheckArgumentNotNull(discount, "Discount can't be null.");

            discountItems[item] = discount;
        }

        /// <summary>
        ///     Get a mapped <see cref="Discount" /> by an <see cref="Item" />.
        ///     If no <see cref="Discount" /> is found, a default value is returned.
        /// </summary>
        /// <param name="item">The <see cref="Item" /> which maps a <see cref="Discount" />.</param>
        /// <param name="defaultDiscount">
        ///     If no <see cref="Discount" /> for a specific <see cref="Item" /> is found use this param
        ///     as return value.
        /// </param>
        /// <returns>The <see cref="defaultDiscount" />.</returns>
        public Discount GetOrDefault(Item item, Discount defaultDiscount)
        {
            return discountItems.TryGetValue(item, out var discount)
                ? discount
                : defaultDiscount;
        }

        /// <summary>
        ///     Checks if a specific <see cref="DateTime" /> value is included in the <see cref="ValidityRange" />.
        /// </summary>
        /// <param name="dateTime">A <see cref="DateTime" /> value to check.</param>
        /// <returns>True if the <see cref="DateTime" /> param is included in <see cref="ValidityRange" />.</returns>
        public bool IsValid(DateTime dateTime)
        {
            return ValidityRange.IsInRange(dateTime);
        }
    }
}