using System.Web.Mvc;
using System.Web.Routing;

namespace ThucPhamSach
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //them gio hang
            routes.MapRoute(
               name: "Add",
               url: "Them-Gio-Hang",
               defaults: new { controller = "ShoppingCart", action = "AddToCart", id = UrlParameter.Optional },
               namespaces: new[] { "ThucPhamSach.Controllers" }
           );

            //xem gio hang
            routes.MapRoute(
                name: "Cart",
                url: "Gio-hang",
                defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ThucPhamSach.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ThucPhamSach.Controllers" }
            );
        }
    }
}
