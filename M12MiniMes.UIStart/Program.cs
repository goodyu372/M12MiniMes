using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using Faster.Core;
using M12MiniMes.UI;
using CommunicateCenter;
using System.Drawing;
using FastInterface;
using System.Net.Sockets;
using System.Net;

namespace M12MiniMes.UIStart
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            Application.Run(new FormItemsView());
        }
    }


    public class ServerView : LazyAbstractView2<frmTcpServer>
    {
        public override Func<frmTcpServer> valueFactory => ()=> new frmTcpServer(TcpServer.Server);

        public override string InsertPath => $@"Mes服务器";

        public override Func<IBar, bool> FuncInitialize => p =>
        {
            bool b1 = ItemManager.Instance.Load();
            foreach (var item in ItemManager.Instance.MachineItems)
            {
                //在底部状态栏显示各设备客户端连接状态
                IBar iBar = IBarManager.Instance.CreateBar(item.设备名称,null,null, 
                    pp => { pp.BarStatus = new BarStatus() { IsEnable = false /*BackColor = Color.Tomato*/ }; return true; },
                    BarType.Check);
                //pp => { pp.BarStatus = new BarStatus() { BackColor = Color.Tomato }; return true; }
            }

            bool b2 = TcpServer.Load();

            //注册客户端连上与断开事件
            TcpServer.Server.NewClientAccepted += Server_NewClientAccepted;
            TcpServer.Server.ClientDropped += Server_ClientDropped;
            TcpServer.Server.Stopped += Server_Stopped;

            bool b3 = TcpServer.Server.Init("");
            TcpServer.StartMasterLogic();

            return b1;
        };
        private MachineItem FindMachineItemByMachineIP(string machineIP) 
        {
            return ItemManager.Instance.MachineItems.FirstOrDefault(p => p.Ip.Equals(machineIP));
        }
        private IBar FindBottomStatusIbarByMachineName(string machineName)
        {
            return IBarManager.Instance.codeCreatedBars.FirstOrDefault(p => p.InsertPath.Equals(machineName));
        }
        private void Server_NewClientAccepted(ITcpServer listener,Socket client, object state)
        {
            IPAddress ip = ((IPEndPoint)client.RemoteEndPoint).Address;
            MachineItem mItem = FindMachineItemByMachineIP(ip.ToString());
            if (mItem != null)
            {
                IBar bar = FindBottomStatusIbarByMachineName(mItem.设备名称);
                if (bar != null)
                {
                    ICheckBar iCheckbar = bar as ICheckBar;
                    iCheckbar.bChecked = true;
                    //bar.BarStatus = new BarStatus() { BackColor = Color.DeepSkyBlue };
                }
            }
        }
        private void Server_Stopped(object sender, EventArgs e)
        {
            foreach (var item in ItemManager.Instance.MachineItems)
            {
                IBar bar = FindBottomStatusIbarByMachineName(item.设备名称);
                if (bar != null)
                {
                    ICheckBar iCheckbar = bar as ICheckBar;
                    iCheckbar.bChecked = false;
                    //bar.BarStatus = new BarStatus() { BackColor = Color.Tomato };
                }
            }
        }

        private void Server_ClientDropped(ITcpServer listener, Socket client)
        {
            IPAddress ip = ((IPEndPoint)client.RemoteEndPoint).Address;
            MachineItem mItem = FindMachineItemByMachineIP(ip.ToString());
            if (mItem != null)
            {
                IBar bar = FindBottomStatusIbarByMachineName(mItem.设备名称);
                if (bar != null)
                {
                    ICheckBar iCheckbar = bar as ICheckBar;
                    iCheckbar.bChecked = false;
                    //bar.BarStatus = new BarStatus() { BackColor = Color.Tomato };
                }
            }
        }

        public override Func<IBar, bool> FuncSave => p =>
        {
            return TcpServer.Save();
        };

        //关闭软件时执行一次保存内存数据操作，减少频繁保存（之前实在TcpServer.cs 451行 通讯一次保存一次）  20200903  
        public override Func<IView, bool> FuncCloseView => p =>
        {
            return ItemManager.Instance.Save();
        };
    }

    public class ItemsView : LazyAbstractView<FormItemsView>
    {
        public override Func<IBar, bool> FuncSave => p =>
        {
            bool b = ItemManager.Instance.Save();
            return b;
        };

        public override string InsertPath => $@"生产内存数据";
    }

    public class View设备工序表 : LazyAbstractView<Frm设备表>
    {
        public override string InsertPath => $@"设备表";
    }
    public class View生产批次生成表 : LazyAbstractView<Frm生产批次生成表>
    {
        public override string InsertPath => $@"生产批次生成表";
    }
    public class View生产数据表 : LazyAbstractView<Frm生产数据表>
    {
        public override string InsertPath => $@"生产数据表";
    }
    public class View物料NG替换记录表 : LazyAbstractView<Frm物料ng替换记录表>
    {
        public override string InsertPath => $@"物料NG替换记录表";
    }
}
