using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// ������ṩ��һЩʵ�õķ�����ת��XML�Ͷ���
    /// </summary>
    public sealed class XmlConvertor
    {
        private XmlConvertor()
        {
        }

        /// <summary>
        /// ת��XML�ַ�����ָ�����͵Ķ���
        /// </summary>
        /// <typeparam name="T">ָ���Ķ�������</typeparam>
        /// <param name="xml">XML�ַ���</param>
        /// <returns>��XML�ַ���ת�������Ķ���</returns>
        public static T XmlToObject<T>(string xml) where T : class
        {
            return XmlConvertor.XmlToObject(xml, typeof(T)) as T;
        }

        /// <summary>
        /// ת��XML�ַ�����ָ�����͵Ķ���
        /// </summary>
        /// <param name="xml">XML�ַ���</param>
        /// <param name="type">ָ���Ķ�������</param>
        /// <returns>��XML�ַ���ת�������Ķ���</returns>
        public static object XmlToObject(string xml, Type type)
        {
            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            object obj = null;
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader strReader = new StringReader(xml);
            XmlReader reader = new XmlTextReader(strReader);

            try
            {
                obj = serializer.Deserialize(reader);
            }
            catch (InvalidOperationException ie)
            {
                throw new InvalidOperationException("Can not convert xml to object", ie);
            }
            finally
            {
                reader.Close();
            }
            return obj;
        }

        /// <summary>
        /// ת��object���󵽾����XML�ַ���
        /// </summary>
        /// <param name="obj">�����л��Ķ���</param>
        /// <param name="toBeIndented"><c>true</c>����, ����<c>false</c>.</param>
        /// <returns>XML�ַ���</returns>
        public static string ObjectToXml(object obj, bool toBeIndented = false)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            UTF8Encoding encoding = new UTF8Encoding(false);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, encoding);
            writer.Formatting = (toBeIndented ? Formatting.Indented : Formatting.None);

            try
            {
                serializer.Serialize(writer, obj, ns);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Can not convert object to xml.");
            }
            finally
            {
                writer.Close();
            }

            string xml = encoding.GetString(stream.ToArray());
            return xml;
        }

        /// <summary>
        /// ��ʽ��XML
        /// </summary>
        /// <param name="xml">����ʽ����XML</param>
        /// <returns></returns>
        public static string FormatXml(string xml)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        } 
    }
}