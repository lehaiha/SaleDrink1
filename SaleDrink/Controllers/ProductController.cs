using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleDrink.Controllers
{

    public class ProductController : Controller
    {
        private Drinkdbcontext db = new Drinkdbcontext();
       
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Product = new ProductDao().getProduct();
            return View();
        }
    }
}