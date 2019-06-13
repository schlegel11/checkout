using System;
using SupermarketCheckout.Utils;

namespace SupermarketCheckout
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var apple = new Item {Name = "Apple", Price = 30};
                var banana = new Item {Name = "Banana", Price = 50};
                var peach = new Item {Name = "Peach", Price = 60};

                var discountApple = new Discount {Name = "2 for 45", Quantity = 2, Price = 45};
                var discountBanana = new Discount {Name = "3 for 130", Quantity = 3, Price = 130};

                var discountCollection = new DiscountCollection
                    {ValidityRange = new DateTimeRange(new DateTime(2019, 6, 10), new DateTime(2019, 6, 16))};
                discountCollection.Add(apple, discountApple);
                discountCollection.Add(banana, discountBanana);

                var checkout = new Checkout {DiscountCollection = discountCollection};
                checkout.AddItem(2, apple);
                checkout.AddItem(10, peach);
                checkout.AddItem(4, apple);
                checkout.AddItem(1, banana);

                var checkoutBill = checkout.PayItems();
                Console.WriteLine(checkoutBill);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}