using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel; 
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WHC.Framework.Commons
{
    /// <summary> 
    /// 动画效果显示窗体（显示、隐藏、关闭）辅助类
    /// </summary> 
    /// <remarks> 
    /// MDI子窗体不支持透明化操作，只支持其他动画效果
    /// </remarks> 
    public sealed class FormAnimator : IDisposable
    {
        /// <summary>
        /// 执行动画窗体的操作
        /// </summary>
        /// <param name="hWnd">窗体句柄</param>
        /// <param name="dwTime"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool AnimateWindow(IntPtr hWnd, int dwTime, int dwFlags); 

        #region 枚举类型定义

        /// <summary> 
        /// 动画效果的枚举
        /// </summary> 
        public enum AnimationMethod
        {
            [Description("Default animation method. Rolls out from edge when showing and into edge when hiding. Requires a direction.")]
            Roll = 0x0,
            [Description("Expands out from centre when showing and collapses into centre when hiding.")]
            Centre = 0x10,
            [Description("Slides out from edge when showing and slides into edge when hiding. Requires a direction.")]
            Slide = 0x40000,
            [Description("Fades from transaprent to opaque when showing and from opaque to transparent when hiding.")]
            Blend = 0x80000
        }

        /// <summary> 
        /// 翻转或者滑动的动画效果方向
        /// </summary> 
        /// <remarks> 
        /// 水平和竖直方向可以组合为对角线动画效果 
        /// </remarks> 
        [Flags()]
        public enum AnimationDirection
        {
            [Description("From left to right.")]
            Right = 0x1,
            [Description("From right to left.")]
            Left = 0x2,
            [Description("From top to bottom.")]
            Down = 0x4,
            [Description("From bottom to top.")]
            Up = 0x8
        }

        #endregion

        #region 常量

        //Hide the form. 
        private const int AW_HIDE = 0x10000;
        //Activate the form. 
        private const int AW_ACTIVATE = 0x20000;

        #endregion

        #region 变量及属性

        //待实现动画的窗体对象
        private Form m_Form;

        //显示或者隐藏窗体的动画操作方式
        private AnimationMethod m_Method;
        //滚动或者滑动窗体的方向
        private AnimationDirection m_Direction;
        //动画效果的毫秒数值
        private int m_Duration;

        /// <summary> 
        /// 获取或设置显示或者隐藏窗体的动画操作方式（默认为翻转方式）
        /// </summary> 
        [Description("获取或设置显示或者隐藏窗体的动画操作")]
        public AnimationMethod Method
        {
            get { return this.m_Method; }
            set { this.m_Method = value; }
        }

        /// <summary> 
        /// 获取或设置滚动或者滑动窗体的方向
        /// </summary> 
        [Description("获取或设置滚动或者滑动窗体的方向")]
        public AnimationDirection Direction
        {
            get { return this.m_Direction; }
            set { this.m_Direction = value; }
        }

        /// <summary> 
        /// 获取或设置动画效果的毫秒数值
        /// </summary> 
        [Description("获取或设置动画效果的毫秒数值")]
        public int Duration
        {
            get { return this.m_Duration; }
            set { this.m_Duration = value; }
        }

        /// <summary> 
        /// 获取待实现动画的窗体对象
        /// </summary> 
        [Description("获取待实现动画的窗体对象")]
        public Form Form
        {
            get { return this.m_Form; }
        }


        #endregion

        #region 构造函数

        /// <summary> 
        /// 为指定的窗体创建动画效果对象
        /// </summary> 
        /// <param name="form">待实现动画的窗体对象</param>
        /// <remarks> 
        /// 只有当动画操作方式、方向等属性指定，才实现动画效果，默认动画时长是250毫秒。
        /// </remarks> 
        public FormAnimator(Form form)
        {
            this.m_Form = form;
            this.m_Form.Load += new EventHandler(m_Form_Load);
            this.m_Form.VisibleChanged += new EventHandler(m_Form_VisibleChanged);
            this.m_Form.Closing += new CancelEventHandler(m_Form_Closing);            
            this.m_Duration = 250;//默认动画时长是250毫秒
        }

        /// <summary> 
        /// 为指定的窗体创建动画效果对象
        /// </summary> 
        /// <param name="form">待实现动画的窗体对象</param> 
        /// <param name="method">显示或者隐藏窗体的动画操作方式</param> 
        /// <param name="duration">动画效果的毫秒数值</param> 
        /// <remarks> 
        /// 只有当动画操作方式、方向等属性指定，才实现动画效果，默认动画时长是250毫秒。
        /// </remarks> 
        public FormAnimator(Form form, AnimationMethod method, int duration)
            : this(form)
        {
            this.m_Method = method;
            this.m_Duration = duration;
        }

        /// <summary> 
        /// 为指定的窗体创建动画效果对象
        /// </summary> 
        /// <param name="form">待实现动画的窗体对象</param> 
        /// <param name="method">显示或者隐藏窗体的动画操作方式</param> 
        /// <param name="direction">滚动或者滑动窗体的方向</param> 
        /// <param name="duration">动画效果的毫秒数值</param> 
        /// <remarks> 
        /// 只有当动画操作方式、方向等属性指定，才实现动画效果，默认动画时长是250毫秒。
        /// </remarks> 
        public FormAnimator(Form form, AnimationMethod method, AnimationDirection direction, int duration)
            : this(form, method, duration)
        {

            this.m_Direction = direction;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~FormAnimator()
        {
            this.m_Form = null;
        }


        #endregion

        #region 事件处理

        //窗体加载的时候，自动实现动画效果
        private void m_Form_Load(object sender, System.EventArgs e)
        {
            //MDI child forms do not support transparency so do not try to use the Blend method. 
            if (this.m_Form.MdiParent == null || this.m_Method != AnimationMethod.Blend)
            {
                //Activate the form. 
                AnimateWindow(this.m_Form.Handle, this.m_Duration, (int)FormAnimator.AW_ACTIVATE | (int)this.m_Method | (int)this.m_Direction);
            }
        }

        //窗体显示或者隐藏的时候，自动实现动画效果
        private void m_Form_VisibleChanged(object sender, System.EventArgs e)  // ERROR: Handles clauses are not supported in C# 
        {
            //Do not attempt to animate MDI child forms while showing or hiding as they do not behave as expected. 
            if (this.m_Form.MdiParent == null)
            {
                int flags = (int)this.m_Method | (int)this.m_Direction;

                if (this.m_Form.Visible)
                {
                    //Activate the form. 
                    flags = flags | FormAnimator.AW_ACTIVATE;
                }
                else
                {
                    //Hide the form. 
                    flags = flags | FormAnimator.AW_HIDE;
                }
                AnimateWindow(this.m_Form.Handle, this.m_Duration, flags);
            }
        }

        //窗体关闭的时候，自动实现动画效果
        private void m_Form_Closing(object sender, System.ComponentModel.CancelEventArgs e) // ERROR: Handles clauses are not supported in C#  
        {
            if (!e.Cancel)
            {
                //MDI child forms do not support transparency so do not try to use the Blend method. 
                if (this.m_Form.MdiParent == null || this.m_Method != AnimationMethod.Blend)
                {
                    //Hide the form. 
                    AnimateWindow(this.m_Form.Handle, this.m_Duration, (int)FormAnimator.AW_HIDE | (int)this.m_Method | (int)AnimationDirection.Down);
                }

            }
        }
        
        /// <summary>
        /// 关闭窗体对象处理
        /// </summary>
        public void Dispose()
        {
            this.m_Form.Load -= new EventHandler(m_Form_Load);
            this.m_Form.VisibleChanged -= new EventHandler(m_Form_VisibleChanged);
            this.m_Form.Closing -= new CancelEventHandler(m_Form_Closing);
        }

        #endregion

    }
}
