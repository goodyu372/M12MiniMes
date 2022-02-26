﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// QQ密码加密操作辅助类
    /// </summary>
    public class QQEncryptUtil
    {
        /// <summary>
        /// QQ根据密码及验证码对数据进行加密
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <param name="verifyCode">验证码</param>
        /// <returns></returns>
        public static string EncodePasswordWithVerifyCode(string password, string verifyCode)
        {
            return MD5(MD5_3(password) + verifyCode.ToUpper());
        }

        static string MD5_3(string arg)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(arg);
            buffer = md5.ComputeHash(buffer);
            buffer = md5.ComputeHash(buffer);
            buffer = md5.ComputeHash(buffer);

            return BitConverter.ToString(buffer).Replace("-", "").ToUpper();
        }
        static string MD5(string arg)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(arg);
            buffer = md5.ComputeHash(buffer);

            return BitConverter.ToString(buffer).Replace("-", "").ToUpper();
        }
    }
}
