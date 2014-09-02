using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterRoleContracts.AzureContracts;

namespace InterRoleContracts.Interfaces
{
    public interface IStorageId
    {
        StorageIdentifier GetIdentifier(UInt64 urlId);
        StorageIdentifier GetIdentifier(string url);
       
    }
}
