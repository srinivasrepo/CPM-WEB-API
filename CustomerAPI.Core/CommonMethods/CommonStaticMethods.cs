using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CustomerAPI.Core.CommonMethods
{
    public class CommonStaticMethods
    {
        public static string Serialize<T>(T t)
        {
            string XMLString = string.Empty;

            if (t == null)
                return XMLString;

            XmlSerializer x = null;

            try
            {
                using (var stringwriter = new System.IO.StringWriter())
                {
                    x = new XmlSerializer(typeof(T));

                    x.Serialize(stringwriter, t);

                    XMLString = stringwriter.ToString();
                }
            }
            catch
            {
                XMLString = string.Empty;
            }
            finally
            {
                x = null;
            }

            return XMLString;
        }

        public string decryptQueryString(string strQueryString)
        {
            return Decrypt(strQueryString, "!#$a54?3");
        }

        public static string Encrypt(string strQueryString)
        {
            return Encrypt(strQueryString, "!#$a54?3");
        }

        public static T Decrypt<T>(string strQueryString)
        {
            T target = default(T);
            try
            {
                target = (T)Convert.ChangeType(Decrypt(strQueryString, "!#$a54?3"), typeof(T));
            }
            catch { }

            return target;
        }

        protected static string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            stringToDecrypt = stringToDecrypt.Replace(" ", "+");

            byte[] inputByteArray = new byte[stringToDecrypt.Length];
            try
            {
                key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8; return encoding.GetString(ms.ToArray());
            }

            catch (System.Exception ex)
            {
                if (ex.Message == "Invalid length for a Base-64 char array.")
                {
                    stringToDecrypt = stringToDecrypt + "+";
                    return Decrypt(stringToDecrypt, sEncryptionKey);
                }
                else
                    throw ex;
            }
        }

        protected static string Encrypt(string stringToEncrypt, string sEncryptionKey)
        {
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length)

            try
            {
                key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray()).Replace("+", " ");
            }

            catch (System.Exception ex)
            {

                throw ex;
            }

        }
    }
}
