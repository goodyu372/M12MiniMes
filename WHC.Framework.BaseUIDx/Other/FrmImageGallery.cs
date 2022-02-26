using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WHC.Framework.Commons;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// DevExpress图标和系统图标选择窗体
    /// </summary>
    public partial class FrmImageGallery : BaseForm
    {
        /// <summary>
        /// 自定义一个委托处理图标选择
        /// </summary>
        public delegate void IconSelectHandlerDelegate(Image image, string name);

        /// <summary>
        /// 图标选择的事件
        /// </summary>
        public event IconSelectHandlerDelegate OnIconSelected;
        private DXImageGalleryLoader loader = null;

        public FrmImageGallery()
        {
            InitializeComponent();

            InitDictItem();//初始化
        }      

        /// <summary>
        /// 处理图标选择的事件触发
        /// </summary>
        public virtual void ProcessIconSelected(Image image, string name)
        {
            if (OnIconSelected != null)
            {
                OnIconSelected(image, name);
            }
        }

        /// <summary>
        /// 初始化字典及相关数据
        /// </summary>
        private  void InitDictItem()
        {
            //加载数据
            loader = DXImageGalleryLoader.Default;

            //大小
            this.lstSize.Items.Clear();
            this.lstSize.Items.Add(new CListItem("16x16"));
            this.lstSize.Items.Add(new CListItem("32x32"), true);

            //集合
            this.lstCollection.Items.Clear();
            this.lstCollection.Items.Add(new CListItem("彩色", "images"), true);
            this.lstCollection.Items.Add(new CListItem("灰度", "grayscaleimages"));
            this.lstCollection.Items.Add(new CListItem("Office 2013", "office2013"));
            this.lstCollection.Items.Add(new CListItem("DevExpress", "devav"));

            //类别
            this.lstCategory.Items.Clear();
            int i = 0;
            foreach (var item in loader.Categories)
            {
                var display = StringUtil.ToProperCase(item.Replace("%20", " "));
                var check = (i++ == 0) ? CheckState.Checked : CheckState.Unchecked;
                this.lstCategory.Items.Add(new CListItem(display, item), check);
            }

            //单击事件触发选择图标
            this.lstSize.ItemCheck += (s, e) => { BindData(); };
            this.lstCollection.ItemCheck += (s, e) => { BindData(); };
            this.lstCategory.ItemCheck += (s, e) => { BindData(); };

            //默认图片
            this.txtEmbedIcon.Image = this.imageCollection1.Images[0];
        }

        private void FrmImageGallery_Load(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 绑定数据显示
        /// </summary>
        /// <param name="fileName">文件名，如果查询则传入</param>
        private void BindData(string fileName = "")
        {
            //根据条件进行过滤
            var items = loader.Search(this.lstCollection.ToList(), this.lstCategory.ToList(), this.lstSize.ToList(), 
                fileName);

            //对图标展示进行分类展示
            galleryControl1.Gallery.Groups.Clear();
            foreach (string key in items.Keys)
            {
                var display = StringUtil.ToProperCase(key.Replace("%20", " "));
                GalleryItemGroup group = new GalleryItemGroup() { Caption = display };
                galleryControl1.Gallery.Groups.Add(group);
                foreach (GalleryItem item in items[key])
                {
                    item.ItemClick += (s, e) =>
                    {
                        //选择处理
                        ProcessIconSelected(item.ImageOptions.Image, item.Description);
                    };
                }
                group.Items.AddRange(items[key].ToArray());
            }
        }

        /// <summary>
        /// 触发查询图标
        /// </summary>
        private void txtFileName_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                BindData(this.txtFileName.Text.Trim());
            }
        }

        /// <summary>
        /// 查询图标
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(this.txtFileName.Text.Trim());
        }

        private void txtFilePath_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string file = GetIconPath();
            if (!string.IsNullOrEmpty(file))
            {
                this.txtFilePath.Text = file;//记录文件名
                this.txtEmbedIcon.Image = LoadIcon(file);//显示图片
                this.txtEmbedIcon.Size = new System.Drawing.Size(64, 64);

                //返回处理
                ProcessIconSelected(this.txtEmbedIcon.Image, file);
            }
        }

        /// <summary>
        /// 加载图标，如果加载不成功，那么使用默认图标
        /// </summary>
        /// <param name="iconPath"></param>
        /// <returns></returns>
        private Image LoadIcon(string iconPath)
        {
            Image result = this.imageCollection1.Images[0];
            try
            {
                if (!string.IsNullOrEmpty(iconPath))
                {
                    string path = Path.Combine(Application.StartupPath, iconPath);
                    if (File.Exists(path))
                    {
                        result = Image.FromFile(path);
                    }
                }
            }
            catch
            {
                LogTextHelper.Error(string.Format("无法识别图标地址：{0}，请确保该文件存在！", iconPath));
            }

            return result;
        }

        private string GetIconPath()
        {
            string iconFile = "Icon File(*.ico)|*.ico|Image Files(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|All File(*.*)|*.*";
            string file = FileDialogHelper.Open("选择图标文件", iconFile);
            string result = "";
            if (!string.IsNullOrEmpty(file))
            {
                result = file.Replace(Application.StartupPath, "").Trim('\\');
            }

            return result;
        }

        private void txtEmbedIcon_Click(object sender, EventArgs e)
        {
            if(this.txtEmbedIcon.Image != null)
            {
                ProcessIconSelected(this.txtEmbedIcon.Image, this.txtFilePath.Text);
            }
        }
    }

    /// <summary>
    /// 图标库加载处理
    /// </summary>
    public class DXImageGalleryLoader
    {
        /// <summary>
        /// 图标字典类别集合
        /// </summary>
        public Dictionary<string, GalleryItem> ImageCollection { get; set; }
        /// <summary>
        /// 图标分类
        /// </summary>
        public List<string> Categories { get; set; }
        /// <summary>
        /// 图标集合
        /// </summary>
        public List<string> Collection { get; set; }
        /// <summary>
        /// 图标尺寸
        /// </summary>
        public List<string> Size { get; set; }

        /// <summary>
        /// 使用缓存处理，获得对象实例
        /// </summary>
        public static DXImageGalleryLoader Default
        {
            get
            {
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string keyName = string.Format("{0}-{1}", method.DeclaringType.FullName, method.Name);

                var result = MemoryCacheHelper.GetCacheItem<DXImageGalleryLoader>(keyName,
                       delegate () { return new DXImageGalleryLoader().LoadData(); },
                       new TimeSpan(0, 30, 0));//30分钟过期
                return result;
            }
        }

        /// <summary>
        /// 初始化加载图标资源
        /// </summary>
        /// <returns></returns>
        private DXImageGalleryLoader LoadData()
        { 
            this.ImageCollection = new Dictionary<string, GalleryItem>();
            this.Categories = new List<string>();
            this.Collection = new List<string>();
            this.Size = new List<string>();

            using (System.Resources.ResourceReader reader = GetResourceReader(DevExpress.Utils.DxImageAssemblyUtil.ImageAssembly))
            {
                System.Collections.IDictionaryEnumerator dict = reader.GetEnumerator();
                while (dict.MoveNext())
                {
                    string key = (string)dict.Key as string;
                    if (!DevExpress.Utils.DxImageAssemblyUtil.ImageProvider.IsBrowsable(key)) continue;
                    if (key.EndsWith(".png", StringComparison.Ordinal))
                    {
                        string reg = @"(?<collection>\S*?)/(?<category>\S*?)/(?<name>\S*)";
                        var collectionItem = CRegex.GetText(key, reg, "collection"); 
                        var categoryItem = CRegex.GetText(key, reg, "category");
                        string sizeReg = @"_(?<size>\S*)\.";
                        var sizeItem = CRegex.GetText(key, sizeReg, "size");

                        if (!this.Collection.Contains(collectionItem))
                        {
                            this.Collection.Add(collectionItem);
                        }
                        if (!this.Categories.Contains(categoryItem))
                        {
                            this.Categories.Add(categoryItem);
                        }
                        if (!this.Size.Contains(sizeItem))
                        {
                            this.Size.Add(sizeItem);
                        }

                        Image image = GetImageFromStream((System.IO.Stream)dict.Value);
                        if (image != null)
                        {
                            var item = new DevExpress.XtraBars.Ribbon.GalleryItem(image, key, key);
                            if (!ImageCollection.ContainsKey(key))
                            {
                                ImageCollection.Add(key, item);
                            }
                        }                        
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, GalleryItemCollection> Search(List<string> collection, List<string> categories, List<string> size, string fileName = "")
        {
            Dictionary<string, GalleryItemCollection> dict = new Dictionary<string, GalleryItemCollection>();

            GalleryItemCollection list = new GalleryItemCollection();
            foreach (var key in ImageCollection.Keys)
            {
                //使用正则表达式获取图标文件名中的集合、类别、大小等信息
                string reg = @"(?<collection>\S*?)/(?<category>\S*?)/(?<name>\S*)";
                var collectionItem = CRegex.GetText(key, reg, "collection");
                var categoryItem = CRegex.GetText(key, reg, "category");
                string sizeReg = @"_(?<size>\S*)\.";
                var sizeItem = CRegex.GetText(key, sizeReg, "size");

                //如果是查询处理，把记录放到查询结果里面
                if (!string.IsNullOrEmpty(fileName))
                {
                    if(key.Contains(fileName))
                    {
                        list.Add(ImageCollection[key]);
                    }
                    dict["查询结果"] = list;
                }
                else
                {
                    //如果是集合和列表中包含的，把它们按类别添加到字典里面
                    if (collection.Contains(collectionItem) && 
                        categories.Contains(categoryItem) && 
                        size.Contains(sizeItem))
                    {
                        if (!dict.ContainsKey(categoryItem))
                        {
                            GalleryItemCollection cateList = new GalleryItemCollection();
                            cateList.Add(ImageCollection[key]);
                            dict[categoryItem] = cateList;
                        }
                        else
                        {
                            GalleryItemCollection cateList = dict[categoryItem];
                            cateList.Add(ImageCollection[key]);
                        }
                    }
                }
            }
            return dict;
        }

        private System.Resources.ResourceReader GetResourceReader(System.Reflection.Assembly imagesAssembly)
        {
            var resources = imagesAssembly.GetManifestResourceNames();
            var imageResources = Array.FindAll(resources, resourceName => resourceName.EndsWith(".resources"));
            if (imageResources.Length != 1)
            {
                throw new Exception("读取异常");
            }
            return new System.Resources.ResourceReader(imagesAssembly.GetManifestResourceStream(imageResources[0]));
        }
        private Image GetImageFromStream(System.IO.Stream stream)
        {
            Image res = null;
            try
            {
                res = Image.FromStream(stream);
            }
            catch { res = null; }
            return res;
        }
    }

    /// <summary>
    /// 定义一个扩展函数，方便获取控件的选择集合
    /// </summary>
    public static class CheckedListBoxControlExtesion
    {
        /// <summary>
        /// 返回选中的集合
        /// </summary>
        /// <returns></returns>
        public static List<string> ToList(this CheckedListBoxControl control)
        {
            List<string> list = new List<string>();
            foreach (var index in control.CheckedIndices)
            {
                var listItem = control.GetItemValue(index) as CListItem;
                if (listItem != null)
                {
                    list.Add(listItem.Value);
                }
            }
            return list;
        }
    }
}
