using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    "MoviesByReleaseDate",
            //    // note that the parameters below are encased in curly braces
            //    "movies/released/{year}/{month}",
            //    // create an anonymous object to hold the parameters
            //    new { controller= "Movies", action= "byReleaseDate"},
            //    // add constraints on the url parameters by passing RegEx in another anonymous object
            //    // the one below for {year} means: digit (\d) with four repititions ({4})
            //    // prefix with @ to get a verbatim string (no character escapes and such..)
            //    //new { year = @"\d{4}", month = @"\d{2}"}
            //    // the one below constrains {year} to either 2015 or 2016
            //    //new { year = @"2015|2016", month = @"\d{2}"}
            //    // this one allows {year} to be between 2015 and 2018
            //    new { year = @"201[5-8]", month = @"\d{2}" }
            //);

            // the route below is the default one. because it is the most generic, it goes last in this list
            // order is important!
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
