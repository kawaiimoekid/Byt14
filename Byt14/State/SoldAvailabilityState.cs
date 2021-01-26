using System;

namespace Byt14.State
{
    public class SoldAvailabilityState : IAvailabilityState
    {
        public string GetName() => "Sprzedana";
        public bool AllowsPurchase() => false;
        public ConsoleColor GetDisplayColor() => ConsoleColor.Red;
    }
}
