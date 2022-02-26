using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Data;
using System.Data.SqlClient;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// OSql命令操作函数（可用于安装程序的时候数据库脚本执行）
    /// </summary>
    public class SqlScriptHelper
    {
        #region OSql操作函数

        /// <summary>
        /// 本地执行SQL脚本
        /// </summary>
        /// <param name="path">脚本文件路径全名</param>
        public static void DoSQL(string path)
        {
            string arguments = String.Format(" -E -S (local) -i \"{0}\"", path);
            RunDos("osql.exe", arguments, false);
        }

        /// <summary>
        /// 执行SQL脚本
        /// </summary>
        /// <param name="path">脚本文件路径全名</param>
        /// <param name="userID">数据库登录ID</param>
        /// <param name="password">数据库登录密码</param>
        /// <param name="server">数据库服务器地址</param>
        public static void DoSQL(string path, string userID, string password, string server)
        {
            // -U -P -S -i关键字区分大小写
            string arguments = String.Format(" -U {0} -P {1} -S {2} -i \"{3}\"", userID, password, server, path);

            RunDos("osql.exe", arguments, false);

            //			if(File.Exists(path))
            //			{
            //				File.Delete(path);
            //			}
        }

        /// <summary>
        /// 后台执行DOS文件
        /// </summary>
        /// <param name="fileName">文件名(包含路径)</param>
        /// <param name="argument">运行参数</param>
        /// <param name="hidden">是否隐藏窗口</param>
        public static void RunDos(string fileName, string argument, bool hidden)
        {
            Process process = new Process();
            process.EnableRaisingEvents = false;
            process.StartInfo.FileName = string.Format("\"{0}\"", fileName);
            process.StartInfo.Arguments = argument;
            if (hidden)
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            else
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }
            process.Start();
            process.WaitForExit();
        }

        ///   <summary>   
        ///   运行指定DOS命令行   
        ///   </summary>   
        ///   <param name="cmd">命令</param>   
        ///   <param name="arg">命令行参数</param>   
        ///   <param name="comfirm">写入命令行的确认信息</param>   
        ///   <param name="showWindow">是否显示窗口</param> 
        ///   <returns></returns>   
        private static string ExecuteCMD(string cmd, string arg, string comfirm, bool showWindow)
        {
            Process p = new Process();
            p.StartInfo.FileName = cmd;
            p.StartInfo.Arguments = arg;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            // 是否显示窗口
            p.StartInfo.CreateNoWindow = !showWindow;
            // 窗口状态
            if (showWindow)
            {
                p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            }
            else
            {
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }

            p.Start();
            if (comfirm != null)
                p.StandardInput.WriteLine(comfirm);
            string msg = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();

            return msg;
        }

        /// <summary>
        /// 在运行脚本之前把脚本中的数据库名称替换成安装界面输入的数据库名称
        /// </summary>
        /// <param name="filePath">脚本文件名</param>
        /// <param name="oldDBName">原有的数据库名称</param>
        /// <param name="newDBName">新的数据库名称</param>
        public static void ReplaceDBName(string filePath, string oldDBName, string newDBName)
        {
            if (newDBName.CompareTo(oldDBName) != 0)
            {
                string fileText = string.Empty;
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.Default))
                {
                    fileText = streamReader.ReadToEnd();
                    fileText = fileText.Replace(oldDBName, newDBName);
                }

                using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.Default))
                {
                    streamWriter.Write(fileText);
                }
            }
        }

        /// <summary>
        /// 为测试使用的函数
        /// </summary>
        /// <param name="fileText">输出的内容</param>
        private static void WriteLog(string fileText)
        {
            string filePath = "C:\\Log.txt";
            using (StreamWriter streamWriter = new StreamWriter(filePath, true, Encoding.Default))
            {
                streamWriter.Write(fileText);
            }
        }

        /// <summary>
        /// 加入安装文件的路径，方便Web端访问
        /// </summary>
        public static void UpdatePathEnvironment(string physicalRoot)
        {
            //得到原来Path的变量值
            string registerKey = "SYSTEM\\ControlSet001\\Control\\Session Manager\\Environment";
            string key = "Path";
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(registerKey);
            string result = regKey.GetValue(key).ToString();

            //添加新的值
            if (result.IndexOf(physicalRoot) < 0)
            {
                result += string.Format(";{0}", physicalRoot);
            }

            regKey = Registry.LocalMachine.OpenSubKey(registerKey, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.SetValue);
            regKey.SetValue(key, result);
        }

        #endregion

        #region 附加、分离、备份、恢复数据库操作

        /// <summary>
        /// 附加SqlServer数据库
        /// </summary>
        public static bool AttachDB(string connectionString, string dataBaseName, string dataBase_MDF, string dataBase_LDF)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "sp_attach_db";
                comm.Parameters.Add(new SqlParameter("dbname", SqlDbType.NVarChar));
                comm.Parameters["dbname"].Value = dataBaseName;
                comm.Parameters.Add(new SqlParameter("filename1", SqlDbType.NVarChar));
                comm.Parameters["filename1"].Value = dataBase_MDF;
                comm.Parameters.Add(new SqlParameter("filename2", SqlDbType.NVarChar));
                comm.Parameters["filename2"].Value = dataBase_LDF;
                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
            }

            return true;
        }

        /// <summary>
        /// 分离SqlServer数据库
        /// </summary>
        public static bool DetachDB(string connectionString, string dataBaseName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "sp_detach_db";
                comm.Parameters.Add(new SqlParameter("dbname", SqlDbType.NVarChar));
                comm.Parameters["dbname"].Value = dataBaseName;
                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
            }
            return true;
        }

        /// <summary>
        /// 还原数据库
        /// </summary>
        public static bool RestoreDataBase(string connectionString, string dataBaseName, string DataBaseOfBackupPath, string DataBaseOfBackupName)
        {
            string filePath = Path.Combine(DataBaseOfBackupPath, DataBaseOfBackupName);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "use master;restore database @DataBaseName From disk = @BackupFile with replace;";
                comm.Parameters.Add(new SqlParameter("DataBaseName", SqlDbType.NVarChar));
                comm.Parameters["DataBaseName"].Value = dataBaseName;
                comm.Parameters.Add(new SqlParameter("BackupFile", SqlDbType.NVarChar));
                comm.Parameters["BackupFile"].Value = filePath;
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }
            return true;
        }

        /// <summary>
        /// 备份SqlServer数据库
        /// </summary>
        public static bool BackupDataBase(string connectionString, string dataBaseName, string dataBaseOfBackupPath, string dataBaseOfBackupName)
        {
            string filePath = Path.Combine(dataBaseOfBackupPath, dataBaseOfBackupName);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "use master;backup database @dbname to disk = @backupname;";
                comm.Parameters.Add(new SqlParameter("dbname", SqlDbType.NVarChar));
                comm.Parameters["dbname"].Value = dataBaseName;
                comm.Parameters.Add(new SqlParameter("backupname", SqlDbType.NVarChar));
                comm.Parameters["backupname"].Value = filePath;
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }
            return true;
        }

        /// <summary>
        /// 获取数据库所有用户表
        /// </summary>
        /// <param name="connectionString">数据库名连接字符串</param>
        /// <returns></returns>
        public static IEnumerable<string> GetAllUserDatabases(string connectionString)
        {
            var databases = new List<String>();
            DataTable databasesTable;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                databasesTable = connection.GetSchema("Databases");
                connection.Close();
            }

            List<string> _systemDatabaseNames = new List<string>{ "master", "tempdb", "model", "msdb" };
            foreach (DataRow row in databasesTable.Rows)
            {
                string databaseName = row["database_name"].ToString();

                if (_systemDatabaseNames.Contains(databaseName))
                    continue;

                databases.Add(databaseName);
            }

            return databases;
        }

        #endregion

    }
}
