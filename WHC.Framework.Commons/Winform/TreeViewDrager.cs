/*-----------��ǰ�������----------------------
 * 1.treeView_ItemDrag��������Ҫ�õ����϶��ڵ��ͼ�꣬�������ͨ��δ����װ��BaseTreeNode���������������ʵ�֡�
 * 2.�Ժ��������е�TreeNode���Ͷ�Ӧ���滻Ϊ�Զ�����չ��������ʵ�֣���BaseTreeNode��
 * 3.�п��ܵĻ����϶��ڵ�ĻӦ�ü�¼����־�
*/
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// TreeView�İ�װ�࣬ʵ������DragDrop�����Ĳ����ĸ����ࡣ
    /// <code>
    ///private void Init()
    ///{
    ///    TreeViewDrager treeViewDrager = new TreeViewDrager(this.treeView1);
    ///    treeViewDrager.TreeImageList = this.imageList1;//���������Ҳ���ԣ�ֻ���϶���ʱ��ûͼ�ꡣ
    ///    treeViewDrager.ProcessDragNode += new ProcessDragNodeEventHandler(treeViewDrager_ProcessDragNode);
    ///}
    ///
    ///private bool treeViewDrager_ProcessDragNode(TreeNode from, TreeNode to)
    ///{
    ///    ///�������from/to�����ڵ��¼����Ϣȥ�������ݿ�־û��Ĺ�����
    ///    ///���ݳ־û��Ľ�������ڵ��Ƿ������ʵ���϶�������
    ///    return true;
    ///}
    /// </code>
    /// </summary>
    public class TreeViewDrager
    {
        #region Data

        private Timer timer = new Timer();
        private TreeNode dragNode = null;
        private TreeNode tempDropNode = null;
        private TreeView tempTreeView;
        private ImageList imageListDrag = new ImageList();
		private ImageList imageListTreeView;
        private Image nodeImage;//����������¼�ڵ��ͼ�ꡣ

        /// <summary>
        /// �����϶�����ʱ����ʾ��ͼƬ����ʾ���е�һ��
        /// </summary>
		public ImageList TreeImageList
		{
			get
			{
				return this.imageListTreeView;
			}
			set
			{
				this.imageListTreeView = value;
			}
		}

        #endregion // Data

        #region Constructors

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public TreeViewDrager()
        {
        }

        /// <summary>
        /// �������캯�������ò�����TreeView
        /// </summary>
        /// <param name="treeView"></param>
        public TreeViewDrager(TreeView treeView)
        {
            this.tempTreeView = treeView;

            this.InitializeDrager();
        }

        private void InitializeDrager()
        {
            this.timer.Interval = 200;
            this.timer.Tick += new EventHandler(timer_Tick);

            this.tempTreeView.AllowDrop = true;
            this.tempTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.tempTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            this.tempTreeView.DragLeave += new System.EventHandler(this.treeView_DragLeave);
            this.tempTreeView.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.treeView_GiveFeedback);
            this.tempTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            this.tempTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
        }
        #endregion // Constructors

        #region Interface

        /// <summary>
        /// �����϶��ڵ����¼�
        /// </summary>
        public event ProcessDragNodeEventHandler ProcessDragNode;
		
        /// <summary>
        /// ��ָ���ļ�ʱ������ѹ�ȥ���Ҽ�ʱ����������״̬ʱ������
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            // ȡ�����λ�õĽڵ�
            Point pt = tempTreeView.PointToClient(Control.MousePosition);
            TreeNode node = this.tempTreeView.GetNodeAt(pt);

            if (node == null) return;

            // �����꿿�����ˣ��������Ϲ�����
            if (pt.Y < 30)
            {
                if (node.PrevVisibleNode != null)
                {
                    node = node.PrevVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    this.tempTreeView.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
            // �����꿿���׶ˣ��������¹�����
            else if (pt.Y > this.tempTreeView.Size.Height - 30)
            {
                if (node.NextVisibleNode != null)
                {
                    node = node.NextVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    this.tempTreeView.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
        }

        /// <summary>
        /// ���û���ʼ�϶��ڵ�ʱ������
        /// </summary>
        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // ȡ���϶��Ľڵ㣬��ѡ������
            this.dragNode = (TreeNode)e.Item;
            this.tempTreeView.SelectedNode = this.dragNode;
            //this.nodeImage =  //֪�����϶��Ľڵ㣬��Ӧ��֪���ڵ��ͼ����ʲô��

            // ���������϶��ڵ��ͼ��
            this.imageListDrag.Images.Clear();
            this.imageListDrag.ImageSize = new Size(this.dragNode.Bounds.Size.Width + this.tempTreeView.Indent, this.dragNode.Bounds.Height);

            // ������ͼ��
            // ���ͼ�꽫�������϶������ڵ��ͼ��
            Bitmap bitmap = new Bitmap(this.dragNode.Bounds.Width + this.tempTreeView.Indent, this.dragNode.Bounds.Height);

            Graphics graphics = Graphics.FromImage(bitmap);

            // ��ͼ����������϶��ڵ��ͼ�� 
			if (this.imageListTreeView != null)
			{
				graphics.DrawImage(this.imageListTreeView.Images[0], 0, 0);
			}

            // �ѱ��϶��ڵ�ı�ǩ
            graphics.DrawString(this.dragNode.Text,
                this.tempTreeView.Font,
                new SolidBrush(this.tempTreeView.ForeColor),
                (float)this.tempTreeView.Indent, 1.0f);

            this.imageListDrag.Images.Add(bitmap);

			Point point = this.tempTreeView.PointToClient(Control.MousePosition);
			int dxHotspot = point.X + this.tempTreeView.Indent - this.dragNode.Bounds.Left;
			int dyHotspot = point.Y - this.dragNode.Bounds.Top;

            // ��ʼ��קͼ��
			if (DragHelper.ImageList_BeginDrag(this.imageListDrag.Handle, 0, dxHotspot, dyHotspot))
            {
                // ��ʼ
				this.tempTreeView.DoDragDrop(bitmap, DragDropEffects.Move);
                // ����
                DragHelper.ImageList_EndDrag();
            }

        }

        /// <summary>
        /// �ڽ������ϵ��ؼ��ı߽��Ϸ�����
        /// </summary>
        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            // �����϶���λ�ò��ƶ�ͼƬ
			Point point = this.tempTreeView.PointToClient(new Point(e.X, e.Y));
			DragHelper.ImageList_DragMove(point.X - this.tempTreeView.Left, point.Y - this.tempTreeView.Top);

            // ȡ��ʵ����ק��Ŀ��ڵ�
			TreeNode nodeAt = this.tempTreeView.GetNodeAt(this.tempTreeView.PointToClient(new Point(e.X, e.Y)));
			if (nodeAt == null)
            {
                e.Effect = DragDropEffects.None;
			}
			else
			{
				e.Effect = DragDropEffects.Move;
				if (this.tempDropNode != nodeAt)
				{
					DragHelper.ImageList_DragShowNolock(false);
					this.tempTreeView.SelectedNode = nodeAt;
					DragHelper.ImageList_DragShowNolock(true);
					this.tempDropNode = nodeAt;
				}
				TreeNode treeNode = nodeAt;
				while (treeNode.Parent != null)
				{
					if (treeNode.Parent == this.dragNode)
					{
						e.Effect = DragDropEffects.None;
					}
					treeNode = treeNode.Parent;
				}
			}
		}

        /// <summary>
        /// ������ϷŲ���ʱ���������о����ƶ����������������
        /// </summary>
        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            // Unlock updates
            DragHelper.ImageList_DragLeave(this.tempTreeView.Handle);

            // ȡ��Ŀ��ڵ�
			TreeNode nodeAt = this.tempTreeView.GetNodeAt(this.tempTreeView.PointToClient(new Point(e.X, e.Y)));

            // �����ק�Ľڵ㲻����Ŀ��ڵ㣬��������ק�Ľڵ�ΪĿ��ڵ���ӽڵ㡣
			if (this.dragNode != nodeAt)
            {
                //ί�г�ȥ���־û�����������־û��ɹ��������ƶ��������ƶ���
				if (this.ProcessDragNode != null && this.ProcessDragNode(this.dragNode, nodeAt))
                {
                    if (this.dragNode.Parent == null)
                    {
                        this.tempTreeView.Nodes.Remove(this.dragNode);
                    }
                    else
                    {
                        this.dragNode.Parent.Nodes.Remove(this.dragNode);
                    }

                    // ����ק�Ľڵ����ӵ�Ŀ��ڵ��ϡ�
					nodeAt.Nodes.Add(this.dragNode);
					nodeAt.ExpandAll();

                    this.dragNode = null;
                    this.timer.Enabled = false;
                }
                else
				{
					MessageUtil.ShowTips("�־û�ʧ�ܣ������ƶ��ڵ㣡");
				}
			}
		}

        /// <summary>
        /// �ڽ���������ؼ��ı߽�ʱ������
        /// </summary>
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
			DragHelper.ImageList_DragEnter(this.tempTreeView.Handle, e.X - this.tempTreeView.Left, e.Y - this.tempTreeView.Top);
            this.timer.Enabled = true;
        }

        /// <summary>
        /// �ڽ������ϳ��ؼ��ı߽�ʱ������
        /// </summary>
        private void treeView_DragLeave(object sender, EventArgs e)
        {
            DragHelper.ImageList_DragLeave(this.tempTreeView.Handle);
            this.timer.Enabled = false;
        }

        /// <summary>
        /// �ڿؼ����ս���ʱ������
        /// </summary>
        private void treeView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                // ��ʾ�϶�ʱ��������ʽ
                e.UseDefaultCursors = false;
                this.tempTreeView.Cursor = Cursors.Default;
            }
			else
			{
				e.UseDefaultCursors = true;
			}
        }

        #endregion //Private Interface
    }

    /// <summary>
    /// �������Ҫ�õ����¼��йصĵط��Ƚ϶��ͳһ�ŵ���һ��Events���
    /// </summary>
    /// <param name="dragNode">�϶��Ľڵ�</param>
    /// <param name="dropNode">���µĽڵ�</param>
    /// <returns></returns>
    public delegate bool ProcessDragNodeEventHandler(TreeNode dragNode, TreeNode dropNode);

    /// <summary>
    /// TreeView �϶�������
    /// </summary>
    internal class DragHelper
    {
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControls();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_BeginDrag(IntPtr himlTrack, int
            iTrack, int dxHotspot, int dyHotspot);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragMove(int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern void ImageList_EndDrag();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragLeave(IntPtr hwndLock);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragShowNolock(bool fShow);

        static DragHelper()
        {
            InitCommonControls();
        }
    }
}
