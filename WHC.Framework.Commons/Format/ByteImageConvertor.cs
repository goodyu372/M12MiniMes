using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// �ṩ�ֽ������ͼƬ֮���ת��������
    /// </summary>
    public sealed class ByteImageConvertor
    {
        private ByteImageConvertor()
        {
        }

        /// <summary>
        /// ��Image����ת��ΪByte����
        /// </summary>
        /// <param name="image">ͼƬImage����</param>
        /// <returns>�ֽڼ���</returns>
        public static byte[] ImageToBytes(Image image)
        {
            byte[] bytes = null;
            if (image != null)
            {
                lock (image)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        bytes = ms.GetBuffer();
                    }
                }
            }
            return bytes;
        }

        /// <summary>
        /// ��Image����ת��ΪByte����
        /// </summary>
        /// <param name="image">image����</param>
        /// <param name="imageFormat">ͼƬ��ʽ����׺����</param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image, ImageFormat imageFormat)
        {
            if (image == null) { return null; }
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(image))
                {
                    bitmap.Save(ms, imageFormat);
                    ms.Position = 0;
                    data = new byte[ms.Length];
                    ms.Read(data, 0, Convert.ToInt32(ms.Length));
                    ms.Flush();
                }
            }
            return data;
        }

        /// <summary>
        /// ת��Byte���鵽Image����
        /// </summary>
        /// <param name="bytes">�ֽ�����</param>
        /// <returns>ImageͼƬ</returns>
        public static Image ImageFromBytes(byte[] bytes)
        {
            Image image = null;
            try
            {
                if (bytes != null)
                {
                    MemoryStream ms = new MemoryStream(bytes, false);
                    using (ms)
                    {
                        image = ImageFromStream(ms);
                    }
                }
            }
            catch
            {
            }
            return image;
        }

        /// <summary>
        /// ת����ַ���ļ�·������URL��ַ����Image����
        /// </summary>
        /// <param name="url">ͼƬ��ַ���ļ�·������URL��ַ��</param>
        /// <returns>Image����</returns>
        public static Image ImageFromUrl(string url)
        {
            Image image = null;
            try
            {
                if (!String.IsNullOrEmpty(url))
                {
                    Uri uri = new Uri(url);
                    if (uri.IsFile)
                    {
                        FileStream fs = new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        using (fs)
                        {
                            image = ImageFromStream(fs);
                        }
                    }
                    else
                    {
                        System.Net.WebClient wc = new System.Net.WebClient();   // TODO: consider changing this to WebClientEx
                        using (wc)
                        {
                            byte[] bytes = wc.DownloadData(uri);
                            MemoryStream ms = new MemoryStream(bytes, false);
                            using (ms)
                            {
                                image = ImageFromStream(ms);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return image;
        }

        /// <summary>
        /// ����ת��ΪImage����
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static Image ImageFromStream(Stream stream)
        {
            Image image = null;
            try
            {
                stream.Position = 0;
                Image tempImage = System.Drawing.Bitmap.FromStream(stream);
                // dont close stream yet, first create a copy
                using (tempImage)
                {
                    image = new Bitmap(tempImage);
                }
            }
            catch
            {
                // ���ļ�Ϊ.icoͼ���ļ���ʱ�����������Ч������ת��
                try
                {
                    stream.Position = 0;
                    Icon icon = new Icon(stream);
                    if (icon != null) image = icon.ToBitmap();
                }
                catch
                { }
            }

            return image;
        }

        /// <summary>
        /// byte[]����ת��ΪBitmap
        /// </summary>
        /// <param name="bytes">byte[]����</param>
        /// <returns></returns>
        public static Bitmap BitmapFromBytes(byte[] bytes)
        {
            Bitmap bitmap = null;
            try
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    bitmap = new Bitmap((Image)new Bitmap(stream));
                }
            }
            catch { }

            return bitmap;
        }

        /// <summary>
        /// Bitmap����ת��Ϊbyte ����
        /// </summary>
        /// <param name="bitmap">Bitmap����</param>
        /// <returns></returns>
        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            byte[] byteImage = null;
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, bitmap.RawFormat);

                    byteImage = new Byte[stream.Length];
                    byteImage = stream.ToArray();
                }
            }
            catch { }

            return byteImage;
        }
    }
}