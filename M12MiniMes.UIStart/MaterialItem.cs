using M12MiniMes.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M12MiniMes.UIStart
{
    /// <summary>
    /// 物料Item
    /// </summary>
    [Serializable]
    public class MaterialItem : IDisposable
    {
        public List<生产数据表Info> 生产数据List { get; set; }

        /// <summary>
        /// 记忆当前物料所在的治具信息，可以为null表示不处于任何治具之中
        /// </summary>
        public FixtureItem Fixture { get; private set; }

        /// <summary>
        /// 物料GUID
        /// </summary>
        public Guid MaterialGuid { get; private set; }

        public string 物料生产批次号 { get; set; }

        public MaterialItem()
        {
            this.MaterialGuid = Guid.NewGuid();
        }

        public MaterialItem(FixtureItem fixture)
        {
            this.Fixture = fixture;
            this.MaterialGuid = Guid.NewGuid();
            this.物料生产批次号 = fixture?.治具生产批次号;
            this.生产数据List = new List<生产数据表Info>();
        }

        /// <summary>
        /// 设置当前物料所在治具
        /// </summary>
        /// <param name="fixture"></param>
        public void SetFixtureItem(FixtureItem fixture)
        {
            this.Fixture = fixture;
        }

        /// <summary>
        /// 获取所在治具上所处孔位索引（0-11），若不在任何治具上则返回-1
        /// </summary>
        /// <returns></returns>
        public int GetHoleIndexInFixture()
        {
            return this.Fixture?[this] ?? -1;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.Fixture = null;
            this.MaterialGuid = Guid.Empty;
            this.物料生产批次号 = null;
            this.生产数据List.Clear();
            this.生产数据List = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect(0);
        }
        #endregion
    }
}
