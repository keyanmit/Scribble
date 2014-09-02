using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterRoleContracts.Enums;

using Microsoft.WindowsAzure;

namespace AzureHelperUtils.CommonObjects
{
    public class StorageContext : DeploymentContext
    {        
        public string StorageConString
        { 
            get 
            {
                return CloudConfigurationManager.GetSetting("Scribble.ConnectionStrings.CommonStorage." + this.CurrentEnvironment.ToString()); 
            }            
        }
        public string QueueName 
        {
            get 
            {
                return CloudConfigurationManager.GetSetting("Scribble.QueueNames.TaskListQueue"); 
            }
        }
        public string TableName
        {
            get
            {
                return CloudConfigurationManager.GetSetting("Scribble.TableNames.TaskListPersistTable"); 
            }
        }
    }
}
