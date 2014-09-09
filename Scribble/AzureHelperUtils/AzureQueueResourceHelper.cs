using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

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

        public static async Task WriteToQueueAsync<T>(this CloudQueue queue, T data, bool overwrite = true)
        {
            var msgToAdd = JsonConvert.SerializeObject(data);
            try
            {
                await queue.AddMessageAsync(new CloudQueueMessage(msgToAdd));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
    }
}
