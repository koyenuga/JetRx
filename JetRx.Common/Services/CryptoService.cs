using AzureEncryptionExtensions.Providers;
using JetRx.Common.Data.SqlServer;
using JetRx.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace JetRx.Common.Services
{
    public class CryptoService
    {
        public static string ComputeHash(string password, byte[] saltBytes)                           
        {

            if (saltBytes == null)
            {
                int minSaltSize = 4;
                int maxSaltSize = 8;
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);
                saltBytes = new byte[saltSize];

                RNGCryptoServiceProvider rngProvider = new RNGCryptoServiceProvider();
                rngProvider.GetNonZeroBytes(saltBytes);

            }
          
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
           
            byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];
           
            for (int i = 0; i < passwordBytes.Length; i++)
                passwordWithSaltBytes[i] = passwordBytes[i];

            
            for (int i = 0; i < saltBytes.Length; i++)
                passwordWithSaltBytes[passwordBytes.Length + i] = saltBytes[i];

      
            HashAlgorithm hash = new SHA512Managed();
            byte[] hashBytes = hash.ComputeHash(passwordWithSaltBytes);

            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

         
            return hashValue;
        }

        public static bool VerifyHash(string password,string hashValue)
        {
          
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

           
            int hashSizeInBits, hashSizeInBytes;
            hashSizeInBits = 512;
            
            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            
            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

           
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

           
            string expectedHashString = ComputeHash(password, saltBytes);

           
            return (hashValue == expectedHashString);
        }

        public static void CacheEncryptionKey()
        {


            var EcryptKey = HttpContext.Current.Cache["EnCode"] as ApplicationConfig;

            if (EcryptKey == null)
            {
                using (var context = new JetRxContext())
                {
                    EcryptKey = context.ConfigSettings.FirstOrDefault(c => c.Key == "EnCode");

                    if (EcryptKey == null)
                    {
                        var provider = new AsymmetricBlobCryptoProvider();
                        EcryptKey = new ApplicationConfig();
                        EcryptKey.Key = "EnCode";
                        EcryptKey.Value = provider.ToKeyFileString();
                        context.ConfigSettings.Add(EcryptKey);
                        context.SaveChanges();

                    }

                    HttpContext.Current.Cache.Add("EnCode", EcryptKey, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                }
            }


        }

        public static string GetEncryptionKey()
        {
            
            var EcryptKey = HttpContext.Current.Cache["EnCode"] as ApplicationConfig;

            return EcryptKey.Value;

        }
    }
}

