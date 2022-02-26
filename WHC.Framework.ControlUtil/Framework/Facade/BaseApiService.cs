using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.ServiceModel;
using System.Reflection;

using WHC.Pager.Entity;
using System.Data.Common;
using System.Collections;
using System.Web.Security;
using WHC.Framework.Commons;
using Newtonsoft.Json;

namespace WHC.Framework.ControlUtil.Facade
{
    /// <summary>
    /// 基于Web API接口的基础API包装类
    /// </summary>
    /// <typeparam name="T">Facade接口</typeparam>
    public class BaseApiService<T> : NormalApiService, IBaseService<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseApiService()
        {            
        }

        /// <summary>
        /// 使用自定义配置
        /// </summary>
        /// <param name="configurationName">API配置项名称</param>
        /// <param name="configurationPath">配置路径</param>
        public BaseApiService(string configurationName, string configurationPath) : base(configurationName, configurationPath)
        {
        }


        #region 对象添加、修改、查询接口

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        public virtual bool Insert(T obj)
        {
            bool result = false;

            var action = "Insert";
            string url = GetPostUrlWithToken(action);
            var postData = obj.ToJson();

            var apiResult = JsonHelper<CommonResult>.ConvertJson(url, postData);
            if (apiResult != null)
            {
                result = apiResult.Success;
            }
            return result;
        }

        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns>执行操作是否成功。</returns>
        public virtual bool InsertRange(List<T> list)
        {
            bool result = false;

            var action = "InsertRange";
            string url = GetPostUrlWithToken(action);
            var postData = list.ToJson();

            var apiResult = JsonHelper<CommonResult>.ConvertJson(url, postData);
            if (apiResult != null)
            {
                result = apiResult.Success;
            }
            return result;
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        public virtual int Insert2(T obj)
        {
            int result = -1;

            var action = "Insert2";
            string url = GetPostUrlWithToken(action);
            var postData = obj.ToJson();

            var apiResult = JsonHelper<CommonResult>.ConvertJson(url, postData);
            if (apiResult != null && !string.IsNullOrEmpty(apiResult.Data1))
            {
                result = apiResult.Data1.ToInt32();
            }
            return result;
        }
                        
        /// <summary>
        /// 插入或更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool InsertUpdate(T obj, object primaryKeyValue)
        {
            bool result = false;

            var action = "InsertUpdate";
            string url = GetPostUrlWithToken(action) + string.Format("&id={0}", primaryKeyValue);
            var postData = obj.ToJson();

            var apiResult = JsonHelper<CommonResult>.ConvertJson(url, postData);
            if (apiResult != null)
            {
                result = apiResult.Success;
            }
            return result;
        }
                
        /// <summary>
        /// 如果不存在记录，则插入对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool InsertIfNew(T obj, object primaryKeyValue)
        {
            bool result = false;

            var action = "InsertIfNew";
            string url = GetPostUrlWithToken(action) + string.Format("&id={0}", primaryKeyValue);
            var postData = obj.ToJson();

            var apiResult = JsonHelper<CommonResult>.ConvertJson(url, postData);
            if (apiResult != null)
            {
                result = apiResult.Success;
            }
            return result;
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="primaryKeyValue">主键的值</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Update(T obj, object primaryKeyValue)
        {
            bool result = false;

            var action = "Update";
            string url = GetPostUrlWithToken(action) + string.Format("&id={0}", primaryKeyValue);
            var postData = obj.ToJson();

            var apiResult = JsonHelper<CommonResult>.ConvertJson(url, postData);
            if (apiResult != null)
            {
                result = apiResult.Success;
            }
            return result;
        }

                        
        /// <summary>
		/// 更新某个表一条记录(只适用于用单键)
		/// </summary>
		/// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="primaryKeyValue">主键的值</param>
        public virtual bool UpdateFields(Hashtable recordField, object primaryKeyValue)
        {
            bool result = false;

            var action = "UpdateFields";
            string url = GetPostUrlWithToken(action) + string.Format("&id={0}", primaryKeyValue);
            var postData = recordField.ToJson();

            var apiResult = JsonHelper<CommonResult>.ConvertJson(url, postData);
            if (apiResult != null)
            {
                result = apiResult.Success;
            }
            return result;
        }

        /// <summary>
        /// 执行SQL查询语句，返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>
        /// 返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </returns>
        public virtual string SqlValueList(string sql)
        {
            string result = "";

            var action = "SqlValueList";
            string url = GetPostUrl(action);
            var postData = new
            {
                sql = sql
            }.ToJson();

            string content = helper.GetHtml(url, postData, true);
            if (!content.Contains("errcode"))
            {
                result = content;
            }
            return result;
        }

        /// <summary>
        /// 执行SQL查询语句，返回所有记录的DataTable集合。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        public virtual DataTable SqlTable(string sql)
        {
            var action = "SqlTable";
            string url = GetPostUrl(action);
            var postData = new
            {
                sql = sql
            }.ToJson();

            var apiResult = JsonHelper<DataTable>.ConvertJson(url, postData);
            return apiResult;
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于字符型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual T FindByID(string key)
        {
            var action = "FindByID";
            string url = GetTokenUrl(action) + string.Format("&id={0}", key);

            return JsonHelper<T>.ConvertJson(url);
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于整型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        public virtual T FindByID2(int key)
        {
            var action = "FindByID2";
            string url = GetTokenUrl(action) + string.Format("&id={0}", key);

            return JsonHelper<T>.ConvertJson(url);
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定的对象</returns>
        public virtual T FindSingle(string condition)
        {
            var action = "FindSingle";
            string url = GetTokenUrl(action) + string.Format("&condition={0}", condition.UrlEncode());

            return JsonHelper<T>.ConvertJson(url);
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定的对象</returns>
        public virtual T FindSingle2(string condition, string orderBy)
        {
            var action = "FindSingle2";
            string url = GetTokenUrl(action) + string.Format("&condition={0}&orderBy={1}", condition.UrlEncode(), orderBy);

            return JsonHelper<T>.ConvertJson(url);
        }

        /// <summary>
        /// 查找记录表中最旧的一条记录
        /// </summary>
        /// <returns></returns>
        public virtual T FindFirst()
        {
            var action = "FindFirst";
            string url = GetTokenUrl(action);

            return JsonHelper<T>.ConvertJson(url);
        }

        /// <summary>
        /// 查找记录表中最新的一条记录
        /// </summary>
        /// <returns></returns>
        public virtual T FindLast()
        {
            var action = "FindLast";
            string url = GetTokenUrl(action);

            return JsonHelper<T>.ConvertJson(url);
        }

        #endregion

        #region 返回集合的接口

        /// <summary>
        /// 根据ID字符串(逗号分隔)获取对象列表
        /// </summary>
        /// <param name="idString">ID字符串(逗号分隔)</param>
        /// <returns>符合条件的对象列表</returns>
        public virtual List<T> FindByIDs(string idString)
        {
            var action = "FindByIDs";
            string url = GetTokenUrl(action) + string.Format("&idString={0}", idString);

            return JsonHelper<List<T>>.ConvertJson(url);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> Find(string condition)
        {
            var action = "Find";
            string url = GetTokenUrl(action) + string.Format("&condition={0}", condition.UrlEncode());

            return JsonHelper<List<T>>.ConvertJson(url);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> Find2(string condition, string orderBy)
        {
            var action = "Find2";
            string url = GetTokenUrl(action) + string.Format("&condition={0}&orderBy={1}", condition.UrlEncode(), orderBy);

            return JsonHelper<List<T>>.ConvertJson(url);
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="pagerInfo">分页实体</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager(string condition, ref PagerInfo pagerInfo)
        {
            var action = "FindWithPager";
            string url = GetTokenUrl(action) + string.Format("&condition={0}", condition.UrlEncode());
            var postData = pagerInfo.ToJson();

            List<T> result = new List<T>();
            PagedList<T> list = JsonHelper<PagedList<T>>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="pagerInfo">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager2(string condition, ref PagerInfo pagerInfo, string fieldToSort)
        {
            var action = "FindWithPager2";
            string url = GetTokenUrl(action) + string.Format("&condition={0}&fieldToSort={1}", condition.UrlEncode(), fieldToSort);
            var postData = pagerInfo.ToJson(); 

            List<T> result = new List<T>();
            PagedList<T> list = JsonHelper<PagedList<T>>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="pagerInfo">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> FindWithPager3(string condition, ref PagerInfo pagerInfo, string fieldToSort, bool desc)
        {
            var action = "FindWithPager3";
            string url = GetTokenUrl(action) + string.Format("&condition={0}&fieldToSort={1}&desc={2}", condition.UrlEncode(), fieldToSort.UrlEncode(), desc);
            var postData = pagerInfo.ToJson(); 

            List<T> result = new List<T>();
            PagedList<T> list = JsonHelper<PagedList<T>>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll()
        {
            var action = "GetAll";
            string url = GetTokenUrl(action);

            return JsonHelper<List<T>>.ConvertJson(url);
        }

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAll2(string orderBy)
        {
            var action = "GetAll";
            string url = GetTokenUrl(action) + string.Format("&orderBy={0}", orderBy);

            return JsonHelper<List<T>>.ConvertJson(url);
        }

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="pagerInfo">分页实体信息</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAllWithPager(ref PagerInfo pagerInfo)
        {
            var action = "GetAllWithPager";
            string url = GetTokenUrl(action);
            var postData = pagerInfo.ToJson(); 

            List<T> result = new List<T>();
            PagedList<T> list = JsonHelper<PagedList<T>>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="pagerInfo">分页实体信息</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定对象的集合</returns>
        public virtual List<T> GetAllWithPager2(ref PagerInfo pagerInfo, string fieldToSort, bool desc)
        {
            var action = "GetAllWithPager2";
            string url = GetTokenUrl(action) + string.Format("&fieldToSort={0}&desc={1}", fieldToSort, desc);
            var postData = pagerInfo.ToJson(); 

            List<T> result = new List<T>();
            PagedList<T> list = JsonHelper<PagedList<T>>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTable()
        {
            var action = "GetAllToDataTable";
            string url = GetTokenUrl(action);

            return JsonHelper<DataTable>.ConvertJson(url);
        }

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <param name="orderBy">自定义排序语句，如Order By Name Desc；如不指定，则使用默认排序</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTable2(string orderBy)
        {
            var action = "GetAllToDataTable2";
            string url = GetTokenUrl(action) + string.Format("&orderBy={0}", orderBy);

            return JsonHelper<DataTable>.ConvertJson(url);
        }

        /// <summary>
        /// 根据分页条件，返回DataSet对象
        /// </summary>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTableWithPager(ref PagerInfo pagerInfo)
        {
            var action = "GetAllToDataTableWithPager";
            string url = GetTokenUrl(action);
            var postData = pagerInfo.ToJson(); 

            DataTable result = new DataTable();
            PageTableList list = JsonHelper<PageTableList>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }

        /// <summary>
        /// 根据分页条件，返回DataSet对象
        /// </summary>
        /// <param name="pagerInfo">分页条件</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns></returns>
        public virtual DataTable GetAllToDataTableWithPager2(ref PagerInfo pagerInfo, string fieldToSort, bool desc)
        {
            var action = "GetAllToDataTableWithPager2";
            string url = GetTokenUrl(action) + string.Format("&fieldToSort={0}&desc={1}", fieldToSort.UrlEncode(), desc);
            var postData = pagerInfo.ToJson(); 

            DataTable result = new DataTable();
            PageTableList list = JsonHelper<PageTableList>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }
        
        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual DataTable FindToDataTable(string condition)
        {
            var action = "FindToDataTable";
            string url = GetTokenUrl(action) + string.Format("&condition={0}", condition.UrlEncode());

            return JsonHelper<DataTable>.ConvertJson(url);
        }

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public virtual DataTable FindToDataTableWithPager(string condition, ref PagerInfo pagerInfo)
        {
            var action = "FindToDataTableWithPager";
            string url = GetTokenUrl(action) + string.Format("&condition={0}", condition.UrlEncode());
            var postData = pagerInfo.ToJson(); 

            DataTable result = new DataTable();
            PageTableList list = JsonHelper<PageTableList>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }

        /// <summary>
        /// 根据条件查询数据库,并返回DataTable集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="pagerInfo">分页实体</param>
        /// <param name="fieldToSort">排序字段</param>
        /// <param name="desc">是否降序</param>
        /// <returns>指定DataTable的集合</returns>
        public virtual DataTable FindToDataTableWithPager2(string condition, ref PagerInfo pagerInfo, string fieldToSort, bool desc)
        {
            var action = "FindToDataTableWithPager2";
            string url = GetTokenUrl(action) + string.Format("&fieldToSort={0}&desc={1}&condition={2}", fieldToSort.UrlEncode(), desc, condition.UrlEncode());
            var postData = pagerInfo.ToJson(); 

            DataTable result = new DataTable();
            PageTableList list = JsonHelper<PageTableList>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }

        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual DataTable FindByView(string viewName, string condition)
        {
            var action = "FindByView";
            string url = GetTokenUrl(action) + string.Format("&condition={0}&viewName={1}", condition.UrlEncode(), viewName.UrlEncode());

            return JsonHelper<DataTable>.ConvertJson(url);
        }

        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isDescending">是否为降序</param>
        /// <returns></returns>
        public virtual DataTable FindByView2(string viewName, string condition, string sortField, bool isDescending)
        {
            var action = "FindByView2";
            string url = GetTokenUrl(action) + string.Format("&condition={0}&viewName={1}&sortField={2}&isDescending={3}", condition.UrlEncode(), viewName.UrlEncode(), sortField.UrlEncode(), isDescending);

            return JsonHelper<DataTable>.ConvertJson(url);
        }

        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="isDescending">是否为降序</param>
        /// <param name="pagerInfo">分页条件</param>
        /// <returns></returns>
        public virtual DataTable FindByViewWithPager(string viewName, string condition, string sortField, bool isDescending, PagerInfo pagerInfo)
        {
            var action = "FindByViewWithPager";
            string url = GetTokenUrl(action) + string.Format("&viewName={0}&condition={1}&sortField={2}&isDescending={3}", viewName.UrlEncode(), condition.UrlEncode(), sortField.UrlEncode(), isDescending);
            var postData = pagerInfo.ToJson(); 

            DataTable result = new DataTable();
            PageTableList list = JsonHelper<PageTableList>.ConvertJson(url, postData);
            if (list != null)
            {
                pagerInfo.RecordCount = list.total_count;//修改总记录数
                result = list.list;
            }
            return result;
        }

        #endregion

        #region 基础接口

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount2(string condition)
        {
            var action = "GetRecordCount2";
            string url = GetTokenUrl(action) + string.Format("&condition={0}", condition.UrlEncode());

            return JsonHelper<DataTable>.ConvertString(url).ToInt32();
        }

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            var action = "GetRecordCount";
            string url = GetTokenUrl(action);

            return JsonHelper<DataTable>.ConvertString(url).ToInt32();
        }

        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="fieldName">指定的属性名</param>
        /// <param name="key">指定的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool IsExistKey(string fieldName, object key)
        {
            var action = "IsExistKey";
            string url = GetTokenUrl(action) + string.Format("&fieldName={0}&key={1}", fieldName.UrlEncode(), key);

            var result = JsonHelper<CommonResult>.ConvertJson(url);
            return (result != null) ? result.Success : false;
        }

        /// <summary>
        /// 根据condition条件，判断是否存在记录
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>如果存在返回True，否则False</returns>
        public virtual bool IsExistRecord(string condition)
        {
            var action = "IsExistRecord";
            string url = GetTokenUrl(action) + string.Format("&condition={0}", condition.UrlEncode());

            var result = JsonHelper<CommonResult>.ConvertJson(url);
            return (result!= null) ? result.Success : false;
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于字符型主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete(string key)
        {
            var action = "Delete";
            string url = GetPostUrlWithToken(action);
            var postData = new
            {
                id = key
            }.ToJson();

            var result = JsonHelper<CommonResult>.ConvertJson(url, postData);
            return (result != null) ? result.Success : false;
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool Delete2(int key)
        {
            var action = "Delete2";
            string url = GetPostUrlWithToken(action); 
            var postData = new
            {
                id = key
            }.ToJson();

            var result = JsonHelper<CommonResult>.ConvertJson(url, postData);
            return (result != null) ? result.Success : false;
        }
                       
        /// <summary>
        /// 删除多个ID的记录
        /// </summary>
        /// <param name="ids">多个id组合，逗号分开（1,2,3,4,5）</param>
        /// <returns></returns>
        public virtual bool DeleteByIds(string ids)
        {
            var action = "DeleteByIds";
            string url = GetPostUrlWithToken(action);
            var postData = new
            {
                ids = ids
            }.ToJson();

            var result = JsonHelper<CommonResult>.ConvertJson(url, postData);
            return (result != null) ? result.Success : false;
        }

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public virtual bool DeleteByCondition(string condition)
        {
            var action = "DeleteByCondition";
            var postData = new
            {
                condition = condition
            }.ToJson();
            string url = GetPostUrlWithToken(action);

            var result = JsonHelper<CommonResult>.ConvertJson(url, postData);
            return (result != null) ? result.Success : false;
        } 
        #endregion

        #region 辅助型接口

        /// <summary>
        /// 在分布式中获取服务器的时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetSystemDateTime()
        {
            var action = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string url = GetTokenUrl(action);
            var result = JsonHelper<string>.ConvertString(url);
            return result.ToDateTime();
        }

        /// <summary>
        /// 根据主键和字段名称，获取对应字段的内容
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public virtual string GetFieldValue(string key, string fieldName)
        {
            var action = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string url = GetTokenUrl(action) + string.Format("&key={0}&fieldName={1}", key, fieldName.UrlEncode());

            var result = JsonHelper<string>.ConvertString(url);
            return result;
        }

        /// <summary>
        /// 根据主键和字段名称，获取对应字段的内容
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <param name="fieldNameList">字段名称列表</param>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetFieldValueList(string key, List<string> fieldNameList)
        {
            var action = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string url = GetTokenUrl(action);
            var postData = new
            {
                key = key,
                fieldNameList = fieldNameList
            }.ToJson();

            var result = JsonHelper<Dictionary<string, string>>.ConvertJson(url, postData);
            return result;
        }


        /// <summary>
        /// 获取字段列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public virtual List<string> GetFieldList(string fieldName)
        {
            var action = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string url = GetTokenUrl(action) + string.Format("&fieldName={0}", fieldName.UrlEncode());

            var result = JsonHelper<List<string>>.ConvertJson(url);
            return result;
        }

        /// <summary>
        /// 根据条件，获取某字段数据字典列表
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="condition">查询的条件</param>
        /// <returns></returns>
        public virtual List<string> GetFieldListByCondition(string fieldName, string condition)
        {
            var action = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string url = GetTokenUrl(action) + string.Format("&fieldName={0}&condition={1}", fieldName.UrlEncode(), condition.UrlEncode());

            var result = JsonHelper<List<string>>.ConvertJson(url);
            return result;
        }

        /// <summary>
        /// 获取表的字段名称和数据类型列表。
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GetFieldTypeList()
        {
            var action = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string url = GetTokenUrl(action);

            var result = JsonHelper<DataTable>.ConvertJson(url);
            return result;
        }

        /// <summary>
        /// 获取字段中文别名（用于界面显示）的字典集合
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetColumnNameAlias()
        {
            var action = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string url = GetTokenUrl(action);

            var result = JsonHelper<Dictionary<string, string>>.ConvertJson(url);
            return result;
        }
               
        /// <summary>
        /// 获取列表显示的字段（用于界面显示）
        /// </summary>
        /// <returns></returns>
        public virtual string GetDisplayColumns()
        {
            var action = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string url = GetTokenUrl(action);

            var result = JsonHelper<string>.ConvertString(url);
            return result;
        }

        /// <summary>
        /// 获取指定字段的报表数据
        /// </summary>
        /// <param name="fieldName">表字段</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public virtual DataTable GetReportData(string fieldName, string condition)
        {
            var action = System.Reflection.MethodBase.GetCurrentMethod().Name;
            string url = GetTokenUrl(action) + string.Format("&fieldName={0}&condition={1}", fieldName.UrlEncode(), condition.UrlEncode());

            var result = JsonHelper<DataTable>.ConvertJson(url);
            return result;
        }

        #endregion

    }
}
