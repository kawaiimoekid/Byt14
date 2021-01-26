using Byt14.State;
using System;

namespace Byt14
{
    public class Degree
    {
        private double _degree;
        public IAvailabilityState AvailabilityState { get; private set; }

        public Degree(double degree)
        {
            _degree = degree;
            AvailabilityState = new AvailableAvailabilityState();
        }

        public void Reserve()
        {
            if (!CanReserve())
            {
                throw new InvalidOperationException("Degree's state doesn't allow it to be reserved.");
            }

            AvailabilityState = new ReservedAvailabilityState();
        }

        public bool CanReserve()
        {
            return AvailabilityState.AllowsPurchase();
        }

        public void RemoveReservation()
        {

            AvailabilityState = new AvailableAvailabilityState();
        }

        public void SetSold()
        {
            if (!CanSell())
            {
                throw new InvalidOperationException("Degree's state doesn't allow it to be sold.");
            }

            AvailabilityState = new SoldAvailabilityState();
        }

        public bool CanSell()
        {
            return AvailabilityState.AllowsPurchase();
        }

        public override string ToString()
        {
            return $"Ocena {string.Format("{0:F1}", _degree)}";
        }

        public DegreeSnapshot CreateSnapshot()
        {
            return new DegreeSnapshot(this);
        }

        public class DegreeSnapshot
        {
            private Degree _degree;
            private IAvailabilityState _availabilityState;

            public DegreeSnapshot(Degree degree)
            {
                _degree = degree;
                _availabilityState = degree.AvailabilityState;
            }

            public void Restore()
            {
                _degree.AvailabilityState = _availabilityState;
            }
        }
    }
}
