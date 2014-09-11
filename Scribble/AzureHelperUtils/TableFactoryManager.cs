using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using AzureHelperUtils.CommonObjects;

namespace AzureHelperUtils
{
    public static class AzureTableFactoryManager
    {
        public static async Task<CloudTable> GetTableInstanceAsync(this DeploymentContext context)
        {
            var storageAccount = CloudStorageAccount.Parse(((StorageContext)context).StorageConString);
            var tableManager = storageAccount.CreateCloudTableClient();
            var tableInstance = tableManager.GetTableReference(((StorageContext)context).TableName);
            if (!tableInstance.Exists())
            {
                await tableInstance.CreateAsync();
            }
            return tableInstance;
        }
    }
}
