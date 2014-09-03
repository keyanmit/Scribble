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

        private string getContent(bool isBig = false)
        {
            if (!isBig) return "dei fucker";

            var stBytes = System.IO.File.ReadAllBytes(@"D:\thakali\Scribble\files for test\googleDump.txt");
            var st = getStringFromBytes(stBytes);
            return st;
        }
    }
}
