using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AzureHelperUtils;
using AzureHelperUtils.CommonObjects;
using InterRoleContracts.Enums;
using Microsoft.WindowsAzure.Storage.Table;
using ScribbleBL.BusinessLogic;
using ScribbleBL.PrivacyHandler;
using ScribbleBL.UrlGeneration;

namespace MvcWebRole1.BusinessLogic
{
    public static class ScribbleWebEndpointResources
    {
        private static DeploymentContext DeploymentContext { get; set; }
        public static CloudTable Table { get; set; }
        public static Base62ShortUrlFactory UrlFactory { get; set; }
        public static Base62StorageHelper StorageHelper { get; set; }
        public static ScribbleCryptographyHandler ScribbleCryptographyHandler { get; set; }

        static ScribbleWebEndpointResources()
        {
            DeploymentContext = new StorageContext()
            {
                CurrentEnvironment = EnvironmentEnum.DevBox
            };

            var tableTask = DeploymentContext.GetTableInstanceAsync();
            tableTask.Wait();
            Table = tableTask.Result;

            UrlFactory = new Base62ShortUrlFactory();
            StorageHelper = new Base62StorageHelper();
            ScribbleCryptographyHandler = new ScribbleCryptographyHandler();
        }
    }
}