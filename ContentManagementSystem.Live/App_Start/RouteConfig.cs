using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContentManagementSystem.Live
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "QuestionSearch",
                 url: "blog/SearchParticularQuestion/{question}",
                 defaults: new { controller = "blog", action = "SearchParticularQuestion", question = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "QuestionSuggestion",
                url: "blog/Search/{questionQueryString}",
                defaults: new { controller = "blog", action = "Search", questionQueryString = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "GetQuestion",
                url: "blog/{questionQueryString}",
                defaults: new { controller = "blog", action = "GetQuestion", questionQueryString = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "TechnologySearch",
                url: "technology/{techQueryString}",
                defaults: new { controller = "Technology", action = "GetTechnology", techQueryString = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
         );

        }
    }
}
