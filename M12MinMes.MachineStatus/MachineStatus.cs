using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M12MinMes.MachineStatus
{
    [Serializable]
    public class MachineStatus
    {
        public Dictionary<int, string> DicMachineStatus = new Dictionary<int, string>();

        public Dictionary<int, string> DicMachineAlarmInformation = new Dictionary<int, string>();

        // List<StatusTimeSpan> ListStatus = new List<StatusTimeSpan>();

        //Dictionary<int, StatusTimeSpan>说明int的类型，1;运行，2等待，3暂停，4手动，5报警，6点检，7维修
        //public Dictionary<int, Dictionary<int, StatusTimeSpan>> Dic = new Dictionary<int, Dictionary<int, StatusTimeSpan>>();//计算时长
       
        //  public Dictionary<int, List<StatusTimeSpan>> DicTimeSpan = new Dictionary<int, List<StatusTimeSpan>>();

        [NonSerialized]
        public static MachineStatus machineStatus = new MachineStatus();//创建全局静态变量
    }

    //public class Status
    //{
    //    public string run;
    //    public string wait;
    //    public string pause;
    //    public string manual;
    //    public string alarm;
    //    public string spotcheck;
    //    public string maintain;

    //}

    //public class StatusTimeSpan//设备状态时长
    //{
    //    public DateTime T1;
    //    public DateTime T2;
    //    public TimeSpan T3;
    //    public bool FirstAppear;
    //    public int BeferMachineStatusID;

    //}

 


}
