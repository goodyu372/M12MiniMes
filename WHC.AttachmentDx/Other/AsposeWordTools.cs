using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WHC.OrderWater.Commons;

namespace WHC.Attachment.UI
{
    public class AsposeWordTools
    {
        /// <summary>
        /// 根据列表的书签引用，导出Word文件。
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="saveFileName">要保存的文件名称</param>
        /// <param name="dictBookMark">书签键值列表</param>
        public static void ExportWordWithBookMark(string templateFile, string saveFileName, Dictionary<string, string> dictBookMark)
        {                        
            if (!File.Exists(templateFile))
            {
                MessageDxUtil.ShowWarning(Path.GetFileName(templateFile));
                return;
            }

            string saveDocFile = FileDialogHelper.Save("保存Word文件", "Word文件(*.doc)|*.doc|All File(*.*)|*.*", saveFileName, "C:\\");
            if (!string.IsNullOrEmpty(saveDocFile))
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(templateFile);
                    foreach (string name in dictBookMark.Keys)
                    {
                        Aspose.Words.Bookmark bookmark = doc.Range.Bookmarks[name];
                        if (bookmark != null)
                        {
                            bookmark.Text = dictBookMark[name];
                        }
                    }                    

                    doc.Save(saveDocFile);
                    if (MessageDxUtil.ShowYesNoAndTips("保存成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDocFile);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                    return;
                }
            }
        }
    }
}
