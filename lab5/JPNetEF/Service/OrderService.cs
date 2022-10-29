using JPNetEF.App;
using JPNetEF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.Service
{
    internal class OrderService
    {
        private AppDBContext db;

        public OrderService(AppDBContext db)
        {
            this.db = db;
        }

        public void PlaceOrder(Order order)
        {
            ValidateOrder(order);
            db.Add<Order>(order);
            db.SaveChanges();
        }

        public void PlaceOrder(OrderRequest req)
        {
            PlaceOrder(BuildOrder(req));
        }

        private OrderItem BuildOrderItem(OrderItemRequest req)
        {
            Item? item = db.Find<Item>(req.itemId);
            if(item == null)
                throw new OrderException("no such item: #" + req.itemId);
            if(req.quantity < 0)
                throw new OrderException("invalid quantity: " + req.quantity);

            return new OrderItem { Item = item, Quantity = req.quantity };
        }

        private Order BuildOrder(OrderRequest req)
        {
            Client? client = db.Find<Client>(req.clientId);
            if (client == null)
                throw new OrderException("no such client: #" + req.clientId);
            Order order = new Order { Client = client, Completed = false, Items = new List<OrderItem>() };
            foreach (OrderItemRequest itReq in req.items)
                order.Items.Add(BuildOrderItem(itReq));

            return order;
        }

        private static void ValidateOrder(Order order)
        {
            foreach (OrderItem orderItem in order.Items)
            {
                if (orderItem.Quantity > orderItem.Item.NumAvailable)
                {
                    throw new OrderException(
                        String.Format("invalid order: requested {0} {1}, but only {2} is available",
                        orderItem.Quantity, orderItem.Item.Name, orderItem.Item.NumAvailable
                    ));
                }
            }
        }

        public void CompleteOrder(int orderId)
        {
            using var tx = db.Database.BeginTransaction();
            Order? order = db.Find<Order>(orderId);

            if (order == null)
                throw new OrderException(String.Format("No such order: {0}", orderId));
            ValidateOrder(order);

            foreach (OrderItem orderItem in order.Items)
            {
                orderItem.Item.NumAvailable -= orderItem.Quantity;
            }
            order.Completed = true;

            db.SaveChanges();
            tx.Commit();
        }

        public List<Order> ListOrders(int page, int pageSize=5)
        {
            return db.Orders.OrderBy(ord => ord.Id).Skip(page * pageSize).Take(pageSize).ToList();
        }
    }

    internal class OrderException: Exception
    {
        public OrderException() { }
        public OrderException(string msg) : base(msg) { }
    }
}
