using Byt14.State;
using System.Collections.Generic;

namespace Byt14.Builder
{
    public class ShopBuilder
    {
        private string _name;
        private IList<Degree> _degrees = new List<Degree>();
        private bool _allowReservation = false;

        public ShopBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public ShopBuilder AddDegree(Degree degree)
        {
            _degrees.Add(degree);
            return this;
        }

        public ShopBuilder AllowReservation(bool allow)
        {
            _allowReservation = allow;
            return this;
        }

        public Shop Build()
        {
            return new Shop(_name, _degrees, _allowReservation);
        }
    }
}
