using System;
using System.Collections.Generic;
using System.Linq;

using Model.EF;

namespace Model.DAO
{

    public class ProductDao
    {
        private Drinkdbcontext db = new Drinkdbcontext();

        public IEnumerable<Product> getProduct()
        {
            // var product = from od in db.ProductCategories join p in db.Products on od.ID equals p.ID  group p by p.Name;
            // return product.ToList();
            return db.Products.Where(x=>x.Status==true).ToList();
            }
       
        public IEnumerable<Product> getProductCata(int top,int t)
        {
            // var product = from od in db.ProductCategories join p in db.Products on od.ID equals p.ID  group p by p.Name;
            // return product.ToList   
            


            return db.Products.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true && x.CategoryID==t).Take(top).ToList();
        }
        public IEnumerable<Product> getProductCa(int id)
        {
            // var product = from od in db.ProductCategories join p in db.Products on od.ID equals p.ID  group p by p.Name;
            // return product.ToList();
            return db.Products.Where(x => x.Status == true && x.CategoryID == id).ToList();
        }
        public IEnumerable<Product> getProductHot(int top,int t)
        {
            // var product = from od in db.ProductCategories join p in db.Products on od.ID equals p.ID  group p by p.Name;
            // return product.ToList();
            return db.Products.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true && x.CategoryID == t).Take(top).ToList();
        }
        
        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }

}