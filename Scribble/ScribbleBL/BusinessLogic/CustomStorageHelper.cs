using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using InterRoleContracts.AzureContracts;
using InterRoleContracts.Interfaces;
using InterRoleContracts.CommonObjects;
using ScribbleBL.UrlGeneration;

namespace ScribbleBL.BusinessLogic
{
    public class Base62StorageHelper : IStorageId
    {        
        public static int ShradCount { get; private set; }
        private static IUrlShortner urlFactory = new Base62ShortUrlFactory();

        public Base62StorageHelper()
        {     
            ShradCount = 10;
        }

        public StorageIdentifier GetIdentifier(UInt64 urlId)
        {            
            return new StorageIdentifier()
            {
                Partitionkey =  (int)(urlId / (UInt64)ShradCount),
                RowKey = (int)(urlId)%ShradCount
            };
        }

        public StorageIdentifier GetIdentifier(string url)
        {
            
            return GetIdentifier(urlFactory.GetUrlId(url));
        }
    }
}
