using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WHC.Framework.Language
{
    /// <summary>
    /// 提供对字符串的一些特殊处理
    /// </summary>
    internal class StringUtil
    {
        /// <summary>
        /// 将字符串转换为合适的大小写
        /// </summary>
        /// <param name="s">操作的字符串</param>
        /// <returns></returns>
        public static string ToProperCase(string s)
        {
            string revised = "";
            if (s.Length > 0)
            {
                if (s.IndexOf(" ") > 0)
                {
                    revised = Strings.StrConv(s, VbStrConv.ProperCase, 1033);
                }
                else
                {
                    string firstLetter = s.Substring(0, 1).ToUpper(new CultureInfo("en-US"));
                    revised = firstLetter + s.Substring(1, s.Length - 1);
                }
            }
            return revised;
        }
    }
}
