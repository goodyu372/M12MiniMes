using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Framework.ControlUtil;
using WHC.Dictionary.Entity;

namespace WHC.Dictionary.IDAL
{
    /// <summary>
    /// 用户参数配置
    /// </summary>
	public interface IUserParameter : IBaseDAL<UserParameterInfo>
	{               
        /// <summary>
        /// 保存配置（插入或更新）到数据库
        /// </summary>
        /// <param name="info">信息对象</param>
        /// <returns></returns>
        bool SaveParamater(UserParameterInfo info);
                      
        /// <summary>
        /// 根据类名称和用户标识获取参数配置内容
        /// </summary>
        /// <param name="name">类名称</param>
        /// <param name="creator">用户标识</param>
        /// <returns></returns>
        string LoadParameter(string name, string creator = null);
    }
}