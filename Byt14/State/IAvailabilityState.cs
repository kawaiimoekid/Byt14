using System;

namespace Byt14.State
{
    public interface IAvailabilityState
    {
        string GetName();
        bool AllowsPurchase();
        ConsoleColor GetDisplayColor();
    }
}
