using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace CommonLibrary
{
    public static class LibUtil
    {
        /// <summary>
        /// 文字列をMD5ハッシュに変換します。
        /// </summary>
        /// <param name="Texts">対象文字列</param>
        /// <returns>MD5ハッシュ値</returns>
        public static string ToMD5(this string Texts)
        {
            byte[] hashCode;
            byte[] originalData;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            StringBuilder str = new StringBuilder();

            originalData = Encoding.Default.GetBytes(Texts);
            hashCode = md5.ComputeHash(originalData);

            foreach (byte byteData in hashCode)
            {
                str.Append(byteData.ToString("x2"));
            }

            return str.ToString();
        }

        /// <summary>
        /// 文字列を全角に変換します。
        /// </summary>
        /// <param name="Texts">対象文字列</param>
        /// <returns>全角にされた文字列</returns>
        public static string ToWide(this string Texts)
        {
            return Strings.StrConv(Texts, VbStrConv.Wide, 0);
        }
    }
}
