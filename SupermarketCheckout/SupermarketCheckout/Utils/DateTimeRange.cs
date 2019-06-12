using System;

namespace SupermarketCheckout.Utils
{
    public class DateTimeRange
    {
        public readonly DateTime Start;
        public readonly DateTime End;

        public DateTimeRange(DateTime start, DateTime end)
        {
            Checks.CheckArgumentNotNull(start, "Start date can't be null.");
            Checks.CheckArgumentNotNull(end, "End date can't be null.");

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