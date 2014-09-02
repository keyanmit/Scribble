using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRoleContracts.AzureContracts
{
    public class StorageIdentifier
    {
        public int RowKey { get; set; }
        public int Partitionkey { get; set; }       
    }
}
