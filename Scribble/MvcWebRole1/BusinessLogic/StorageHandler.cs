using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using AzureHelperUtils;
using InterRoleContracts.CommonObjects;
using Microsoft.WindowsAzure.Storage.Table;
using ScribbleBL.Persist;

namespace MvcWebRole1.BusinessLogic
{
    public class StorageHandler
    {
        private enum StorageStatusCode
        {
            success = 200
        }

        public static async Task<WorkTaskModel> GetWorkTask(string scribbleUrl)
        {
            try
            {


                var scribbleId = ScribbleWebEndpointResources.UrlFactory.GetUrlId(scribbleUrl);
                var storageIdentifier = ScribbleWebEndpointResources.StorageHelper.GetIdentifier(scribbleId);
                var result =
                    await
                        ScribbleWebEndpointResources.Table.GetDataAsync<ScribblePersistDataModel>(
                            storageIdentifier.Partitionkey.ToString(), storageIdentifier.RowKey.ToString());
                //ScribblePersistDataModel persistedTask = result.Result;
                var returnModel = new WorkTaskModel();
                if (result.HttpStatusCode == (int) StorageStatusCode.success &&
                    result.Result.GetType() == typeof (ScribblePersistDataModel))
                {
                    var persistedTask = (ScribblePersistDataModel) result.Result;
                    returnModel.Id = persistedTask.UrlId;
                    var tmpstring =
                        ScribbleWebEndpointResources.ScribbleCryptographyHandler.GetDecryptedString(persistedTask.Data);
                    returnModel.RequestData = tmpstring;
                    returnModel.RequestId = persistedTask.RequestId;
                }

                return returnModel;
            }
            catch (CryptographicException exception)
            {
                Trace.TraceError("Decryption failed. " + exception.Message);
                throw;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error reading from azure storage. " + ex.Message);
                throw;
            }
        }
    }
}