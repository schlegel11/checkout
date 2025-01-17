using System;
using System.Text;
using SupermarketCheckout.Utils;

namespace SupermarketCheckout.Entities
{
    public class CheckoutItem : IEquatable<CheckoutItem>
    {
        public Item Item { get; }
        public Discount Discount { get; }
        public int Amount { get; set; }
        public int AppliedDiscounts { get; set; }
        public decimal Price { get; set; }

        public CheckoutItem(Item item, Discount discount)
        {
            Checks.CheckArgumentNotNull(item, "Item is null.");
            Checks.CheckArgumentNotNull(discount, "Discount is null.");

            Item = item;
            Discount = discount;
        }

        public bool Equals(CheckoutItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Item, other.Item) && Equals(Discount, other.Discount) && Amount == other.Amount &&
                   AppliedDiscounts == other.AppliedDiscounts && Price == other.Price;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CheckoutItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Item.GetHashCode();
                hashCode = (hashCode * 397) ^ Discount.GetHashCode();
                hashCode = (hashCode * 397) ^ Amount;
                hashCode = (hashCode * 397) ^ AppliedDiscounts;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(CheckoutItem left, CheckoutItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CheckoutItem left, CheckoutItem right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Name: {Item.Name}");
            stringBuilder.Append(" | ");
            stringBuilder.Append($"Amount: {Amount}");
            stringBuilder.Append(" | ");
            stringBuilder.Append($"Unit price: {Item.Price}");
            stringBuilder.Append(" | ");
            stringBuilder.Append($"Price: {Price}");
            stringBuilder.Append(" | ");
            stringBuilder.Append(
                $"Applied discount: {(AppliedDiscounts == 0 ? "No discounts" : AppliedDiscounts + " X " + $"\"{Discount.Name}\"")}");
            return stringBuilder.ToString();
        }
    }
}