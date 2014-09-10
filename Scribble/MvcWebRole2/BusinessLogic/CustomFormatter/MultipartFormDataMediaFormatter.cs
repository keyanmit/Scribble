using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using InterRoleContracts.CommonObjects;
using Newtonsoft.Json;

namespace MvcWebRole2.BusinessLogic.CustomFormatter
{
    public class MultipartFormDataMediaFormatter : MediaTypeFormatter
    {
        private const string supportedMediaType = "multipart/form-data";

        public MultipartFormDataMediaFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(supportedMediaType));
        }

        public override bool CanReadType(Type type)
        {
            return type == typeof (RawWorkTaskModel);
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content,
                                                               IFormatterLogger formatterLogger)
        {   
            if(type != typeof(RawWorkTaskModel))
                throw new Exception("Media formatter called for unknow type.");

            try
            {
                var x = await content.ReadAsMultipartAsync();
                var reqObj = await FormDataToWorkTaskConverter.Convert<RawWorkTaskModel>(x); //JsonConvert.DeserializeObject<responseMy>(requestString);
                return reqObj;
            }
            catch (JsonSerializationException jex)
            {
                Trace.TraceInformation("request failed in media formatter. " + jex.Message);
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}