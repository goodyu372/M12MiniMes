﻿using System.Globalization;
namespace WHC.Framework.Commons
{    
    /// <summary>
    /// 十六进制帮助类
    /// </summary>
    public static class HexHelper
    {
        #region Methods
        
        /// <summary>
        ///将uint转换成十六进制字符串
        /// </summary>
        /// <param name="number">uint</param>
        /// <returns>十六进制字符串</returns>
        public static string ToHexString(uint number)
        {
            string _hex = string.Format("{0:X}", number);
            
            if(_hex.Length % 2 != 0)
                _hex = string.Format("0{0}", _hex);
                
            return _hex;
        }
        
        /// <summary>
        ///将ushort转换成十六进制字符串
        /// </summary>
        /// <param name="number">ushort</param>
        /// <returns>十六进制字符串</returns>
        public static string ToHexString(ushort number)
        {
            string _hex = string.Format("{0:X}", number);
            
            if(_hex.Length % 2 != 0)
                _hex = string.Format("0{0}", _hex);
                
            return _hex;
        }
        
        /// <summary>
        ///将ulong转换成十六进制字符串
        /// </summary>
        /// <param name="number">ulong</param>
        /// <returns>十六进制字符串</returns>
        public static string ToHexString(ulong number)
        {
            string _hex = string.Format("{0:X}", number);
            
            if(_hex.Length % 2 != 0)
                _hex = string.Format("0{0}", _hex);
                
            return _hex;
        }
        
        /// <summary>
        /// 将十六进制字符串转换UINT
        /// </summary>
        /// <param name="hexString">十六进制字符串</param>
        /// <returns>INT</returns>
        public static uint ToUInt(string hexString)
        {
            return uint.Parse(hexString, NumberStyles.AllowHexSpecifier);
        }
        
        /// <summary>
        /// 将十六进制字符串转换ToULong
        /// </summary>
        /// <param name="hexString">十六进制字符串</param>
        /// <returns>ulong</returns>
        public static ulong ToULong(string hexString)
        {
            return ulong.Parse(hexString, NumberStyles.AllowHexSpecifier);
        }
        
        /// <summary>
        /// 将十六进制字符串转换ushort
        /// </summary>
        /// <param name="hexString">十六进制字符串</param>
        /// <returns>ushort</returns>
        public static ushort ToUShort(string hexString)
        {
            return ushort.Parse(hexString, NumberStyles.AllowHexSpecifier);
        }
        
        #endregion Methods
    }
}