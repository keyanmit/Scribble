using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace ScribbleBL.PrivacyHandler
{
    public class EnvelopedEncryptionHelper
    {

    }

    public class EncryptEnvelopedMessage
    {
        private ContentInfo envelopedContentInfo;
        private EnvelopedCms envelopedCms;
        private CmsRecipient envelopeCmsResipient;
        private X509Certificate2 cryptographyClientCert;

        public EncryptEnvelopedMessage(X509Certificate2 cert)
        {
            cryptographyClientCert = cert;
        }

        public byte[] encrypt(byte[] plainTest)
        {            
            envelopedContentInfo = new ContentInfo(plainTest);
            envelopedCms = new EnvelopedCms(envelopedContentInfo);
            envelopeCmsResipient = new CmsRecipient(SubjectIdentifierType.IssuerAndSerialNumber, cryptographyClientCert);            

            envelopedCms.Encrypt(envelopeCmsResipient);
            return envelopedCms.Encode();
        }
    }

    public class DecryptEnvelopedMessage
    {
        public byte[] decrypt(byte[] data)
        {
            var envelopedCms = new EnvelopedCms();
            envelopedCms.Decode(data);
            envelopedCms.Decrypt(envelopedCms.RecipientInfos[0]);
            return envelopedCms.ContentInfo.Content;
        }
    }
}
