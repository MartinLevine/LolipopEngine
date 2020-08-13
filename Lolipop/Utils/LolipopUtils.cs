using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lolipop.Utils
{
    public class LolipopUtils
    {

        /// <summary>
        /// 往字符串数组相邻两个元素之间插入指定字符
        /// </summary>
        /// <param name="chr">需要插入的字符</param>
        /// <param name="arr">字符串数组元素</param>
        /// <returns>返回插入字符后的字符串</returns>
        public static string Join(char chr, string[] arr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in arr)
            {
                sb.Append(chr);
                sb.Append(str);
            }
            return sb.ToString().Substring(1);
        }

        /// <summary>
        /// 往字符串数组相邻两个元素之间插入指定字符串
        /// </summary>
        /// <param name="chr">需要插入的字符串</param>
        /// <param name="arr">字符串数组元素</param>
        /// <returns>返回插入字符后的字符串</returns>
        public static string Join(string chr, string[] arr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in arr)
            {
                sb.Append(chr);
                sb.Append(str);
            }
            return sb.ToString().Substring(chr.Length);
        }

        public static void Debug(string code, string content)
        {
            Console.WriteLine($"===================={ code }====================");
            Console.WriteLine(content);
        }

    }
}
