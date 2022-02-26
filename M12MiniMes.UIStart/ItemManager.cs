using Faster.Core;
using M12MiniMes.BLL;
using M12MiniMes.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHC.Framework.ControlUtil;

namespace M12MiniMes.UIStart
{

    [Serializable]
    public class ItemManager
    {
        private static ItemManager instance;
        public static ItemManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// 保存线尾机器上一个治具的批次号，以用来对线尾当前治具批次号判断是否相同，若不相同则判断上一个治具的批次已经生产完成。
        /// </summary>
        public string str线尾上一个治具批次号;

        /// <summary>
        /// 内存储存当前在产的所有设备数据
        /// </summary>
        public List<MachineItem> MachineItems { get; private set; } = new List<MachineItem>();

        /// <summary>
        /// 内存储存当前在产的所有设备的所有治具的数据汇总
        /// </summary>
        [field: NonSerialized]
        public List<FixtureItem> AllCurrentFixtureItems
        {
            get
            {
                List<FixtureItem> items = new List<FixtureItem>();
                foreach (var p in this.MachineItems)
                {
                    if (p.CurrentFixtureItems != null && p.CurrentFixtureItems.Count > 0)
                    {
                        items = items.Union(p.CurrentFixtureItems).ToList();
                    }
                }
                return items;
            }
        }

        /// <summary>
        /// 内存储存当前在产的所有设备的所有治具的所有物料数据汇总
        /// </summary>
        [field: NonSerialized]
        public List<MaterialItem> AllCurrentMaterialItems
        {
            get
            {
                List<MaterialItem> items = new List<MaterialItem>();
                this.AllCurrentFixtureItems?.ForEach(p =>
                {
                    if (p.MaterialItems != null && p.MaterialItems.Count > 0)
                    {
                        items = items.Union(p.MaterialItems).ToList();
                    }
                });
                return items;
            }
        }

        /// <summary>
        /// 保存内存数据
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            CommonSerializer.SaveObjAsBinaryFile(instance, $@"D:\Faster.Data\ItemManager.xml", out bool bSaveOK, out Exception ex);
            return bSaveOK;
        }

        /// <summary>
        /// 加载内存数据
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            instance = CommonSerializer.LoadObjFromBinaryFile<ItemManager>($@"D:\Faster.Data\ItemManager.xml", out bool bLoadOK, out Exception ex);
            return bLoadOK; 
        }

        public List<生产批次生成表Info> Get批次列表() //获取数据库中所有批次列表，不管生产状态
        {
            List<生产批次生成表Info> list = BLLFactory<生产批次生成表>.Instance.GetAll();  //旧到新排序
            return list;
        }

        public List<生产批次生成表Info> Get当前在产批次列表()  //只会获取在产或待生产的批次列表
        {
            string condition = $@"状态 != '生产完成' and (计划投入数 > 0) and (计划投入数 > 上线数)";
            #region 下发批次打折
            MachineItem machine = ItemManager.Instance.MachineItems.FirstOrDefault();
            if (machine != null)
            {
                condition = $@"状态 != '生产完成' and (计划投入数 > 0) and (计划投入数 * {machine.ReduceOffsetsPercent} - {machine.ReduceOffsets} > 上线数)";
            }
            #endregion
            List<生产批次生成表Info> list = BLLFactory<生产批次生成表>.Instance.Find(condition);  //旧到新排序
            return list;
        }

        public 生产批次生成表Info GetFirst在产批次()
        {
            return Get当前在产批次列表()?.FirstOrDefault();
        }


        /// <summary>
        /// 从数据库设备表中同步
        /// </summary>
        public void GetMachineItems() 
        {
            List<设备表Info> list = BLLFactory<设备表>.Instance.GetAll();
            var var = list.Select(p => new MachineItem() 
            {
                设备id = p.设备id,
                设备名称 = p.设备名称,
                Ip = p.Ip
            }).ToList();
            this.MachineItems = new List<MachineItem>(var);
        }


        /// <summary>
        /// 通过RFID获取当前在产的治具
        /// </summary>
        /// <param name="RFID"></param>
        /// <returns></returns>
        public FixtureItem GetCurrentFixtureItemByRFID(string RFID)
        {
            if (string.IsNullOrEmpty(RFID))
            {
                throw new Exception("RFID为空！");
            }
            return this.AllCurrentFixtureItems?.FirstOrDefault(p => p.RFID.Equals(RFID));
        }

        /// <summary>
        /// 通过设备ID获取该设备信息Item
        /// </summary>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public MachineItem GetMachineItemByID(string machineID)
        {
            MachineItem tempMachineItem = this.MachineItems?.FirstOrDefault(p => p.设备id.ToString().Equals(machineID));
            if (tempMachineItem == null)
            {
                throw new Exception($@"ID为[{machineID}]的设备不存在！");
            }
            return tempMachineItem;
        }

        /// <summary>
        /// 通过设备IP地址获取该设备信息Item
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public MachineItem GetMachineItemByIP(string ip)
        {
            MachineItem tempMachineItem = this.MachineItems?.FirstOrDefault(p => p.Ip.Equals(ip));
            //if (tempMachineItem == null)
            //{
            //    LogService.Error($@"IP为[{ip}]的设备不存在！");
            //}
            return tempMachineItem;
        }

        public class historyInfo 
        {
            public DateTime createdTime;
            public string RFID;
            public string machineID;
        }

        [NonSerialized]
        public historyInfo last0MachineXRHistoryInfo;  //0#设备上一次写入的治具信息

        /// <summary>
        /// 根据治具RFID和流入设备ID查询当前在产的治具信息
        /// </summary>
        /// <param name="RFID"></param>
        /// <returns></returns>
        public FixtureItem GetFixtureItem(string RFID, string machineID , bool allowClearIfForm0machine = true)
        {
            MachineItem tempMachineItem = GetMachineItemByID(machineID);
            FixtureItem tempFixtureItem = GetCurrentFixtureItemByRFID(RFID);
            if (machineID == "0" && allowClearIfForm0machine)  //在线头设备表示治具回流或流入新治具，需要清空治具原携带信息
            {
                tempFixtureItem?.Dispose(); 
                tempFixtureItem = null;
            }
            if (tempFixtureItem == null) //找不到该RFID的治具时就赋予一个新的，或是在线头设备
            {
                tempFixtureItem = new FixtureItem();
                tempFixtureItem.RFID = RFID;
                if (machineID != "0")  //如果这个新治具不是从线头产生的查询，则赋予默认空批次
                {
                    tempFixtureItem.治具生产批次号 = "noneBatchSN";
                }
                //添加12个null物料信息
                for (int i = 0; i < 12; i++)
                {
                    tempFixtureItem.MaterialItems.Add(null);
                }
            }
            tempFixtureItem.SetMachineItem(tempMachineItem);//更新治具所在设备
            return tempFixtureItem;
        }
    }
}
