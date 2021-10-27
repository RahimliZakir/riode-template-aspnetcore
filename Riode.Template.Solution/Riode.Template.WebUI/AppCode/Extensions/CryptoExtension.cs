using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.AppCode.Extensions
{
    public static partial class CryptoExtension
    {
        const string securityKey = "riode-aspnetcore-hash";

        public static string ToHashMD5(this string value)
        {
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(value);

                buffer = provider.ComputeHash(buffer);

                StringBuilder sb = new StringBuilder();

                foreach (byte item in buffer)
                {
                    sb.Append(item.ToString("x2"));
                }

                return sb.ToString();

                //return string.Join("", buffer.Select(b => b.ToString("x2")));
            }
        }

        public static string Encrypt(this string value, string key)
        {
            try
            {
                using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
                using (MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] keyBuffer = md5Provider.ComputeHash(Encoding.UTF8.GetBytes($"#{key}!"));
                    byte[] IVBuffer = md5Provider.ComputeHash(Encoding.UTF8.GetBytes($"@{key}?"));


                    ICryptoTransform transform = provider.CreateEncryptor(keyBuffer, IVBuffer);

                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        byte[] valueBuffer = Encoding.UTF8.GetBytes(value);
                        cs.Write(valueBuffer, 0, valueBuffer.Length);
                        cs.FlushFinalBlock();

                        ms.Position = 0;
                        byte[] result = new byte[ms.Length];

                        ms.Read(result, 0, result.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Encrypt(this string value)
        {
            return Encrypt(value, securityKey.ToHashMD5());
        }

        public static string Decrypt(this string value, string key)
        {
            try
            {
                using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
                using (MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] keyBuffer = md5Provider.ComputeHash(Encoding.UTF8.GetBytes($"#{key}!"));
                    byte[] IVBuffer = md5Provider.ComputeHash(Encoding.UTF8.GetBytes($"@{key}?"));


                    ICryptoTransform transform = provider.CreateDecryptor(keyBuffer, IVBuffer);

                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        byte[] valueBuffer = Convert.FromBase64String(value);

                        cs.Write(valueBuffer, 0, valueBuffer.Length);
                        cs.FlushFinalBlock();

                        ms.Position = 0;
                        byte[] result = new byte[ms.Length];

                        ms.Read(result, 0, result.Length);

                        return Encoding.UTF8.GetString(result);
                    }
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Decrypt(this string value)
        {
            return Decrypt(value, securityKey.ToHashMD5());
        }
    }
}
