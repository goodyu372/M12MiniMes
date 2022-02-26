using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M12MiniMes.UIStart
{
    /// <summary>
    /// 治具Item
    /// </summary>
    [Serializable]
    public class FixtureItem : IDisposable
    {
        /// <summary>
        /// 治具上RFID扫描出
        /// </summary>
        public string RFID { get; set; }

        /// <summary>
        /// 治具有12个孔位，记录12个物料信息
        /// </summary>
        public List<MaterialItem> MaterialItems { get; set; }

        /// <summary>
        /// 当前治具所在的设备信息，可以为null
        /// </summary>
        public MachineItem MachineItem { get; private set; }

        /// <summary>
        /// 治具GUID
        /// </summary>
        public Guid FixtureGuid { get; private set; }

        public string 治具生产批次号 { get; set; }

        /// <summary>
        /// 根据物料获取孔号iHoleIndex（0至11），若该治具不携带该物料则返回-1.
        /// </summary>
        public int this[MaterialItem materialItem]
        {
            get
            {
                return MaterialItems?.IndexOf(materialItem) ?? -1;
            }
        }

        /// <summary>
        /// 通过孔号HoleIndex（0至11）获取物料信息，若该治具不携带该物料则返回null
        /// </summary>
        /// <param name="strHoleIndex"></param>
        /// <returns></returns>
        public MaterialItem this[string strHoleIndex]
        {
            get
            {
                int index = int.Parse(strHoleIndex);
                return MaterialItems?[index];
            }
        }

        public FixtureItem()
        {
            this.FixtureGuid = Guid.NewGuid();
            this.MaterialItems = new List<MaterialItem>();
            //从生产批次表中拿
            var var = ItemManager.Instance.GetFirst在产批次();
            this.治具生产批次号 = var?.生产批次号 ?? "noneBatchSN";
        }

        /// <summary>
        /// 设置当前治具所在设备  第一步清除旧设备，第二步设置新设备
        /// </summary>
        /// <param name="fixture"></param>
        public void SetMachineItem(MachineItem machine)
        {
            //先删除旧信息
            this.MachineItem?.RemoveFixtureItem(this);
            this.MachineItem = machine;
            this.MachineItem?.InsertFixtureItem(this);
        }

        public bool InsertMaterialItem(int index, MaterialItem mItem)
        {
            try
            {
                if (index >= 12)
                {
                    throw new Exception("孔号索引不能超过12（正常范围0-11）！");
                }
                if (mItem == null)
                {
                    return false;
                }
                //判断插入索引位置是否为空
                MaterialItem mpItem = this.MaterialItems.ElementAtOrDefault(index);
                if (mpItem != null)
                {
                    throw new Exception("插入索引位置已存在一个物料！请先清除该索引位置的物料后再行插入新物料！");
                }
                if (!this.MaterialItems.Contains(mItem))
                {
                    this.MaterialItems.RemoveAt(index);
                    this.MaterialItems.Insert(index, mItem);
                    mItem.SetFixtureItem(this);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveMaterialItem(MaterialItem mItem)
        {
            if (mItem == null)
            {
                return false;
            }
            if (this.MaterialItems.Contains(mItem))
            {
                int index = this.MaterialItems.IndexOf(mItem);
                mItem.SetFixtureItem(null);
                this.MaterialItems.RemoveAt(index); //删除旧值
                this.MaterialItems.Insert(index, null); //插入新值，保持List为12个
                return true;
            }
            return false;
        }

        public bool RemoveMaterialItemByIndex(int index)
        {
            try
            {
                MaterialItem mItem = this.MaterialItems[index];
                if (mItem != null)
                {
                    mItem.SetFixtureItem(null);
                    this.MaterialItems.RemoveAt(index); //删除旧值
                    this.MaterialItems.Insert(index, null); //插入新值，保持List为12个
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.SetMachineItem(null);
            this.MaterialItems.Clear();
            this.MaterialItems = null;
            this.RFID = null;
            this.FixtureGuid = Guid.Empty;
            this.治具生产批次号 = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect(0);
        }
        #endregion
    }
}
