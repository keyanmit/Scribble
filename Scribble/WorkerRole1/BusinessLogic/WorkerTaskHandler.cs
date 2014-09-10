using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InterRoleContracts.CommonObjects;
using InterRoleContracts.Enums;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using ScribbleBL.Persist;

namespace WorkerRole1.BusinessLogic
{
    public class WorkerTaskHandler
    {
        private CancellationToken cancellationToken;
        private CloudQueueMessage task;
        private AzurePersistHelper storageManager;

        public WorkerTaskHandler(CloudQueueMessage workTask, CancellationToken cancelTkn)
        {
            cancellationToken = cancelTkn;
            task = workTask;
            storageManager = new AzurePersistHelper();
        }

        public async Task RunTask()
        {

            await Task.Run(async () =>
            {
                var request = new WorkTaskModel();
                try
                {
                    request = JsonConvert.DeserializeObject<WorkTaskModel>(task.AsString);
                    request.Validate();

                    switch (request.RequestType)
                    {
                        case TaskListEnumeration.PersistNewPaste:
                            await storageManager.PersistWorkTask(ScribbleWorkerRoleResource.Table, request);// request.RequestData, request.Id);
                            Trace.TraceInformation("Deleting processed message from the queue");
                            await ScribbleWorkerRoleResource.Queue.DeleteMessageAsync(task, cancellationToken);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("error: " + ex.Message
                                    + "error Trace: " + ex.StackTrace
                                    + request.ToString());
                    ScribbleWorkerRoleResource.Queue.DeleteMessage(task);
                }
            },cancellationToken);
        }
    }
}
