using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WHC.Dictionary.Entity;
using WHC.Dictionary.IDAL;

namespace WHC.Dictionary.DALAccess
{
    /// <summary>
    /// 应用程序渠道信息
    /// </summary>
    public class AppChannel : BaseDALAccess<AppChannelInfo>, IAppChannel
	{
		#region 对象实例及构造函数

		public static AppChannel Instance
		{
			get
			{
				return new AppChannel();
			}
		}
		public AppChannel() : base("TB_AppChannel","ID")
		{
		}

        #endregion

        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// </summary>
        /// <param name="dataReader">有效的DataReader对象</param>
        /// <returns>实体类对象</returns>
        protected override AppChannelInfo DataReaderToEntity(IDataReader dataReader)
		{
			AppChannelInfo info = new AppChannelInfo();
			SmartDataReader reader = new SmartDataReader(dataReader);
			
			info.ID = reader.GetString("ID");
			info.AppId = reader.GetString("AppId");
			info.AppSecret = reader.GetString("AppSecret");
			info.Channel = reader.GetString("Channel");
			info.ChannelName = reader.GetString("ChannelName");
			info.Note = reader.GetString("Note");
			
			return info;
		}

		/// <summary>
		/// 将实体对象的属性值转化为Hashtable对应的键值
		/// </summary>
		/// <param name="obj">有效的实体对象</param>
		/// <returns>包含键值映射的Hashtable</returns>
        protected override Hashtable GetHashByEntity(AppChannelInfo obj)
		{
		    AppChannelInfo info = obj as AppChannelInfo;
			Hashtable hash = new Hashtable(); 
			
			hash.Add("ID", info.ID);
 			hash.Add("AppId", info.AppId);
 			hash.Add("AppSecret", info.AppSecret);
 			hash.Add("Channel", info.Channel);
 			hash.Add("ChannelName", info.ChannelName);
 			hash.Add("Note", info.Note);
 				
			return hash;
		}

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetColumnNameAlias()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            #region 添加别名解析
            //dict.Add("ID", "编号");
            dict.Add("ID", "");
            dict.Add("AppId", "应用ID");
            dict.Add("AppSecret", "应用秘钥");
            dict.Add("Channel", "渠道类别");
            dict.Add("ChannelName", "渠道名称");
            dict.Add("Note", "备注");
            #endregion

            return dict;
        }

    }
}