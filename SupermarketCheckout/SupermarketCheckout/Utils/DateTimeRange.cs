using System;

namespace SupermarketCheckout.Utils
{
    public class DateTimeRange
    {
        public readonly DateTime Start;
        public readonly DateTime End;

        public DateTimeRange(DateTime start, DateTime end)
        {
            Checks.CheckArgument(start <= end, "Start date should be <= end date.");

            Start = start;
            End = end;
        }

        public bool IsInRange(DateTime dateTime)
        {
            return dateTime >= Start && dateTime <= End;
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
        }
    }
}