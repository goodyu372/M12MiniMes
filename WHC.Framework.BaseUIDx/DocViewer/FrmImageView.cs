using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.Framework.BaseUI;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// 图片显示窗体
    /// </summary>
    public partial class FrmImageView : BaseForm
    {
        /// <summary>
        /// 设置显示的图片内容
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmImageView()
        {
            InitializeComponent();
        }

        private void pictureEdit1_Properties_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
            }
        }

        private void FrmImageView_Load(object sender, EventArgs e)
        {
            this.pictureEdit1.Image = Image;
        }

        private void FrmImageView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
