using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AzureHelperUtils;
using AzureHelperUtils.CommonObjects;
using InterRoleContracts.Enums;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using MvcWebRole2.BusinessLogic.CustomFormatter;
using ScribbleBL.UrlGeneration;

namespace MvcWebRole2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.Add(new MultipartFormDataMediaFormatter());
        }
    }

    public class ScribbleResources
    {
        public static CloudQueue Queue;
        public static Base62ShortUrlFactory UrlGenerator; 

        static ScribbleResources()
        {
            DeploymentContext deploymentContext = new StorageContext()
            {
                CurrentEnvironment = EnvironmentEnum.DevBox
            };
            var tmp = deploymentContext.GetQueueInstanceAsync();
            tmp.Wait();
            Queue = tmp.Result;

            UrlGenerator = new Base62ShortUrlFactory();
        }
    }
}