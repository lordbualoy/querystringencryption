using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QueryStringEncryption.Helper
{
    public class Encryption
    {
        public static string keyString = "E546C8DF278CD5931069B522E695D4F2";
        //http://mikaelkoskinen.net/post/encrypt-decrypt-string-asp-net-core

        public static string Encrypt(bool? bo)
        {
            if (bo.HasValue)
            {
                return Encrypt(bo.Value.ToString());
            }
            return null;
        }

        public static string Encrypt(int? str)
        {
            if (str.HasValue)
            {
                return Encrypt(str.Value.ToString());
            }
            return null;
        }

        public static string Encrypt(decimal? str)
        {
            if (str.HasValue)
            {
                return Encrypt(str.Value.ToString());
            }
            return null;
        }

        public static string Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = text.Trim();
            var key = Encoding.UTF8.GetBytes(keyString);
            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }
                        var iv = aesAlg.IV;
                        var decryptedContent = msEncrypt.ToArray();
                        var result = new byte[iv.Length + decryptedContent.Length];
                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);
                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            try
            {
                cipherText = cipherText.Trim();
                var fullCipher = Convert.FromBase64String(cipherText);
                var iv = new byte[16];
                var cipher = new byte[16];
                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
                var key = Encoding.UTF8.GetBytes(keyString);
                using (var aesAlg = Aes.Create())
                {
                    using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                    {
                        string result;
                        using (var msDecrypt = new MemoryStream(cipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static int DecryptToInt(string str)
        {
            return int.Parse(str);
        }
    }
}
