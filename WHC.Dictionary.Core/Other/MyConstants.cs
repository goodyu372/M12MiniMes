using System;
using System.Collections.Generic;
using System.Text;
using WHC.Framework.Commons;
using System.IO;

namespace WHC.Dictionary
{
    public class MyConstants
    {
        private static string m_ConfigFile = "";

        /// <summary>
        /// 默认的配置文件路径
        /// </summary>
        public static string ConfigFile
        {
            get
            {
                if (string.IsNullOrEmpty(m_ConfigFile))
                {
                    string searchPattern = "*.exe.config";
                    string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, searchPattern, SearchOption.TopDirectoryOnly);
                    foreach (string filePath in files)
                    {
                        if (!filePath.Contains(".vshost"))
                        {
                            m_ConfigFile = filePath;
                            break;
                        }
                    }
                }
                return m_ConfigFile;
            }
            set
            {
                m_ConfigFile = value;
            }
        }

        private static string m_License = "";

        /// <summary>
        /// 授权用户的授权码
        /// </summary>
        public static string License
        {
            get
            {
                if (string.IsNullOrEmpty(m_License))
                {
                    AppConfig config = new AppConfig(MyConstants.ConfigFile);
                    string SecurityLicense = config.AppConfigGet("DictionaryLicense");
                    return SecurityLicense;
                }
                else
                {
                    return m_License;
                }
            }
            set { MyConstants.m_License = value; }
        }
    }
}
