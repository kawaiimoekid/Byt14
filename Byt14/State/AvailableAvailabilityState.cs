using System;

namespace Byt14.State
{
    public class AvailableAvailabilityState : IAvailabilityState
    {
        public string GetName() => "Dostepna";
        public bool AllowsPurchase() => true;
        public ConsoleColor GetDisplayColor() => ConsoleColor.Green;
    }
}
