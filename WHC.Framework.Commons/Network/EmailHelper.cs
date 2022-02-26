using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Reflection;
using System.Net.Mail;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// �����ʼ��ĸ����࣬���Է��͸�����Ƕ��ͼƬ��HTML�������ʼ���ʹ�õײ�SMTPЭ��ָ����з��͡�
    /// </summary>
    public class EmailHelper
    {
        #region public�����ֶ�

        /// <summary>
        /// �趨���Դ��룬Ĭ���趨ΪGB2312���粻��Ҫ������Ϊ""
        /// </summary>
        public string Charset = "GB2312";

        /// <summary>
        /// ���������
        /// </summary>
        public string MailServer
        {
            get { return mailserver; }
            set { mailserver = value; }
        }

        /// <summary>
        /// �ʼ��������˿ں�,Ĭ�϶˿�Ϊ25
        /// </summary>	
        public int MailServerPort
        {
            set
            {
                mailserverport = value;
            }
            get
            {
                return mailserverport;
            }
        }

        /// <summary>
        /// SMTP��֤ʱʹ�õ��û���
        /// </summary>
        public string MailServerUsername
        {
            set
            {
                if (value.Trim() != "")
                {
                    username = value.Trim();
                    useSmtpAuth = true;
                }
                else
                {
                    username = "";
                    useSmtpAuth = false;
                }
            }
            get
            {
                return username;
            }
        }

        /// <summary>
        /// SMTP��֤ʱʹ�õ�����
        /// </summary>
        public string MailServerPassword
        {
            set
            {
                password = value;
            }
            get
            {
                return password;
            }
        }

        /// <summary>
        /// �����˵�ַ
        /// </summary>
        public string From
        {
            get { return from; }
            set
            {
                from = value;
                //���δ����fromName����fromNameʹ�÷���������
                if (string.IsNullOrEmpty(fromName)) 
                { 
                    fromName = from; 
                }
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public string FromName
        {
            get { return fromName; }
            set { fromName = value; }
        }

        /// <summary>
        /// �ظ��ʼ���ַ
        /// </summary>
        public string ReplyTo = "";

        /// <summary>
        /// �ʼ�����
        /// </summary>		
        public string Subject = "";

        /// <summary>
        /// �Ƿ�Html�ʼ�
        /// </summary>		
        public bool IsHtml = false;

        /// <summary>
        /// �ռ����Ƿ�������
        /// </summary>
        public bool ReturnReceipt = false;

        /// <summary>
        /// �ʼ�����
        /// </summary>		
        public string Body = "";

        /// <summary>
        /// �ʼ��������ȼ���������Ϊ"High","Normal","Low"��"1","3","5"
        /// </summary>
        public string Priority
        {
            set
            {
                switch (value.ToLower())
                {
                    case "high":
                        priority = "High";
                        break;

                    case "1":
                        priority = "High";
                        break;

                    case "normal":
                        priority = "Normal";
                        break;

                    case "3":
                        priority = "Normal";
                        break;

                    case "low":
                        priority = "Low";
                        break;

                    case "5":
                        priority = "Low";
                        break;

                    default:
                        priority = "Normal";
                        break;
                }
            }
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>		
        public string ErrorMessage
        {
            get
            {
                return errmsg;
            }
        }

        /// <summary>
        /// �ռ�������
        /// </summary>	
        public string RecipientName = "";

        #endregion

        #region private�����ֶ�

        /// <summary>
        /// �ʼ�����������
        /// </summary>	
        private string mailserver;

        /// <summary>
        /// �ʼ��������˿ں�
        /// </summary>	
        private int mailserverport = 25;
        /// <summary>
        /// �����˵�ַ
        /// </summary>
        private string from = "";
        /// <summary>
        /// ����������
        /// </summary>
        private string fromName = "";

        /// <summary>
        /// �Ƿ���ҪSMTP��֤
        /// </summary>		
        private bool useSmtpAuth = false;

        /// <summary>
        /// SMTP��֤ʱʹ�õ��û���
        /// </summary>
        private string username = "";

        /// <summary>
        /// SMTP��֤ʱʹ�õ�����
        /// </summary>
        private string password = "";

        /// <summary>
        /// �ռ���������������ںܶ�SMTP�������ռ��˵�����������Է�ֹ����ʼ����ģ��������һ�㶼������10�����¡�
        /// </summary>
        private int RecipientMaxNum = 10;

        /// <summary>
        /// �ռ����б�
        /// </summary>
        private ArrayList Recipient = new ArrayList();

        /// <summary>
        ///�����ռ����б�
        /// </summary>
        private ArrayList RecipientCC = new ArrayList();

        /// <summary>
        /// �����ռ����б�
        /// </summary>
        private ArrayList RecipientBCC = new ArrayList();

        /// <summary>
        /// �ʼ��������ȼ���������Ϊ"High","Normal","Low"��"1","3","5"
        /// </summary>
        private string priority = "Normal";

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        private string errmsg;

        /// <summary>
        /// �س�����
        /// </summary>
        private string enter = "\r\n";

        /// <summary>
        /// TcpClient�����������ӷ�����
        /// </summary>	
        private TcpClient tc;

        /// <summary>
        /// NetworkStream����
        /// </summary>	
        private NetworkStream ns;

        /// <summary>
        /// SMTP��������ϣ��
        /// </summary>
        private Hashtable ErrCodeHT = new Hashtable();

        /// <summary>
        /// SMTP��ȷ�����ϣ��
        /// </summary>
        private Hashtable RightCodeHT = new Hashtable();

        #endregion

        /// <summary>
        /// SMTP��Ӧ�����ϣ��
        /// </summary>
        private void SMTPCodeAdd()
        {
            ErrCodeHT.Add("500", "�����ַ����");
            ErrCodeHT.Add("501", "������ʽ����");
            ErrCodeHT.Add("502", "�����ʵ��");
            ErrCodeHT.Add("503", "��������ҪSMTP��֤");
            ErrCodeHT.Add("504", "�����������ʵ��");
            ErrCodeHT.Add("421", "����δ�������رմ����ŵ�");
            ErrCodeHT.Add("450", "Ҫ����ʼ�����δ��ɣ����䲻���ã����磬����æ��");
            ErrCodeHT.Add("550", "Ҫ����ʼ�����δ��ɣ����䲻���ã����磬����δ�ҵ����򲻿ɷ��ʣ�");
            ErrCodeHT.Add("451", "����Ҫ��Ĳ�������������г���");
            ErrCodeHT.Add("551", "�û��Ǳ��أ��볢��<forward-path>");
            ErrCodeHT.Add("452", "ϵͳ�洢���㣬Ҫ��Ĳ���δִ��");
            ErrCodeHT.Add("552", "�����Ĵ洢���䣬Ҫ��Ĳ���δִ��");
            ErrCodeHT.Add("553", "�����������ã�Ҫ��Ĳ���δִ�У����������ʽ����");
            ErrCodeHT.Add("432", "��Ҫһ������ת��");
            ErrCodeHT.Add("534", "��֤���ƹ��ڼ�");
            ErrCodeHT.Add("538", "��ǰ�������֤������Ҫ����");
            ErrCodeHT.Add("454", "��ʱ��֤ʧ��");
            ErrCodeHT.Add("530", "��Ҫ��֤");

            RightCodeHT.Add("220", "�������");
            RightCodeHT.Add("250", "Ҫ����ʼ��������");
            RightCodeHT.Add("251", "�û��Ǳ��أ���ת����<forward-path>");
            RightCodeHT.Add("354", "��ʼ�ʼ����룬��<CRLF>.<CRLF>����");
            RightCodeHT.Add("221", "����رմ����ŵ�");
            RightCodeHT.Add("334", "��������Ӧ��֤Base64�ַ���");
            RightCodeHT.Add("235", "��֤�ɹ�");
        }

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public EmailHelper()
        {
            SMTPCodeAdd();
        }

        /// <summary>
        /// �����䷢�����ò����Ĺ��캯��
        /// </summary>
        /// <param name="mailServer">�ʼ�������</param>
        /// <param name="username">�û���</param>
        /// <param name="password">�û�����</param>
        public EmailHelper(string mailServer, string username, string password) : 
            this(mailServer, username, password, 25)
        {

        }

        /// <summary>
        /// �����䷢�����ò����Ĺ��캯��
        /// </summary>
        /// <param name="mailServer">�ʼ�������</param>
        /// <param name="username">�û���</param>
        /// <param name="password">�û�����</param>
        /// <param name="port">����������˿�</param>
        public EmailHelper(string mailServer, string username, string password, int port)
        {
            this.MailServer = mailServer;
            this.MailServerUsername = username;
            this.MailServerPassword = password;
            this.MailServerPort = port;

            SMTPCodeAdd();
        }

        /// <summary>
        /// ��������
        /// </summary>
        ~EmailHelper()
        {
            if (ns != null)
            {
                ns.Close();
            }
            if (tc != null)
            {
                tc.Close();
            }
        }

        #region ����

        /// <summary>
        /// ���һ������,��ʹ�þ���·��
        /// </summary>	
        public bool AddAttachment(string path)
        {
            if (File.Exists(path))
            {
                Attachments.Add(path);
                return true;
            }
            else
            {
                errmsg += "Ҫ���ӵ��ļ�������" + enter;
                return false;
            }
        }

        /// <summary>
        /// ���ڷָ���ķָ��.
        /// </summary>
        private string boundary  = "=====000_HuolxPubClass113273537350_=====";
        /// <summary>
        /// �ָ���
        /// </summary>
        private string boundary1 = "=====001_HuolxPubClass113273537350_=====";

        /// <summary>
        /// ���ڴ�Ÿ���·������Ϣ
        /// </summary>		
        private List<string> Attachments = new List<string>();

        /// <summary>
        /// ������BASE64�����ַ���
        /// </summary>
        /// <param name="path">����·��</param>
        private string AttachmentB64Str(string path)
        {
            FileStream fs;
            try
            {
                fs = new FileStream(path, System.IO.FileMode.Open,  FileAccess.Read, FileShare.Read);
            }
            catch(Exception ex)
            {
                errmsg += "Ҫ���ӵ��ļ�������" + enter;
                LogTextHelper.Error(errmsg, ex);

                return Base64Encode("Ҫ���ӵ��ļ�:" + path + "������");
            }
            int fl = (int)fs.Length;
            byte[] barray = new byte[fl];
            fs.Read(barray, 0, fl);
            fs.Close();
            return B64StrLine(Convert.ToBase64String(barray));
        }

        /// <summary>
        /// ����ļ����к��з�Ӣ����ĸ���������
        /// </summary>
        private string AttachmentNameStr(string fn)
        {
            if (Encoding.Default.GetByteCount(fn) > fn.Length)
            {
                return "=?" + Charset.ToUpper() + "?B?" + Base64Encode(fn) + "?=";
            }
            else
            {
                return fn;
            }
        }

        private string B64StrLine(string str)
        {
            StringBuilder B64sb = new StringBuilder(str);
            for (int i = 76; i < B64sb.Length; i += 78)
            {
                B64sb.Insert(i, enter);
            }
            return B64sb.ToString();
        }

        /// <summary>
        /// ���ַ�������ΪBase64�ַ���
        /// </summary>
        /// <param name="str">Ҫ������ַ���</param>
        private string Base64Encode(string str)
        {
            byte[] barray;
            barray = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(barray);
        }

        /// <summary>
        /// ��Base64�ַ�������Ϊ��ͨ�ַ���
        /// </summary>
        /// <param name="dstr">Ҫ������ַ���</param>
        private string Base64Decode(string dstr)
        {
            byte[] barray;
            barray = Convert.FromBase64String(dstr);
            return Encoding.Default.GetString(barray);
        }

        #endregion

        #region Ƕ��ͼƬ����

        private Hashtable EmbedList = new Hashtable(); //widened scope for MatchEvaluator

        /// <summary>
        /// �޸�HTMLҳ���е�ͼƬ����ΪǶ��ʽͼƬ�ʼ�����
        /// </summary>
        /// <param name="rawHtml">ԭʼHTML����</param>
        /// <param name="extras"></param>
        /// <param name="boundaryString"></param>
        /// <returns></returns>
        private string FixupReferences(string rawHtml, ref StringBuilder extras, string boundaryString)
        {
            //Build a symbol table to avoid redundant embedding.
            Regex imgRE, linkRE, hrefRE;
            MatchCollection imgMatches;

            //ͼƬ����������ʽ
            string imgMatchExpression = @"(?<=img+.+src\=[\x27\x22])(?<Url>[^\x27\x22]*)(?=[\x27\x22])";
            imgRE = new Regex(imgMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            //Link���ݲ���������ʽ
            string linkMatchExpression = "<\\s*link[^>]+href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))[^>]*>";
            linkRE = new Regex(linkMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            //����ҳ�������URL��ַ���ʽ
            string refMatchExpression = "href\\s*=\\s*(?:['\"](?<1>[^\"]*)['\"]|(?<1>\\S+))";
            hrefRE = new Regex(refMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            imgMatches = imgRE.Matches(rawHtml);
            foreach (Match m in imgMatches)
            {
                if (!EmbedList.ContainsKey(m.Groups[1].Value))
                {
                    EmbedList.Add(m.Groups[1].Value, Guid.NewGuid());
                }
            }

            //׼��Ƕ������
            extras.Length = 0;
            string contentType;
            ArrayList embeddees = new ArrayList(EmbedList.Keys);
            foreach (string embeddee in embeddees)
            {
                contentType = embeddee.Substring(embeddee.LastIndexOf(".") + 1).ToLower();
                extras.AppendFormat(boundaryString);
                if (contentType.Equals("jpg")) contentType = "jpeg";
                switch (contentType)
                {
                    case "jpeg":
                    case "gif":
                    case "png":
                    case "bmp":
                        extras.AppendFormat("Content-Type: image/{0}; charset=\"iso-8859-1\"\r\n", contentType);
                        extras.Append("Content-Transfer-Encoding: base64\r\n");
                        extras.Append("Content-Disposition: inline\r\n");
                        extras.AppendFormat("Content-ID: <{0}>\r\n\r\n", EmbedList[embeddee]);
                        extras.Append(GetDataAsBase64(embeddee));
                        extras.Append("\r\n");
                        break;
                }
            }
            //Fixups for references to items now embedded
            rawHtml = imgRE.Replace(rawHtml, new MatchEvaluator(FixupEmbedPath));
            return rawHtml;
        }

        /// <summary>
        /// ����Ƕ��ͼƬ�ĵ�ַӦ��Ϊcid:*** 
        /// </summary>
        private string FixupEmbedPath(Match m)
        {
            string replaceThis = m.Groups[1].Value;
            string withThis = string.Format("cid:{0}", EmbedList[replaceThis]);
            return m.Value.Replace(replaceThis, withThis);
        }

        private string GetDataAsBase64(string sUrl)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible;MSIE 6.0)");
            MemoryStream memoryStream = new MemoryStream();
            Stream stream = webClient.OpenRead(sUrl);
            byte[] chunk = new byte[4096];
            int cbChunk;
            while ((cbChunk = stream.Read(chunk, 0, 4096)) > 0)
                memoryStream.Write(chunk, 0, cbChunk);
            stream.Close();
            byte[] buf = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            memoryStream.Read(buf, 0, (int)memoryStream.Length);
            memoryStream.Close();
            string b64 = Convert.ToBase64String(buf);
            StringBuilder base64 = new StringBuilder();
            int i;
            for (i = 0; i + 60 < b64.Length; i += 60)
                base64.AppendFormat("{0}\r\n", b64.Substring(i, 60));
            base64.Append(b64.Substring(i));
            for (i = 0; i < (60 - (b64.Length % 60)); i++) base64.Append('=');
            base64.Append("\r\n");
            return base64.ToString();
        }

        private string GetDataAsString(string sUrl)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible;MSIE 6.0)");
            return (new StreamReader(webClient.OpenRead(sUrl))).ReadToEnd();
        }
        #endregion

        #region �ռ���
        /// <summary>
        /// ���һ���ռ���
        /// </summary>	
        /// <param name="str">�ռ��˵�ַ</param>
        /// <param name="ra"></param>
        private bool AddRs(string str, ArrayList ra)
        {
            str = str.Trim();

            if (str == null || str == "" || str.IndexOf("@") == -1)
            {
                return true;
                //				���������Զ��˳���Ч���ռ��ˣ�Ϊ�˲�Ӱ������������δ���ش����������Ҫ�ϸ�ļ���ռ��ˣ����滻Ϊ�������䡣
                //				errmsg+="������Ч�ռ��ˣ�" +str;
                //				return false;
            }

            if (ra.Count < RecipientMaxNum)
            {
                ra.Add(str);
                return true;
            }
            else
            {
                errmsg += "�ռ��˹���";
                return false;
            }
        }
        /// <summary>
        /// ���һ���ռ��ˣ�������10����������Ϊ�ַ�������
        /// </summary>
        /// <param name="str">�������ռ��˵�ַ���ַ������飨������10����</param>	
        /// <param name="ra">��ӵ������б�</param>
        private bool AddRs(string[] str, ArrayList ra)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!AddRs(str[i], ra))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ���һ���ռ���
        /// </summary>	
        /// <param name="str">�ռ��˵�ַ</param>
        public bool AddRecipient(string str)
        {
            return AddRs(str, Recipient);
        }

        /// <summary>
        /// ָ��һ���ռ���
        /// </summary>	
        /// <param name="str">�ռ��˵�ַ</param>
        public void SetRecipient(string str)
        {
            //return AddRs(str, Recipient);
            Recipient.Clear();
            Recipient.Add(str);
        }

        /// <summary>
        /// ���һ���ռ��ˣ�������10����������Ϊ�ַ�������
        /// </summary>
        /// <param name="str">�������ռ��˵�ַ���ַ������飨������RecipientMaxNum����</param>	
        public bool AddRecipient(string[] str)
        {
            return AddRs(str, Recipient);
        }

        /// <summary>
        /// ���һ�������ռ���
        /// </summary>
        /// <param name="str">�ռ��˵�ַ</param>
        public bool AddRecipientCC(string str)
        {
            return AddRs(str, RecipientCC);
        }

        /// <summary>
        /// ���һ�鳭���ռ��ˣ�������10����������Ϊ�ַ�������
        /// </summary>	
        /// <param name="str">�������ռ��˵�ַ���ַ������飨������RecipientMaxNum����</param>
        public bool AddRecipientCC(string[] str)
        {
            return AddRs(str, RecipientCC);
        }

        /// <summary>
        /// ���һ���ܼ��ռ���
        /// </summary>
        /// <param name="str">�ռ��˵�ַ</param>
        public bool AddRecipientBCC(string str)
        {
            return AddRs(str, RecipientBCC);
        }

        /// <summary>
        /// ���һ���ܼ��ռ��ˣ�������10����������Ϊ�ַ�������
        /// </summary>	
        /// <param name="str">�������ռ��˵�ַ���ַ������飨������RecipientMaxNum����</param>
        public bool AddRecipientBCC(string[] str)
        {
            return AddRs(str, RecipientBCC);
        }

        /// <summary>
        /// ����ռ����б�
        /// </summary>
        public void ClearRecipient()
        {
            Recipient.Clear();
        }

        #endregion

        #region �����ʼ�������

        /// <summary>
        /// ����SMTP����
        /// </summary>	
        private bool SendCommand(string Command)
        {
            byte[] WriteBuffer;
            if (Command == null || Command.Trim() == "")
            {
                return true;
            }
            //logs+=Command;
            WriteBuffer = Encoding.Default.GetBytes(Command);
            try
            {
                ns.Write(WriteBuffer, 0, WriteBuffer.Length);
            }
            catch
            {
                errmsg = "�������Ӵ���";
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����SMTP��������Ӧ
        /// </summary>
        private string RecvResponse()
        {
            int StreamSize;
            string ReturnValue = "false";
            byte[] ReadBuffer = new byte[4096];

            try
            {
                StreamSize = ns.Read(ReadBuffer, 0, ReadBuffer.Length);
            }
            catch
            {
                errmsg = "�������Ӵ���";
                return ReturnValue;
            }

            if (StreamSize == 0)
            {
                return ReturnValue;
            }
            else
            {
                ReturnValue = Encoding.Default.GetString(ReadBuffer).Substring(0, StreamSize).Trim(); ;
                //logs+=ReturnValue;
                return ReturnValue;
            }
        }

        /// <summary>
        /// �����������������һ��������ջ�Ӧ��
        /// </summary>
        /// <param name="Command">һ��Ҫ���͵�����</param>
        /// <param name="errstr">�������Ҫ��������Ϣ</param>
        private bool Dialog(string Command, string errstr)
        {
            if (Command == null || Command.Trim() == "")
            {
                return true;
            }
            if (SendCommand(Command))
            {
                string RR = RecvResponse();
                if (RR == "false")
                {
                    return false;
                }
                string RRCode = "";

                if (RR.Length >= 3)
                    RRCode = RR.Substring(0, 3);
                else
                    RRCode = RR;

                if (ErrCodeHT[RRCode] != null)
                {
                    errmsg += (RRCode + ErrCodeHT[RRCode].ToString());
                    errmsg += enter;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// �����������������һ��������ջ�Ӧ��
        /// </summary>
        private bool Dialog(ArrayList Command, string errstr)
        {
            foreach (String item in Command)
            {
                if (!Dialog(item, ""))
                {
                    errmsg += enter;
                    errmsg += errstr;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// SMTP��֤����.
        /// </summary>
        private bool SmtpAuth()
        {
            ArrayList SendBuffer = new ArrayList();
            string SendBufferstr;
            SendBufferstr = "EHLO " + mailserver + enter;
            //			SendBufferstr="HELO " + mailserver + enter;
            //����ط��������������λ�������Լ���������ƴ��룬��������ִ�С�
            //�Ժ�������и��õĽ���취��
            if (SendCommand(SendBufferstr))
            {
                while (true)
                {
                    int i = 0;
                    if (ns.DataAvailable)
                    {
                        string RR = RecvResponse();
                        if (RR == "false")
                        {
                            return false;
                        }
                        string RRCode = RR.Substring(0, 3);
                        if (RightCodeHT[RRCode] != null)
                        {
                            if (RR.IndexOf("AUTH") != -1)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (ErrCodeHT[RRCode] != null)
                            {
                                errmsg += (RRCode + ErrCodeHT[RRCode].ToString());
                                errmsg += enter;
                                errmsg += "����EHLO����������������ܲ���Ҫ��֤" + enter;
                            }
                            else
                            {
                                errmsg += RR;
                                errmsg += "����EHLO���������������,����������ϵ" + enter;
                            }
                            return false;
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(50);
                        i++;
                        if (i > 6)
                        {
                            errmsg += "�ղ���AUTHָ����������ӳ�ʱ�����߷�������������Ҫ��֤" + enter;
                            return false;

                        }
                    }

                }
            }
            else
            {
                errmsg += "����ehlo����ʧ��";
                return false;

            }

            SendBuffer.Add("AUTH LOGIN" + enter);
            SendBuffer.Add(Base64Encode(username) + enter);
            SendBuffer.Add(Base64Encode(password) + enter);
            if (!Dialog(SendBuffer, "SMTP��������֤ʧ�ܣ���˶��û��������롣"))
                return false;
            return true;
        }

        #endregion

        #region ����

        /// <summary>
        /// �����ʼ�
        /// </summary>
        public bool SendEmail()
        {
            bool checkFlag = Check();
            if (!checkFlag)
                return false;

            #region ��������
            try
            {
                tc = new TcpClient(mailserver, mailserverport);
                ns = tc.GetStream();
            }
            catch (Exception e)
            {
                errmsg = e.ToString();
                return false;
            }

            //��֤���������Ƿ���ȷ
            if (RightCodeHT[RecvResponse().Substring(0, 3)] == null)
            {
                errmsg = "��������ʧ��";
                return false;
            } 
            #endregion

            #region ��֤�����ռ���

            ArrayList SendBuffer = new ArrayList();
            string SendBufferstr;

            //����SMTP��֤
            if (useSmtpAuth)
            {
                if (!SmtpAuth())
                    return false;
            }
            else
            {
                SendBufferstr = "HELO " + mailserver + enter;
                if (!Dialog(SendBufferstr, ""))
                    return false;
            }

            //��������Ϣ
            SendBufferstr = "MAIL FROM:<" + From + ">" + enter;
            if (!Dialog(SendBufferstr, "�����˵�ַ���󣬻���Ϊ��"))
                return false;

            //�ռ����б�
            SendBuffer.Clear();
            foreach (String item in Recipient)
            {
                SendBuffer.Add("RCPT TO:<" + item + ">" + enter);
                RecipientName = item;//������ʵֻ��֧��һ���ռ���
            }
            if (!Dialog(SendBuffer, "�ռ��˵�ַ����"))
                return false; 

            #endregion

            #region �ʼ�ͷ��

            //��ʼ�����ż�����
            SendBufferstr = "DATA" + enter;
            if (!Dialog(SendBufferstr, ""))
                return false;
		           
            //������
            SendBufferstr = "From:\"" + FromName + "\" <" + From + ">" + enter;
            //�ռ���
            SendBufferstr += "To:\"" + RecipientName + "\" <" + RecipientName + ">" + enter;

            //�ظ���ַ
            if (ReplyTo.Trim() != "")
            {
                SendBufferstr += "Reply-To: " + ReplyTo + enter;
            }

            //�����ռ����б�
            if (RecipientCC.Count > 0)
            {
                SendBufferstr += "CC:";
                foreach (String item in RecipientCC)
                {
                    SendBufferstr += item + "<" + item + ">," + enter;
                }
                SendBufferstr = SendBufferstr.Substring(0, SendBufferstr.Length - 3) + enter;
            }

            //�ܼ��ռ����б�
            if (RecipientBCC.Count > 0)
            {
                SendBufferstr += "BCC:";
                foreach (String item in RecipientBCC)
                {
                    SendBufferstr += item + "<" + item + ">," + enter;
                }
                SendBufferstr = SendBufferstr.Substring(0, SendBufferstr.Length - 3) + enter;
            }

            //�ʼ�����
            if (Charset == "")
            {
                SendBufferstr += "Subject:" + Subject + enter;
            }
            else
            {
                SendBufferstr += "Subject:" + "=?" + Charset.ToUpper() + "?B?" + Base64Encode(Subject) + "?=" + enter;
            }

            //�Ƿ���Ҫ�ռ��˷�������
            if (true == ReturnReceipt)
            {
                SendBufferstr += "Disposition-Notification-To: \"" + FromName + "\" <" + ReplyTo + ">" + enter;
            }

	        #endregion         

            #region �ʼ�����

            SendBufferstr += "X-Priority:" + priority + enter;
            SendBufferstr += "X-MSMail-Priority:" + priority + enter;
            SendBufferstr += "Importance:" + priority + enter;
            SendBufferstr += "X-Mailer: Huolx.Pubclass" + enter;
            SendBufferstr += "MIME-Version: 1.0" + enter;

            SendBufferstr += "Content-Type: multipart/mixed;" + enter;
            SendBufferstr += "	boundary=\"" + boundary + "\"" + enter + enter;
            SendBufferstr += "This is a multi-part message in MIME format." + enter + enter;
            SendBufferstr += "--" + boundary + enter;
            SendBufferstr += "Content-Type: multipart/alternative;" + enter;
            SendBufferstr += "	boundary=\"" + boundary1 + "\"" + enter + enter + enter;
            SendBufferstr += "--" + boundary1 + enter;

            //�ж��ż���ʽ�Ƿ�html
            if (IsHtml)
            {
                SendBufferstr += "Content-Type: text/html;" + enter;
            }
            else
            {
                SendBufferstr += "Content-Type: text/plain;" + enter;
            }
            //������Ϣ
            if (Charset == "")
            {
                SendBufferstr += "	charset=\"iso-8859-1\"" + enter;
            }
            else
            {
                SendBufferstr += "	charset=\"" + Charset.ToLower() + "\"" + enter;
            }
            SendBufferstr += "Content-Transfer-Encoding: base64" + enter;

            StringBuilder extras = new StringBuilder();
            string extrasBoundary = "--" + boundary + enter;
            string newBodyHtml = FixupReferences(this.Body, ref extras, extrasBoundary);
            SendBufferstr += enter + enter;
            SendBufferstr += B64StrLine(Base64Encode(newBodyHtml)) + enter;

            SendBufferstr += enter + "--" + boundary1 + "--" + enter + enter;
            SendBufferstr += extras.ToString();

            //����и���,��ʼ���͸���.
            if (Attachments.Count > 0)
            {
                SendBufferstr += enter + "--" + boundary1 + "--" + enter + enter;
                foreach (String item in Attachments)
                {
                    SendBufferstr += "--" + boundary + enter;
                    SendBufferstr += "Content-Type: application/octet-stream;" + enter;
                    SendBufferstr += "	name=\"" + AttachmentNameStr(item.Substring(item.LastIndexOf("\\") + 1)) + "\"" + enter;
                    SendBufferstr += "Content-Transfer-Encoding: base64" + enter;
                    SendBufferstr += "Content-Disposition: attachment;" + enter;
                    SendBufferstr += "	filename=\"" + AttachmentNameStr(item.Substring(item.LastIndexOf("\\") + 1)) + "\"" + enter + enter;
                    SendBufferstr += AttachmentB64Str(item) + enter + enter;
                }
                SendBufferstr += "--" + boundary + "--" + enter + enter;
            }
            SendBufferstr += enter + "." + enter; 

            if (!Dialog(SendBufferstr, "�����ż���Ϣ"))
                return false;

            #endregion

            SendBufferstr = "QUIT" + enter;
            if (!Dialog(SendBufferstr, "�Ͽ�����ʱ����"))
                return false;
            
            ns.Close();
            tc.Close();
            return true;
        }

        /// <summary>
        /// �����ʼ�ǰ�Բ������м�飬ͨ����鷵��True������ΪFalse
        /// </summary>
        /// <returns>���ͨ����鷵��true������Ϊfalse</returns>
        private bool Check()
        {
            if (Recipient.Count == 0)
            {
                errmsg = "�ռ����б���Ϊ��";
                return false;
            }

            if (RecipientName == "")
            {
                RecipientName = Recipient[0].ToString();
            }

            if (mailserver.Trim() == "")
            {
                errmsg = "����ָ��SMTP������";
                return false;
            }

            return true;
        }

        #endregion
    }

}

