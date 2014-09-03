using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace ScribbleBL.PrivacyHandler
{
    public class ScribbleCryptographyHandler
    {
        private EncryptEnvelopedMessage encryptionHandler;
        private DecryptEnvelopedMessage decryptionHandler;

        public ScribbleCryptographyHandler(X509Certificate2 cert)
        {
            encryptionHandler = new EncryptEnvelopedMessage(cert);
            decryptionHandler = new DecryptEnvelopedMessage();
        }

        public string GetEncryptedString(string data)
        {
            var tmp = encryptionHandler.encrypt(Encoding.Unicode.GetBytes(data));
            var base64EncodedString = Convert.ToBase64String(tmp);
            return base64EncodedString;
        }

        public string GetDecryptedString(string data)
        {            
            var decodedFromBase64 = Convert.FromBase64String(data);
            var decodedBytes = decryptionHandler.decrypt(decodedFromBase64);
            return Encoding.Unicode.GetString(decodedBytes);
        }
    }
}
