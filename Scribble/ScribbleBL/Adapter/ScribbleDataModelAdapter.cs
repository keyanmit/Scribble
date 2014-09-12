using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using InterRoleContracts.AzureContracts;
using InterRoleContracts.CommonObjects;
using InterRoleContracts.Interfaces;
using ScribbleBL.Persist;
using ScribbleBL.BusinessLogic;
using ScribbleBL.PrivacyHandler;

namespace ScribbleBL.Adapter
{
    public class ScribbleDataModelAdapter
    {
        private static IStorageId storagehelper;
        private static ScribbleCryptographyHandler cryptoHelper;
        private static X509Certificate2 cryptCert;
        static ScribbleDataModelAdapter()
        {
            storagehelper = new Base62StorageHelper();

            try
            {
                var machineCertStore = new X509Store(StoreLocation.LocalMachine);
                machineCertStore.Open(OpenFlags.ReadOnly);
                var certs = machineCertStore.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
                cryptCert = certs[0];
                machineCertStore.Close();
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error initializing Encryption Cert." + ex.Message);
                throw;
            }


            cryptoHelper = new ScribbleCryptographyHandler(cryptCert); //new X509Cryptography(cryptCert,true);
        }

        public static ScribblePersistDataModel GetScribbleDataModel(WorkTaskModel task, StorageIdentifier storageIdForRequest)
        {                         
            return new ScribblePersistDataModel(storageIdForRequest){                
                RequestId = task.RequestId,
                Data = cryptoHelper.GetEncryptedString(task.RequestData),
                UrlId = task.Id
            };
        }
    }
}
