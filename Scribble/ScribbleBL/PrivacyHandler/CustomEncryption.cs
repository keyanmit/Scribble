    #region

    using System;
    using System.Collections;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    #endregion

    
namespace ScribbleBL.PrivacyHandler
{
        public class X509Cryptography
        {
            public readonly X509Certificate2 _CertFile;
            public readonly bool _FOaep;

            public X509Cryptography(X509Certificate2 certFile, bool fOAEP)
            {
                if (certFile != null) _CertFile = certFile;
                else throw new SystemException("Certificate is null");
                _FOaep = fOAEP;
            }

            public X509Cryptography(X509Certificate2 certFile)
            {
                if (certFile != null) _CertFile = certFile;
                else throw new SystemException("Certificate is null");
                _FOaep = false;
            }

            public static byte[] ReadStream(Stream input)
            {
                using (var ms = new MemoryStream())
                {
                    input.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            #region Encryption

            public string EncryptStringTest(string inputString)
            {
                // TODO: Add Proper Exception Handlers
                var rsaCryptoServiceProvider = (RSACryptoServiceProvider)_CertFile.PublicKey.Key;
                int keySize = rsaCryptoServiceProvider.KeySize / 8;
                byte[] bytes = Encoding.UTF32.GetBytes(inputString);
                // The hash function in use by the .NET RSACryptoServiceProvider here 
                // is SHA1
                // int maxLength = ( keySize ) - 2 - 
                //              ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
                int maxLength = keySize - 42;
                int dataLength = bytes.Length;
                int iterations = dataLength / maxLength;
                var stringBuilder = new StringBuilder();
                for (int i = 0; i <= iterations; i++)
                {
                    var tempBytes = new byte[
                        (dataLength - maxLength * i > maxLength)
                            ? maxLength
                            : dataLength - maxLength * i];
                    Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                                     tempBytes.Length);
                    byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes, _FOaep);

                    // Be aware the RSACryptoServiceProvider reverses the order of 
                    // encrypted bytes. It does this after encryption and before 
                    // decryption. If you do not require compatibility with Microsoft 
                    // Cryptographic API (CAPI) and/or other vendors. Comment out the 
                    // next line and the corresponding one in the DecryptString function.
                    Array.Reverse(encryptedBytes);
                    // Why convert to base 64?
                    // Because it is the largest power-of-two base printable using only 
                    // ASCII characters
                    stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
                }
                return stringBuilder.ToString();
            }

            public string DecryptStringTest(string inputString)
            {
                // TODO: Add Proper Exception Handlers
                var rsaCryptoServiceProvider = (RSACryptoServiceProvider)_CertFile.PrivateKey;
                int keySize = rsaCryptoServiceProvider.KeySize / 8;
                int base64BlockSize = ((keySize / 8) % 3 != 0) ? (((keySize / 8) / 3) * 4) + 4 : ((keySize / 8) / 3) * 4;

                int iterations = inputString.Length / base64BlockSize;
                var arrayList = new ArrayList();
                for (int i = 0; i < iterations; i++)
                {
                    byte[] encryptedBytes = Convert.FromBase64String(
                        inputString.Substring(base64BlockSize * i, base64BlockSize));
                    // Be aware the RSACryptoServiceProvider reverses the order of 
                    // encrypted bytes after encryption and before decryption.
                    // If you do not require compatibility with Microsoft Cryptographic 
                    // API (CAPI) and/or other vendors.
                    // Comment out the next line and the corresponding one in the 
                    // EncryptString function.
                    Array.Reverse(encryptedBytes);
                    arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, _FOaep));
                }
                return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
            }


            public string EncryptString(string inputString)
            {
                try
                {
                    // TODO: Add Proper Exception Handlers
                    var rsaCryptoServiceProvider = (RSACryptoServiceProvider)_CertFile.PublicKey.Key;

                    int keySize = rsaCryptoServiceProvider.KeySize / 8;
                    byte[] bytes = Encoding.Unicode.GetBytes(inputString);
                    // The hash function in use by the .NET RSACryptoServiceProvider here 
                    // is SHA1
                    // int maxLength = ( keySize ) - 2 - 
                    //              ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
                    int maxLength = keySize - 42;
                    int dataLength = bytes.Length;
                    int iterations = dataLength / maxLength;
                    var stringBuilder = new StringBuilder();
                    for (int i = 0; i <= iterations; i++)
                    {
                        var tempBytes = new byte[
                            (dataLength - maxLength * i > maxLength)
                                ? maxLength
                                : dataLength - maxLength * i];
                        Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                                         tempBytes.Length);
                        byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes,
                                                                                 true);
                        // Be aware the RSACryptoServiceProvider reverses the order of 
                        // encrypted bytes. It does this after encryption and before 
                        // decryption. If you do not require compatibility with Microsoft 
                        // Cryptographic API (CAPI) and/or other vendors. Comment out the 
                        // next line and the corresponding one in the DecryptString function.
                        Array.Reverse(encryptedBytes);
                        // Why convert to base 64?
                        // Because it is the largest power-of-two base printable using only 
                        // ASCII characters
                        stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
                    }
                    return stringBuilder.ToString();
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }

            public string DecryptString(string inputString)
            {
                var tmpString = string.Empty;

                try
                {
                    // TODO: Add Proper Exception Handlers
                    var rsaCryptoServiceProvider
                        = (RSACryptoServiceProvider)_CertFile.PrivateKey;
                    int dwKeySize = rsaCryptoServiceProvider.KeySize;
                    int base64BlockSize = ((dwKeySize / 8) % 3 != 0)
                                              ? (((dwKeySize / 8) / 3) * 4) + 4
                                              : ((dwKeySize / 8) / 3) * 4;
                    int iterations = inputString.Length / base64BlockSize;
                    var arrayList = new ArrayList();
                    for (int i = 0; i < iterations; i++)
                    {
                        byte[] encryptedBytes = Convert.FromBase64String(
                            inputString.Substring(base64BlockSize * i, base64BlockSize));
                        // Be aware the RSACryptoServiceProvider reverses the order of 
                        // encrypted bytes after encryption and before decryption.
                        // If you do not require compatibility with Microsoft Cryptographic 
                        // API (CAPI) and/or other vendors.
                        // Comment out the next line and the corresponding one in the 
                        // EncryptString function.
                        Array.Reverse(encryptedBytes);
                        var tmp = rsaCryptoServiceProvider.Decrypt(
                            encryptedBytes, true);
                        arrayList.AddRange(tmp);
                        tmpString += Encoding.Unicode.GetString(tmp);
                    }
                    //return retString;
                    //return Encoding.ASCII.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
                    return tmpString;
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }

            #endregion            

        }
}
