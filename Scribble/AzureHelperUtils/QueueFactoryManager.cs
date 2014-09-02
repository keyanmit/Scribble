using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureHelperUtils.CommonObjects;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureHelperUtils
{
    public static class QueueFactoryManager
    {
        public static async Task<CloudQueue>  GetQueueInstanceAsync(this DeploymentContext context)
        {
            var storageAccount = CloudStorageAccount.Parse(((StorageContext)context).StorageConString);
            var queueManager = storageAccount.CreateCloudQueueClient();
            var queueInstance = queueManager.GetQueueReference(((StorageContext)context).QueueName);
            await queueInstance.CreateIfNotExistsAsync();
            return queueInstance;
        }
    }
}
