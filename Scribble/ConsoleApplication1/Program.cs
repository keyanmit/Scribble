using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureHelperUtils.CommonObjects;
using InterRoleContracts.CommonObjects;
using InterRoleContracts.Enums;
using Newtonsoft.Json;
using ScribbleBL.UrlGeneration;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace ConsoleApplication1
{
    class Program
    {

        /*
         *{"RequestType":0,"Id":1212,"RequestData":"keyan is testing","RequestId":"a450ac54-1b69-4d0c-b5a9-f16c08a442b7"}
         * 
         * 
         */
        static void Main(string[] args)
        {
            var x = new WorkTaskModel()
            {
                Id = 1212,
                RequestData = "keyan is testing",
                RequestId = Guid.NewGuid(),
                RequestType = TaskListEnumeration.PersistNewPaste
            };                        
            Console.WriteLine(JsonConvert.SerializeObject(x));
            Console.Read();
            //trial();
            testCert();

        }

        static void testCert()
        {
            var x = new X509Store(StoreLocation.LocalMachine);
            //Scribble.DevBox.EncryptionCert
            x.Open(OpenFlags.ReadOnly);
            var certCol = x.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
            //var certCol = x.Certificates.Find(X509FindType.findb, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
            var cert = certCol[0];
            var st = "keyan is here";

            byte[] returnByt;
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider()) 
            {
                provider.FromXmlString(cert.PublicKey.Key.ToXmlString(false));
                returnByt = provider.Encrypt(GetBytes(st), true);                
            }

            using (RSACryptoServiceProvider provider = (RSACryptoServiceProvider)cert.PrivateKey)
            {
                var bytes = provider.Decrypt(returnByt, true);
                char[] chars = new char[bytes.Length / sizeof(char)];
                System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
                var strin= new string(chars);
            }
            
        }

        static byte[] GetBytes(string st)
        {
            byte[] byt = new byte[st.Length * sizeof(char)];
            System.Buffer.BlockCopy(st.ToCharArray(), 0, byt, 0, byt.Length);
            return byt;
        }

        static void trial()
        {
            var tits = new Base62ShortUrlFactory();
            Console.WriteLine(tits.GetShortUrl(12));
            Console.WriteLine(tits.GetShortUrl(26));
            Console.WriteLine(tits.GetShortUrl(52));
            Console.WriteLine(tits.GetShortUrl(61));
            Console.WriteLine(tits.GetShortUrl(62));
            Console.WriteLine(tits.GetShortUrl(63));
            Console.Read();
        }

        static void checkCert()
        {
            
        }
    }
}
