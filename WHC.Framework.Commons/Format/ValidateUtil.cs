using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// ���������ʽ��֤������
    /// </summary>
    public class ValidateUtil
    {
        #region ������ʽ

        /// <summary>
        /// �����ʼ�������ʽ
        /// </summary>
        public static readonly string EmailRegex = @"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$";
        // @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";//w Ӣ����ĸ�����ֵ��ַ������� [a-zA-Z0-9] �﷨һ�� 

        /// <summary>
        /// ����Ƿ��������ַ�������ʽ
        /// </summary>
        public static readonly string CHZNRegex = "[\u4e00-\u9fa5]";

        /// <summary>
        /// ����û�����ʽ�Ƿ���Ч(ֻ���Ǻ��֡���ĸ���»��ߡ�����)
        /// </summary>
        public static readonly string UserNameRegex = @"^([\u4e00-\u9fa5A-Za-z_0-9]{0,})$";

        /// <summary>
        /// ����������ʽ(�������ַ������»��ߣ�6~16λ
        /// </summary>
        public static readonly string PasswordCharNumberRegex = @"^[A-Za-z_0-9]{6,16}$";

        /// <summary>
        /// ����������ʽ�������ֻ��ߴ���ĸ����ͨ���� 6~16λ
        /// </summary>
        public static readonly string PasswordRegex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,16}$";

        /// <summary>
        /// INT��������������ʽ
        /// </summary>
        public static readonly string ValidIntRegex = @"^[1-9]\d*\.?[0]*$";

        /// <summary>
        /// �Ƿ�����������ʽ
        /// </summary>
        public static readonly string NumericRegex = @"^[-]?\d+[.]?\d*$";

        /// <summary>
        /// �Ƿ�������������ʽ
        /// </summary>
        public static readonly string NumberRegex = @"^[0-9]+$";

        /// <summary>
        /// �������������ʽ
        /// </summary>
        public static readonly string IntCheck = @"^[0-9]+[0-9]*$";

        /// <summary>
        /// �Ƿ�����������ʽ���ɴ��������ţ�
        /// </summary>
        public static readonly string NumberSignRegex = @"^[+-]?[0-9]+$";
        
        /// <summary>
        /// �Ƿ��Ǹ�����������ʽ
        /// </summary>
        public static readonly string DecimalRegex = "^[0-9]+[.]?[0-9]+$";

        /// <summary>
        /// �Ƿ��Ǹ�����������ʽ(�ɴ�������)
        /// </summary>
        public static readonly string DecimalSignRegex = "^[+-]?[0-9]+[.]?[0-9]+$";//�ȼ���^[+-]?\d+[.]?\d+$

        /// <summary>
        /// �̶��绰������ʽ
        /// </summary>
        public static readonly string PhoneRegex = @"^(\(\d{3,4}\)|\d{3,4}-)?\d{7,8}$";
        
        /// <summary>
        /// �ƶ��绰������ʽ
        /// </summary>
        public static readonly string MobileRegex = @"^(13|15|18)\d{9}$";

        /// <summary>
        /// �̶��绰���ƶ��绰������ʽ
        /// </summary>
        public static readonly string PhoneMobileRegex = @"^(\(\d{3,4}\)|\d{3,4}-)?\d{7,8}$|^(13|15|18)\d{9}$";

        /// <summary>
        /// ���֤15λ������ʽ
        /// </summary>
        public static readonly string ID15Regex = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";

        /// <summary>
        /// ���֤18λ������ʽ
        /// </summary>
        public static readonly string ID18Regex = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$";

        /// <summary>
        /// URL������ʽ
        /// </summary>
        public static readonly string UrlRegex = @"\b(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]*[-A-Za-z0-9+&@#/%=~_|]";

        /// <summary>
        /// IP������ʽ
        /// </summary>
        public static readonly string IPRegex = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"; //@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";

        /// <summary>
        /// Base64����������ʽ��
        /// ��Сд��ĸ��26��������10�����֣��ͼӺš�+����б�ܡ�/����һ��64���ַ����Ⱥš�=��������Ϊ��׺��;
        /// </summary>
        public static readonly string Base64Regex = @"[A-Za-z0-9\+\/\=]";

        /// <summary>
        /// �Ƿ�Ϊ���ַ���������ʽ
        /// </summary>
        public static readonly string LetterRegex = @"^[A-Za-z]+$";

        /// <summary>
        /// GUID������ʽ
        /// </summary>
        public static readonly string GuidRegex = "[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}|[A-F0-9]{32}";

        /// <summary>
        /// �ļ�·�����������ʽ
        /// </summary>
        public static readonly string FilePathRegex = @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+)(?<namext>(\.[\w]+)*)(?<suffix>\.[\w]+)";
              
        /// <summary>
        /// �Ƿ�ʮ�������ַ������������ʽ
        /// </summary>
        public static readonly string HexStringRegex = @"\A\b[0-9a-fA-F]+\b\Z";

        /// <summary>
        /// ���Ƹ�ʽ���������ʽ
        /// </summary>
        public static readonly string CarLicenseCheck = @"^([\u4e00-\u9fa5]|[A-Z]){1,2}[A-Za-z0-9]{1,2}-[0-9A-Za-z]{5}$";

        /// <summary>
        /// Mac��ַ(7������)������ʽ
        /// </summary>
        public static readonly string MacAddr7Check = @"^([0-9A-F][0-9A-F]-[0-9A-F][0-9A-F]-[0-9A-F][0-9A-F]-[0-9A-F][0-9A-F]-[0-9A-F][0-9A-F]-[0-9A-F][0-9A-F]-[0-9A-F][0-9A-F])$";

        /// <summary>
        ///Mac��ַ��6�����ȣ�������ʽ
        /// </summary>
        public static readonly string MacAddr6Check = @"^([0-9A-F][0-9A-F]-[0-9A-F][0-9A-F]-[0-9A-F][0-9A-F]-[0-9A-F][0-9A-F]-[0-9A-F][0-9A-F]-[0-9A-F][0-9A-F])$";

        #endregion

        #region ��֤�����ַ����Ƿ���ģʽ�ַ���ƥ��

        /// <summary>
        /// ��֤�����ַ����Ƿ���ģʽ�ַ���ƥ�䣬ƥ�䷵��true
        /// </summary>
        /// <param name="input">�����ַ���</param>
        /// <param name="pattern">ģʽ�ַ���</param>        
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// ��֤�����ַ����Ƿ���ģʽ�ַ���ƥ�䣬ƥ�䷵��true
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <param name="pattern">ģʽ�ַ���</param>
        /// <param name="options">ɸѡ����</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        #endregion

        #region �û��������ʽ

        /// <summary>
        /// �����ַ�����ʵ����, 1�����ֳ���Ϊ2
        /// </summary>
        /// <returns>�ַ�����</returns>
        public static int GetStringLength(string stringValue)
        {
            return Encoding.Default.GetBytes(stringValue).Length;
        }

        /// <summary>
        /// ����û�����ʽ�Ƿ���Ч
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        public static bool IsValidUserName(string userName)
        {
            int userNameLength = GetStringLength(userName);
            if (userNameLength >= 4 && userNameLength <= 20 && Regex.IsMatch(userName, UserNameRegex))
            {   // �ж��û����ĳ��ȣ�4-20���ַ��������ݣ�ֻ���Ǻ��֡���ĸ���»��ߡ����֣��Ƿ�Ϸ�
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �Ƿ�����Ч���루�����ֻ��ߴ���ĸ����ͨ����
        /// </summary>
        /// <param name="password">�����ַ���</param>
        /// <returns></returns>
        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, PasswordRegex);
        }
        #endregion

        #region �����ַ������

        /// <summary>
        /// �Ƿ�int�ַ���
        /// </summary>
        public static bool IsInt(string val)
        {
            return Regex.IsMatch(val, ValidIntRegex);
        }

        /// <summary>
        /// �Ƿ������ַ���
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsNumeric(string inputData)
        {
            Regex RegNumeric = new Regex(NumericRegex);
            Match m = RegNumeric.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ������ַ���
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Regex RegNumber = new Regex(NumberRegex);
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ������ַ������������ţ�
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Regex RegNumberSign = new Regex(NumberSignRegex);
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ��Ǹ�����
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Regex RegDecimal = new Regex(DecimalRegex);
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ��Ǹ����� �ɴ�������
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Regex RegDecimalSign = new Regex(DecimalSignRegex); 
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion

        #region ���ļ��

        /// <summary>
        /// ����Ƿ��������ַ�
        /// </summary>
        public static bool IsChinese(string inputData)
        {
            Regex RegCHZN = new Regex(CHZNRegex);
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        /// <summary> 
        /// ��⺬�������ַ�����ʵ�ʳ��� 
        /// </summary> 
        /// <param name="inputData">�ַ���</param> 
        public static int GetChineseLength(string inputData)
        {
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] bytes = n.GetBytes(inputData);

            int length = 0; // l Ϊ�ַ���֮ʵ�ʳ��� 
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                if (bytes[i] == 63) //�ж��Ƿ�Ϊ���ֻ�ȫ�ŷ��� 
                {
                    length++;
                }
                length++;
            }
            return length;

        }

        #endregion

        #region ���ø�ʽ

        /// <summary>
        /// ��֤������ĸ  "^[A-Za-z]+$"
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public bool IsLetter(string inputData)
        {
            return Regex.IsMatch(inputData, LetterRegex);
        } 

        /// <summary>
        /// ��֤���֤�Ƿ�Ϸ�  15 ��  18λ����
        /// </summary>
        /// <param name="idCard">Ҫ��֤�����֤</param>
        public static bool IsIdCard(string idCard)
        {
            if (string.IsNullOrEmpty(idCard))
            {
                return false;
            }

            if (idCard.Length == 15)
            {
                return Regex.IsMatch(idCard, ID15Regex);
            }
            else if (idCard.Length == 18)
            {
                return Regex.IsMatch(idCard, ID18Regex, RegexOptions.IgnoreCase);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �Ƿ����ʼ���ַ
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Regex RegEmail = new Regex(EmailRegex);
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ����ʱ�
        /// </summary>
        public static bool IsPostCode(string zip)
        {
            Regex rx = new Regex(@"^\d{6}$", RegexOptions.None);
            Match m = rx.Match(zip);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ��ǹ̶��绰
        /// </summary>
        public static bool IsPhone(string phone)
        {
            Regex rx = new Regex(PhoneRegex, RegexOptions.None);
            Match m = rx.Match(phone);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ����ֻ�����
        /// </summary>
        public static bool IsMobile(string mobile)
        {
            Regex rx = new Regex(MobileRegex, RegexOptions.None);
            Match m = rx.Match(mobile);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ��ǵ绰�ַ������̻����ֻ� ��
        /// </summary>
        public static bool IsPhoneAndMobile(string number)
        {
            Regex rx = new Regex(PhoneMobileRegex, RegexOptions.None);
            Match m = rx.Match(number);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ�����ЧUrl
        /// </summary>
        public static bool IsURL(string url)
        {
            return Regex.IsMatch(url, UrlRegex);
        }

        /// <summary>
        /// �Ƿ���IP��ַ
        /// </summary>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, IPRegex);
        }

        /// <summary>
        /// �Ƿ��������ַ���
        /// </summary>
        /// <param name="host">����</param>
        /// <returns></returns>
        public static bool IsDomain(string host)
        {
            Regex r = new Regex(@"^\d+$");
            if (host.IndexOf(".") == -1)
            {
                return false;
            }
            return r.IsMatch(host.Replace(".", string.Empty)) ? false : true;
        }

        /// <summary>
        /// �ж��Ƿ�Ϊbase64�ַ���
        /// </summary>
        public static bool IsBase64String(string str)
        {
            return Regex.IsMatch(str, Base64Regex);
        }

        /// <summary>
        /// ��֤�ַ����Ƿ���GUID
        /// </summary>
        /// <param name="guid">�ַ���</param>
        /// <returns></returns>
        public static bool IsGuid(string guid)
        {
            if (string.IsNullOrEmpty(guid))
                return false;

            return Regex.IsMatch(guid, GuidRegex, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// ��֤�Ƿ����ļ�·��
        /// <para>eg:CheckHelper.IsFilePath(@"C:\alipay\log.txt");==>true</para>
        /// </summary>
        /// <param name="filePath">��֤�ַ���</param>
        /// <returns>�Ƿ����ļ�·��</returns>
        public static bool IsFilePath(string filePath)
        {
            return Regex.IsMatch(filePath, FilePathRegex);
        }

        /// <summary>
        /// �Ƿ���ʮ�������ַ���
        /// </summary>
        /// <param name="hexString">��֤����</param>
        /// <returns>�Ƿ���ʮ�������ַ���</returns>
        public static bool IsHexString(string hexString)
        {
            return Regex.IsMatch(hexString, HexStringRegex);
        }

        /// <summary>
        /// ������õĶ˿ں��Ƿ���ȷ
        /// </summary>
        /// <param name="port">�˿ں�</param>
        /// <returns>�˿ں��Ƿ���ȷ</returns>
        public static bool IsPort(string port)
        {
            bool _result = false;
            int _minPORT = 0, _maxPORT = 65535;
            int _portValue = -1;

            if (int.TryParse(port, out _portValue))
            {
                _result = !((_portValue < _minPORT) || (_portValue > _maxPORT));
            }

            return _result;
        }

        #endregion

        #region ���ڼ��

        /// <summary>
        /// �ж�������ַ��Ƿ�Ϊ����
        /// </summary>
        public static bool IsDate(string strValue)
        {
            return Regex.IsMatch(strValue, @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))");
        }

        /// <summary>
        /// �ж�������ַ��Ƿ�Ϊ����,��2004-07-12 14:25|||1900-01-01 00:00|||9999-12-31 23:59
        /// </summary>
        public static bool IsDateHourMinute(string strValue)
        {
            return Regex.IsMatch(strValue, @"^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){1}$");
        }

        #endregion

        #region ����

        /// <summary>
        /// ����ַ�����󳤶ȣ�����ָ�����ȵĴ�
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <returns></returns>			
        public static string CheckMathLength(string inputData, int maxLength)
        {
            if (inputData != null && inputData != string.Empty)
            {
                inputData = inputData.Trim();
                if (inputData.Length > maxLength)//����󳤶Ƚ�ȡ�ַ���
                {
                    inputData = inputData.Substring(0, maxLength);
                }
            }
            return inputData;
        }

        /// <summary>
        /// ת���� HTML code
        /// </summary>
        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }
        /// <summary>
        ///����html�� ��ͨ�ı�
        /// </summary>
        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        #endregion
    }
}
