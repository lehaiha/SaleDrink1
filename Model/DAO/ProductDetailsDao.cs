using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
  public  class ProductDetailsDao
    {
        private Drinkdbcontext db = new Drinkdbcontext();
       
        public IEnumerable<Product> getProductDetails(int id)
        {
            return db.Products.Where(x => x.Status == true && x.ID==id).ToList();


        }
    }
}
