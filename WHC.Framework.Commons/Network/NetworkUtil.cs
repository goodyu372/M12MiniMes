using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Management;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// ������ز���������
    /// </summary>
    public class NetworkUtil
    {
        /// <summary>
        /// ��ȡMAC��ַ
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            string mac = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            return mac;
        }

        /// <summary>
        /// ��ȡ��̫�����������ַ
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress2()
        {
            NetworkInterface[] aclLocalNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            string result = "";

            // �ж�����������mac��ַ
            foreach (NetworkInterface adapter in aclLocalNetworkInterfaces)
            {
                // ��������̫������
                if (!IsEthernet(adapter))
                {
                    continue;
                }

                // ��mac��ַ��֯Ϊ00:11:22:33:44:55�ĸ�ʽ
               result = GetMacAddr(adapter);
            }
            return result;
        }

        // ��ȡmac��ַ
        private static string GetMacAddr(NetworkInterface adapter)
        {
            String strMacAddr = "";
            PhysicalAddress clMacAddr = adapter.GetPhysicalAddress();
            byte[] abMacAddr = clMacAddr.GetAddressBytes();

            for (int i = 0; i < abMacAddr.Length; i++)
            {
                strMacAddr = strMacAddr + abMacAddr[i].ToString("X2");

                // ��ÿ���ֽڼ����ð��
                if (abMacAddr.Length - 1 != i)
                {
                    strMacAddr = strMacAddr + ":";
                }
            }

            return strMacAddr;
        }

        // ��������̫������
        private static  bool IsEthernet(NetworkInterface adapter)
        {
            if (NetworkInterfaceType.Ethernet == adapter.NetworkInterfaceType)
            {
                return true;
            }

            return false;
        }
                
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetNetCardList()
        {
            List<KeyValuePair<string, string>> cardList = new List<KeyValuePair<string, string>>();
            try
            {
                RegistryKey regNetCards = Registry.LocalMachine.OpenSubKey(Win32Utils.REG_NET_CARDS_KEY);
                if (regNetCards != null)
                {
                    string[] names = regNetCards.GetSubKeyNames();
                    RegistryKey subKey = null;
                    foreach (string name in names)
                    {
                        subKey = regNetCards.OpenSubKey(name);
                        if (subKey != null)
                        {
                            object o = subKey.GetValue("ServiceName");
                            object Description = subKey.GetValue("Description");
                            if (o != null)
                            {
                                KeyValuePair<string, string> p = new KeyValuePair<string, string>(o.ToString(), Description.ToString());
                                cardList.Add(p);
                            }
                        }
                    }
                }
            }
            catch { }

            return cardList;
        }

        /// <summary>
        /// ��ȡδ���޸Ĺ���MAC��ַ����ʵ�������ַ��
        /// </summary>
        /// <param name="cardId">����ID</param>
        /// <returns></returns>
        public static string GetPhysicalAddr(string cardId)
        {
            string macAddress = string.Empty;
            uint device = 0;
            try
            {
                string driveName = "\\\\.\\" + cardId;
                device = Win32Utils.CreateFile(driveName,
                                         Win32Utils.GENERIC_READ | Win32Utils.GENERIC_WRITE,
                                         Win32Utils.FILE_SHARE_READ | Win32Utils.FILE_SHARE_WRITE,
                                         0, Win32Utils.OPEN_EXISTING, 0, 0);
                if (device != Win32Utils.INVALID_HANDLE_VALUE)
                {
                    byte[] outBuff = new byte[6];
                    uint bytRv = 0;
                    int intBuff = Win32Utils.PERMANENT_ADDRESS;

                    if (0 != Win32Utils.DeviceIoControl(device, Win32Utils.IOCTL_NDIS_QUERY_GLOBAL_STATS,
                                        ref intBuff, 4, outBuff, 6, ref bytRv, 0))
                    {
                        string temp = string.Empty;
                        foreach (byte b in outBuff)
                        {
                            temp = Convert.ToString(b, 16).PadLeft(2, '0');
                            macAddress += temp;
                            temp = string.Empty;
                        }
                    }
                }
            }
            finally
            {
                if (device != 0)
                {
                    Win32Utils.CloseHandle(device);
                }
            }

            return macAddress;
        }

        /// <summary>
        /// ��ȡIP��ַ
        /// </summary>
        public static string GetIPAddress()
        {
            string st = "";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    //st=mo["IpAddress"].ToString();
                    System.Array ar;
                    ar = (System.Array)(mo.Properties["IpAddress"].Value);
                    st = ar.GetValue(0).ToString();
                    break;
                }
            }
            moc = null;
            mc = null;
            return st;
        }

        /// <summary>
        /// ��ȡ���ػ���IP��ַ
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {        
            //string strHostIP = string.Empty;
            //IPHostEntry oIPHost = Dns.GetHostEntry(Environment.MachineName);
            //if (oIPHost.AddressList.Length > 0)
            //{
            //    strHostIP = oIPHost.AddressList[0].ToString();
            //}
            //return strHostIP;
           return GetIPAddress();
        }

        /// <summary>
        /// ������õ�IP��ַ�Ƿ���ȷ����������ȷ��IP��ַ,��ЧIP��ַ����"-1"��
        /// </summary>
        /// <param name="ip">���õ�IP��ַ</param>
        /// <returns>�Ƿ�IP �򷵻� -1 </returns>
        public static string GetValidIP(string ip)
        {
            if (ValidateUtil.IsIP(ip))
            {
                return ip;
            }
            else
            {
                return "-1";
            }
        }

        /// <summary>
        /// ������õĶ˿ں��Ƿ���ȷ����������ȷ�Ķ˿ں�,��Ч�˿ںŷ���-1��
        /// </summary>
        /// <param name="port">���õĶ˿ں�</param>        
        public static int GetValidPort(string port)
        {
            //�������ص���ȷ�˿ں�
            int validPort = -1;

            validPort = ConvertHelper.ConvertTo<int>(port);

            //��С��Ч�˿ں�
            const int MINPORT = 0;
            //�����Ч�˿ں�
            const int MAXPORT = 65535;

            if (validPort <= MINPORT || validPort > MAXPORT)
            {
                throw new ArgumentException("����port�˿ںŷ�Χ��Ч��");
            }

            return validPort;
        }

        /// <summary>
        /// ���ַ�����ʽ��IP��ַת����IPAddress����
        /// </summary>
        /// <param name="ip">�ַ�����ʽ��IP��ַ</param>        
        public static IPAddress StringToIPAddress(string ip)
        {
            return IPAddress.Parse(ip);
        }

        /// <summary>
        /// ���ָ����IP��ַ�Ƿ�������IP����
        /// </summary>
        /// <param name="ip">ָ����IP��ַ</param>
        /// <param name="begip">��ʼip</param>
        /// <param name="endip">����ip</param>
        /// <returns></returns>
        public static bool IsInIp(string ip, string begip, string endip)
        {
            int[] inip, begipint, endipint = new int[4];
            inip = GetIp(ip);
            begipint = GetIp(begip);
            endipint = GetIp(endip);
            for (int i = 0; i < 4; i++)
            {
                if (inip[i] < begipint[i] || inip[i] > endipint[i])
                {
                    return false;
                }
                else if (inip[i] > begipint[i] || inip[i] < endipint[i])
                {
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// ��ip��ַת����������
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        protected static int[] GetIp(string ip)
        {
            int[] retip = new int[4];
            int i, count;
            char c;
            for (i = 0, count = 0; i < ip.Length; i++)
            {
                c = ip[i];
                if (c != '.')
                {
                    retip[count] = retip[count] * 10 + int.Parse(c.ToString());
                }
                else
                {
                    count++;
                }
            }

            return retip;

        }

        /// <summary>
        /// ��ȡ�����ļ������
        /// </summary>
        public static string LocalHostName
        {
            get
            {
                return Dns.GetHostName();
            }
        }

        /// <summary>
        /// ��ȡ�����ľ�����IP
        /// </summary>        
        public static string LANIP
        {
            get
            {
                //��ȡ������IP�б�,IP�б��еĵ�һ���Ǿ�����IP���ڶ����ǹ�����IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //�������IP�б�Ϊ�գ��򷵻ؿ��ַ���
                if (addressList.Length < 1)
                {
                    return "";
                }

                //���ر����ľ�����IP
                return addressList[0].ToString();
            }
        }

        /// <summary>
        /// ��ȡ������Internet����Ĺ�����IP
        /// </summary>        
        public static string WANIP
        {
            get
            {
                //��ȡ������IP�б�,IP�б��еĵ�һ���Ǿ�����IP���ڶ����ǹ�����IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //�������IP�б�С��2���򷵻ؿ��ַ���
                if (addressList.Length < 2)
                {
                    return "";
                }

                //���ر����Ĺ�����IP
                return addressList[1].ToString();
            }
        }

        /// <summary>
        /// ��ȡԶ�̿ͻ�����IP��ַ
        /// </summary>
        /// <param name="clientSocket">�ͻ��˵�socket����</param>        
        public static string GetClientIP(Socket clientSocket)
        {
            IPEndPoint client = (IPEndPoint)clientSocket.RemoteEndPoint;
            return client.Address.ToString();
        }

        /// <summary>
        /// ����һ��IPEndPoint����
        /// </summary>
        /// <param name="ip">IP��ַ</param>
        /// <param name="port">�˿ں�</param>        
        public static IPEndPoint CreateIPEndPoint(string ip, int port)
        {
            IPAddress ipAddress = StringToIPAddress(ip);
            return new IPEndPoint(ipAddress, port);
        }

        /// <summary>
        /// ����һ���Զ�����IP�Ͷ˿ڵ�TcpListener����
        /// </summary>        
        public static TcpListener CreateTcpListener()
        {
            //����һ���Զ����������ڵ�
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 0);

            return new TcpListener(localEndPoint);
        }
        /// <summary>
        /// ����һ��TcpListener����
        /// </summary>
        /// <param name="ip">IP��ַ</param>
        /// <param name="port">�˿�</param>        
        public static TcpListener CreateTcpListener(string ip, int port)
        {
            //����һ������ڵ�
            IPAddress ipAddress = StringToIPAddress(ip);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            return new TcpListener(localEndPoint);
        }

        /// <summary>
        /// ����һ������TCPЭ���Socket����
        /// </summary>        
        public static Socket CreateTcpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// ����һ������UDPЭ���Socket����
        /// </summary>        
        public static Socket CreateUdpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        #region ��ȡTcpListener����ı����ս��
        /// <summary>
        /// ��ȡTcpListener����ı����ս��
        /// </summary>
        /// <param name="tcpListener">TcpListener����</param>        
        public static IPEndPoint GetLocalPoint(TcpListener tcpListener)
        {
            return (IPEndPoint)tcpListener.LocalEndpoint;
        }

        /// <summary>
        /// ��ȡTcpListener����ı����ս���IP��ַ
        /// </summary>
        /// <param name="tcpListener">TcpListener����</param>        
        public static string GetLocalPoint_IP(TcpListener tcpListener)
        {
            IPEndPoint localEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
            return localEndPoint.Address.ToString();
        }

        /// <summary>
        /// ��ȡTcpListener����ı����ս��Ķ˿ں�
        /// </summary>
        /// <param name="tcpListener">TcpListener����</param>        
        public static int GetLocalPoint_Port(TcpListener tcpListener)
        {
            IPEndPoint localEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
            return localEndPoint.Port;
        }

        /// <summary>  
        /// ��ȡ�����ѱ�ʹ�õ�����˵�  
        /// </summary>  
        public IList<IPEndPoint> GetUsedIPEndPoint()
        {
            //��ȡһ�����󣬸ö����ṩ�йر��ؼ�������������Ӻ�ͨ��ͳ�����ݵ���Ϣ��  
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //��ȡ�йر��ؼ�����ϵ� Internet Э��汾 4 (IPV4) �������Э�� (TCP) ���������ս����Ϣ��  
            IPEndPoint[] ipEndPointTCP = ipGlobalProperties.GetActiveTcpListeners();

            //��ȡ�йر��ؼ�����ϵ� Internet Э��汾 4 (IPv4) �û����ݱ�Э�� (UDP) ����������Ϣ��  
            IPEndPoint[] ipEndPointUDP = ipGlobalProperties.GetActiveUdpListeners();

            //��ȡ�йر��ؼ�����ϵ� Internet Э��汾 4 (IPV4) �������Э�� (TCP) ���ӵ���Ϣ��  
            TcpConnectionInformation[] tcpConnectionInformation = ipGlobalProperties.GetActiveTcpConnections();

            IList<IPEndPoint> allIPEndPoint = new List<IPEndPoint>();
            foreach (IPEndPoint iep in ipEndPointTCP) allIPEndPoint.Add(iep);
            foreach (IPEndPoint iep in ipEndPointUDP) allIPEndPoint.Add(iep);
            foreach (TcpConnectionInformation tci in tcpConnectionInformation) allIPEndPoint.Add(tci.LocalEndPoint);

            return allIPEndPoint;
        }

        /// <summary>  
        /// �ж�ָ��������˵㣨ֻ�ж϶˿ڣ��Ƿ�ʹ��  
        /// </summary>  
        public bool IsUsedIPEndPoint(int port)
        {
            foreach (IPEndPoint iep in GetUsedIPEndPoint())
            {
                if (iep.Port == port)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>  
        /// �ж�ָ��������˵㣨�ж�IP�Ͷ˿ڣ��Ƿ�ʹ��  
        /// </summary>  
        public bool IsUsedIPEndPoint(string ip, int port)
        {
            foreach (IPEndPoint iep in GetUsedIPEndPoint())
            {
                if (iep.Address.ToString() == ip && iep.Port == port)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region ��ȡSocket����ı����ս��
        /// <summary>
        /// ��ȡSocket����ı����ս��
        /// </summary>
        /// <param name="socket">Socket����</param>        
        public static IPEndPoint GetLocalPoint(Socket socket)
        {
            return (IPEndPoint)socket.LocalEndPoint;
        }

        /// <summary>
        /// ��ȡSocket����ı����ս���IP��ַ
        /// </summary>
        /// <param name="socket">Socket����</param>        
        public static string GetLocalPoint_IP(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint)socket.LocalEndPoint;
            return localEndPoint.Address.ToString();
        }

        /// <summary>
        /// ��ȡSocket����ı����ս��Ķ˿ں�
        /// </summary>
        /// <param name="socket">Socket����</param>        
        public static int GetLocalPoint_Port(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint)socket.LocalEndPoint;
            return localEndPoint.Port;
        }
        #endregion

        /// <summary>
        /// ���ս��
        /// </summary>
        /// <param name="socket">Socket����</param>
        /// <param name="endPoint">Ҫ�󶨵��ս��</param>
        public static void BindEndPoint(Socket socket, IPEndPoint endPoint)
        {
            if (!socket.IsBound)
            {
                socket.Bind(endPoint);
            }
        }

        /// <summary>
        /// ���ս��
        /// </summary>
        /// <param name="socket">Socket����</param>        
        /// <param name="ip">������IP��ַ</param>
        /// <param name="port">�������˿�</param>
        public static void BindEndPoint(Socket socket, string ip, int port)
        {
            //�����ս��
            IPEndPoint endPoint = CreateIPEndPoint(ip, port);

            //���ս��
            if (!socket.IsBound)
            {
                socket.Bind(endPoint);
            }
        }

        /// <summary>
        /// ָ��Socket����ִ�м�����Ĭ�������������������Ϊ100
        /// </summary>
        /// <param name="socket">ִ�м�����Socket����</param>
        /// <param name="port">�����Ķ˿ں�</param>
        public static void StartListen(Socket socket, int port)
        {
            //���������ս��
            IPEndPoint localPoint = CreateIPEndPoint(NetworkUtil.LocalHostName, port);

            //�󶨵������ս��
            BindEndPoint(socket, localPoint);

            //��ʼ����
            socket.Listen(100);
        }

        /// <summary>
        /// ָ��Socket����ִ�м���
        /// </summary>
        /// <param name="socket">ִ�м�����Socket����</param>
        /// <param name="port">�����Ķ˿ں�</param>
        /// <param name="maxConnection">�����������������</param>
        public static void StartListen(Socket socket, int port, int maxConnection)
        {
            //���������ս��
            IPEndPoint localPoint = CreateIPEndPoint(NetworkUtil.LocalHostName, port);

            //�󶨵������ս��
            BindEndPoint(socket, localPoint);

            //��ʼ����
            socket.Listen(maxConnection);
        }

        /// <summary>
        /// ָ��Socket����ִ�м���
        /// </summary>
        /// <param name="socket">ִ�м�����Socket����</param>
        /// <param name="ip">������IP��ַ</param>
        /// <param name="port">�����Ķ˿ں�</param>
        /// <param name="maxConnection">�����������������</param>
        public static void StartListen(Socket socket, string ip, int port, int maxConnection)
        {
            //�󶨵������ս��
            BindEndPoint(socket, ip, port);

            //��ʼ����
            socket.Listen(maxConnection);
        }

        /// <summary>
        /// ���ӵ�����TCPЭ��ķ�����,���ӳɹ�����true�����򷵻�false
        /// </summary>
        /// <param name="socket">Socket����</param>
        /// <param name="ip">������IP��ַ</param>
        /// <param name="port">�������˿ں�</param>     
        public static bool Connect(Socket socket, string ip, int port)
        {
            //���ӷ�����
            socket.Connect(ip, port);

            //�������״̬
            return socket.Poll(-1, SelectMode.SelectWrite);

        }

        /// <summary>
        /// ��ͬ����ʽ��ָ����Socket��������Ϣ
        /// </summary>
        /// <param name="socket">socket����</param>
        /// <param name="msg">���͵���Ϣ</param>
        public static void SendMsg(Socket socket, byte[] msg)
        {
            //������Ϣ
            socket.Send(msg, msg.Length, SocketFlags.None);
        }

        /// <summary>
        /// ʹ��UTF8�����ʽ��ͬ����ʽ��ָ����Socket��������Ϣ
        /// </summary>
        /// <param name="socket">socket����</param>
        /// <param name="msg">���͵���Ϣ</param>
        public static void SendMsg(Socket socket, string msg)
        {
            //���ַ�����Ϣת�����ַ�����
            byte[] buffer = ConvertHelper.StringToBytes(msg);

            //������Ϣ
            socket.Send(buffer, buffer.Length, SocketFlags.None);
        }

        /// <summary>
        /// ��ͬ����ʽ������Ϣ
        /// </summary>
        /// <param name="socket">socket����</param>
        /// <param name="buffer">������Ϣ�Ļ�����</param>
        public static void ReceiveMsg(Socket socket, byte[] buffer)
        {
            socket.Receive(buffer);
        }

        /// <summary>
        /// ��ͬ����ʽ������Ϣ����ת��ΪUTF8�����ʽ���ַ���,ʹ��5000�ֽڵ�Ĭ�ϻ��������ա�
        /// </summary>
        /// <param name="socket">socket����</param>        
        public static string ReceiveMsg(Socket socket)
        {
            //������ջ�����
            byte[] buffer = new byte[5000];
            //�������ݣ���ȡ���յ����ֽ���
            int receiveCount = socket.Receive(buffer);

            //������ʱ������
            byte[] tempBuffer = new byte[receiveCount];
            //�����յ�������д����ʱ������
            Buffer.BlockCopy(buffer, 0, tempBuffer, 0, receiveCount);
            //ת�����ַ����������䷵��
            return ConvertHelper.BytesToString(tempBuffer);
        }

        /// <summary>
        /// �رջ���TcpЭ���Socket����
        /// </summary>
        /// <param name="socket">Ҫ�رյ�Socket����</param>
        public static void Close(Socket socket)
        {
            try
            {
                //��ֹSocket������պͷ�������
                socket.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
            finally
            {
                //�ر�Socket����
                socket.Close();
            }
        }

        /// <summary>
        /// ��Ȿ���Ƿ���������������
        /// </summary>
        /// <param name="connectionDescription"></param>
        /// <param name="reservedValue"></param>
        /// <returns></returns>
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        
        /// <summary>
        /// ��Ȿ���Ƿ�����
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedInternet()
        {
            int i = 0;
            if (InternetGetConnectedState(out i, 0))
            {
                //������
                return true;
            }
            else
            {
                //δ����
                return false;
            }

        }

        [DllImport("WININET", CharSet = CharSet.Auto)]
        private static extern bool InternetGetConnectedState(ref InternetConnectionStatesType lpdwFlags, int dwReserved);

        /// <summary>
        /// ��Ȿ���Ƿ���������������
        /// </summary>
        public static InternetConnectionStatesType CurrentState
        {
            get
            {
                InternetConnectionStatesType state = 0;

                InternetGetConnectedState(ref state, 0);

                return state;
            }
        }

        /// <summary>
        /// ��Ȿ���Ƿ���������������
        /// </summary>
        /// <returns></returns>
        public static bool IsOnline()
        {
            InternetConnectionStatesType connectionStatus = CurrentState;
            return (!IsFlagged((int)InternetConnectionStatesType.Offline, (int)connectionStatus));
        }

        internal static bool IsFlagged(int flaggedEnum, int flaggedValue)
        {
            if ((flaggedEnum & flaggedValue) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ת����������DNS��IP��ַ
        /// </summary>
        /// <param name="hostname">��������DNS</param>
        /// <returns></returns>
        public static string ConvertDnsToIp(string hostname)
        {
            IPHostEntry ipHostEntry = Dns.GetHostByName(hostname);

            if (ipHostEntry != null)
            {
                return ipHostEntry.AddressList[0].ToString();
            }
            return null;
        }


        /// <summary>
        /// ת������IP��ַ��DNS����
        /// </summary>
        /// <param name="ipAddress">����IP��ַ</param>
        /// <returns></returns>
        public static string ConvertIpToDns(string ipAddress)
        {
            IPHostEntry ipHostEntry = Dns.Resolve(ipAddress);

            if (ipHostEntry != null)
            {
                return ipHostEntry.HostName;
            }
            return null;
        }

        /// <summary>
        /// ����IP�˵��ȡ��������
        /// </summary>
        /// <param name="ipEndPoint">IP�˵�</param>
        /// <returns></returns>
        public static string GetHostName(IPEndPoint ipEndPoint)
        {
            return GetHostName(ipEndPoint.Address);
        }

        /// <summary>
        /// ��������IP��ַ�����ȡ��������
        /// </summary>
        /// <param name="ip">����IP��ַ����</param>
        /// <returns></returns>
        public static string GetHostName(IPAddress ip)
        {
            return GetHostName(ip.ToString());
        }

        /// <summary>
        /// ��������IP��ȡ��������
        /// </summary>
        /// <param name="hostIP">����IP</param>
        /// <returns></returns>
        public static string GetHostName(string hostIP)
        {
            try
            {
                return Dns.Resolve(hostIP).HostName;
            }
            catch
            {
            }
            return null;

        }

        /// <summary>
        /// �õ�һ̨������EndPoint�˵�
        /// </summary>
        /// <param name="entry">����ʵ��</param>
        /// <returns></returns>
        public static EndPoint GetNetworkAddressEndPoing(IPHostEntry entry)
        {
            return (new IPEndPoint(entry.AddressList[0], 0)) as EndPoint;
        }

        /// <summary>
        /// �������Ƿ����
        /// </summary>
        /// <param name="host">������</param>
        /// <returns></returns>
        public static bool IsHostAvailable(string host)
        {
            return (ResolveHost(host) != null);
        }


        /// <summary>
        /// ��������������һ��IP����ʵ��
        /// </summary>
        /// <param name="host">������</param>
        /// <returns></returns>
        public static IPHostEntry ResolveHost(string host)
        {
            try { return Dns.Resolve(host); }
            catch { }

            return null;
        }

    }

    /// <summary>
    /// Internet����״̬ö��
    /// </summary>
    [Flags]
    public enum InternetConnectionStatesType : int
    {
        ModemConnection = 0x1,
        LANConnection = 0x2,
        ProxyConnection = 0x4,
        RASInstalled = 0x10,
        Offline = 0x20,
        ConnectionConfigured = 0x40
    }

    #region Win32Utils
    internal class Win32Utils
    {
        public const string REG_NET_CARDS_KEY = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\NetworkCards";
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint FILE_SHARE_READ = 0x00000001;
        public const uint FILE_SHARE_WRITE = 0x00000002;
        public const uint OPEN_EXISTING = 3;
        public const uint INVALID_HANDLE_VALUE = 0xffffffff;
        public const uint IOCTL_NDIS_QUERY_GLOBAL_STATS = 0x00170002;
        public const int PERMANENT_ADDRESS = 0x01010101;

        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(uint hObject);

        [DllImport("kernel32.dll")]
        public static extern int DeviceIoControl(uint hDevice,
                                                  uint dwIoControlCode,
                                                  ref int lpInBuffer,
                                                  int nInBufferSize,
                                                  byte[] lpOutBuffer,
                                                  int nOutBufferSize,
                                                  ref uint lpbytesReturned,
                                                  int lpOverlapped);

        [DllImport("kernel32.dll")]
        public static extern uint CreateFile(string lpFileName,
                                              uint dwDesiredAccess,
                                              uint dwShareMode,
                                              int lpSecurityAttributes,
                                              uint dwCreationDisposition,
                                              uint dwFlagsAndAttributes,
                                              int hTemplateFile);

    }
    #endregion
}
