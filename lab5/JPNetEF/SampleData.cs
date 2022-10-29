using JPNetEF.App;
using JPNetEF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF
{
    internal static class SampleData
    {
        public static readonly List<Item> items = new()
        {
            new Item(1, "Brylka rudy", 1.00M, 1000),
            new Item(2, "Niewykonczony miecz", 150.00M, 5),
            new Item(3, "Woda", 4.99M, 30),
            new Item(4, "Mapa kolonii", 75.00M, 3)
        };

        public static readonly List<Client> clients = new()
        {
            new Client(1, "client1", "addr1", new List<Order>()),
            new Client(2, "client2", "addr2", new List<Order>()),
            new Client(3, "client3", "addr3", new List<Order>()),
            new Client(4, "client4", "addr4", new List<Order>()),
            new Client(5, "client5", "addr5", new List<Order>()),
            new ECommClient(6, "client6", "addr6", new List<Order>(), "139.63.216.77"),
            new Client(7, "client7", "addr7", new List<Order>())
        };

        public static void InsertIntoDB(AppDBContext ctx)
        {
            ctx.AddRange(items);
            ctx.AddRange(clients);
            ctx.SaveChanges();
        }
    }
}
