using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.Entity
{
    internal class ECommClient : Client
    {
        [Required]
        public string IpAddress { get; set; }

        public ECommClient() { }

        public ECommClient(int id, string name, string address, ICollection<Order> orders, string ipAddress)
            : base(id, name, address, orders)
        {
            IpAddress = ipAddress;
        }

        public override string ToString()
        {
            return base.ToString() + " | inet " + IpAddress;
        }
    }
}
