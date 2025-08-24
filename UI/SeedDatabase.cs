using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace UI
{
    internal class SeedDatabase
    {
        public void Seed()
        {
            var customers = new List<Customer>();
            customers.AddRange(
            [
                Customer.Create("Peter", "Hansson", "peterhansson@hotmail.com"),
                Customer.Create("Josef", "Lagerqvist", "joseflagerqvist@hotmail.com"),
                Customer.Create("Sara", "Lindblad", "saralindblad@outlook.com")
             ]);

            var items = new List<Item>();
            items.AddRange(
                [
                Item.Create("Taylormade R7 Minidriver", "Newest minidriver from Taylormade with even higher MOI", 599, 100 ),
                Item.Create("Callaway Elyte TD", "Lower spin and goes further than ever", 749.9, 50 ),
                Item.Create("Ping G440 Max", "10k MOI with higher clubspeed than previous generations", 599, 100 )
                ]);
            
            var orderRow1 = new List<OrderRow>();
            orderRow1.AddRange(
                [
                OrderRow.Create(items[0],1),
                ]);
            var orderRow2 = new List<OrderRow>();
            orderRow2.AddRange(
                [
                OrderRow.Create(items[0],1),
                OrderRow.Create(items[2],1),
                ]);
            var orders = new List<Order>();
            orders.AddRange(
                [
                Order.Create(customers[1],orderRow2),
                Order.Create(customers[2],orderRow1)
                ]);

            Console.WriteLine(
                $"{orders[1].Customer.FirstName} beställde {orders[1].OrderRows[0].Item.Name} (antal: {orders[1].OrderRows[0].Quantity})"
);



        }
    }
}
