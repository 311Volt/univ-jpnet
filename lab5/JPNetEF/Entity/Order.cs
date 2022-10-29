using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.Entity
{
    internal class Order
    {
        [Key]
        public int Id { get; set; }
        public Client Client { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public bool Completed { get; set; }

        public Order(){}

        public Order(int id, Client client, ICollection<OrderItem> items, bool completed)
        {
            Id = id;
            Client = client;
            Items = items;
            Completed = completed;
        }

        public decimal GetValue()
        {
            return Items.Sum(orderItem => orderItem.GetValue());
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendFormat("[#{0,-5}] | cl: {1} | ", Id, Client.Name);
            bool c = false;
            foreach(OrderItem orderItem in Items)
            {
                if (c)
                    sb.Append(", ");
                c = true;
                sb.Append(orderItem.ToString());
            }
            sb.AppendFormat(" ({0:C2}) Completed={1}", GetValue(), Completed);
            return sb.ToString();
        }
    }

    internal class OrderRequest
    {
        public int clientId;
        public List<OrderItemRequest> items = new();
    }
}
