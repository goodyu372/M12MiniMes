using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHC.Attachment.UI
{
    /// <summary>
    /// 公用辅助函数
    /// </summary>
    internal class MyHelper
    {
        public static bool IsImageFile(string extension)
        {
            List<string> imageExList = new List<string>() { ".bmp", ".jpg", ".gif", ".png", ".jpeg",".tif"};
            return imageExList.Contains(extension.ToLower());
        }
    }
}
