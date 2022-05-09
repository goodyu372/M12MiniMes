using CommunicateCenter;
using Fi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using M12MiniMes.Entity;
using WHC.Framework.ControlUtil;
using M12MiniMes.BLL;
using System.Runtime.CompilerServices;
//using M12MinMes.MachineStatus;

namespace M12MiniMes.UIStart
{
    public static class TcpServer
    {
        public static object objLock = new object();
        public static AsyncTcpServer Server;

        public static bool Save() 
        {
            CommonSerializer.SaveObjAsBinaryFile(Server, $@"D:\Fi.Data\TcpServer.xml", out bool bSaveOK, out Exception ex);
            return bSaveOK;
        }

        public static bool Load() 
        {
            Server = CommonSerializer.LoadObjFromBinaryFile<AsyncTcpServer>($@"D:\Fi.Data\TcpServer.xml", out bool bLoadOK, out Exception ex);
            return bLoadOK;
        }

        //主逻辑处理
        public static void StartMasterLogic() 
        {
            Server.DataReceived += Server_DataReceived;

            //开一个线程 给所有客户端发送心跳 10S一次
            //Task.Factory.StartNew(async() =>
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            byte[] bt = Encoding.UTF8.GetBytes($@"{Header.XT.ToString()}");
            //            Server.SendMesAsyncToAllClients(bt);
            //            await Task.Delay(10000);
            //        }
            //        catch (Exception ex)
            //        {
            //            LogService.Warn(ex.Message, ex);
            //        }
            //    }
            //});
        }

