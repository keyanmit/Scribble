using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using InterRoleContracts.CommonObjects;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using AzureHelperUtils;
using AzureHelperUtils.CommonObjects;
using InterRoleContracts.Enums;
using InterRoleContracts.Interfaces;
using Newtonsoft.Json;
using ScribbleBL.Persist;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        private CloudQueue queue;
        private CloudTable table;

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole1 is running");

            try
            {
                var deploymentContext = new StorageContext()
                {
                    CurrentEnvironment = EnvironmentEnum.DevBox
                };

                var iQueue = deploymentContext.GetQueueInstanceAsync();
                iQueue.Wait();
                queue = iQueue.Result;

                var iTable = deploymentContext.GetTableInstanceAsync();
                iTable.Wait();
                table = iTable.Result;

                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("WorkerRole1 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole1 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole1 has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            CloudQueueMessage workTask = null;
                        
            var storageManager = new AzurePersistHelper();

            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Checking queue for Task");
                if ((workTask = await queue.GetTaskIfAny(5)) != null)
                {
                    var task = workTask;                    
                    Task.Run(async () =>
                    {
                        WorkTaskModel request = new WorkTaskModel();
                        try
                        {
                            request = JsonConvert.DeserializeObject<WorkTaskModel>(task.AsString);
                            request.Validate();

                            switch (request.RequestType)
                            {
                                case TaskListEnumeration.PersistNewPaste:
                                    await storageManager.PersistWorkTask(table, request);// request.RequestData, request.Id);
                                    Trace.TraceInformation("Deleting processed message from the queue");
                                    await queue.DeleteMessageAsync(workTask, cancellationToken);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError("error: " + ex.Message
                                            + "error Trace: " + ex.StackTrace
                                            + request.ToString());
                            queue.DeleteMessage(workTask);
                        }                        
                    }).Wait(cancellationToken);
                }
                await Task.Delay(1000,cancellationToken);
            }
        }
    }
}
