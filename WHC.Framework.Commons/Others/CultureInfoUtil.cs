﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 系统语言环境辅助类
    /// </summary>
    public class CultureInfoUtil
    {
        /// <summary>
        /// 初始化语言环境
        /// </summary>
        public static void InitializeCulture()
        {
            string language = LoadLanguage();

            if (!string.IsNullOrEmpty(language))
            {
                CultureInfo culture = new CultureInfo(language);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        /// <summary>
        /// 加载语言
        /// </summary>
        /// <returns></returns>
        public static string LoadLanguage()
        {
            const string key = "language";
            string language = RegistryHelper.GetValue(key);

            if (string.IsNullOrEmpty(language))
            {
                //如果用户未设置语言，如果当前系统为中文，那么显示中文，否则显示英文
                if (Thread.CurrentThread.CurrentCulture.Name.IndexOf("CN", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    language = "zh-CN";
                }
                else
                {
                    language = "en-US"; //其他语言默认为英文
                }
            }
            return language;
        }

        /// <summary>
        /// Region信息（中文）:
        ///       Name:                            CN
        ///       DisplayName:                     中华人民共和国
        ///       EnglishName:                     People's Republic of China
        ///       IsMetric:                        True
        ///       ThreeLetterISORegionName:        CHN
        ///       ThreeLetterWindowsRegionName: CHN
        ///       TwoLetterISORegionName:          CN
        ///       CurrencySymbol:                  ￥
        ///       ISOCurrencySymbol:               CNY
        /// </summary>
        public static RegionInfo CurrentRegion
        {
            get
            {
                return RegionInfo.CurrentRegion;
            }
        }
    }
}
