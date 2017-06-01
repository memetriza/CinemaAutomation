using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CinemaAutomation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" });
            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" });
            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" });
            routes.MapRoute("NewAccount", "uyeol", new { controller = "Account", action = "New" });
            routes.MapRoute("Edit", "hesapduzenle", new { controller = "Account", action = "Edit" });
            routes.MapRoute("BuyTicket", "buyticket", new { controller = "Home", action = "BuyTicket" });

        }
    }
}
