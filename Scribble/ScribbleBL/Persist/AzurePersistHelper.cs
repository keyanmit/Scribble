using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterRoleContracts.CommonObjects;
using InterRoleContracts.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using AzureHelperUtils;
using ScribbleBL.Adapter;
using ScribbleBL.BusinessLogic;

namespace ScribbleBL.Persist
{
    public class AzurePersistHelper
    {
        private Base62StorageHelper storageHelper;

        public AzurePersistHelper()
        {
            storageHelper = new Base62StorageHelper();
        }

        public async Task PersistWorkTask(CloudTable table, WorkTaskModel task)
        {
            //convert this to storage model
            var tableEntity = ScribbleDataModelAdapter.GetScribbleDataModel(task, storageHelper.GetIdentifier(task.Id));
            await table.PersistDataAsync(tableEntity);
        }
    }
}
