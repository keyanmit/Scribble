using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using InterRoleContracts.CommonObjects;
using Newtonsoft.Json;

namespace MvcWebRole2.BusinessLogic.CustomFormatter
{
    public static class FormDataToWorkTaskConverter
    {
        public static async Task<T> Convert<T>(MultipartMemoryStreamProvider mimeProvider)
        {
            if(typeof(T) != typeof(RawWorkTaskModel))
                throw new Exception("Media type called for an unknown object");

            if(mimeProvider == null)
                throw new Exception("MIME stream Provider is Null");
            
            try
            {
                var potentialJson = string.Empty;
                foreach (var httpContent in mimeProvider.Contents)
                {
                    var header = httpContent.Headers.ContentDisposition;
                    if (header.Name.Contains("ok"))
                    {
                        continue;                        
                    }
                    potentialJson += header.Name;
                    potentialJson += ":";
                    potentialJson += "\"" + await httpContent.ReadAsStringAsync() + "\",";                    
                               
                }
                potentialJson = "{" + potentialJson.Substring(0, potentialJson.Length - 1) + "}";
                var chumma = JsonConvert.DeserializeObject<T>(potentialJson);
                return chumma;
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}