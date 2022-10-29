
using JPNetEF.App;
using JPNetEF.Entity;
using JPNetEF.Service;
using Microsoft.EntityFrameworkCore;

namespace JPNetEF
{
    class Program
    {
        public static void Main(String[] args)
        {
            using AppDBContext ctx = new();

            ClientService clientService = new(ctx);
            OrderService orderService = new(ctx);
            ItemService itemService = new(ctx);

            AppCLI cli = new(ctx, clientService, orderService, itemService);

            cli.Run();
        }
    }
}