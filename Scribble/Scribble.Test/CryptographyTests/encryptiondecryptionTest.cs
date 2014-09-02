using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScribbleBL.PrivacyHandler;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace Scribble.Test.CryptographyTests
{
    [TestClass]
    public class encryptiondecryptionTest
    {
        [TestMethod]
        public void EncryptionDecryptionTest()
        {
            string[] testStrings = new string[] { "this is karthikeyan",
                                                  "#include<stdio.h>int main(){int x=1;}" };

            try
            {

                foreach (var testString in testStrings)
                {
                    var st = CryptographyHelper.GetEncryptedString(testString);
                    var st1 = CryptographyHelper.GetDecryptedString(st);
                    Assert.AreEqual(st1, testString);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        [TestMethod]
        public void testCodePlexCode()
        {

            var machineCertStore = new X509Store(StoreLocation.LocalMachine);
                machineCertStore.Open(OpenFlags.ReadOnly);
                var certs = machineCertStore.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
                var cryptCert = certs[0];
                machineCertStore.Close();

                var x = new X509Cryptography(cryptCert,true);
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
            try
            {
                var stBytes = System.IO.File.ReadAllBytes(@"D:\thakali\Scribble\files for test\googleDump.txt");
                var st = getStringFromBytes(stBytes);
                
                var machineCertStore = new X509Store(StoreLocation.LocalMachine);
                machineCertStore.Open(OpenFlags.ReadOnly);
                var certs = machineCertStore.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
                var cryptCert = certs[0];
                machineCertStore.Close();

                var x = new X509Cryptography(cryptCert, true);
                var y = x.EncryptString(st);
                var z = x.DecryptString(y);
                Assert.AreEqual(st,z);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation(ex.Message);
                throw ex;
            }
        }
    }
}
