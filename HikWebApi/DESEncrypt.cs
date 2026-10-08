using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HikWebApi
{

    public static class DESEncrypt
    {
        private const string DefaultKey = "Beg38NWSFpKrqF4D";

        // Java Charset.forName("GBK")
        private static readonly Encoding DefaultEncoding =
            Encoding.GetEncoding(936);

        public static string Encrypt(string text)
        {
            return Encrypt(text, DefaultKey);
        }

        public static string Decrypt(string text)
        {
            return Decrypt(text, DefaultKey);
        }

        public static string Encrypt(string text, string key)
        {
            if (text == null)
            {
                return null;
            }

            byte[] desKey = GetKey(key);

            byte[] input =
                DefaultEncoding.GetBytes(text);

            using (DESCryptoServiceProvider des =
                   new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;

                des.Key = desKey;
                des.IV = desKey;

                using (ICryptoTransform encryptor =
                       des.CreateEncryptor())
                using (MemoryStream ms =
                       new MemoryStream())
                using (CryptoStream cs =
                       new CryptoStream(
                           ms,
                           encryptor,
                           CryptoStreamMode.Write))
                {
                    cs.Write(
                        input,
                        0,
                        input.Length);

                    cs.FlushFinalBlock();

                    return BytesToHex(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string text, string key)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            byte[] desKey = GetKey(key);

            byte[] input =
                HexToBytes(text);

            using (DESCryptoServiceProvider des =
                   new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;

                des.Key = desKey;
                des.IV = desKey;

                using (ICryptoTransform decryptor =
                       des.CreateDecryptor())
                using (MemoryStream ms =
                       new MemoryStream())
                using (CryptoStream cs =
                       new CryptoStream(
                           ms,
                           decryptor,
                           CryptoStreamMode.Write))
                {
                    cs.Write(
                        input,
                        0,
                        input.Length);

                    cs.FlushFinalBlock();

                    return DefaultEncoding.GetString(
                        ms.ToArray());
                }
            }
        }

        private static byte[] GetKey(string sourceKey)
        {
            byte[] input =
                Encoding.UTF8.GetBytes(sourceKey);

            byte[] hash;

            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(input);
            }

            // Java:
            // bytesToHex(digest).substring(0, 8).toLowerCase()
            StringBuilder md5Hex =
                new StringBuilder(32);

            foreach (byte b in hash)
            {
                md5Hex.Append(
                    b.ToString("x2"));
            }

            string key =
                md5Hex.ToString()
                      .Substring(0, 8);

            // Java:
            // key.getBytes(StandardCharsets.US_ASCII)
            return Encoding.ASCII.GetBytes(key);
        }

        private static string BytesToHex(byte[] bytes)
        {
            StringBuilder sb =
                new StringBuilder(bytes.Length * 2);

            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        private static byte[] HexToBytes(string hex)
        {
            if ((hex.Length & 1) != 0)
            {
                throw new ArgumentException(
                    "HEX字符串长度必须是偶数");
            }

            byte[] result =
                new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length; i += 2)
            {
                result[i / 2] =
                    Convert.ToByte(
                        hex.Substring(i, 2),
                        16);
            }

            return result;
        }
    }
}

