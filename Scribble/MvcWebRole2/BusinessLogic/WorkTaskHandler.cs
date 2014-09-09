using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using InterRoleContracts.CommonObjects;
using InterRoleContracts.Enums;
using MvcWebRole2.BusinessLogic;

namespace MvcWebRole2.BusinessLogic
{
    public class WorkTaskHandler
    {
        public static WorkTaskModel GenerateWorkTaskModel(byte[] WorkTaskAsBytes, Encoding WorkTaskEncoding)
        {
            var newTask = new WorkTaskModel()
            {
                RequestId = new Guid(),
                RequestType = TaskListEnumeration.PersistNewPaste
            };

            try
            {
                var workTaskString = WorkTaskEncoding.GetString(WorkTaskAsBytes); //WorkTaskAsBytes
                newTask.Id = IdUrlGenerator.GetIdForRequest();
                newTask.RequestData = workTaskString;
                return newTask;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Parsing request byte failed for: " + newTask + " input char seq :"+WorkTaskAsBytes);
                throw ex;
            }
        }
    }
}