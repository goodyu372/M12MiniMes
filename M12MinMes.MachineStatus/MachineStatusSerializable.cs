using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace M12MinMes.MachineStatus
{
   public static class MachineStatusSerializable
    {
        const string serializeFileName = @"D:\Fi.Data\MachineStatus\MachineStatus.bin";//const表示常量

        public static bool SaveMachineStatus()
        {
            try
            {
                string strDirectory = Path.GetDirectoryName(serializeFileName);
                if (!Directory.Exists(strDirectory))
                {
                    Directory.CreateDirectory(strDirectory);
                }

                using(FileStream fsWrite=new FileStream(serializeFileName,FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter(); //创建一个二进制序列化器                    
                    bf.Serialize(fsWrite, MachineStatus.machineStatus);//执行序列化 
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool loadMachineStatus()
        {
            try
            {
                if (!File.Exists(serializeFileName))
                {
                    return false;
                }
                using (FileStream fsRead = new FileStream(serializeFileName, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    MachineStatus.machineStatus = bf.Deserialize(fsRead) as MachineStatus;
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
