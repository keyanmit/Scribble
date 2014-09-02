using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureHelperUtils
{
    public static class AzureQueueResourceHelper
    {
        public static async Task<CloudQueueMessage> GetTaskIfAny(this CloudQueue queue, int lockTimeInSeconds)
        {
            if (queue.PeekMessage() != null)
            {
                var msg = await queue.GetMessageAsync(TimeSpan.FromSeconds(lockTimeInSeconds), null, null);
                return msg;
            }
            return null;
        } 
    }
}
