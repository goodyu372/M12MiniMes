using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// 摄像头采集图片
    /// </summary>
    public partial class FrmCamera : BaseForm
    {
        /// <summary>
        /// 采集图片
        /// </summary>
        public Image CameraImage { get; set; }

        public FrmCamera()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            this.CameraImage = this.cameraControl1.TakeSnapshot();
            this.pictureEdit1.Image = this.CameraImage;
        }

        private void FrmCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            //释放设备资源
            this.cameraControl1.Dispose();
        }
    }
}
