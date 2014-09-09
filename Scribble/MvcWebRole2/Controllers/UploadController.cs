using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using AzureHelperUtils;
using AzureHelperUtils.CommonObjects;
using InterRoleContracts.CommonObjects;
using InterRoleContracts.Constants;
using InterRoleContracts.Enums;
using Microsoft.WindowsAzure.Storage.Queue;
using MvcWebRole2.BusinessLogic;

namespace MvcWebRole2.Controllers
{
    public class UploadController : ApiController
    {
        private static DeploymentContext deploymentContext;
        private static CloudQueue queue;

        public UploadController()
        {
            deploymentContext = new StorageContext()
            {
                CurrentEnvironment = EnvironmentEnum.DevBox,                
            };
            queue = deploymentContext.GetQueueInstanceAsync().Result;
        }

        // POST api/upload  
        [HttpPost]
        [ActionName("Upload")]
        public async Task<HttpResponseMessage> UploadWorkTask()
        {
            var curHttpRequest = HttpContext.Current.Request;
            var filesInCurrentRequest = curHttpRequest.Files;
            if (filesInCurrentRequest[HtmlConstants.HtmlWorkTaskName] == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Request Object does not contain proper file name");                
            }

            var formData = filesInCurrentRequest[HtmlConstants.HtmlWorkTaskName];
            var workTaskBytes = new byte[formData.ContentLength];
            await formData.InputStream.ReadAsync(workTaskBytes, 0, workTaskBytes.Length);
            
            var workTask = WorkTaskHandler.GenerateWorkTaskModel(workTaskBytes, curHttpRequest.ContentEncoding);
            await queue.WriteToQueueAsync(workTask);
            
            return Request.CreateResponse(HttpStatusCode.Accepted, "super ma");
        }        
    }
}
