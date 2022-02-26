using M12MiniMes.BLL;
using M12MiniMes.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHC.Framework.ControlUtil;

namespace M12MiniMes.UIStart
{
    /// <summary>
    /// 设备Item
    /// </summary>
    [Serializable]
    public class MachineItem
    {
        #region Property Members 

        /// <summary>
        /// 设备ID
        /// </summary>
        //[field: NonSerialized]
        public int 设备id { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        //[field: NonSerialized]
        public string 设备名称 { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        //[field: NonSerialized]
        public string Ip { get; set; }

        /// <summary>
        /// 批次下发时计划投入数再减少多少个
        /// </summary>
        public int ReduceOffsets { get; set; } = 0;

        /// <summary>
        /// 批次下发时计划投入数再乘以损耗率（优先于ReduceOffsets）
        /// </summary>
        public float ReduceOffsetsPercent { get; set; } = 1;

        #endregion

        /// <summary>
        /// 内存储存当前在产的该设备上的所有治具信息
        /// </summary>
        public List<FixtureItem> CurrentFixtureItems { get; set; } = new List<FixtureItem>();

        /// <summary>
        /// 指定治具FixtureItem插入到指定设备MachineItem中
        /// </summary>
        /// <param name="fItem"></param>
        /// <returns></returns>
        public bool InsertFixtureItem(FixtureItem fItem)
        {
            if (fItem != null && fItem.RFID != null)
            {
                //找出相同RFID的治具
                var var = ItemManager.Instance.AllCurrentFixtureItems.Where(p => p.RFID.Equals(fItem.RFID));
                if (var.Count() > 1)
                {
                    MessageBox.Show($@"已具有相同RFID[{fItem.RFID}]的治具！");
                    return false ;
                }
            }
            if (!this.CurrentFixtureItems.Contains(fItem)) //如果不包含
            {
                this.CurrentFixtureItems.Add(fItem);
                return true;
            }
            return false;
        }

        public bool RemoveFixtureItem(FixtureItem fItem)
        {
            bool b = this.CurrentFixtureItems.Remove(fItem);
            return b;
        }
    }
}
