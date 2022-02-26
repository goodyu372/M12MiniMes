using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// FTP����������
    /// </summary>
    public class FTPHelper
    {
        #region ��������

        /// <summary>
        /// IP��ַ����������
        /// </summary>
        public string server;

        /// <summary>
        /// ��¼�û���
        /// </summary>
        public string user;

        /// <summary>
        /// ��¼������
        /// </summary>
        public string pass;

        /// <summary>
        /// FTP�������˿�
        /// </summary>
        public int port;

        /// <summary>
        /// ���ݵȴ���ʱʱ�䣨���룩
        /// </summary>
        public int timeout;

        #endregion

        #region Private Variables

        private string messages; // server messages
        private string responseStr; // server response if the user wants it.
        private bool passive_mode;		// #######################################
        private long bytes_total; // upload/download info if the user wants it.
        private long file_size; // gets set when an upload or download takes place
        private Socket main_sock;
        private IPEndPoint main_ipEndPoint;
        private Socket listening_sock;
        private Socket data_sock;
        private IPEndPoint data_ipEndPoint;
        private FileStream file;
        private int response;
        private string bucket;

        #endregion

        #region Constructors

        /// <summary>
        /// ���캯��
        /// </summary>
        public FTPHelper()
        {
            server = null;
            user = null;
            pass = null;
            port = 21;
            passive_mode = true;		// #######################################
            main_sock = null;
            main_ipEndPoint = null;
            listening_sock = null;
            data_sock = null;
            data_ipEndPoint = null;
            file = null;
            bucket = "";
            bytes_total = 0;
            timeout = 10000;	// 10 seconds
            messages = "";
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="server">�����ӵķ�����</param>
        /// <param name="user">��¼�û���</param>
        /// <param name="pass">��¼����</param>
        public FTPHelper(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            port = 21;
            passive_mode = true;		// #######################################
            main_sock = null;
            main_ipEndPoint = null;
            listening_sock = null;
            data_sock = null;
            data_ipEndPoint = null;
            file = null;
            bucket = "";
            bytes_total = 0;
            timeout = 10000;	// 10 seconds
            messages = "";
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="server">�����ӵ�FTP������</param>
        /// <param name="port">FTP�������˿�</param>
        /// <param name="user">��¼�û���</param>
        /// <param name="pass">��¼����</param>
        public FTPHelper(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
            passive_mode = true;		// #######################################
            main_sock = null;
            main_ipEndPoint = null;
            listening_sock = null;
            data_sock = null;
            data_ipEndPoint = null;
            file = null;
            bucket = "";
            bytes_total = 0;
            timeout = 10000;	// 10 seconds
            messages = "";
        }

        #endregion

        /// <summary>
        /// ����������״̬
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (main_sock != null)
                    return main_sock.Connected;
                return false;
            }
        }
        /// <summary>
        /// �ж���Ϣ�����Ƿ������ݴ���
        /// </summary>
        public bool MessagesAvailable
        {
            get
            {
                if (messages.Length > 0)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// ��������Ϣ�����ʸ����Ժ�ֵ�����
        /// </summary>
        public string Messages
        {
            get
            {
                string tmp = messages;
                messages = "";
                return tmp;
            }
        }

        /// <summary>
        /// ��������Ӧ������
        /// </summary>
        public string ResponseString
        {
            get
            {
                return responseStr;
            }
        }

        /// <summary>
        /// ���ͻ��߽��յ���������
        /// </summary>
        public long BytesTotal
        {
            get
            {
                return bytes_total;
            }
        }

        /// <summary>
        /// �ϴ������ص��ļ���С (���û������ָ��������Ϊ0)
        /// </summary>
        public long FileSize
        {
            get
            {
                return file_size;
            }
        }

        /// <summary>
        /// True:  Passive mode [default]
        /// False: Active Mode
        /// </summary>
        public bool PassiveMode
        {
            get
            {
                return passive_mode;
            }
            set
            {
                passive_mode = value;
            }
        }

        private void Fail()
        {
            Disconnect();
            throw new Exception(responseStr);
        }

        private void SetBinaryMode(bool mode)
        {
            if (mode)
                SendCommand("TYPE I");
            else
                SendCommand("TYPE A");

            ReadResponse();
            if (response != 200)
                Fail();
        }

        private void SendCommand(string command)
        {
            Byte[] cmd = Encoding.Default.GetBytes((command + "\r\n").ToCharArray());

#if (FTP_DEBUG)
			if (command.Length > 3 && command.Substring(0, 4) == "PASS")
				Console.WriteLine("\rPASS xxx");
			else
				Console.WriteLine("\r" + command);
#endif

            main_sock.Send(cmd, cmd.Length, 0);
        }

        private void FillBucket()
        {
            Byte[] bytes = new Byte[512];
            long bytesgot;
            int msecs_passed = 0;		// #######################################

            while (main_sock.Available < 1)
            {
                System.Threading.Thread.Sleep(50);
                msecs_passed += 50;
                // this code is just a fail safe option 
                // so the code doesn't hang if there is 
                // no data comming.
                if (msecs_passed > timeout)
                {
                    Disconnect();
                    throw new Exception("Timed out waiting on server to respond.");
                }
            }

            while (main_sock.Available > 0)
            {
                bytesgot = main_sock.Receive(bytes, 512, 0);
                bucket += Encoding.Default.GetString(bytes, 0, (int)bytesgot);
                // this may not be needed, gives any more data that hasn't arrived
                // just yet a small chance to get there.
                System.Threading.Thread.Sleep(50);
            }
        }

        private string GetLineFromBucket()
        {
            int i;
            string buf = "";

            if ((i = bucket.IndexOf('\n')) < 0)
            {
                while (i < 0)
                {
                    FillBucket();
                    i = bucket.IndexOf('\n');
                }
            }

            buf = bucket.Substring(0, i);
            bucket = bucket.Substring(i + 1);

            return buf;
        }

        // Any time a command is sent, use ReadResponse() to get the response
        // from the server. The variable responseStr holds the entire string and
        // the variable response holds the response number.
        private void ReadResponse()
        {
            string buf;
            messages = "";

            while (true)
            {
                //buf = GetLineFromBucket();
                buf = GetLineFromBucket();

#if (FTP_DEBUG)
				Console.WriteLine(buf);
#endif
                // the server will respond with "000-Foo bar" on multi line responses
                // "000 Foo bar" would be the last line it sent for that response.
                // Better example:
                // "000-This is a multiline response"
                // "000-Foo bar"
                // "000 This is the end of the response"
                if (Regex.Match(buf, "^[0-9]+ ").Success)
                {
                    responseStr = buf;
                    response = int.Parse(buf.Substring(0, 3));
                    break;
                }
                else
                    messages += Regex.Replace(buf, "^[0-9]+-", "") + "\n";
            }
        }

        // if you add code that needs a data socket, i.e. a PASV or PORT command required,
        // call this function to do the dirty work. It sends the PASV or PORT command,
        // parses out the port and ip info and opens the appropriate data socket
        // for you. The socket variable is private Socket data_socket. Once you
        // are done with it, be sure to call CloseDataSocket()
        private void OpenDataSocket()
        {
            if (passive_mode)		// #######################################
            {
                string[] pasv;
                string server;
                int port;

                Connect();
                SendCommand("PASV");
                ReadResponse();
                if (response != 227)
                    Fail();

                try
                {
                    int i1, i2;

                    i1 = responseStr.IndexOf('(') + 1;
                    i2 = responseStr.IndexOf(')') - i1;
                    pasv = responseStr.Substring(i1, i2).Split(',');
                }
                catch (Exception)
                {
                    Disconnect();
                    throw new Exception("Malformed PASV response: " + responseStr);
                }

                if (pasv.Length < 6)
                {
                    Disconnect();
                    throw new Exception("Malformed PASV response: " + responseStr);
                }

                server = String.Format("{0}.{1}.{2}.{3}", pasv[0], pasv[1], pasv[2], pasv[3]);
                port = (int.Parse(pasv[4]) << 8) + int.Parse(pasv[5]);

                try
                {
#if (FTP_DEBUG)
					Console.WriteLine("Data socket: {0}:{1}", server, port);
#endif
                    CloseDataSocket();

#if (FTP_DEBUG)
					Console.WriteLine("Creating socket...");
#endif
                    data_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

#if (FTP_DEBUG)
					Console.WriteLine("Resolving host");
#endif

                    data_ipEndPoint = new IPEndPoint(Dns.GetHostByName(server).AddressList[0], port);


#if (FTP_DEBUG)
					Console.WriteLine("Connecting..");
#endif
                    data_sock.Connect(data_ipEndPoint);

#if (FTP_DEBUG)
					Console.WriteLine("Connected.");
#endif
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to connect for data transfer: " + ex.Message);
                }
            }
            else		// #######################################
            {
                Connect();

                try
                {
#if (FTP_DEBUG)
					Console.WriteLine("Data socket (active mode)");
#endif
                    CloseDataSocket();

#if (FTP_DEBUG)
					Console.WriteLine("Creating listening socket...");
#endif
                    listening_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

#if (FTP_DEBUG)
					Console.WriteLine("Binding it to local address/port");
#endif
                    // for the PORT command we need to send our IP address; let's extract it
                    // from the LocalEndPoint of the main socket, that's already connected
                    string sLocAddr = main_sock.LocalEndPoint.ToString();
                    int ix = sLocAddr.IndexOf(':');
                    if (ix < 0)
                    {
                        throw new Exception("Failed to parse the local address: " + sLocAddr);
                    }
                    string sIPAddr = sLocAddr.Substring(0, ix);
                    // let the system automatically assign a port number (setting port = 0)
                    System.Net.IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(sIPAddr), 0);

                    listening_sock.Bind(localEP);
                    sLocAddr = listening_sock.LocalEndPoint.ToString();
                    ix = sLocAddr.IndexOf(':');
                    if (ix < 0)
                    {
                        throw new Exception("Failed to parse the local address: " + sLocAddr);
                    }
                    int nPort = int.Parse(sLocAddr.Substring(ix + 1));
#if (FTP_DEBUG)
					Console.WriteLine("Listening on {0}:{1}", sIPAddr, nPort);
#endif
                    // start to listen for a connection request from the host (note that
                    // Listen is not blocking) and send the PORT command
                    listening_sock.Listen(1);
                    string sPortCmd = string.Format("PORT {0},{1},{2}",
                                                    sIPAddr.Replace('.', ','),
                                                    nPort / 256, nPort % 256);
                    SendCommand(sPortCmd);
                    ReadResponse();
                    if (response != 200)
                        Fail();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to connect for data transfer: " + ex.Message);
                }
            }
        }

        private void ConnectDataSocket()
        {
            if (data_sock != null)		// already connected (always so if passive mode)
                return;

            try
            {
#if (FTP_DEBUG)
				Console.WriteLine("Accepting the data connection.");
#endif
                data_sock = listening_sock.Accept();	// Accept is blocking
                listening_sock.Close();
                listening_sock = null;

                if (data_sock == null)
                {
                    throw new Exception("Winsock error: " +
                        Convert.ToString(System.Runtime.InteropServices.Marshal.GetLastWin32Error()));
                }
#if (FTP_DEBUG)
				Console.WriteLine("Connected.");
#endif
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to connect for data transfer: " + ex.Message);
            }
        }

        private void CloseDataSocket()
        {
#if (FTP_DEBUG)
			Console.WriteLine("Attempting to close data channel socket...");
#endif
            if (data_sock != null)
            {
                if (data_sock.Connected)
                {
#if (FTP_DEBUG)
						Console.WriteLine("Closing data channel socket!");
#endif
                    data_sock.Close();
#if (FTP_DEBUG)
						Console.WriteLine("Data channel socket closed!");
#endif
                }
                data_sock = null;
            }

            data_ipEndPoint = null;
        }

        /// <summary>
        /// �ر�FTP����������������
        /// </summary>
        public void Disconnect()
        {
            CloseDataSocket();

            if (main_sock != null)
            {
                if (main_sock.Connected)
                {
                    SendCommand("QUIT");
                    main_sock.Close();
                }
                main_sock = null;
            }

            if (file != null)
                file.Close();

            main_ipEndPoint = null;
            file = null;
        }

        /// <summary>
        /// ���ӵ�FTP������
        /// </summary>
        /// <param name="server">FTP��������IP����������</param>
        /// <param name="port">FTP�������˿�</param>
        /// <param name="user">��¼�û���</param>
        /// <param name="pass">��¼����</param>
        public void Connect(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;

            Connect();
        }
        /// <summary>
        /// ���ӵ�FTP������
        /// </summary>
        /// <param name="server">FTP��������IP����������</param>
        /// <param name="user">��¼�û���</param>
        /// <param name="pass">��¼����</param>
        public void Connect(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;

            Connect();
        }

        /// <summary>
        /// ���ӵ�FTP������
        /// </summary>
        public void Connect()
        {
            if (server == null)
                throw new Exception("No server has been set.");
            if (user == null)
                throw new Exception("No username has been set.");

            if (main_sock != null)
                if (main_sock.Connected)
                    return;

            main_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            main_ipEndPoint = new IPEndPoint(Dns.GetHostByName(server).AddressList[0], port);

            try
            {
                main_sock.Connect(main_ipEndPoint);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ReadResponse();
            if (response != 220)
                Fail();

            SendCommand("USER " + user);
            ReadResponse();

            switch (response)
            {
                case 331:
                    if (pass == null)
                    {
                        Disconnect();
                        throw new Exception("No password has been set.");
                    }
                    SendCommand("PASS " + pass);
                    ReadResponse();
                    if (response != 230)
                        Fail();
                    break;
                case 230:
                    break;
            }

            return;
        }

        /// <summary>
        /// ��ȡFTP���������ļ���Ŀ¼�б�
        /// </summary>
        /// <returns>�ļ��б����鼯��</returns>
        public List<string> ListToString()
        {
            Byte[] bytes = new Byte[512];
            string file_list = "";
            long bytesgot = 0;
            int msecs_passed = 0;
            List<string> list = new List<string>();

            Connect();
            OpenDataSocket();
            SendCommand("LIST");
            ReadResponse();

            //FILIPE MADUREIRA.
            //Added response 125
            switch (response)
            {
                case 125:
                case 150:
                    break;
                default:
                    CloseDataSocket();
                    throw new Exception(responseStr);
            }
            ConnectDataSocket();		// #######################################

            while (data_sock.Available < 1)
            {
                System.Threading.Thread.Sleep(50);
                msecs_passed += 50;
                // this code is just a fail safe option 
                // so the code doesn't hang if there is 
                // no data comming.
                if (msecs_passed > (timeout / 10))
                {
                    //CloseDataSocket();
                    //throw new Exception("Timed out waiting on server to respond.");

                    //FILIPE MADUREIRA.
                    //If there are no files to list it gives timeout.
                    //So I wait less time and if no data is received, means that there are no files
                    break;//Maybe there are no files
                }
            }

            while (data_sock.Available > 0)
            {
                bytesgot = data_sock.Receive(bytes, bytes.Length, 0);
                file_list += Encoding.ASCII.GetString(bytes, 0, (int)bytesgot);
                System.Threading.Thread.Sleep(50); // *shrug*, sometimes there is data comming but it isn't there yet.
            }

            CloseDataSocket();

            ReadResponse();
            if (response != 226)
                throw new Exception(responseStr);

            foreach (string f in file_list.Split('\n'))
            {
                if (f.Length > 0 && !Regex.Match(f, "^total").Success)
                {
                    list.Add(f.Substring(0, f.Length - 1));
                }
            }

            return list;
        }

        /// <summary>
        /// ����ȡ�ļ��б�
        /// </summary>
        /// <returns>��ȡ���ļ��б�</returns>
        public List<string> ListFilesToString()
        {
            List<string> list = new List<string>();
            foreach (string f in ListToString())
            {
                //FILIPE MADUREIRA
                //In Windows servers it is identified by <DIR>
                if ((f.Length > 0))
                {
                    if ((f[0] != 'd') && (f.ToUpper().IndexOf("<DIR>") < 0))
                        list.Add(f);
                }
            }

            return list;
        }

        /// <summary>
        /// ����ȡĿ¼�б�
        /// </summary>
        /// <returns>��ȡ����Ŀ¼�б�</returns>
        public List<string> ListDirectoriesToString()
        {
            List<string> list = new List<string>();

            foreach (string f in ListToString())
            {
                //FILIPE MADUREIRA
                //In Windows servers it is identified by <DIR>
                if (f.Length > 0)
                {
                    if ((f[0] == 'd') || (f.ToUpper().IndexOf("<DIR>") >= 0))
                    {
                        list.Add(f);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// ����FTPԭʼ��RAW����������Ϣ (YYYYMMDDhhmmss)��
        /// ʹ��GetFileDate�ɷ���һ���Ϻõ����ڶ���
        /// </summary>
        /// <param name="fileName">��ѯ��Զ���ļ���</param>
        /// <returns>����FTPԭʼ��RAW����������Ϣ</returns>
        public string GetFileDateRaw(string fileName)
        {
            Connect();

            SendCommand("MDTM " + fileName);
            ReadResponse();
            if (response != 213)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }

            return (this.responseStr.Substring(4));
        }

        /// <summary>
        /// ��ѯFTP��������Զ���ļ�������
        /// </summary>
        /// <param name="fileName">��ѯ��Զ���ļ���</param>
        /// <returns>�ļ�������</returns>
        public DateTime GetFileDate(string fileName)
        {
            return ConvertFTPDateToDateTime(GetFileDateRaw(fileName));
        }

        private DateTime ConvertFTPDateToDateTime(string input)
        {
            if (input.Length < 14)
                throw new ArgumentException("Input Value for ConvertFTPDateToDateTime method was too short.");

            //YYYYMMDDhhmmss": 
            int year = Convert.ToInt16(input.Substring(0, 4));
            int month = Convert.ToInt16(input.Substring(4, 2));
            int day = Convert.ToInt16(input.Substring(6, 2));
            int hour = Convert.ToInt16(input.Substring(8, 2));
            int min = Convert.ToInt16(input.Substring(10, 2));
            int sec = Convert.ToInt16(input.Substring(12, 2));

            return new DateTime(year, month, day, hour, min, sec);
        }

        /// <summary>
        /// ��ȡFTP�ĵ�ǰ����Ŀ¼
        /// </summary>
        public string GetWorkingDirectory()
        {
            //PWD - print working directory
            Connect();
            SendCommand("PWD");
            ReadResponse();

            if (response != 257)
                throw new Exception(responseStr);

            string pwd;
            try
            {
                pwd = responseStr.Substring(responseStr.IndexOf("\"", 0) + 1);//5);
                pwd = pwd.Substring(0, pwd.LastIndexOf("\""));
                pwd = pwd.Replace("\"\"", "\""); // directories with quotes in the name come out as "" from the server
            }
            catch (Exception ex)
            {
                throw new Exception("Uhandled PWD response: " + ex.Message);
            }

            return pwd;
        }

        /// <summary>
        /// ��FTP�������ϸı�Ŀ¼
        /// </summary>
        public void ChangeDir(string path)
        {
            Connect();
            SendCommand("CWD " + path);
            ReadResponse();
            if (response != 250)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }
        }

        /// <summary>
        /// ��FTP�������ϴ����µ�Ŀ¼
        /// </summary>
        public void MakeDir(string dir)
        {
            Connect();
            SendCommand("MKD " + dir);
            ReadResponse();

            switch (response)
            {
                case 257:
                case 250:
                    break;
                default:
#if (FTP_DEBUG)
                    Console.Write("\r" + responseStr);
#endif
                    throw new Exception(responseStr);
            }
        }

        /// <summary>
        /// ��FTP���������Ƴ�Ŀ¼
        /// </summary>
        public void RemoveDir(string dir)
        {
            Connect();
            SendCommand("RMD " + dir);
            ReadResponse();
            if (response != 250)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }
        }

        /// <summary>
        /// ��FTP���Ƴ�һ���ļ�
        /// </summary>
        public void RemoveFile(string filename)
        {
            Connect();
            SendCommand("DELE " + filename);
            ReadResponse();
            if (response != 250)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }
        }

        /// <summary>
        /// ��FTP��������������һ���ļ�
        /// </summary>
        /// <param name="oldfilename">���ļ���</param>
        /// <param name="newfilename">���ļ���</param>
        public void RenameFile(string oldfilename, string newfilename)
        {
            Connect();
            SendCommand("RNFR " + oldfilename);
            ReadResponse();
            if (response != 350)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }
            else
            {
                SendCommand("RNTO " + newfilename);
                ReadResponse();
                if (response != 250)
                {
#if (FTP_DEBUG)
					Console.Write("\r" + responseStr);
#endif
                    throw new Exception(responseStr);
                }
            }
        }


        /// <summary>
        /// ��ȡ�ļ��Ĵ�С����ҪFTP������֧�֣�
        /// </summary>
        /// <param name="filename">�ļ���</param>
        /// <returns>ָ���ļ��Ĵ�С</returns>
        public long GetFileSize(string filename)
        {
            Connect();
            SendCommand("SIZE " + filename);
            ReadResponse();
            if (response != 213)
            {
#if (FTP_DEBUG)
				Console.Write("\r" + responseStr);
#endif
                throw new Exception(responseStr);
            }

            return Int64.Parse(responseStr.Substring(4));
        }


        /// <summary>
        /// ��һ�����ڵ��ļ������������ϴ�
        /// </summary>
        /// <param name="filename">���ϴ����ļ�</param>
        public void OpenUpload(string filename)
        {
            OpenUpload(filename, filename, false);
        }

        /// <summary>
        /// ��һ�����ڵ��ļ������������ϴ�
        /// </summary>
        /// <param name="filename">�����ϴ����ļ��������ļ�·����</param>
        /// <param name="remotefilename">������FTP�ϵ��ļ���</param>
        public void OpenUpload(string filename, string remotefilename)
        {
            OpenUpload(filename, remotefilename, false);
        }

        /// <summary>
        /// ��һ�����ڵ��ļ���֧���������ϴ�
        /// </summary>
        /// <param name="filename">�����ϴ����ļ��������ļ�·����</param>
        /// <param name="resume">����ļ����ڣ�ָ���Ƿ�����</param>
        public void OpenUpload(string filename, bool resume)
        {
            OpenUpload(filename, filename, resume);
        }

        /// <summary>
        /// ��һ�����ڵ��ļ���֧���������ϴ�
        /// </summary>
        /// <param name="filename">�����ϴ����ļ��������ļ�·����</param>
        /// <param name="remote_filename">�洢��FTP�ϵ��ļ�����</param>
        /// <param name="resume">������ڣ�ָ���Ƿ�����</param>
        public void OpenUpload(string filename, string remote_filename, bool resume)
        {
            Connect();
            SetBinaryMode(true);
            OpenDataSocket();

            bytes_total = 0;

            try
            {
                file = new FileStream(filename, FileMode.Open);
            }
            catch (Exception ex)
            {
                file = null;
                throw new Exception(ex.Message);
            }

            file_size = file.Length;

            if (resume)
            {
                long size = GetFileSize(remote_filename);
                SendCommand("REST " + size);
                ReadResponse();
                if (response == 350)
                    file.Seek(size, SeekOrigin.Begin);
            }

            SendCommand("STOR " + remote_filename);
            ReadResponse();

            switch (response)
            {
                case 125:
                case 150:
                    break;
                default:
                    if (file != null)
                    {
                        file.Close();
                        file = null;
                    }
                    throw new Exception(responseStr);
            }
            ConnectDataSocket();		// #######################################	

            return;
        }
    

        /// <summary>
        /// ������һ���ļ�����֧��������
        /// </summary>
        /// <param name="filename">FTP�ϵ�Զ���ļ���</param>
        public void OpenDownload(string filename)
        {
            OpenDownload(filename, filename, false);
        }

        /// <summary>
        /// ������һ���ļ�����ѡ�Ƿ�������
        /// </summary>
        /// <param name="filename">FTP�ϵ�Զ���ļ���</param>
        /// <param name="resume">������ڣ�ָ���Ƿ�����</param>
        public void OpenDownload(string filename, bool resume)
        {
            OpenDownload(filename, filename, resume);
        }

        /// <summary>
        /// ������һ���ļ�����֧��������
        /// </summary>
        /// <param name="filename">FTP�ϵ�Զ���ļ���</param>
        /// <param name="localfilename">�����ļ����������ļ�·����</param>
        public void OpenDownload(string filename, string localfilename)
        {
            OpenDownload(filename, localfilename, false);
        }

        /// <summary>
        /// ������һ���ļ�
        /// </summary>
        /// <param name="remote_filename">FTP�ϵ�Զ���ļ���</param>
        /// <param name="local_filename">���Ϊ���ļ�����(�����ļ�·��)</param>
        /// <param name="resume">������ڣ�ָ���Ƿ�����</param>
        public void OpenDownload(string remote_filename, string local_filename, bool resume)
        {
            Connect();
            SetBinaryMode(true);

            bytes_total = 0;

            try
            {
                file_size = GetFileSize(remote_filename);
            }
            catch
            {
                file_size = 0;
            }

            if (resume && File.Exists(local_filename))
            {
                try
                {
                    file = new FileStream(local_filename, FileMode.Open);
                }
                catch (Exception ex)
                {
                    file = null;
                    throw new Exception(ex.Message);
                }

                SendCommand("REST " + file.Length);
                ReadResponse();
                if (response != 350)
                    throw new Exception(responseStr);

                file.Seek(file.Length, SeekOrigin.Begin);
                bytes_total = file.Length;
            }
            else
            {
                try
                {
                    file = new FileStream(local_filename, FileMode.Create);
                }
                catch (Exception ex)
                {
                    file = null;
                    throw new Exception(ex.Message);
                }
            }

            OpenDataSocket();
            SendCommand("RETR " + remote_filename);
            ReadResponse();

            switch (response)
            {
                case 125:
                case 150:
                    break;
                default:
                    if (file != null)
                    {
                        file.Close();
                        file = null;
                    }
                    throw new Exception(responseStr);

            }
            ConnectDataSocket();		// #######################################	

            return;
        }

        /// <summary>
        /// �ϴ��ļ���ѭ������ֱ���ļ�ȫ���ϴ����
        /// </summary>
        /// <returns>���͵��ֽ������С</returns>
        public long DoUpload()
        {
            Byte[] bytes = new Byte[512];
            long bytes_got;

            try
            {
                while ((bytes_got = file.Read(bytes, 0, bytes.Length)) > 0)
                {
                    bytes_total += bytes_got;
                    data_sock.Send(bytes, (int)bytes_got, 0);
                }

                // the upload is complete or an error occured
                if (file != null)
                {
                    file.Close();
                    file = null;
                }
                CloseDataSocket();
                ReadResponse();

                switch (response)
                {
                    case 226:
                    case 250:
                        break;
                    default:
                        throw new Exception(responseStr);
                }

                SetBinaryMode(false);
            }
            catch (Exception ex)
            {
                if (file != null)
                {
                    file.Close();
                    file = null;
                }

                CloseDataSocket();
                ReadResponse();
                SetBinaryMode(false);
                throw ex;
            }

            return bytes_total;
        }

        /// <summary>
        /// �����ļ���ѭ������ֱ���ļ�ȫ���������
        /// </summary>
        /// <returns>�յ����ֽ������С</returns>
        public long DoDownload()
        {
            Byte[] bytes = new Byte[512];
            long bytes_got;

            try
            {
                while ((bytes_got = data_sock.Receive(bytes, bytes.Length, 0)) > 0)
                {
                    file.Write(bytes, 0, (int)bytes_got);
                    bytes_total += bytes_got;
                };

                // the download is done or an error occured
                CloseDataSocket();
                if (file != null)
                {
                    file.Close();
                    file = null;
                }

                ReadResponse();
                switch (response)
                {
                    case 226:
                    case 250:
                        break;
                    default:
                        throw new Exception(responseStr);
                }

                SetBinaryMode(false);
            }
            catch (Exception ex)
            {
                CloseDataSocket();
                if (file != null)
                {
                    file.Close();
                    file = null;
                }
                ReadResponse();
                SetBinaryMode(false);
                throw ex;
            }

            return bytes_total;
        }

        /// <summary>
        /// ����ȡ�ļ��б�
        /// </summary>
        /// <returns>��ȡ���ļ��б�</returns>
        public List<FileStruct> ListFiles()
        {
            var dataRecords = ListFilesToString();
            return ConvertStruct(dataRecords);
        }
        /// <summary>
        /// ����ȡĿ¼�б�
        /// </summary>
        /// <returns>��ȡ����Ŀ¼�б�</returns>
        public List<FileStruct> ListDirectories()
        {
            var dataRecords = ListDirectoriesToString();
            return ConvertStruct(dataRecords);
        }

        /// <summary>
        /// ����ļ���Ŀ¼�б�
        /// </summary>
        public List<FileStruct> List()
        {
            var dataRecords = ListToString();
            return ConvertStruct(dataRecords);
        }

        /// <summary>
        /// ת����ȡ����Ŀ¼�����ļ�Ϊ��Ӧ�Ķ����б�
        /// </summary>
        /// <param name="dataRecords">Ŀ¼�����ļ����ַ���</param>
        /// <returns></returns>
        private List<FileStruct> ConvertStruct(List<string> dataRecords)
        {
            List<FileStruct> list = new List<FileStruct>();
            FileListStyle listStyle = GuessFileListStyle(dataRecords);
            foreach (string s in dataRecords)
            {
                if (listStyle != FileListStyle.Unknown && s != "")
                {
                    FileStruct f = new FileStruct();
                    f.Name = "..";
                    switch (listStyle)
                    {
                        case FileListStyle.UnixStyle:
                            f = ParseFileStructFromUnixStyle(s);
                            break;
                        case FileListStyle.WindowsStyle:
                            f = ParseFileStructFromWindowsStyle(s);
                            break;
                    }
                    if (!(f.Name == "." || f.Name == ".."))
                    {
                        list.Add(f);
                    }
                }
            }
            return list;
        }
        

        /// <summary>
        /// ��Windows��ʽ�з����ļ���Ϣ
        /// </summary>
        /// <param name="record">�ļ���Ϣ</param>
        public FileStruct ParseFileStructFromWindowsStyle(string record)
        {
            FileStruct f = new FileStruct();
            string processstr = record.Trim();
            string dateStr = processstr.Substring(0, 8);
            processstr = (processstr.Substring(8, processstr.Length - 8)).Trim();
            string timeStr = processstr.Substring(0, 7);
            processstr = (processstr.Substring(7, processstr.Length - 7)).Trim();

            DateTimeFormatInfo dtFormat = new CultureInfo("en-US", false).DateTimeFormat;
            dtFormat.ShortTimePattern = "t";
            f.CreateTime = DateTime.Parse(dateStr + " " + timeStr, dtFormat);
            if (processstr.Substring(0, 5) == "<DIR>")
            {
                f.IsDirectory = true;
                processstr = (processstr.Substring(5, processstr.Length - 5)).Trim();
            }
            else
            {
                string[] strs = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);   // true);
                processstr = strs[1];
                f.IsDirectory = false;
            }
            f.Name = processstr;
            return f;
        }


        /// <summary>
        /// �ж��ļ��б�ķ�ʽWindow��ʽ����Unix��ʽ
        /// </summary>
        /// <param name="recordList">�ļ���Ϣ�б�</param>
        public FileListStyle GuessFileListStyle(List<string> recordList)
        {
            foreach (string s in recordList)
            {
                if (s.Length > 10
                 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                {
                    return FileListStyle.UnixStyle;
                }
                else if (s.Length > 8
                 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                {
                    return FileListStyle.WindowsStyle;
                }
            }
            return FileListStyle.Unknown;
        }

        /// <summary>
        /// ��Unix��ʽ�з����ļ���Ϣ
        /// </summary>
        /// <param name="record">�ļ���Ϣ</param>
        public FileStruct ParseFileStructFromUnixStyle(string record)
        {
            FileStruct f = new FileStruct();
            string processstr = record.Trim();
            f.Flags = processstr.Substring(0, 10);
            f.IsDirectory = (f.Flags[0] == 'd');
            processstr = (processstr.Substring(11)).Trim();

            CutStringByRule(ref processstr, ' ', 0);   //����һ����
            f.Owner = CutStringByRule(ref processstr, ' ', 0);
            f.Group = CutStringByRule(ref processstr, ' ', 0);
            CutStringByRule(ref processstr, ' ', 0);   //����һ����

            string yearOrTime = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
            if (yearOrTime.IndexOf(":") >= 0)  //time
            {
                processstr = processstr.Replace(yearOrTime, DateTime.Now.Year.ToString());
            }
            f.CreateTime = DateTime.Parse(CutStringByRule(ref processstr, ' ', 8));
            f.Name = processstr;   //����������
            return f;
        }

        /// <summary>
        /// ����һ���Ĺ�������ַ�����ȡ
        /// </summary>
        /// <param name="s">��ȡ���ַ���</param>
        /// <param name="c">���ҵ��ַ�</param>
        /// <param name="startIndex">���ҵ�λ��</param>
        private string CutStringByRule(ref string s, char c, int startIndex)
        {
            int pos1 = s.IndexOf(c, startIndex);
            string retString = s.Substring(0, pos1);
            s = (s.Substring(pos1)).Trim();
            return retString;
        }

        /// <summary>         
        /// �жϵ�ǰĿ¼��ָ������Ŀ¼���ļ��Ƿ����         
        /// </summary>         
        /// <param name="remoteName">ָ����Ŀ¼���ļ���</param>        
        public bool IsExist(string remoteName)
        {
            var list = List();
            if (list.Count(m => m.Name == remoteName) > 0)
                return true;
            return false;
        }

        /// <summary>         
        /// �жϵ�ǰĿ¼��ָ����һ����Ŀ¼�Ƿ����         
        /// </summary>         
        /// <param name="RemoteDirectoryName">ָ����Ŀ¼��</param>        
        public bool IsDirectoryExist(string remoteDirectoryName)
        {
            var listDir = ListDirectories();
            if (listDir.Count(m => m.Name == remoteDirectoryName) > 0)
                return true;
            return false;
        }

        /// <summary>         
        /// �жϵ�ǰĿ¼��ָ�������ļ��Ƿ����        
        /// </summary>         
        /// <param name="RemoteFileName">Զ���ļ���</param>         
        public bool IsFileExist(string remoteFileName)
        {
            var listFile = ListFiles();
            if (listFile.Count(m => m.Name == remoteFileName) > 0)
                return true;
            return false;
        }  
  
    }

    /// <summary>
    /// �ļ���Ϣ�ṹ
    /// </summary>
    public struct FileStruct
    {
        /// <summary>
        /// Ȩ�ޱ�־
        /// </summary>
        public string Flags;
        /// <summary>
        /// �ļ�ӵ����
        /// </summary>
        public string Owner;
        /// <summary>
        /// ����
        /// </summary>
        public string Group;

        /// <summary>
        /// �Ƿ�Ŀ¼
        /// </summary>
        public bool IsDirectory;

        /// <summary>
        /// �ļ���Ŀ¼����ʱ��
        /// </summary>
        public DateTime CreateTime;

        /// <summary>
        /// �ļ���Ŀ¼����
        /// </summary>
        public string Name;
    }

    public enum FileListStyle
    {
        UnixStyle,
        WindowsStyle,
        Unknown
    }
}
