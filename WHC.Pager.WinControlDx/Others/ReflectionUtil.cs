using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace WHC.Pager.WinControl
{
    internal sealed class ReflectionUtil
    {
        private ReflectionUtil()
        {
        }

        public static BindingFlags bf = BindingFlags.DeclaredOnly | BindingFlags.Public |
                                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        public static object InvokeMethod(object obj, string methodName, object[] args)
        {
            object objReturn = null;
            Type type = obj.GetType();
            objReturn = type.InvokeMember(methodName, bf | BindingFlags.InvokeMethod, null, obj, args);
            return objReturn;
        }

        public static void SetField(object obj, string name, object value)
        {
            FieldInfo fi = obj.GetType().GetField(name, bf);
            fi.SetValue(obj, value);
        }

        public static object GetField(object obj, string name)
        {
            FieldInfo fi = obj.GetType().GetField(name, bf);
            return fi.GetValue(obj);
        }

        /// <summary>
        /// ���ö������Ե�ֵ
        /// </summary>
        public static void SetProperty(object obj, string name, object value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name, bf);
            object objValue = Convert.ChangeType(value, propertyInfo.PropertyType);
            propertyInfo.SetValue(obj, objValue, null);
        }

        /// <summary>
        /// ��ȡ�������Ե�ֵ
        /// </summary>
        public static object GetProperty(object obj, string name)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name, bf);
            return propertyInfo.GetValue(obj, null);
        }

        /// <summary>
        /// ��ȡ����������Ϣ����װ���ַ��������
        /// </summary>
        public static List<string> GetPropertyNames(object obj)
        {
            List<string> nameList = new List<string>();
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);

            foreach (PropertyInfo property in propertyInfos)
            {
                nameList.Add(property.Name);
            }

            return nameList;
        }

        /// <summary>
        /// ��ȡ����������Ϣ����װ���ַ��������
        /// </summary>
        public static Dictionary<string,string> GetPropertyNameTypes(object obj)
        {
            Dictionary<string, string> nameList = new Dictionary<string, string>();
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);

            foreach (PropertyInfo property in propertyInfos)
            {
                nameList.Add(property.Name, property.PropertyType.FullName);
            }

            return nameList;
        }

        public static DataTable CreateTable(object objSource)
        {
            DataTable table = null;
            IEnumerable objList = objSource as IEnumerable;
            if (objList == null)
                return null;

            foreach (object obj in objList)
            {
                if (table == null)
                {
                    List<string> nameList = ReflectionUtil.GetPropertyNames(obj);
                    table = new DataTable("");
                    DataColumn column;

                    foreach (string name in nameList)
                    {
                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = name;
                        column.Caption = name;
                        table.Columns.Add(column);
                    }
                }

                DataRow row = table.NewRow();
                PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);
                foreach (PropertyInfo property in propertyInfos)
                {
                    row[property.Name] = property.GetValue(obj, null);                    
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}