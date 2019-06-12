using System.Text;

namespace SupermarketCheckout.Entities
{
    public class CheckoutItem
    {
        public Item Item { get; set; }
        public Discount Discount { get; set; }
        public int Amount { get; set; }
        public int AppliedDiscounts { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Name: {Item?.Name}");
            stringBuilder.Append(" | ");
            stringBuilder.Append($"Amount: {Amount}");
            stringBuilder.Append(" | ");
            stringBuilder.Append($"Unit price: {Item?.Price}");
            stringBuilder.Append(" | ");
            stringBuilder.Append($"Price: {Price}");
            stringBuilder.Append(" | ");
            stringBuilder.Append(
                $"Applied discount: {(AppliedDiscounts == 0 ? "No discounts" : AppliedDiscounts + " X " + $"\"{Discount?.Name}\"")}");
            return stringBuilder.ToString();
        }
    }
}