        private static void Server_DataReceived(FiInterface.ITcpServer listener, Socket client, byte[] data, int length)
        {
            lock (objLock)
            {
                try
                {
                    string mes = Encoding.UTF8.GetString(data, 0, length);  //客户端发送过来的数据
                    if (string.IsNullOrEmpty(mes))
                    {
                        return;
                    }
                    string[] mess = mes.Split(',');
                    if (mess.Count() <= 1)  //无效信息，不在定义的通讯协议之内
                    {
                        var var = Encoding.UTF8.GetBytes("invalid data!");
                        listener.SendMesAsyncToClient(client, var);
                        return;
                    }
                    string strHeader = mess[0]; //行头
                    if (!Enum.GetNames(typeof(Header)).Contains(strHeader)) //不在定义的行头//例如 CX, //查询数据， XR, //写入生产数据等
                    {
                        //未定义的通讯格式！;
                        var var = Encoding.UTF8.GetBytes("Undefined Header!");
                        listener.SendMesAsyncToClient(client, var);
                        return;
                    }
                  
                    IPAddress ip = ((IPEndPoint)client.RemoteEndPoint).Address;
                    string strIP = ip.ToString();
                    //找到该地址对应的设备信息
                    MachineItem machineItem = ItemManager.Instance.GetMachineItemByIP(strIP);
                    if (machineItem == null)
                    {
                        throw new Exception($@"查询设备信息无该记录IP {strIP} ，请先进行同步！");
                    }

                    Header header = (Header)Enum.Parse(typeof(Header), strHeader);
                    string[] parameters = mess.Skip(1).ToArray();
                    string strInMachineID = ""; //当前流入设备ID
                    string strInMachineName = ""; //当前流入设备名称
                    MachineItem InMachineItem = null; //当前流入设备Item
                    string strCMachineID = ""; //被查询设备ID
                    string strCMachineName = ""; //被查询设备名称
                    MachineItem CMachineItem = null; //被查询设备Item
                    string rfid = "";
                    FixtureItem fixtureItem = null; //当前治具
                    byte[] dataSend = null;

                    //设备状态表添加
                    string strInMachineStatus = "";//当前流入的设备状态
                    string strInMachineAlarmInformation = "";//当前流入的设备报警信息

                    switch (header)
                    {
                        case Header.CX:  //查询生产数据
                            strInMachineID = parameters[0];
                            InMachineItem = ItemManager.Instance.GetMachineItemByID(strInMachineID);
                            strInMachineName = InMachineItem.设备名称;
                            strCMachineID = parameters[1];
                            rfid = parameters[2];
                            fixtureItem = ItemManager.Instance.GetFixtureItem(rfid, strInMachineID);
                            if (fixtureItem == null) //找不到该RFID治具的内存信息  不允许新治具跳过线头设备流入生产线（即要求新治具必须从线头开始流入）
                            {
                                var var = Encoding.UTF8.GetBytes($@"get the fixture failed which rfid is {rfid} !");
                                listener.SendMesAsyncToClient(client, var);
                                return;
                            }
                            string strData = $@"CX,{strCMachineID},{rfid},{fixtureItem.治具生产批次号}";
                            if (fixtureItem.MaterialItems.Count == 0) //如果第一次治具不携带任何物料，则赋予12个null进去
                            {
                                for (int i = 0; i < 12; i++)
                                {
                                    fixtureItem.MaterialItems.Add(null);
                                }
                            }
                            #region 特殊设备需要读写两个参数
                            string[] specialMachine = new string[6] { "1", "2", "3", "6", "10", "11" };
                            string itemEmptyMes = "";
                            if (specialMachine.Contains(strInMachineID))
                            {
                                itemEmptyMes = @",0/0";
                            }
                            else
                            {
                                itemEmptyMes = ",0";
                            }
                            #endregion
                            foreach (var item in fixtureItem.MaterialItems)
                            {
                                if (item == null)
                                {
                                    strData += itemEmptyMes;
                                    continue;
                                }
                                var var = item.生产数据List.Where(p => p.设备id.ToString() == strCMachineID);
                                if (var == null)
                                {
                                    strData += itemEmptyMes;
                                    continue;
                                }
                                //找出物料指定设备ID的工序数据
                                var var2 = var.Select(p => p.工序数据).FirstOrDefault() ?? itemEmptyMes;
                                if (!var2.StartsWith(","))
                                {
                                    var2 = $@",{var2}";
                                }
                                strData += var2;
                            }
                            dataSend = Encoding.UTF8.GetBytes(strData);
                            listener.SendMesAsyncToClient(client, dataSend);
                            break;
                        case Header.XR: //写入生产数据  //当设备做完工序时，治具准备流出时
                            rfid = parameters[0];
                            strInMachineID = parameters[1];
                            #region 防止线头设备的重复写入
                            bool allowClearIfForm0machine = true;
                            if (strInMachineID == "0")
                            {
                                if (ItemManager.Instance.last0MachineXRHistoryInfo != null)
                                {
                                    if (ItemManager.Instance.last0MachineXRHistoryInfo.RFID == rfid && ItemManager.Instance.last0MachineXRHistoryInfo.machineID == strInMachineID)
                                    {
                                        TimeSpan timeSpan = DateTime.Now - ItemManager.Instance.last0MachineXRHistoryInfo.createdTime;
                                        if (timeSpan.TotalSeconds <= 5)  //5秒内重复写入的话忽视掉(不清空治具信息)
                                        {
                                            allowClearIfForm0machine = false;
                                        }
                                    }
                                }
                                ItemManager.Instance.last0MachineXRHistoryInfo = new ItemManager.historyInfo() { createdTime = DateTime.Now, RFID = rfid, machineID = strInMachineID };
                            }
                            #endregion
                            InMachineItem = ItemManager.Instance.GetMachineItemByID(strInMachineID);
                            fixtureItem = ItemManager.Instance.GetFixtureItem(rfid, strInMachineID, allowClearIfForm0machine);
                            if (fixtureItem == null) //找不到该RFID治具的内存信息
                            {
                                var var = Encoding.UTF8.GetBytes($@"get the fixture failed which rfid is {rfid} !");
                                listener.SendMesAsyncToClient(client, var);
                                return;
                            }
                            int index = 2; //parameters解析索引
                            int numbers = 12; //一般是写入12个物料信息
                            int k = parameters.Skip(2).Count() / 2;
                            numbers = Math.Min(k, 12);
                            for (int i = 0; i < numbers; i++)   //写入数据格式：值1,是否ok1，值2,是否ok2，……值12,是否ok12
                            {
                                MaterialItem materialItem = fixtureItem.MaterialItems[i];
                                if (materialItem == null)
                                {
                                    materialItem = new MaterialItem(fixtureItem);
                                    fixtureItem.InsertMaterialItem(i, materialItem);
                                }

                                string 物料guid = materialItem.MaterialGuid.ToString();
                                string 治具guid = materialItem.Fixture.FixtureGuid.ToString();
                                int 设备id = int.Parse(strInMachineID);

                                //检测是第一次写入还是再次写入刷新生产数据 最好规定下位机只允许写入一次
                                生产数据表Info scData = materialItem.生产数据List.FirstOrDefault(p =>
                                    p.物料guid.Equals(物料guid)
                                    && p.治具guid.Equals(治具guid)
                                    && p.设备id.Equals(设备id)
                                    );

                                bool firstWrite = false;  //是否第一次写入
                                if (scData == null)  //是第一次写入
                                {
                                    scData = new 生产数据表Info();
                                    materialItem.生产数据List.Add(scData);
                                    firstWrite = true;
                                }
                                scData.生产时间 = DateTime.Now;
                                scData.物料生产批次号 = materialItem.物料生产批次号;
                                scData.治具生产批次号 = materialItem.Fixture.治具生产批次号;
                                scData.物料guid = 物料guid;
                                scData.治具guid = 治具guid;
                                scData.治具rfid = rfid;
                                scData.治具孔号 = materialItem.GetHoleIndexInFixture();
                                scData.设备id = 设备id;
                                scData.设备名称 = strInMachineName;
                                scData.工位号 = "";
                                scData.工序数据 = parameters[index];
                                scData.结果ok = parameters[index + 1] == "1";  //0表示无 1表示OK 2表示NG
                                if (firstWrite)
                                {
                                    //写入一条生产数据到数据库中
                                    scData.生产数据id = BLLFactory<生产数据表>.Instance.Insert2(scData);

                                    //如果是线头/线尾 则更新 上线数/下线数
                                    if (i == 11 && (strInMachineID == "0" || strInMachineID == "14"))
                                    {
                                        string str当前治具生产批次号 = materialItem.Fixture.治具生产批次号;
                                        string condition = $@"生产批次号 = '{str当前治具生产批次号}'";
                                        var var = BLLFactory<生产批次生成表>.Instance.FindLast(condition);
                                        if (var != null)
                                        {
                                            if (strInMachineID == "0")
                                            {
                                                var.上线数 += 12;
                                                var.状态 = "生产中";
                                            }
                                            else if (strInMachineID == "14")
                                            {
                                                var.下线数 += 12;

                                                //如果这个治具批次号与上一个治具的批次号不相同，则认为上一个治具批次的生产状态为生产完成
                                                if (str当前治具生产批次号 != ItemManager.Instance.str线尾上一个治具批次号)
                                                {
                                                    condition = $@"生产批次号 = '{ItemManager.Instance.str线尾上一个治具批次号}'";
                                                    var var2 = BLLFactory<生产批次生成表>.Instance.FindLast(condition);
                                                    if (var2 != null)
                                                    {
                                                        var2.状态 = "生产完成";
                                                        BLLFactory<生产批次生成表>.Instance.Update(var2, var2.生产批次id);
                                                    }
                                                }

                                                ItemManager.Instance.str线尾上一个治具批次号 = str当前治具生产批次号;
                                            }
                                            BLLFactory<生产批次生成表>.Instance.Update(var, var.生产批次id);
                                        }
                                    }
                                }
                                else
                                {
                                    BLLFactory<生产数据表>.Instance.Update(scData, scData.生产数据id);  //更新一条数据到数据库中
                                }
                                index += 2;
                            }
                            dataSend = Encoding.UTF8.GetBytes("XROK"); //返回下位机"写入完成"
                            listener.SendMesAsyncToClient(client, dataSend);
                            break;
                        case Header.NGTH:  //NG替换
                                           //1、把一个NG物料从治具上取出并丢弃后，腾出位置
                                           //2、从暂存位的治具上取一个好物料出来，放到上述腾出位置
                            string preRFID = parameters[0];
                            string strPreHoleIndex = parameters[1];
                            int iPreHoleIndex = int.Parse(strPreHoleIndex);
                            string nowRFID = parameters[2];
                            string strNowHoleIndex = parameters[3];
                            int iNowHoleIndex = int.Parse(strNowHoleIndex);
                            strInMachineID = parameters[4];
                            string stationID = parameters[5];

                            InMachineItem = ItemManager.Instance.GetMachineItemByID(strInMachineID);
                            strInMachineName = InMachineItem.设备名称;
                            FixtureItem preFixture = ItemManager.Instance.GetFixtureItem(preRFID, strInMachineID); //替换前治具
                            FixtureItem nowFixture = ItemManager.Instance.GetFixtureItem(nowRFID, strInMachineID); //替换后治具
                            MaterialItem thMaterialItem = preFixture.MaterialItems.ElementAtOrDefault(iPreHoleIndex); //替换的物料
                            MaterialItem ngMaterialItem = nowFixture.MaterialItems.ElementAtOrDefault(iNowHoleIndex);

                            物料ng替换记录表Info ngInfo = new 物料ng替换记录表Info();
                            ngInfo.Ng替换时间 = DateTime.Now;
                            ngInfo.物料生产批次号 = ngMaterialItem?.物料生产批次号;
                            ngInfo.设备id = int.Parse(strInMachineID);
                            ngInfo.设备名称 = strInMachineName;
                            ngInfo.工位号 = stationID;
                            ngInfo.物料guid = ngMaterialItem?.MaterialGuid.ToString();
                            ngInfo.替换前治具guid = preFixture.FixtureGuid.ToString();
                            ngInfo.替换前治具rfid = preRFID;
                            ngInfo.替换前治具孔号 = iPreHoleIndex;
                            ngInfo.前治具生产批次号 = preFixture.治具生产批次号;
                            ngInfo.替换后治具guid = nowFixture.FixtureGuid.ToString();
                            ngInfo.替换后治具rfid = nowRFID;
                            ngInfo.替换后治具孔号 = iNowHoleIndex;
                            ngInfo.后治具生产批次号 = nowFixture.治具生产批次号;

                            //检测是第一次替换还是再次替换刷新数据 最好规定下位机只允许发送替换一次
                            bool bFirstTH = false;
                            string condition2 = $@"物料生产批次号 = '{ngInfo.物料生产批次号}' and 设备id = {ngInfo.设备id} 
                                            and 物料guid = '{ngInfo.物料guid}' and 替换前治具guid = '{ngInfo.替换前治具guid}'
                                            and 替换前治具rfid = '{ngInfo.替换前治具rfid}' and 替换前治具孔号 = {ngInfo.替换前治具孔号}
                                            and 前治具生产批次号 = '{ngInfo.前治具生产批次号}' and 替换后治具guid = '{ngInfo.替换后治具guid}'
                                            and 替换后治具rfid = '{ngInfo.替换后治具rfid}' and 替换后治具孔号 = {ngInfo.替换后治具孔号}
                                            and 后治具生产批次号 = '{ngInfo.后治具生产批次号}'";
                            var var3 = BLLFactory<物料ng替换记录表>.Instance.FindLast(condition2);
                            if (var3 == null)
                            {
                                bFirstTH = true;
                            }
                            if (bFirstTH)
                            {
                                preFixture.RemoveMaterialItem(thMaterialItem);
                                nowFixture.RemoveMaterialItem(ngMaterialItem);
                                nowFixture.InsertMaterialItem(iNowHoleIndex, thMaterialItem);

                                BLLFactory<物料ng替换记录表>.Instance.Insert(ngInfo);  //写入一条数据到数据库中
                            }
                            else
                            {
                                BLLFactory<物料ng替换记录表>.Instance.Update(ngInfo, var3.Ng替换记录id);
                            }

                            dataSend = Encoding.UTF8.GetBytes("NGTHOK"); //返回下位机"NG替换完成"
                            listener.SendMesAsyncToClient(client, dataSend);
                            break;
                        case Header.XT:  //心跳

                            break;
                        case Header.TL:  //投料
                            strInMachineID = parameters[0];
                            string pc = parameters[1];
                            if (!string.IsNullOrEmpty(pc))
                            {
                                string condition4 = $@"生产批次号 = '{pc}'";
                                var var4 = BLLFactory<生产批次生成表>.Instance.FindLast(condition4);
                                if (var4 != null)
                                {
                                    int number = int.Parse(parameters[2]);
                                    switch (strInMachineID)
                                    {
                                        case "1":
                                            var4.镜片105投料数 = number;
                                            break;
                                        case "2":
                                            var4.镜片104投料数 = number;
                                            break;
                                        case "6":
                                            var4.镜片g3投料数 = number;
                                            break;
                                        case "10":
                                            var4.镜片102投料数 = number;
                                            break;
                                        case "11":
                                            var4.镜片95b投料数 = number;
                                            break;
                                    }
                                    BLLFactory<生产批次生成表>.Instance.Update(var4, var4.生产批次id);
                                    dataSend = Encoding.UTF8.GetBytes("TLOK"); //返回下位机"TL完成"
                                    listener.SendMesAsyncToClient(client, dataSend);
                                }
                            }
                            break;
                        case Header.CXPC:  //查询批次信息
                            strInMachineID = parameters[0];
                            string pcOLD = parameters[1];
                            生产批次生成表Info iNEW = null;
                            if (string.IsNullOrEmpty(pcOLD) || pcOLD.Equals("noneBatchSN"))
                            {
                                iNEW = ItemManager.Instance.GetFirst在产批次();
                            }
                            else
                            {
                                //var vaL = ItemManager.Instance.Get当前在产批次列表();  //注意这里只会获取在产的批次列表，已生产完成的批次将会被过滤掉
                                //if (vaL.Count >0)
                                //{
                                //    生产批次生成表Info iOLD = vaL.FirstOrDefault(p => p.生产批次号.Equals(pcOLD));
                                //    if (iOLD != null)
                                //    {
                                //        int indexOLD = vaL.IndexOf(iOLD);
                                //        if (indexOLD < vaL.Count -1)
                                //        {
                                //            iNEW = vaL[indexOLD + 1]; //下一个批次
                                //        }
                                //    }
                                //}

                                //2020-03-28改为获取数据库中所有批次列表
                                var vaL = ItemManager.Instance.Get批次列表();
                                if (vaL.Count > 0)
                                {
                                    生产批次生成表Info iOLD = vaL.FirstOrDefault(p => p.生产批次号.Equals(pcOLD));
                                    if (iOLD != null)
                                    {
                                        int indexOLD = vaL.IndexOf(iOLD);
                                        //跳过旧批次，过滤出旧批次后面的列表
                                        vaL = vaL.Skip(indexOLD + 1).ToList();
                                        if (vaL != null && vaL.Count > 0)
                                        {
                                            iNEW = vaL.FirstOrDefault(); //下一个批次
                                        }
                                    }
                                }
                            }
                            string strM = "";
                            if (iNEW != null)
                            {
                                #region 下发批次打折
                                MachineItem machine = ItemManager.Instance.MachineItems.FirstOrDefault();
                                if (machine != null)
                                {
                                    iNEW.计划投入数 = (int)(iNEW.计划投入数 * machine.ReduceOffsetsPercent) - machine.ReduceOffsets;
                                }
                                #endregion

                                //返回下位机下一个批次信息
                                strM = $@"CXPC,{strInMachineID},{iNEW.生产批次号},{iNEW.机种},{iNEW.镜框日期.ToString("yyyyMMdd")},{iNEW.镜筒模穴号},{iNEW.镜框批次},{iNEW.穴号105},{iNEW.穴号104},{iNEW.穴号102},{iNEW.日期105.ToString("yyyyMMdd")},{iNEW.日期104.ToString("yyyyMMdd")},{iNEW.日期102.ToString("yyyyMMdd")},{iNEW.角度},{iNEW.系列号},{iNEW.隔圈模穴号113b},{iNEW.成型日113b.ToString("yyyyMMdd")},{iNEW.隔圈模穴号112},{iNEW.成型日112.ToString("yyyyMMdd")},{iNEW.G3来料供应商},{iNEW.G3镜片来料日期.ToString("yyyyMMdd")},{iNEW.G1来料供应商},{iNEW.G1来料日期.ToString("yyyyMMdd")},{iNEW.配对监控批次},{iNEW.计划投入数}";
                            }
                            else
                            {
                                strM = $@"CXPC,{strInMachineID},noneBatchSN,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1--1--1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1";
                            }
                            dataSend = Encoding.UTF8.GetBytes(strM);
                            listener.SendMesAsyncToClient(client, dataSend);
                            break;

                        case Header.CXSBZT://查询设备状态//plc发送数据格式：CXSBZT,查询的设备id

                            break;

                        case Header.XRSBZT://写入设备状态//plc发送数据格式：{XRSBZT,设备id，设备名称，设备状态代号，报警代号}举例：XRSBZT,1,,01，02
                            strInMachineID = parameters[0];
                            strInMachineName = parameters[1];
                            int strInMachineStatusID= int.Parse(parameters[2]);//需要根据代号来获取设备状态（启动、暂停、维修、点检、报警）                          
                            if (MachineStatus.machineStatus.DicMachineStatus.Keys.Contains(strInMachineStatusID))
                            {
                                strInMachineStatus = MachineStatus.machineStatus.DicMachineStatus[strInMachineStatusID];
                            }
                            else
                            {
                                strInMachineStatus = "";
                            }

                            bool b = int.TryParse(parameters[3], out int strInMachineAlarmID);                                                    
                            if(b)
                            {
                                if (MachineStatus.machineStatus.DicMachineAlarmInformation.Keys.Contains(strInMachineAlarmID))
                                {
                                    strInMachineAlarmInformation = MachineStatus.machineStatus.DicMachineAlarmInformation[strInMachineAlarmID];
                                }
                                else
                                {
                                    strInMachineAlarmInformation = "";                                    
                                }
                            }
                            else
                            {
                                strInMachineAlarmInformation = "";
                            }

                            设备状态表Info MachineStatu = new 设备状态表Info();
                            MachineStatu.发生时间 = DateTime.Now;
                            MachineStatu.设备id = int.Parse(strInMachineID);
                            MachineStatu.设备名称 = strInMachineName;
                            MachineStatu.设备状态 = strInMachineStatus;
                            MachineStatu.报警信息 = strInMachineAlarmInformation;
                            
                            BLLFactory<设备状态表>.Instance.Insert(MachineStatu);//向表中插入一条信息
                            dataSend = Encoding.UTF8.GetBytes("XRSBZTOK"); //返回下位机"写入完成"
                            listener.SendMesAsyncToClient(client, dataSend);                           
                             
                            break;
                        case Header.XRSC://写入设备各状态运行时长，plc发送数据格式：{XRSC,设备id，设备名称，运行时长，等待时长，暂停时长，手动时长，报警时长，点检时长，维修时长}举例：XRSC,1,,2000,1500,1800,2300,1000,5000,6000
                            strInMachineID =parameters[0];
                            strInMachineName = parameters[1];

                            bool brun, bwait, bpause, bmanual, balarm, bspotcheck, bmaintain;

                            brun =int.TryParse( parameters[2],out int runtime);
                            bwait = int.TryParse(parameters[3], out int waittime);
                            bpause = int.TryParse(parameters[4], out int pausetime);
                            bmanual = int.TryParse(parameters[5], out int manualtime);
                            balarm = int.TryParse(parameters[6], out int alarmtime);
                            bspotcheck = int.TryParse(parameters[7], out int spotchecktime);
                            bmaintain = int.TryParse(parameters[8], out int maintaintime);

                            string run, wait, pause, manual, alarm, spotcheck, maintain;
                            if (brun)//1
                            {
                                run= CacluationMethod.GetHMS(runtime);
                            }
                            else
                            {
                                run = "0";
                            }

                            if (bwait)//2
                            {
                                wait = CacluationMethod.GetHMS(waittime);
                            }
                            else
                            {
                                wait = "0";
                            }

                            if (bpause)//3
                            {
                                pause = CacluationMethod.GetHMS(pausetime);
                            }
                            else
                            {
                                pause = "0";
                            }

                            if (bmanual)//4
                            {
                                manual = CacluationMethod.GetHMS(manualtime);
                            }
                            else
                            {
                                manual = "0";
                            }

                            if (balarm)//5
                            {
                                alarm = CacluationMethod.GetHMS(alarmtime);
                            }
                            else
                            {
                                alarm = "0";
                            }

                            if (bspotcheck)//6
                            {
                                spotcheck = CacluationMethod.GetHMS(spotchecktime);
                            }
                            else
                            {
                                spotcheck = "0";
                            }

                            if (bmaintain)//7
                            {
                                maintain = CacluationMethod.GetHMS(maintaintime);
                            }
                            else
                            {
                                maintain = "0";
                            }
                            设备状态时长表Info StatuTime = new 设备状态时长表Info();

                            StatuTime.设备id = int.Parse(strInMachineID);
                            StatuTime.设备名称 = strInMachineName;
                            StatuTime.记录时间 = DateTime.Now;
                            StatuTime.运行 = run;
                            StatuTime.等待 = wait;
                            StatuTime.暂停 = pause;
                            StatuTime.手动 = manual;
                            StatuTime.报警 = alarm;
                            StatuTime.点检 = spotcheck;
                            StatuTime.维修 = maintain;

                            BLLFactory<设备状态时长表>.Instance.Insert(StatuTime);//向表中插入一条信息
                            dataSend = Encoding.UTF8.GetBytes("XRCSOK"); //返回下位机"写入完成"
                            listener.SendMesAsyncToClient(client, dataSend);

                            break;
                    }
                    
                    //ItemManager.Instance.Save(); //每通讯一次就保存一次内存数据  20200903改为Program.cs 124行 关闭软件时保存内存数据 减少保存频率
                }
                catch (Exception ex)
                {
                    LogService.Warn(ex.Message, ex);
                }
            }
        }
    }

    //定义协议头
    public enum Header  
    {
        CX, //查询数据
        XR, //写入生产数据
        NGTH, //NG替换
        XT, //心跳
        TL, //投料
        CXPC ,//查询批次信息
        XRSBZT,//写入设备状态
        CXSBZT,//查询设备状态
        XRSC//写入时长
    }

   


}
