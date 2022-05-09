using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fi.Core;


namespace M12MiniMes.UIStart
{
    public partial class MachineStatusSetting : Form
    {
        public MachineStatusSetting()
        {
            InitializeComponent();
        }

        private void MachineStatusForm_Load(object sender, EventArgs e)
        {

        }
        private void CreatMachineStatus_button_Click(object sender, EventArgs e)//添加设备状态种类
        {
            AddMachineStatus();
        }


        private void AddMachineStatus()//添加设备状态种类
        {
            //string MachineStatusID = MachineStatusID_textBox.Text.Trim();
            DialogResult resault = MessageBox.Show("确定要创建此设备ID吗？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (resault == DialogResult.OK)
            {
                bool bIsInt1 = int.TryParse(MachineStatusID_textBox.Text.Trim().ToString(), out int MachineStatusID);
                if (!bIsInt1)
                {
                    MessageBox.Show("设备状态ID需填整数！");
                    return;
                }
                string MachineStatusName = MachineStatusName_textBox.Text.Trim();
                if (string.IsNullOrEmpty(MachineStatusName))
                {
                    MessageBox.Show("设备状态名称不能为空！");
                    return;
                }
                if (MachineStatus.machineStatus.DicMachineStatus.Keys.Contains(MachineStatusID))
                {
                    MessageBox.Show("已经存在此设备状态ID，请重写！");
                    return;
                }
                MachineStatus.machineStatus.DicMachineStatus.Add(MachineStatusID, MachineStatusName);
            }
        }

        private void UpdateMachineStatusbutton_Click(object sender, EventArgs e)//修改设备状态种类
        {
            DialogResult resault = MessageBox.Show("确定要修改此设备状态ID吗？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (resault == DialogResult.OK)
            {
                bool bIsInt1 = int.TryParse(MachineStatusID_textBox.Text.Trim().ToString(), out int MachineStatusID);
                if (!bIsInt1)
                {
                    MessageBox.Show("设备状态ID需填整数！");
                    return;
                }
                string MachineStatusName = MachineStatusName_textBox.Text.Trim();
                if (string.IsNullOrEmpty(MachineStatusName))
                {
                    MessageBox.Show("设备状态名称不能为空！");
                    return;
                }
                if (MachineStatus.machineStatus.DicMachineStatus.Keys.Contains(MachineStatusID))
                {

                    //return;
                    MachineStatus.machineStatus.DicMachineStatus.Remove(MachineStatusID);
                    MachineStatus.machineStatus.DicMachineStatus.Add(MachineStatusID, MachineStatusName);
                    MessageBox.Show("修改成功！");
                }
                else
                {
                    MessageBox.Show("不存在此设备状态ID，请重新选择或移除对应的设备状态ID后再点击“创建”！");
                }
            }
            
        }

        private void refresh_button_Click(object sender, EventArgs e)//更新设备状态种类到表格中
        {

            //int index = this.MachineStatus_dataGridView.Rows.Add();
            this.MachineStatus_dataGridView.DataSource = MachineStatus.machineStatus.DicMachineStatus.ToArray();
            //this.MachineStatus_dataGridView.Rows[index].Cells[0].Value= MachineStatus.machineStatus.DicMachineStatus.

        }

        private void MachineStatus_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

      

        private void SaveMachineStatus_button_Click(object sender, EventArgs e)//序列化
        {
            MachineStatusSerializable.SaveMachineStatus();
        }

        private void MachineStatus_dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int selectedRow = this.MachineStatus_dataGridView.SelectedRows.Count;
                if (selectedRow == 0)
                {
                    MessageBox.Show("未选中任何行！");
                    return;
                }
                if (selectedRow > 1)
                {
                    MessageBox.Show("只能选中单行！");
                    return;
                }
                int index = this.MachineStatus_dataGridView.SelectedRows[0].Index;

                MachineStatusID_textBox.Text = this.MachineStatus_dataGridView.Rows[index].Cells[0].Value.ToString();
                MachineStatusName_textBox.Text = this.MachineStatus_dataGridView.Rows[index].Cells[1].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeleteMachineStatusbutton_Click(object sender, EventArgs e)
        {
            DialogResult resault = MessageBox.Show("确定要删除此设备状态ID吗？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (resault == DialogResult.OK)
            {
                bool bIsInt1 = int.TryParse(MachineStatusID_textBox.Text.Trim().ToString(), out int MachineStatusID);

                if (MachineStatus.machineStatus.DicMachineStatus.Keys.Contains(MachineStatusID))
                {
                    MachineStatus.machineStatus.DicMachineStatus.Remove(MachineStatusID);

                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("不存在此设备状态ID，删除失败！");
                }
            }
        }

        private void CreatAlarm_button_Click(object sender, EventArgs e)
        {
            DialogResult resault = MessageBox.Show("确定要创建报警内容吗？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (resault == DialogResult.OK)
            {
                AddAlarmID();
            }
        }

        private void AddAlarmID()//添加报警内容
        {
            bool bIsInt1 = int.TryParse(AlarmID_textBox.Text.Trim().ToString(), out int AlarmID);
            if (!bIsInt1)
            {
                MessageBox.Show("报警代码，需填整数！");
                return;
            }
            string AlarmName = AlarmName_textBox.Text.Trim();
            if (string.IsNullOrEmpty(AlarmName))
            {
                MessageBox.Show("报警内容，不能为空！");
                return;
            }
            if (MachineStatus.machineStatus.DicMachineAlarmInformation.Keys.Contains(AlarmID))
            {
                MessageBox.Show("已经存在此报警代码，请重写！");
                return;
            }
            MachineStatus.machineStatus.DicMachineAlarmInformation.Add(AlarmID, AlarmName);
        }

        private void RefreshAlarm_button_Click(object sender, EventArgs e)
        {
            this.Alarm_dataGridView.DataSource = MachineStatus.machineStatus.DicMachineAlarmInformation.ToArray();
        }

        private void DeleteAlarm_button_Click(object sender, EventArgs e)
        {
            DialogResult resault = MessageBox.Show("确定要删除此报警内容吗？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (resault == DialogResult.OK)
            {
                bool bIsInt1 = int.TryParse(AlarmID_textBox.Text.Trim().ToString(), out int AlarmID);
                if (MachineStatus.machineStatus.DicMachineAlarmInformation.Keys.Contains(AlarmID))
                {
                    MachineStatus.machineStatus.DicMachineAlarmInformation.Remove(AlarmID);

                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("不存在此报警代码，删除失败！");
                }
            }
        }

        private void Alarm_dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int selectedRow = this.Alarm_dataGridView.SelectedRows.Count;
                if (selectedRow == 0)
                {
                    MessageBox.Show("未选中任何行！");
                    return;
                }
                if (selectedRow > 1)
                {
                    MessageBox.Show("只能选中单行！");
                    return;
                }
                int index = this.Alarm_dataGridView.SelectedRows[0].Index;

                AlarmID_textBox.Text = this.Alarm_dataGridView.Rows[index].Cells[0].Value.ToString();
                AlarmName_textBox.Text = this.Alarm_dataGridView.Rows[index].Cells[1].Value.ToString();

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateAlarm_button_Click(object sender, EventArgs e)
        {
            DialogResult resault = MessageBox.Show("确定要修改此报警代码吗？", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (resault == DialogResult.OK)
            {
                bool bIsInt1 = int.TryParse(AlarmID_textBox.Text.Trim().ToString(), out int AlarmID);
                if (!bIsInt1)
                {
                    MessageBox.Show("报警代码，需填整数！");
                    return;
                }
                string AlarmName = AlarmName_textBox.Text.Trim();
                if (string.IsNullOrEmpty(AlarmName))
                {
                    MessageBox.Show("报警内容，不能为空！");
                    return;
                }
                if (MachineStatus.machineStatus.DicMachineStatus.Keys.Contains(AlarmID))
                {

                    //return;
                    MachineStatus.machineStatus.DicMachineStatus.Remove(AlarmID);
                    MachineStatus.machineStatus.DicMachineStatus.Add(AlarmID, AlarmName);
                    MessageBox.Show("修改成功！");
                }
                else
                {
                    MessageBox.Show("不存在此报警代码，请重新选择或移除对应的报警代码后再点击“创建”！");
                }
            }
        }

        private void SaveAlarm_button1_Click(object sender, EventArgs e)
        {
            MachineStatusSerializable.SaveMachineStatus();
        }
    }

   

}
