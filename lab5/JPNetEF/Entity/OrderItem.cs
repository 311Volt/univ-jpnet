using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.Entity
{
    internal class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public OrderItem() { }

        public OrderItem(int id, Item item, int quantity)
        {
            Id = id;
            Item = item;
            Quantity = quantity;
        }

        public decimal GetValue()
        {
            return Item.Price * Quantity;
        }

        public override string ToString()
        {
            return String.Format("{0}x{1}[#{2}]", Quantity, Item.Name, Item.Id);
        }
    }

    internal class OrderItemRequest
    {
        public int itemId;
        public int quantity;
    }
}
