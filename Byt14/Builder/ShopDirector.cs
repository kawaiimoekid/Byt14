using Byt14.State;

namespace Byt14.Builder
{
    public class ShopDirector
    {
        public Shop BuildDemoShop()
        {
            return new ShopBuilder()
                .SetName("Sklep ocen BYT (demo)")
                .AddDegree(new Degree(3))
                .AddDegree(new Degree(4))
                .AddDegree(new Degree(5))
                .Build();
        }

        public Shop BuildProdShop()
        {
            return new ShopBuilder()
                .SetName("Sklep ocen BYT")
                .AddDegree(new Degree(3))
                .AddDegree(new Degree(3.5))
                .AddDegree(new Degree(4))
                .AddDegree(new Degree(4.5))
                .AddDegree(new Degree(5))
                .AllowReservation(true)
                .Build();
        }
    }
}
