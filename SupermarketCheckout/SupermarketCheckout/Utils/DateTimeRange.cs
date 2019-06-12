using System;

namespace SupermarketCheckout
{
    public class DateTimeRange
    {
        public readonly DateTime Start;
        public readonly DateTime End;

        public DateTimeRange(DateTime start, DateTime end)
        {
            // Add param check
            Start = start;
            End = end;
        }

        public bool IsInRange(DateTime dateTime)
        {
            return dateTime >= Start && dateTime <= End;
        }
    }
}