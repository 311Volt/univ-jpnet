using JPNetEF.Entity;
using JPNetEF.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.App
{
    internal class AppCLI
    {
        private AppDBContext ctx;
        private ClientService clientService;
        private OrderService orderService;
        private ItemService itemService;

        CommandDispatcher commandDispatcher;

        public delegate void CommandCallback(string[] args);

        public AppCLI(AppDBContext ctx, ClientService clientService, OrderService orderService, ItemService itemService)
        {
            this.ctx = ctx;
            this.clientService = clientService;
            this.orderService = orderService;
            this.itemService = itemService;

            commandDispatcher = new();

            commandDispatcher.Register("help", Help); 
            commandDispatcher.Register("init", Init);
            commandDispatcher.Register("lc", ListClients, 1);
            commandDispatcher.Register("li", ListItems, 1);
            commandDispatcher.Register("lo", ListOrders, 1);
            commandDispatcher.Register("place", PlaceOrder, 2);
            commandDispatcher.Register("complete", CompleteOrder, 1);

        }

        private void ListClients(string[] args)
        {
            int page = Int32.Parse(args[0]);
            Console.WriteLine(String.Format("Clients [page {0}]: ", page));
            foreach(Client client in clientService.ListClients(page))
                Console.WriteLine(client);
            Console.WriteLine();
        }

        private void ListItems(string[] args)
        {
            int page = Int32.Parse(args[0]);
            Console.WriteLine(String.Format("Items [page {0}]: ", page));
            foreach (Item item in itemService.ListItems(page))
                Console.WriteLine(item);
            Console.WriteLine();
        }

        private void ListOrders(string[] args)
        {
            int page = Int32.Parse(args[0]);
            Console.WriteLine(String.Format("Orders [page {0}]: ", page));
            foreach (Order order in orderService.ListOrders(page))
                Console.WriteLine(order);
            Console.WriteLine();
        }

        private void PlaceOrder(string[] args)
        {
            string[] orderItems = args[1].Split(",", StringSplitOptions.RemoveEmptyEntries);
            OrderRequest req = new OrderRequest { 
                clientId = Int32.Parse(args[0]), 
                items = new List<OrderItemRequest>()
            };

            foreach(string item in orderItems)
            {
                string[] parts = item.Split("x", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    throw new Exception("cannot parse order item " + item);

                int quantity, itemId;
                if (!Int32.TryParse(parts[0], out quantity))
                    throw new Exception(item + ": quantity should be a number");
                if (!Int32.TryParse(parts[1], out itemId))
                    throw new Exception(item + ": itemId should be a number");

                req.items.Add(new OrderItemRequest { itemId = itemId, quantity = quantity });
            }

            orderService.PlaceOrder(req);
        }

        private void CompleteOrder(string[] args)
        {
            int orderID = Int32.Parse(args[0]);
            orderService.CompleteOrder(orderID);
        }

        private void Help(string[] args)
        {
            Console.WriteLine("\nAvailable commands: ");
            Console.WriteLine("exit -- exit");
            Console.WriteLine("init -- wipe DB and init with sample data");
            Console.WriteLine("lc [pageNum] -- list clients");
            Console.WriteLine("li [pageNum] -- list items");
            Console.WriteLine("lo [pageNum] -- list orders");
            Console.WriteLine("place [clientId], itemList{ [qty],[itemId] } -- place order (ex. \"place 2 3x0,7x1\")");
            Console.WriteLine("complete [orderId] -- complete order");

            Console.WriteLine();
        }

        private void Init(string[] args)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.Migrate();
            SampleData.InsertIntoDB(ctx);
        }

        private void Prompt()
        {
            ConsoleUtil.WriteColor("EFLab CLI>", ConsoleColor.Yellow);
        }

        public void Run()
        {
            string cmd;
            Console.WriteLine("JP.Net Lab5 EF v1.0");
            Console.WriteLine("To initialize the database, type \"init\".\n");
            Console.WriteLine("For help, type \"help\".\n");
            while(true)
            {
                Prompt();
                cmd = Console.ReadLine()!;
                if (cmd.Trim().ToLower() == "exit")
                    break;

                commandDispatcher.Dispatch(cmd);
            }
        }
    }
}
