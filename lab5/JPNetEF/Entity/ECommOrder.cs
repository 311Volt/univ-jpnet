using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.Entity
{
    internal class ECommOrder: Order
    {
        public string IpAddress { get; set; }

        public ECommOrder() { }

        public ECommOrder(int id, Client client, ICollection<OrderItem> items, bool completed, string ipAddress)
            : base(id, client, items, completed)
        {
            IpAddress = ipAddress;
        }

        public override string ToString()
        {
            return base.ToString() + " | inet " + IpAddress;
        }
    }
}
