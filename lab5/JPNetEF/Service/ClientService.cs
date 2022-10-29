using JPNetEF.App;
using JPNetEF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.Service
{
    internal class ClientService
    {
        private AppDBContext db;

        public ClientService(AppDBContext db)
        {
            this.db = db;
        }

        public List<Client> ListClients(int page, int pageSize=5)
        {
            return db.Clients.OrderBy(cl => cl.Name).Skip(page * pageSize).Take(pageSize).ToList();
        }
    }
}
