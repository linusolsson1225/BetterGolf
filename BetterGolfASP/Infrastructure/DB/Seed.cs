using BetterGolfASP.Domain.Models;
using BetterGolfASP.Domain.Models.Products;


namespace BetterGolfASP.Infrastructure.DB
{
    public class Seed
    {
        public void SeedDb(Context context)
        {
           
            var customers = new List<Customer>
            {
                Customer.Create("Peter", "Hansson", "peterhansson@hotmail.com","Sweden","Stockholm","103 16","+4670123456"),
                Customer.Create("Josef", "Lagerqvist", "joseflagerqvist@hotmail.com","USA","New York","07008","+1 555 555 5555"),
                Customer.Create("Sara", "Lindblad", "saralindblad@outlook.com", "Sweden", "Stockholm", "103 17", "+4670133456")
            };
            context.AddRange(customers);

            
            
            
            var irons = new List<IronClub>
            {
                IronClub.Create("Taylormade P7TW", "Tiger Woods bladed irons with soft feel", 1499.9m, 10, IronClub.TypeOfIron.Blade, GolfClub.RightOrLeftHanded.Right),
                IronClub.Create("Titliest CB", "Cavity back iron with creativity", 1599.9m, 30, IronClub.TypeOfIron.CavityBack, GolfClub.RightOrLeftHanded.Right),
                IronClub.Create("Cobra Darkspeed", "Good improvement iron for higher handicappers", 1399.9m, 50, IronClub.TypeOfIron.MuscleBack, GolfClub.RightOrLeftHanded.Left)
            };

         
            var putters = new List<PutterClub>
            {
                PutterClub.Create("L.A.B OZ.1", "Zero torque putter, straighter start every time", 599.9m, 20, PutterClub.ShaftType.Broomstick, PutterClub.PutterType.Mallet, GolfClub.RightOrLeftHanded.Right),
                PutterClub.Create("Ping Anser", "Classic blade putter from Ping", 299.9m, 40, PutterClub.ShaftType.Standard, PutterClub.PutterType.Blade, GolfClub.RightOrLeftHanded.Left)
            };

           
            var woods = new List<WoodClub>
            {
                WoodClub.Create("Taylormade R7 Minidriver", "Newest minidriver from Taylormade", 599.9m, WoodClub.TypeOfWood.Spoon, GolfClub.RightOrLeftHanded.Right),
                WoodClub.Create("Callaway Elyte TD", "Lower spin and goes further", 749.9m, WoodClub.TypeOfWood.Driver, GolfClub.RightOrLeftHanded.Right),
                WoodClub.Create("Ping G440 Max", "10k MOI, higher club speed", 599.9m, WoodClub.TypeOfWood.Driver, GolfClub.RightOrLeftHanded.Left)
            };

            foreach (var wood in woods)
            {
                wood.AddLoftVariant(10.5, 50);
                wood.AddLoftVariant(9, 50);
                wood.AddLoftVariant(12, 20);
            }

           
            var tops = new List<Clothing>
            {
                Clothing.Create("Polo Shirt", "Breathable cotton polo", 399.9m, ClothingType.Top),
                Clothing.Create("Golf Vest", "Waterproof vest for cool mornings", 599.9m, ClothingType.Top),
                Clothing.Create("Long Sleeve Shirt", "Lightweight long sleeve shirt", 449.9m, ClothingType.Top)
            };
            foreach (var top in tops)
            {
                top.AddSizeVariant("S", 10);
                top.AddSizeVariant("M", 15);
                top.AddSizeVariant("L", 12);
            }

            var bottoms = new List<Clothing>
            {
                Clothing.Create("Golf Trousers", "Stretchable golf pants", 499.9m, ClothingType.Bottom),
                Clothing.Create("Golf Shorts", "Lightweight summer shorts", 349.9m, ClothingType.Bottom),
                Clothing.Create("Skort", "Golf skirt with built-in shorts", 399.9m, ClothingType.Bottom)
            };
            foreach (var bottom in bottoms)
            {
                bottom.AddSizeVariant("S", 8);
                bottom.AddSizeVariant("M", 12);
                bottom.AddSizeVariant("L", 10);
            }

            var shoes = new List<Clothing>
            {
                Clothing.Create("Nike Air Golf Shoes", "Comfortable spikeless golf shoes", 1299.9m, ClothingType.Shoes),
                Clothing.Create("Adidas Tour360", "Premium stability golf shoes", 1599.9m, ClothingType.Shoes),
                Clothing.Create("FootJoy Pro/SL", "Classic golf shoes with spikes", 1399.9m, ClothingType.Shoes)
            };
            foreach (var shoe in shoes)
            {
                shoe.AddSizeVariant("40", 5);
                shoe.AddSizeVariant("41", 10);
                shoe.AddSizeVariant("42", 8);
            }

            var balls = new List<GolfBall>
            {
                GolfBall.Create("Titleist ProV1", "High performance ball", 599.9m),
                GolfBall.Create("Callaway Chrome Soft", "Soft feel with distance", 499.9m)
            };
            foreach (var ball in balls)
            {
                ball.AddPackageSizeVariant(12, 50);
                ball.AddPackageSizeVariant(48, 12);
            }

           
            
            context.AddRange(irons);
            context.AddRange(putters);
            context.AddRange(woods);
            context.AddRange(tops);
            context.AddRange(bottoms);
            context.AddRange(shoes);
            context.AddRange(balls);

            context.SaveChanges(); 

            
            var orderRow1 = OrderRow.Create(irons[0], 1, irons[0].Variants.FirstOrDefault());
            var orderRow2 = OrderRow.Create(woods[0], 1, woods[0].Variants.FirstOrDefault());
            var orderRow3 = OrderRow.Create(putters[0], 1, putters[0].Variants.FirstOrDefault());
            var orderRow4 = OrderRow.Create(tops[0], 2, tops[0].Variants.FirstOrDefault());
            var orderRow5 = OrderRow.Create(shoes[0], 1, shoes[0].Variants.FirstOrDefault());

            context.AddRange(orderRow1, orderRow2, orderRow3, orderRow4, orderRow5);
            context.SaveChanges();

            
            var order1 = Order.Create(customers[0], new List<OrderRow> { orderRow1, orderRow2, orderRow4 });
            var order2 = Order.Create(customers[1], new List<OrderRow> { orderRow3, orderRow5 });
            var order3 = Order.Create(customers[2], new List<OrderRow> { orderRow1, orderRow3 });

            context.AddRange(order1, order2, order3);
            context.SaveChanges();
        }
    }
}
