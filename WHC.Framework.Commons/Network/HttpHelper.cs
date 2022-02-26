using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Web;
using System.Collections.Specialized;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// ��ȡ��ҳ���ݸ�����⡣
    /// </summary>
    public class HttpHelper
    {
        #region ˽�б���
        private CookieContainer cc;
        private string contentType = "application/x-www-form-urlencoded";
        private string accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
        private string userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
        
        private Encoding encoding = Encoding.GetEncoding("utf-8");
        private int delay = 1000;
        private int maxTry = 3;
        private int currentTry = 0;
        #endregion

        #region ����

        /// <summary>
        /// �������ͣ�Ĭ��Ϊ"application/x-www-form-urlencoded"
        /// </summary>
        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        /// <summary>
        /// Acceptֵ��Ĭ��֧�ָ�������
        /// </summary>
        public string Accept
        {
            get { return accept; }
            set { accept = value; }
        }

        /// <summary>
        /// UserAgent��Ĭ��֧��Mozilla/MSIE��
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }  

        /// <summary>
        /// Cookie����
        /// </summary>
        public CookieContainer CookieContainer
        {
            get
            {
                return cc;
            }
        }

        /// <summary>
        /// ��ȡ��ҳԴ��ʱʹ�õı���
        /// </summary>
        /// <value></value>
        public Encoding Encoding
        {
            get
            {
                return encoding;
            }
            set
            {
                encoding = value;
            }
        }

        /// <summary>
        /// ������ʱ
        /// </summary>
        public int NetworkDelay
        {
            get
            {
                Random r = new Random();
                return (r.Next(delay / 1000, delay / 1000 * 2))*1000;
            }
            set
            {
                delay = value;
            }
        }

        /// <summary>
        /// ����Դ���
        /// </summary>
        public int MaxTry
        {
            get
            {
                return maxTry;
            }
            set
            {
                maxTry = value;
            }
        }

        /// <summary>
        /// X509Certificate2֤�鼯��
        /// </summary>
        public X509CertificateCollection ClientCertificates { get; set; }

        /// <summary>
        /// �����Header���ݼ���
        /// </summary>
        public NameValueCollection Header { get; set; }

        /// <summary>
        /// HTTP��������
        /// </summary>
        public WebProxy Proxy { get; set; }

        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        public HttpHelper()
        {
            cc = new CookieContainer();
            Header = new NameValueCollection();

            //���������Ѿ��ر�: δ��ΪSSL/TLS ��ȫͨ���������ι�ϵ���Ĵ�����
            ServicePointManager.ServerCertificateValidationCallback = (s, cer, ch, ssl) => true;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="cc">ָ��CookieContainer��ֵ</param>
        public HttpHelper(CookieContainer cc) : this()
        {
            this.cc = cc;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="contentType">��������</param>
        /// <param name="accept">Accept����</param>
        /// <param name="userAgent">UserAgent����</param>
        public HttpHelper(string contentType, string accept, string userAgent) : this()
        {
            this.contentType = contentType;
            this.accept = accept;
            this.userAgent = userAgent;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="cc">ָ��CookieContainer��ֵ</param>
        /// <param name="contentType">��������</param>
        /// <param name="accept">Accept����</param>
        /// <param name="userAgent">UserAgent����</param>
        public HttpHelper(CookieContainer cc, string contentType, string accept, string userAgent) : this()
        {
            this.cc = cc;
            this.contentType = contentType;
            this.accept = accept;
            this.userAgent = userAgent;
        }

        #endregion

        #region ��������                     

        /// <summary>
        /// ��ȡָ��ҳ���HTML����
        /// </summary>
        /// <param name="url">ָ��ҳ���·��</param>
        /// <param name="cookieContainer">Cookie���϶���</param>
        /// <param name="postData">�ط�������</param>
        /// <param name="isPost">�Ƿ���post��ʽ��������</param>
        /// <param name="referer">ҳ������</param>
        /// <returns></returns>
        public string GetHtml(string url, CookieContainer cookieContainer, string postData = null, bool isPost= false, string referer = null)
        {
            currentTry++;
            try
            {
                string newUrl = url;

                //�������POST��ʽ����ôʹ��POSTData���������ַ���
                if(!isPost && !string.IsNullOrEmpty(postData))
                {
                    string concatChar = newUrl.Contains("?") ? "&" : "?";
                    newUrl += string.Format("{0}{1}", concatChar, postData);
                }

                byte[] byteRequest = null;
                if (isPost)
                {
                    byteRequest = Encoding.GetBytes(postData);
                }

                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(newUrl);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Referer = string.IsNullOrEmpty(referer) ? newUrl : referer;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = isPost ? "POST" : "GET";
                httpWebRequest.ContentLength = isPost ? byteRequest.Length : 0;
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.Proxy = this.Proxy; //����HTTP����

                //��������Header����
                if(this.Header != null)
                {
                    httpWebRequest.Headers.Add(this.Header);
                }

                //���֤�鼯�ϲ�Ϊ�գ�����ӵ�����
                if(this.ClientCertificates != null)
                {
                    foreach(X509Certificate cert in this.ClientCertificates)
                    {
                        httpWebRequest.ClientCertificates.Add(cert);
                    }
                }

                //�����POST��ʽ��д��������
                if (isPost)
                {
                    Stream stream = httpWebRequest.GetRequestStream();
                    stream.Write(byteRequest, 0, byteRequest.Length);
                    stream.Close();
                }

                HttpWebResponse httpWebResponse;
                try
                {
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    //redirectURL = httpWebResponse.Headers["Location"];// Get redirected uri
                }
                catch (WebException ex)
                {
                    httpWebResponse = (HttpWebResponse)ex.Response;
                }
                Stream responseStream = httpWebResponse.GetResponseStream();

                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                currentTry = 0;
                return html;
            }
            catch (Exception e)
            {
                if (currentTry <= maxTry)
                {
                    GetHtml(url, cookieContainer, postData, isPost);
                }

                currentTry = 0;
                return string.Empty;
            }
        }


        /// <summary>
        /// ��ȡָ��ҳ���HTML����
        /// </summary>
        /// <param name="url">ָ��ҳ���·��</param>
        /// <param name="postData">�ط�������</param>
        /// <param name="isPost">�Ƿ���post��ʽ��������</param>
        /// <returns></returns>
        public string GetHtml(string url, string postData = null, bool isPost = false, string referer = null)
        {
            return GetHtml(url, cc, postData, isPost, referer);
        }
                        
        /// <summary>
        /// ��ȡָ��ҳ���Stream
        /// </summary>
        /// <param name="url">ָ��ҳ���·��</param>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="cookieContainer">Cookie���϶���</param>
        /// <returns></returns>
        public Stream GetStream(string url, ref string fileName, CookieContainer cookieContainer)
        {
            return GetStream(url, ref fileName, cookieContainer, url);
        }

        /// <summary>
        /// ��ȡָ��ҳ���Stream
        /// </summary>
        /// <param name="url">ָ��ҳ���·��</param>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="cookieContainer">Cookie����</param>
        /// <param name="reference">ҳ������</param>
        public Stream GetStream(string url, ref string fileName, CookieContainer cookieContainer, string reference)
        {
            currentTry++;
            try
            {
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Referer = reference;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = "GET";
                httpWebRequest.Proxy = this.Proxy; //����HTTP����

                //��������Header����
                if (this.Header != null)
                {
                    httpWebRequest.Headers.Add(this.Header);
                }

                //���֤�鼯�ϲ�Ϊ�գ�����ӵ�����
                if (this.ClientCertificates != null)
                {
                    foreach (X509Certificate cert in this.ClientCertificates)
                    {
                        httpWebRequest.ClientCertificates.Add(cert);
                    }
                }

                HttpWebResponse httpWebResponse;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                fileName = httpWebResponse.Headers["Content-Disposition"] != null ?
                    httpWebResponse.Headers["Content-Disposition"].Replace("attachment; filename=", "").Replace("\"", "") :
                    httpWebResponse.Headers["Location"] != null ? Path.GetFileName(httpWebResponse.Headers["Location"]) :
                    Path.GetFileName(url).Contains("=") ? url.Substring(url.LastIndexOf("=") + 1) :
                    Path.GetFileName(url).Contains("?") ? url.Substring(url.LastIndexOf("?") + 1) :
                    Path.GetFileName(httpWebResponse.ResponseUri.ToString());

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileName = Path.GetFileName(fileName);
                }
                else
                {
                    fileName = "UnKnowFileName";//Ĭ���ļ�����
                }

                currentTry = 0;
                return responseStream;
            }
            catch (Exception e)
            {
                if (currentTry <= maxTry)
                {
                    CookieCollection cookie = new CookieCollection();
                    GetStream(url, ref fileName, cookieContainer, reference);
                }

                currentTry = 0;
                return null;
            }
        }

        /// <summary>
        /// ��ȡָ��ҳ���Stream
        /// </summary>
        /// <param name="url">ָ��ҳ���·��</param>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="cookieContainer">Cookie����</param>
        /// <param name="postData">POST����</param>
        /// <param name="isPost">�Ƿ�ʹ��POST��ʽ</param>
        /// <param name="reference">ҳ������</param>
        public Stream GetStream(string url, ref string fileName, CookieContainer cookieContainer, string postData, bool isPost, string reference)
        {
            currentTry++;
            try
            {
                byte[] byteRequest = null;
                if (isPost)
                {
                    byteRequest = Encoding.GetBytes(postData);
                }

                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Referer = reference;
                httpWebRequest.Accept = accept;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.Method = isPost ? "POST" : "GET";
                httpWebRequest.Proxy = this.Proxy; //����HTTP����

                //��������Header����
                if (this.Header != null)
                {
                    httpWebRequest.Headers.Add(this.Header);
                }

                //���֤�鼯�ϲ�Ϊ�գ�����ӵ�����
                if (this.ClientCertificates != null)
                {
                    foreach (X509Certificate cert in this.ClientCertificates)
                    {
                        httpWebRequest.ClientCertificates.Add(cert);
                    }
                }

                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();

                HttpWebResponse httpWebResponse;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                fileName = httpWebResponse.Headers["Content-Disposition"] != null ?
                    httpWebResponse.Headers["Content-Disposition"].Replace("attachment; filename=", "").Replace("\"", "") :
                    httpWebResponse.Headers["Location"] != null ? Path.GetFileName(httpWebResponse.Headers["Location"]) :
                    Path.GetFileName(url).Contains("=") ? url.Substring(url.LastIndexOf("=") + 1) :
                    Path.GetFileName(url).Contains("?") ? url.Substring(url.LastIndexOf("?") + 1) :
                    Path.GetFileName(httpWebResponse.ResponseUri.ToString());

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileName = Path.GetFileName(fileName);
                }
                else
                {
                    fileName = "UnKnowFileName";//Ĭ���ļ�����
                }

                currentTry = 0;
                return responseStream;
            }
            catch (Exception e)
            {
                if (currentTry <= maxTry)
                {
                    CookieCollection cookie = new CookieCollection();
                    GetStream(url, ref fileName, cookieContainer, postData, isPost, reference);
                }

                currentTry = 0;
                return null;
            }
        }

        /// <summary>
        /// �ύ�ļ����������ַ
        /// </summary>
        /// <param name="url">ָ��ҳ���·��</param>
        /// <param name="files">�ύ���ļ��б�</param>
        /// <param name="nvc">�������ݣ�����-ֵ��ֵ��</param>
        /// <param name="cookieContainer">Cookie����</param>
        /// <param name="reference">ҳ������</param>
        /// <returns></returns>
        public string PostStream(string url, string[] files, NameValueCollection nvc = null, CookieContainer cookieContainer = null, string reference = null)
        {
            string boundary = "----------------------------" +
            DateTime.Now.Ticks.ToString("x");

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            httpWebRequest.Method = "POST";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.Referer = reference;
            httpWebRequest.Proxy = this.Proxy; //����HTTP����
            
            //��������Header����
            if (this.Header != null)
            {
                httpWebRequest.Headers.Add(this.Header);
            }

            //���֤�鼯�ϲ�Ϊ�գ�����ӵ�����
            if (this.ClientCertificates != null)
            {
                foreach (X509Certificate cert in this.ClientCertificates)
                {
                    httpWebRequest.ClientCertificates.Add(cert);
                }
            }

            Stream memStream = new System.IO.MemoryStream();
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +  boundary + "\r\n");            
            string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            if (nvc != null)
            {
                foreach (string key in nvc.Keys)
                {
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = Encoding.GetBytes(formitem);
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }
            }
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            string fileHeaderName = "media";//form-data��ý���ļ���ʶ���˴�Ĭ��Ϊmedia
            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    //string header = string.Format(headerTemplate, "file" + i, files[i]);
                    var fileName = new FileInfo(files[i]).Name;
                    string header = string.Format(headerTemplate, fileHeaderName, fileName);
                    byte[] headerbytes = Encoding.GetBytes(header);
                    memStream.Write(headerbytes, 0, headerbytes.Length);

                    FileStream fileStream = new FileStream(files[i], FileMode.Open,
                    FileAccess.Read);
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                    }

                    memStream.Write(boundarybytes, 0, boundarybytes.Length);
                    fileStream.Close();
                }
            }

            httpWebRequest.ContentLength = memStream.Length;
            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                requestStream.Close();
            }

            string result = null;
            using (WebResponse webResponse2 = httpWebRequest.GetResponse())
            {
                using (Stream stream2 = webResponse2.GetResponseStream())
                {
                    using (StreamReader reader2 = new StreamReader(stream2))
                    {
                        result = reader2.ReadToEnd();
                    }
                }
            }
            httpWebRequest = null;

            return result;
        }

        /// <summary>
        /// ����Cookie�ַ�����ȡCookie�ļ���
        /// </summary>
        /// <param name="cookieString">Cookie�ַ���</param>
        /// <returns></returns>
        public CookieCollection GetCookieCollection(string cookieString)
        {
            CookieCollection cc = new CookieCollection();
            //string cookieString = "SID=ARRGy4M1QVBtTU-ymi8bL6X8mVkctYbSbyDgdH8inu48rh_7FFxHE6MKYwqBFAJqlplUxq7hnBK5eqoh3E54jqk=;Domain=.google.com;Path=/,LSID=AaMBTixN1MqutGovVSOejyb8mVkctYbSbyDgdH8inu48rh_7FFxHE6MKYwqBFAJqlhCe_QqxLg00W5OZejb_UeQ=;Domain=www.google.com;Path=/accounts";
            Regex re = new Regex("([^;,]+)=([^;,]+);Domain=([^;,]+);Path=([^;,]+)", RegexOptions.IgnoreCase);
            foreach (Match m in re.Matches(cookieString))
            {
                //name,   value,   path,   domain   
                Cookie c = new Cookie(m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, m.Groups[3].Value);
                cc.Add(c);
            }
            return cc;
        }

        /// <summary>
        /// ��ȡHTMLҳ������ָ��������Key��Value����
        /// </summary>
        /// <param name="html">��������HTMLҳ������</param>
        /// <param name="key">�����������</param>
        /// <returns></returns>
        public string GetHiddenKeyValue(string html, string key)
        {
            string result = "";
            string sRegex = string.Format("<input\\s*type=\"hidden\".*?name=\"{0}\".*?\\s*value=[\"|'](?<value>.*?)[\"|'^/]", key);
            Regex re = new Regex(sRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            Match mc = re.Match(html);
            if (mc.Success)
            {
                result = mc.Groups[1].Value;
            }
            return result;
        }

        /// <summary>
        /// ��ȡ��ҳ�ı����ʽ
        /// </summary>
        /// <param name="url">��ҳ��ַ</param>
        /// <returns></returns>
        public string GetEncoding(string url)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Timeout = 5000;
                httpWebRequest.AllowAutoRedirect = false;
                httpWebRequest.Proxy = this.Proxy; //����HTTP����

                response = (HttpWebResponse)httpWebRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024)
                {
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
                    }
                    else
                    {
                        reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
                    }
                    
                    string html = reader.ReadToEnd();
                    Regex reg_charset = new Regex(@"charset\b\s*=\s*(?<charset>[^""]*)");
                    if (reg_charset.IsMatch(html))
                    {
                        return reg_charset.Match(html).Groups["charset"].Value;
                    }
                    else if (response.CharacterSet != string.Empty)
                    {
                        return response.CharacterSet;
                    }
                    else
                    {
                        return Encoding.Default.BodyName;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (reader != null)
                    reader.Close();
                if (httpWebRequest != null)
                    httpWebRequest = null;
            }
            return Encoding.Default.BodyName;
        }

        /// <summary>
        /// ���ҳ���Ƿ����(�������ʣ�
        /// </summary>
        public bool UrlExist(string url, WebProxy proxy = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Proxy = proxy;

                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    resp.Close();
                    return true;
                }
            }
            catch (WebException webex)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// �ж�URL�Ƿ���Ч
        /// </summary>
        /// <param name="url">���жϵ�URL����������ҳ�Լ�ͼƬ���ӵ�</param>
        /// <returns>200Ϊ��ȷ������Ϊ������ҳ�������</returns>
        public int GetUrlError(string url)
        {
            int num = 200;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                request.Proxy = this.Proxy; //����HTTP����

                ServicePointManager.Expect100Continue = false;
                ((HttpWebResponse)request.GetResponse()).Close();
            }
            catch (WebException exception)
            {
                if (exception.Status != WebExceptionStatus.ProtocolError)
                {
                    return num;
                }
                if (exception.Message.IndexOf("500 ") > 0)
                {
                    return 500;
                }
                if (exception.Message.IndexOf("401 ") > 0)
                {
                    return 401;
                }
                if (exception.Message.IndexOf("404") > 0)
                {
                    num = 404;
                }
            }
            catch
            {
                num = 401;
            }
            return num;
        }

        /// <summary>
        /// �Ƴ�Html���
        /// </summary>
        public string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// ���� HTML �ַ����ı�����
        /// </summary>
        /// <param name="inputData">�ַ���</param>
        /// <returns>������</returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }

        /// <summary>
        /// ���� HTML �ַ����Ľ�����
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>������</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// ���� url �ַ����еĲ�����Ϣ
        /// </summary>
        /// <param name="url">����� URL</param>
        /// <param name="baseUrl">��� URL �Ļ�������</param>
        /// <param name="nvc">���������õ��� (������,����ֵ) �ļ���</param>
        public static NameValueCollection ParseUrl(string url, out string baseUrl)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            NameValueCollection result = new NameValueCollection();

            baseUrl = "";
            if (url == "")
                return result;

            int questionMarkIndex = url.IndexOf('?');
            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return result;
            }

            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
                return result;

            string ps = url.Substring(questionMarkIndex + 1);
            // ��ʼ����������  
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);
            foreach (Match m in mc)
            {
                result.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }

            return result;
        }

        #endregion
    }
}
