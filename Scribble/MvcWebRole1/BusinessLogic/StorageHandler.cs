using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AzureHelperUtils;
using InterRoleContracts.CommonObjects;
using Microsoft.WindowsAzure.Storage.Table;

namespace MvcWebRole1.BusinessLogic
{
    public class StorageHandler
    {
        public static async Task<WorkTaskModel> GetWorkTask(string scribbleUrl)
        {
            var scribbleId = ScribbleWebEndpointResources.UrlFactory.GetUrlId(scribbleUrl);
            var storageIdentifier = ScribbleWebEndpointResources.StorageHelper.GetIdentifier(scribbleId);
            var result = await ScribbleWebEndpointResources.Table.GetDataAsync(storageIdentifier.Partitionkey.ToString(), storageIdentifier.RowKey.ToString());
            var t = result.Result;
            return new WorkTaskModel();
        }
    }
}