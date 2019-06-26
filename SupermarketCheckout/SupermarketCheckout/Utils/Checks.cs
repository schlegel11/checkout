using System;

namespace SupermarketCheckout.Utils
{
    public static class Checks
    {
        public static void CheckArgument(bool term, string message = "")
        {
            Check(term, new ArgumentException(message));
        }

        public static void CheckArgumentNotNull(object argument, string message = "")
        {
            Check(argument != null, new ArgumentNullException(message));
        }
        
        public static void Check(bool term, Exception exception)
        {
            if (!term) throw exception;
        }
    }
}