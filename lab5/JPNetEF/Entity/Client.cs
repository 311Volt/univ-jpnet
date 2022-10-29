using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.Entity
{
    internal class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Client() { }

        public Client(int id, string name, string address, ICollection<Order> orders)
        {
            Id = id;
            Name = name;
            Address = address;
            Orders = orders;
        }


        public decimal TotalOrderValue()
        {
            return Orders.Sum(order => order.GetValue());
        }

        public override string ToString()
        {
            return String.Format(
                "[#{0,-5}] | {1} @ {2} | {3}",
                Id, Name, Address, (this is ECommClient)?"EComm":"Regular"
            );
        }
    }
}
