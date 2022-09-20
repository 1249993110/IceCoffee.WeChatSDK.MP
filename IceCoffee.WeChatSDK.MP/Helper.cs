using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace IceCoffee.WeChatSDK.MP
{
    public static class Helper
    {
        /// <summary>
        /// 进行Sha1加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Sha1Encrypt(string input)
        {
            var pwBytes = Encoding.UTF8.GetBytes(input);
            using var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(pwBytes);
            var hex = new StringBuilder();

            foreach (var b in hash)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce, string token)
        {
            if (string.IsNullOrEmpty(signature) ||
                string.IsNullOrEmpty(timestamp) ||
                string.IsNullOrEmpty(nonce))
            {
                return false;
            }

            string[] strParameters = new string[] { token, timestamp, nonce };
            string strSignature = GenerateSignature(strParameters);
            return strSignature == signature;
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="array"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GenerateSignature(string[] array, string separator = "")
        {
            // 排序数组
            Array.Sort(array);
            return Sha1Encrypt(string.Join(separator, array));
        }
    }
}
