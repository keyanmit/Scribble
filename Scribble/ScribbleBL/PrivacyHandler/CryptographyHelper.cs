using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace ScribbleBL.PrivacyHandler
{
    public class CryptographyHelper
    {
        private static X509Certificate2 cryptCert;
        private static RSACryptoServiceProvider encryptionProvider;
        private static RSACryptoServiceProvider decryptionProvider;

        static CryptographyHelper()
        {
            try
            {
                var machineCertStore = new X509Store(StoreLocation.LocalMachine);
                machineCertStore.Open(OpenFlags.ReadOnly);
                var certs = machineCertStore.Certificates.Find(X509FindType.FindByThumbprint, "ce51edf145eea7ed912b2b5099554f68175273c7", true);
                cryptCert = certs[0];
                machineCertStore.Close();

                encryptionProvider = new RSACryptoServiceProvider();
                encryptionProvider.FromXmlString(cryptCert.PublicKey.Key.ToXmlString(false));

                decryptionProvider = new RSACryptoServiceProvider();
                decryptionProvider = (RSACryptoServiceProvider)cryptCert.PrivateKey;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error fetching cryptography cert. " +
                                  ex.Message);
                throw ex;
            }
        }

        public static string GetEncryptedString(string data)
        {
            byte[] returnByt;
            returnByt = encryptionProvider.Encrypt(GetBytes(data), true);
            return Convert.ToBase64String(returnByt);
            return getStringFromBytes(returnByt);
        }

        public static string GetDecryptedString(string data)
        {
            var bytes = Convert.FromBase64String(data);
            var returnBytes = decryptionProvider.Decrypt(bytes, true);
            return getStringFromBytes(returnBytes);
            /*var bytes = decryptionProvider.Decrypt(GetBytes(data), true);
            return getStringFromBytes(bytes);*/
        }

        private static string getStringFromBytes(byte[] bytes)
        {
            //return UTF8Encoding.UTF8.GetString(bytes);            
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private static byte[] GetBytes(string st)
        {
            //return UTF8Encoding.UTF8.GetBytes(st);
            //byte[] tmpString = Convert.FromBase64String(st);
            //return tmpString;

            byte[] byt = new byte[st.Length * sizeof(char)];
            System.Buffer.BlockCopy(st.ToCharArray(), 0, byt, 0, byt.Length);
            return byt;
        }

        public static string EncryptBigString(string data)
        {
             string hugeEncryptedEncodedString = string.Empty;
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(data);
                var dataLenght = bytes.Length;
                int keySize = encryptionProvider.KeySize / 8;
                int maxLengthRSA = keySize - 42;
                int iterationRequired = dataLenght / maxLengthRSA;                

                for (int i = 0; i <= iterationRequired; i++)
                {
                    var bytesToEncrypt = new byte[(i==iterationRequired)? dataLenght % maxLengthRSA : maxLengthRSA];
                    Buffer.BlockCopy(bytes, i * maxLengthRSA, bytesToEncrypt, 0, (i == iterationRequired) ? dataLenght % maxLengthRSA : maxLengthRSA);
                    var encryptedBytes = encryptionProvider.Encrypt(bytesToEncrypt,true);
                    var base64EncodedString = Convert.ToBase64String(encryptedBytes);
                    hugeEncryptedEncodedString += base64EncodedString;
                }
                return hugeEncryptedEncodedString;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception while encodeing" + ex.Message);
            }
            return hugeEncryptedEncodedString;
        }

        public static string DecryptBigString(string data)
        {
            string hugeDecryptedDecodedString = string.Empty;
            try
            {
                //byte[] bytes = Encoding.Default.GetBytes(data);
                var dataLenght = data.Length;
                int keySize = decryptionProvider.KeySize / 8;
                int base64BlockSize = 344;//keySize - 42 + 42;
                int iterationRequired = dataLenght / base64BlockSize;                

                for (int i = 0; i < iterationRequired; i++)
                {
                    byte[] base64decoded = Convert.FromBase64String(data.Substring(i*base64BlockSize,base64BlockSize));
                    //var bytesToDecrypt = new byte[(i==iterationRequired)? dataLenght % base64BlockSize : base64BlockSize];
                    //Buffer.BlockCopy(bytes, i * base64BlockSize, bytesToDecrypt, 0, (i == iterationRequired) ? dataLenght % base64BlockSize : base64BlockSize);
                    //var tmp64Base = getStringFromBytes(bytesToDecrypt);
                    //var base64DecodedBytes = Convert.FromBase64String(tmp64Base);                    
                    var decryptedBytes = getStringFromBytes(decryptionProvider.Decrypt(base64decoded,true));                    
                    hugeDecryptedDecodedString += decryptedBytes;                                       
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception while encodeing" + ex.Message);
                return hugeDecryptedDecodedString;
            }
            return hugeDecryptedDecodedString;
        }

        /*
         * string hugeDecryptedDecodedString = string.Empty;
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(data);
                var dataLenght = bytes.Length;
                int keySize = decryptionProvider.KeySize / 8;
                int maxLengthRSA = 44;//keySize - 42 + 42;
                int iterationRequired = dataLenght / maxLengthRSA;                

                for (int i = 0; i < iterationRequired; i++)
                {
                    var bytesToDecrypt = new byte[(i==iterationRequired)? dataLenght % maxLengthRSA : maxLengthRSA];
                    Buffer.BlockCopy(bytes, i * maxLengthRSA, bytesToDecrypt, 0, (i == iterationRequired) ? dataLenght % maxLengthRSA : maxLengthRSA);
                    var tmp64Base = getStringFromBytes(bytesToDecrypt);
                    var base64DecodedBytes = Convert.FromBase64String(tmp64Base);                    
                    var decryptedBytes = decryptionProvider.Decrypt(base64DecodedBytes,true);                    
                    hugeDecryptedDecodedString += decryptedBytes;                                       
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception while encodeing" + ex.Message);
                return hugeDecryptedDecodedString;
            }
         */
    }
}
