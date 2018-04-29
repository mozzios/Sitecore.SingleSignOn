using Sitecore.SingleSignOn.Utility.Enums;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sitecore.SingleSignOn.Utility.Security
{
    public class EncryptionHelper : IEncryptionHelper
    {
        public string EncryptString(string text, EncryptionTypeEnums type)
        {
            try
            {
                string password = GetPassword(type);

                byte[] baPwd = Encoding.UTF8.GetBytes(password);

                byte[] baPwdHash = SHA256.Create().ComputeHash(baPwd);

                byte[] baText = Encoding.UTF8.GetBytes(text);

                byte[] baSalt = GetRandomBytes();
                byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

                for (int i = 0; i < baSalt.Length; i++)
                    baEncrypted[i] = baSalt[i];
                for (int i = 0; i < baText.Length; i++)
                    baEncrypted[i + baSalt.Length] = baText[i];

                baEncrypted = AES_Encrypt(baEncrypted, baPwdHash);

                string result = Convert.ToBase64String(baEncrypted);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
        }

        public string DecryptString(string text, EncryptionTypeEnums type)
        {
            try
            {
                string password = GetPassword(type);

                byte[] baPwd = Encoding.UTF8.GetBytes(password);

                byte[] baPwdHash = SHA256.Create().ComputeHash(baPwd);

                byte[] baText = Convert.FromBase64String(text);

                byte[] baDecrypted = AES_Decrypt(baText, baPwdHash);

                int saltLength = GetSaltLength();
                byte[] baResult = new byte[baDecrypted.Length - saltLength];
                for (int i = 0; i < baResult.Length; i++)
                    baResult[i] = baDecrypted[i + saltLength];

                string result = Encoding.UTF8.GetString(baResult);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string EncryptText(string input, string password)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        private string DecryptText(string input, string password)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes;

            byte[] saltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes;

            byte[] saltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        private static byte[] GetRandomBytes()
        {
            int saltLength = GetSaltLength();
            byte[] ba = new byte[saltLength];
            RandomNumberGenerator.Create().GetBytes(ba);
            return ba;
        }

        private static int GetSaltLength()
        {
            return 8;
        }

        private string GetPassword(EncryptionTypeEnums type)
        {
            switch (type)
            {
                case EncryptionTypeEnums.Member:
                    return EncryptionPassword.MemberEncryptionPassword;
                default:
                    throw new Exception("Encryption password not available");
            }
        }
    }
}
