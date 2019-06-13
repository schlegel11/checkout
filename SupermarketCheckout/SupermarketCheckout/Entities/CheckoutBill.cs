using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketCheckout.Entities
{
    public class CheckoutBill
    {
        public IList<CheckoutItem> Items { get; } = new List<CheckoutItem>();

        public decimal Total
        {
            get { return Items.Select(item => item.Price).Aggregate((a, b) => a + b); }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("--- Items ---");
            foreach (var checkoutItem in Items) stringBuilder.AppendLine(checkoutItem.ToString());

            stringBuilder.AppendLine("--- * ---");
            stringBuilder.AppendLine($"Total: {Total}");
            return stringBuilder.ToString();
        }
    }
}