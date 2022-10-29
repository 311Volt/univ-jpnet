using JPNetEF.App;
using JPNetEF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPNetEF.Service
{
    internal class ItemService
    {
        private AppDBContext db;

        public ItemService(AppDBContext db)
        {
            this.db = db;
        }

        public List<Item> ListItems(int page, int pageSize = 5)
        {
            return db.Items.OrderBy(cl => cl.Id).Skip(page * pageSize).Take(pageSize).ToList();
        }
    }
}
