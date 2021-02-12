using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wuyu.Tool.Common
{
    public static class HashHelp
    {
        private static MD5 md5Hasher;

        private static MD5 GetMD5Hasher()
        {
            if (md5Hasher == null)
            {
                md5Hasher = MD5.Create();
            }
            return md5Hasher;
        }

        /// <summary>
        /// 对字符串进行MD5加密,返回的字符串长度为32
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return MD5Encrypt(Encoding.Unicode.GetBytes(s));
        }

        /// <summary>
        /// 对字符串进行MD5加密,返回的字符串长度为32
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MD5Encrypt(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            byte[] hashBytes = GetMD5Hasher().ComputeHash(bytes);
            string result = BitConverter.ToString(hashBytes);

            return result.Replace("-", "").ToUpper();
        }
    }
}
