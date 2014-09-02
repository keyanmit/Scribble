using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using InterRoleContracts.AzureContracts;

namespace ScribbleBL.Persist
{
    public class ScribblePersistDataModel : TableEntity 
    {
        public string Data { get; set; }
        public Guid RequestId { get; set; }
        public UInt64 UrlId { get; set; }

        public ScribblePersistDataModel(StorageIdentifier storageIdFromUrl)
        {
            this.PartitionKey = storageIdFromUrl.Partitionkey.ToString();
            this.RowKey = storageIdFromUrl.RowKey.ToString();
        }
    }
}
