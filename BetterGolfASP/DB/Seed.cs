using BetterGolfASP.DB;
using BetterGolfASP.Models;
using BetterGolfASP.Models.Products;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BetterGolfASP.Models.Products.WoodClub;

namespace DB
{
    public class Seed
    {
        public void SeedDB(Context context)
        {
            var customers = new List<Customer>();
            customers.AddRange(
            [
                Customer.Create("Peter", "Hansson", "peterhansson@hotmail.com"),
                Customer.Create("Josef", "Lagerqvist", "joseflagerqvist@hotmail.com"),
                Customer.Create("Sara", "Lindblad", "saralindblad@outlook.com")
             ]);

            foreach (Customer customer in customers)
            {
                context.Add(customer);
            }

            var irons = new List<IronClub>();
            irons.AddRange(
                [
                IronClub.Create("Taylormade P7TW", "Tiger Woods bladed irons with an soft feel",1499.9,10,IronClub.TypeOfIron.Blade,GolfClub.RightOrLeftHanded.Right),
                IronClub.Create("Titliest CB", "Cavity back iron with a lot of creativity",1599.9,30,IronClub.TypeOfIron.CavityBack,GolfClub.RightOrLeftHanded.Right),
                IronClub.Create("Cobra Darkspeed","Good improvement iron for higher handicapers",1399.9,50,IronClub.TypeOfIron.MuscleBack,GolfClub.RightOrLeftHanded.Left)

                ]);

            var putters = new List<PutterClub>();
            putters.AddRange(
                [
                PutterClub.Create("L.A.B OZ.1","Zero torque putter, to get the ball to start straighter every time",599.9,20,PutterClub.ShaftType.Broomstick,PutterClub.PutterType.Mallet,GolfClub.RightOrLeftHanded.Right),
                PutterClub.Create("Ping Anser", "Classic blade putter from Ping",299.9,40,PutterClub.ShaftType.Standard,PutterClub.PutterType.Blade,GolfClub.RightOrLeftHanded.Left)
                ]);

            var woods = new List<WoodClub>();
            woods.AddRange(
                [
                WoodClub.Create("Taylormade R7 Minidriver", "Newest minidriver from Taylormade with even higher MOI", 599, 100, 13.5,TypeOfWood.Spoon, GolfClub.RightOrLeftHanded.Right),
                WoodClub.Create("Callaway Elyte TD", "Lower spin and goes further than ever", 749.9, 50,9,TypeOfWood.Driver,GolfClub.RightOrLeftHanded.Right),
                WoodClub.Create("Ping G440 Max", "10k MOI with higher clubspeed than previous generations", 599, 100, 10.5,TypeOfWood.Driver,GolfClub.RightOrLeftHanded.Left)
                ]);
            foreach( WoodClub wood in woods)
            {
                context.Add(wood);
            }

            OrderRow orderRow1 = OrderRow.Create(irons[0], 1);
            

            OrderRow orderRow2 = OrderRow.Create(woods[0], 1);

            OrderRow orderRow3 = OrderRow.Create(putters[0],1);
            OrderRow orderRow4 = OrderRow.Create(irons[2], 2);
            OrderRow orderRow5 = OrderRow.Create(woods[1], 1);
            OrderRow orderRow6 = OrderRow.Create(putters[1], 2);

            var orderRows1 = new List<OrderRow>();
            var orderRows2 = new List<OrderRow>();

            orderRows1.AddRange(
                [
                orderRow1,
                orderRow2,
                orderRow3
                ]);

            orderRows2.AddRange(
                [
                orderRow4,
                orderRow5,
                orderRow6
                ]);

            context.Add(orderRow1);
            context.Add(orderRow2);

            var orders = new List<Order>();
            orders.AddRange(
                [
                Order.Create(customers[0],orderRows1),
                Order.Create(customers[1],orderRows2),
                Order.Create(customers[2],orderRows1)
                ]);
            foreach (Order order in orders)
            {
                context.Add(order);
            }
            context.SaveChanges();
            

        }
    }
}
