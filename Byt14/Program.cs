using Byt14.Builder;

namespace Byt14
{
    class Program
    {
        static void Main(string[] args)
        {
            var director = new ShopDirector();
            var shop = director.BuildDemoShop();
            //var shop = director.BuildProdShop();
            shop.Start();
        }
    }
}
