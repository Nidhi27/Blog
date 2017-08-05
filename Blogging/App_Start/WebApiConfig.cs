using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Data.Entity;

namespace Blogging
{
    public static class WebApiConfig
    {
       // private static MediaTypeHeaderValue appXmlType;

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
            //.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //GlobalConfiguration.Configuration.Formatters
            //    .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

           GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;

            //config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
           config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
