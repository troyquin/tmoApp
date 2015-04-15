using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace tmoWebAPI3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

						var corsAttr = new EnableCorsAttribute("http://localhost:3000", "*", "*", "X-Paging-PageCount, X-Paging-PageNo, X-Paging-PageSize, X-Paging-TotalRecordCount, Link");					
						config.EnableCors(corsAttr);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

						config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(
							config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml"));

						config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
						= Newtonsoft.Json.ReferenceLoopHandling.Ignore;

						config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

						
        }
    }
}
