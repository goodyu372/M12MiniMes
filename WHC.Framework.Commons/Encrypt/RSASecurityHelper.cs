using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// �ǶԳƼ��ܡ����ܡ���֤������
    /// </summary>
    public class RSASecurityHelper
    {
        //RSA�ǳ��õķǶԳƼ����㷨��ʹ��System.Security����е�RSA�����㷨ʱ�������ˡ�����ȷ�ĳ��ȡ���������Ϊ�����ܵ����ݳ������¡�
        //.NET ���ṩ��RSA�㷨�涨��ÿ�μ��ܵ��ֽ��������ܳ�����Կ�ĳ���ֵ��ȥ11, ��ÿ�μ��ܵõ������ĳ��ȣ�ȴǡǡ����Կ�ĳ��ȡ�
        //���ԣ����Ҫ���ܽϳ������ݣ����Բ������ݽ�ȡ�ķ������ֶν��мӽ��ܡ�

        /// <summary>
        /// �ǶԳƼ������ɵ�˽Կ�͹�Կ 
        /// </summary>
        /// <param name="privateKey">˽Կ</param>
        /// <param name="publicKey">��Կ</param>
        public static void GenerateRSAKey(out string privateKey, out string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            privateKey = rsa.ToXmlString(true);
            publicKey = rsa.ToXmlString(false);
        }
        
        #region �ǶԳ����ݼ��ܣ���Կ���ܣ�

        /// <summary>
        /// �ǶԳƼ����ַ������ݣ����ؼ��ܺ������
        /// </summary>
        /// <param name="publicKey">��Կ</param>
        /// <param name="originalString">�����ܵ��ַ���</param>
        /// <returns>���ܺ������</returns>
        public static string RSAEncrypt(string publicKey, string originalString)
        {
            //byte[] PlainTextBArray;
            //byte[] CypherTextBArray;
            //string Result;
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //rsa.FromXmlString(publicKey);
            //PlainTextBArray = (new UnicodeEncoding()).GetBytes(originalString);
            //CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            //Result = Convert.ToBase64String(CypherTextBArray);
            //return Result;

            //�ֶμ��ܷ���
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            byte[] originalBytes = (new UnicodeEncoding()).GetBytes(originalString);

            //ʵ�ֶַ�
            int keySize = rsa.KeySize / 8;
            int bufferSize = keySize - 11;
            byte[] buffer = new byte[bufferSize];
            MemoryStream msInput = new MemoryStream(originalBytes);
            MemoryStream msOutput = new MemoryStream();

            int readLen = msInput.Read(buffer, 0, bufferSize);
            while (readLen > 0)
            {
                byte[] dataToEnc = new byte[readLen];
                Array.Copy(buffer, 0, dataToEnc, 0, readLen);

                byte[] encData = rsa.Encrypt(dataToEnc, false);
                msOutput.Write(encData, 0, encData.Length);

                readLen = msInput.Read(buffer, 0, bufferSize);
            }

            msInput.Close();
            byte[] result = msOutput.ToArray();
            var strResult = Convert.ToBase64String(result);

            //�õ����ܽ��
            msOutput.Close();
            rsa.Clear();

            return strResult;
        }

        /// <summary>
        /// �ǶԳƼ����ֽ����飬���ؼ��ܺ������
        /// </summary>
        /// <param name="publicKey">��Կ</param>
        /// <param name="originalBytes">�����ܵ��ֽ�����</param>
        /// <returns>���ؼ��ܺ������</returns>
        public static byte[] RSAEncrypt(string publicKey, byte[] originalBytes)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);

            //ʵ�ֶַ�
            int keySize = rsa.KeySize / 8;
            int bufferSize = keySize - 11;
            byte[] buffer = new byte[bufferSize];
            MemoryStream msInput = new MemoryStream(originalBytes);
            MemoryStream msOutput = new MemoryStream();
            
            int readLen = msInput.Read(buffer, 0,  bufferSize);
            while (readLen > 0)
            {
                byte[] dataToEnc = new byte[readLen];
                Array.Copy(buffer, 0, dataToEnc, 0, readLen);
                
                byte[] encData = rsa.Encrypt(dataToEnc, false);
                msOutput.Write(encData, 0, encData.Length);

                readLen = msInput.Read(buffer, 0, bufferSize);
            }

            msInput.Close();
            byte[] result = msOutput.ToArray();
            
            //�õ����ܽ��
            msOutput.Close();
            rsa.Clear();

            return result;
        } 

        #endregion

        #region �ǶԳƽ��ܣ�˽Կ���ܣ�

        /// <summary>
        /// �ǶԳƽ����ַ��������ؽ��ܺ������
        /// </summary>
        /// <param name="privateKey">˽Կ</param>
        /// <param name="encryptedString">����������</param>
        /// <returns>���ؽ��ܺ������</returns>
        public static string RSADecrypt(string privateKey, string encryptedString)
        {
            //byte[] PlainTextBArray;
            //byte[] DypherTextBArray;
            //string Result;
            //System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //rsa.FromXmlString(privateKey);
            //PlainTextBArray = Convert.FromBase64String(encryptedString);
            //DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
            //Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            //return Result;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);

            int keySize = rsa.KeySize / 8;
            byte[] buffer = new byte[keySize];
            
            var dataEnc = Convert.FromBase64String(encryptedString);
            MemoryStream msInput = new MemoryStream(dataEnc);            
            MemoryStream msOutput = new MemoryStream();
            
            int readLen = msInput.Read(buffer, 0, keySize);
            while (readLen > 0)
            {
                byte[] dataToDec = new byte[readLen];                
                Array.Copy(buffer, 0, dataToDec, 0, readLen);
                
                byte[] decData = rsa.Decrypt(dataToDec, false);
                msOutput.Write(decData, 0, decData.Length);                
                readLen = msInput.Read(buffer, 0, keySize);
            }
            msInput.Close();

            byte[] result = msOutput.ToArray();
            var strResult = (new UnicodeEncoding()).GetString(result);//�õ����ܽ��            
            msOutput.Close();
            rsa.Clear();

            return strResult;
        }

        /// <summary>
        /// �ǶԳƽ����ֽ����飬���ؽ��ܺ������
        /// </summary>
        /// <param name="privateKey">˽Կ</param>
        /// <param name="encryptedBytes">����������</param>
        /// <returns></returns>
        public static byte[] RSADecrypt(string privateKey, byte[] encryptedBytes)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);

            int keySize = rsa.KeySize / 8;
            byte[] buffer = new byte[keySize];

            MemoryStream msInput = new MemoryStream(encryptedBytes);
            MemoryStream msOutput = new MemoryStream();

            int readLen = msInput.Read(buffer, 0, keySize);
            while (readLen > 0)
            {
                byte[] dataToDec = new byte[readLen];
                Array.Copy(buffer, 0, dataToDec, 0, readLen);

                byte[] decData = rsa.Decrypt(dataToDec, false);
                msOutput.Write(decData, 0, decData.Length);
                readLen = msInput.Read(buffer, 0, keySize);
            }
            msInput.Close();

            byte[] result = msOutput.ToArray();//�õ����ܽ��            
            msOutput.Close();
            rsa.Clear();

            return result;
        } 
        #endregion

        #region �ǶԳƼ���ǩ������֤

        /// <summary>
        /// ʹ�÷ǶԳƼ���ǩ������
        /// </summary>
        /// <param name="privateKey">˽Կ</param>
        /// <param name="originalString">�����ܵ��ַ���</param>
        /// <returns>���ܺ������</returns>
        public static string RSAEncrypSignature(string privateKey, string originalString)
        {
            string signature;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey); //˽Կ
                // ���ܶ��� 
                RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsa);
                f.SetHashAlgorithm("SHA1");
                byte[] source = ASCIIEncoding.ASCII.GetBytes(originalString);
                SHA1Managed sha = new SHA1Managed();
                byte[] result = sha.ComputeHash(source);
                byte[] b = f.CreateSignature(result);
                signature = Convert.ToBase64String(b);
            }
            return signature;
        }
              
        /// <summary>
        /// ��˽Կ����ǩ�����ַ�����ʹ�ù�Կ���������֤
        /// </summary>
        /// <param name="originalString">δ���ܵ��ı����������</param>
        /// <param name="encrytedString">���ܺ���ı�����ע�����к�</param>
        /// <returns>�����֤�ɹ�����True������ΪFalse</returns>
        public static bool Validate(string originalString, string encrytedString)
        {
            return Validate(originalString, encrytedString, UIConstants.PublicKey);
        }

        /// <summary>
        /// ��˽Կ���ܵ��ַ�����ʹ�ù�Կ���������֤
        /// </summary>
        /// <param name="originalString">δ���ܵ��ı����������</param>
        /// <param name="encrytedString">���ܺ���ı�����ע�����к�</param>
        /// <param name="publicKey">�ǶԳƼ��ܵĹ�Կ</param>
        /// <returns>�����֤�ɹ�����True������ΪFalse</returns>
        public static bool Validate(string originalString, string encrytedString, string publicKey)
        {
            bool bPassed = false;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(publicKey); //��Կ
                    RSAPKCS1SignatureDeformatter formatter = new RSAPKCS1SignatureDeformatter(rsa);
                    formatter.SetHashAlgorithm("SHA1");

                    byte[] key = Convert.FromBase64String(encrytedString); //��֤
                    SHA1Managed sha = new SHA1Managed();
                    byte[] name = sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(originalString));
                    if (formatter.VerifySignature(name, key))
                    {
                        bPassed = true;
                    }
                }
                catch
                {
                }
            }
            return bPassed;
        }

        #endregion

        #region Hash ����

        /// <summary> Hash ���� </summary>
        /// <param name="str2Hash"></param>
        /// <returns></returns>
        public static int HashEncrypt(string str2Hash)
        {
            const int salt = 100716;    // ��ֵ
            str2Hash += "Commons";       // ����һ�������ַ���

            int len = str2Hash.Length;
            int result = (str2Hash[len - 1] - 31) * 95 + salt;
            for (int i = 0; i < len - 1; i++)
            {
                result = (result * 95) + (str2Hash[i] - 32);
            }

            return result;
        }

        /// <summary>
        /// MD5����
        /// </summary>
        /// <param name="str">�������ִ�</param>
        /// <returns>���ܺ���ִ�</returns>
        public static string ComputeMD5(string str)
        {
            byte[] hashValue = ComputeMD5Data(str);
            return BitConverter.ToString(hashValue).Replace("-", "");
        }

        /// <summary>
        /// MD5����
        /// </summary>
        /// <param name="input">�������ִ�</param>
        /// <returns>���ܺ���ִ�</returns>
        public static byte[] ComputeMD5Data(string input)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            return MD5.Create().ComputeHash(buffer);
        }

        /// <summary>
        /// MD5����
        /// </summary>
        /// <param name="data">����������</param>
        /// <returns>���ܺ���ִ�</returns>
        public static byte[] ComputeMD5Data(byte[] data)
        {
            return MD5.Create().ComputeHash(data);
        }

        /// <summary>
        /// MD5����
        /// </summary>
        /// <param name="stream">��������</param>
        /// <returns>���ܺ���ִ�</returns>
        public static byte[] ComputeMD5Data(Stream stream)
        {
            return MD5.Create().ComputeHash(stream);
        }
        #endregion
    }
}