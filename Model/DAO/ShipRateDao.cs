using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
   public class ShipRateDao
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        public IEnumerable<ProductsShip> getMoney(int distance)
        {
            return db.ProductsShip.Where(x=>x.FromDes<=distance && x.ToDes>=distance).ToList();
        }
    }
}
