using System;

namespace Byt14.State
{
    public class ReservedAvailabilityState : IAvailabilityState
    {
        public string GetName() => "Zarezerwowana";
        public bool AllowsPurchase() => false;
        public ConsoleColor GetDisplayColor() => ConsoleColor.Yellow;
    }
}
