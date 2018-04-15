using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SaleDrink
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
          
            routes.MapRoute(
           name: "TrangChu",
           url: "Home/{alias}",
           defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "SaleDrink.Controllers" }
       );
              routes.MapRoute(
             name: "SanPham",
             url: "{alias}",
             defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "SaleDrink.Controllers" }
         );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "SaleDrink.Controllers" }
         );


        }
    }
}
