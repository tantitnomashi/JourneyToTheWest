using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace JourneyToTheWest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            HttpConfiguration config = GlobalConfiguration.Configuration;

          
            config.Formatters.JsonFormatter.MediaTypeMappings
                        .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept",
                              "text/html",
                              StringComparison.InvariantCultureIgnoreCase,
                              true,
                              "application/json"));

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
  .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

        }
    }
}
