using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScribbleBL.PrivacyHandler;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

namespace Scribble.Test.CryptographyTests
{
    [TestClass]
    public class encryptiondecryptionTest
    {        
        [TestMethod]
        public void testCodePlexCode()
        {

            var machineCertStore = new X509Store(StoreLocation.LocalMachine);
            machineCertStore.Open(OpenFlags.ReadOnly);
            var certs = machineCertStore.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
            var cryptCert = certs[0];
            machineCertStore.Close();

            var x = new X509Cryptography(cryptCert, true);
            var y = x.EncryptString("testing this sring da mama");
            var z = x.DecryptString(y);

        }

        private string getStringFromBytes(byte[] bytes)
        {
            //return UTF8Encoding.UTF8.GetString(bytes);            
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        [TestMethod]
        public void TestHugeCryptography()
        {
            var stBytes = System.IO.File.ReadAllBytes(@"D:\thakali\Scribble\files for test\googleDump.txt");
            var st = getContent();//getStringFromBytes(stBytes);

            var machineCertStore = new X509Store(StoreLocation.LocalMachine);
            machineCertStore.Open(OpenFlags.ReadOnly);
            var certs = machineCertStore.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
            var cryptCert = certs[0];
            machineCertStore.Close();

            var x = new X509Cryptography(cryptCert, true);
            var y = x.EncryptString(st);
            var z = x.DecryptString(y);
            Assert.AreEqual(st, z);

            st = getContent(true);
            x = new X509Cryptography(cryptCert, true);
            y = x.EncryptString(st);
            z = x.DecryptString(y);
            Assert.AreEqual(st, z);

        }

        [TestMethod]
        public void TestPkcsInfra()
        {
            #region setup

            var machineCertStore = new X509Store(StoreLocation.LocalMachine);
            machineCertStore.Open(OpenFlags.ReadOnly);
            var certs = machineCertStore.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
            var cryptCert = certs[0];
            machineCertStore.Close();

            var cryptHandler = new ScribbleCryptographyHandler(cryptCert);

            #endregion

            // small case
            var dak = getContent();

            var x = cryptHandler.GetEncryptedString(dak);
            var y = cryptHandler.GetDecryptedString(x);
            Assert.AreEqual<string>(dak, y);


            //huge case
            dak = getContent(true);

            x = cryptHandler.GetEncryptedString(dak);
            y = cryptHandler.GetDecryptedString(x);
            Assert.AreEqual<string>(dak, y);

        }

        [TestMethod]
        public void testEnvelopedCms()
        {
            var machineCertStore = new X509Store(StoreLocation.LocalMachine);
            machineCertStore.Open(OpenFlags.ReadOnly);
            var certs = machineCertStore.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
            var cryptCert = certs[0];
            machineCertStore.Close();

            var encryptHandler = new EncryptEnvelopedMessage(cryptCert);
            var decryptHandler = new DecryptEnvelopedMessage();

            // small case
            var dak = getContent();

            var x = encryptHandler.encrypt(Encoding.Unicode.GetBytes(dak));
            var x1 = Encoding.Unicode.GetString(x);
            var y = decryptHandler.decrypt(Encoding.Unicode.GetBytes(x1));
            var y1 = Encoding.Unicode.GetString(y);
            Assert.AreEqual<string>(dak, y1);


            //huge case
            dak = getContent(true);

            x = encryptHandler.encrypt(Encoding.Unicode.GetBytes(dak));
            x1 = Encoding.Unicode.GetString(x);
            y = decryptHandler.decrypt(Encoding.Unicode.GetBytes(x1));
            y1 = Encoding.Unicode.GetString(y);
            Assert.AreEqual<string>(dak, y1);

        }

        private string getContent(bool isBig = false)
        {
            if (!isBig) return "dei fucker";

            var stBytes = System.IO.File.ReadAllBytes(@"D:\thakali\Scribble\files for test\googleDump.txt");
            var st = getStringFromBytes(stBytes);
            return st;
        }

        [TestMethod]
        public void TestScribbleEncHandler()
        {
            var machineCertStore = new X509Store(StoreLocation.LocalMachine);
            machineCertStore.Open(OpenFlags.ReadOnly);
            var certs = machineCertStore.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
            var cryptCert = certs[0];
            machineCertStore.Close();

            var cryptBro = new ScribbleCryptographyHandler(cryptCert);

            var w = getContent();
            var x = cryptBro.GetEncryptedString(w);
            var y = cryptBro.GetDecryptedString(x);
            Assert.AreEqual(w,y);

            var cryptBroDecrypt = new ScribbleCryptographyHandler();
            y =
                cryptBroDecrypt.GetDecryptedString(
                    "MIIBvAYJKoZIhvcNAQcDoIIBrTCCAakCAQAxggFlMIIBYQIBADBJMDUxMzAxBgNVBAMTKktBUlRISU1VLUxFTk9WTy5mYXJlYXN0LmNvcnAubWljcm9zb2Z0LmNvbQIQaWS6+2JgsYtHPz99I8AKozANBgkqhkiG9w0BAQEFAASCAQChNV8J1A8ySHq/RuH5yqwK7WwDBi08hKSnsud0tcQv/Q/0AMgd714WROXoYkpJMdqHRhwPfAJsa0LSoVq3F0Of7/KD40q7H7O9SGGDCNNFQx8FJ1IfDHwA6BtP7ekRrZ6w3W7X1bVlpxjdT7X0wrXBiiHQWaCLQZkQjCea+zpjbGw0+pRZGhQnGGsutldoEBroRbHQgTNu05/CdetrKVnohTZJrawcwZ1d4uSoWXY/eUjLVf8SFuB5lNZyCcSISt2NTB91HYewVtroHINt9akUR7FsYlC41QNQtBr5GVn42odh3TpjcziV8hCkYOvQIZBL+0jyoCcZdommbCnneAe0MDsGCSqGSIb3DQEHATAUBggqhkiG9w0DBwQIsA/h4MuoUsSAGAkXTSEAOV/QF77ONiwUH2VqtGQFAnuKaw==");

            Assert.AreEqual(y,w);
        }
    }
}
