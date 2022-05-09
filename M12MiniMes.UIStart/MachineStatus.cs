using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M12MiniMes.UIStart
{
    [Serializable]
    public class MachineStatus
    {
        public Dictionary<int, string> DicMachineStatus = new Dictionary<int, string>();

        public Dictionary<int, string> DicMachineAlarmInformation = new Dictionary<int, string>();     

        [NonSerialized]
        public static MachineStatus machineStatus = new MachineStatus();//创建全局静态变量
    }


}
