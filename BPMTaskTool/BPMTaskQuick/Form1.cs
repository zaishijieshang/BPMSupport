using BPMTaskDispatch.DBManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dapper;
using BPMTaskQuick.Entity;
using BPMTaskQuick.Code;

namespace BPMTaskQuick
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitData();
            InitSecurityData();
        }

        private void InitData()
        {
            comboState.Items.Add("Aborted");
            comboState.Items.Add("Approved");
            comboState.Items.Add("Deleted");
            comboState.Items.Add("Rejected");
            comboState.Items.Add("Running");
        }

        #region 流程信息页
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string tstr = txtTask.Text.Trim();
            if (!string.IsNullOrEmpty(tstr))
            {
                int taskID = 0;
                int.TryParse(tstr, out taskID);

                ETask eTask = null;
                if (taskID > 0)
                {
                    eTask = GetTaskByID(taskID);
                }
                else
                {
                    eTask = GetTaskBySn(tstr);
                }


                if (eTask != null)
                {
                    txtTaskID.Text = eTask.TaskID.ToString();
                    txtQuery.Text = eTask.Queryfield;
                    txtDesc.Text = eTask.Description;
                    txtOwnerPositionID.Text = eTask.OwnerPositionID.ToString();
                    SetState(eTask.State.Trim());
                    //comboState.SelectedText = eTask.State.Trim();
                    for (int i = 0; i < comboState.Items.Count; i++)
                    {
                        if (comboState.Items[i].ToString() == eTask.State.Trim())
                        {
                            comboState.SelectedIndex = i;
                            break;
                        }
                    }

                }

            }

        }

        private ETask GetTaskBySn(string Sn)
        {
            ETask task = null;
            using (var db = DB.BPMConnection)
            {
                string sql = "select top 1 TaskID from BPMInstTasks where SerialNum=@SerialNum";
                int taskId = db.ExecuteScalar<int>(sql, new { SerialNum = Sn });

                if (taskId > 0)
                {
                    task = GetTaskByID(taskId);
                }
            }
            return task;
        }

        private ETask GetTaskByID(int TaskID)
        {
            ETask task = null;
            using (var db = DB.BPMConnection)
            {
                string sql = "select TaskID,Description,State,Queryfield,OwnerPositionID from BPMInstTasks where TaskID=@TaskID";
                task = db.Query<ETask>(sql, new { TaskID = TaskID }).FirstOrDefault();
            }
            return task;
        }

        private bool SetTaskByTaskID(ETask eTask)
        {
            using (var db = DB.BPMConnection)
            {
                string sql = "update BPMInstTasks set Description=@Description,Queryfield=@Queryfield,State=@State,OwnerPositionID=@OwnerPositionID where TaskID=@TaskID";
                return db.Execute(sql, eTask) > 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ETask task = new ETask();
            task.TaskID = Convert.ToInt32(txtTaskID.Text.Trim());
            task.Description = txtDesc.Text.Trim();
            task.State = comboState.SelectedItem.ToString();
            task.Queryfield = txtQuery.Text.Trim();
            task.OwnerPositionID = Convert.ToInt32(txtOwnerPositionID.Text.Trim());
            bool flag = SetTaskByTaskID(task);

            MessageBox.Show(flag ? "修改【成功】" : "修改【失败】", "提示");
        }

        //Aborted 撤销
        //Approved 批准
        //Deleted 删除
        //Reject 拒绝
        //Rejected 拒绝
        //Running 运行
        private void SetState(string state)
        {
            if (state == "Aborted")
            {
                labStatus.Text = "撤销";
                labStatus.ForeColor = Color.BurlyWood;
            }
            else if (state == "Approved")
            {
                labStatus.Text = "批准";
                labStatus.ForeColor = Color.Blue;
            }
            else if (state == "Deleted")
            {
                labStatus.Text = "删除";
                labStatus.ForeColor = Color.Red;
            }
            else if (state == "Reject")
            {
                labStatus.Text = "拒绝Reject";
                labStatus.ForeColor = Color.Red;
            }
            else if (state == "Rejected")
            {
                labStatus.Text = "拒绝";
                labStatus.ForeColor = Color.Red;
            }
            else if (state == "Running")
            {
                labStatus.Text = "运行";
                labStatus.ForeColor = Color.Green;
            }
        }

        private void comboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetState(comboState.SelectedItem.ToString());
        }

        #endregion

        #region 权限
        private void InitSecurityData()
        {
            GroupBind();
            TasksBind();

            DBDispatch.ShowDataToUI = (msg) =>
            {
                SetExeRecord(msg);
            };

            DBDispatch.RefreshUI = (SID) =>
            {
                RefreshTaskByGroup(SID);
            };


            
        }

        private void TasksBind(string ProcessName = null)
        {
            var listTaskCount = DBDispatch.ListTasksCount(ProcessName);
            listViewTasks.Items.Clear();
            foreach (ETaskCount eTaskCount in listTaskCount)
            {
                ListViewItem item = new ListViewItem(new string[] {
                     eTaskCount.ProcessName,
                     eTaskCount.Count.ToString()
                });

                item.Tag = eTaskCount;

                listViewTasks.Items.Add(item);
            }
        }

        private void GroupBind(string GroupName = null)
        {
            var listGroup = DBDispatch.ListGroup(GroupName);
            listViewGroup.Items.Clear();
            foreach (EGroup eGroup in listGroup)
            {
                ListViewItem item = new ListViewItem(new string[] {
                     eGroup.GroupName
                });

                item.Tag = eGroup;

                listViewGroup.Items.Add(item);
            }
        }





        private void btnClearSecurity_Click(object sender, EventArgs e)
        {
            string SName = txtTargetSecurityGroups.Text.Trim();
            string SID = txtTargetSecurityGroupSID.Text.Trim();
            string TaskName = txtTargetTask.Text.Trim();

            if (!string.IsNullOrEmpty(SName) && !string.IsNullOrEmpty(SID) && !string.IsNullOrEmpty(TaskName))
            {
                SetExeRecord(string.Format("清除【查看】权限...【{0}->{1}】 开始执行..", TaskName, SName, SID));//({2})
                DBDispatch.ClearSecurity(TaskName, SName, SID,false);
            }
        }

        private void btnClearSecurityManager_Click(object sender, EventArgs e)
        {
            string SName = txtTargetSecurityGroups.Text.Trim();
            string SID = txtTargetSecurityGroupSID.Text.Trim();
            string TaskName = txtTargetTask.Text.Trim();

            if (!string.IsNullOrEmpty(SName) && !string.IsNullOrEmpty(SID) && !string.IsNullOrEmpty(TaskName))
            {
                SetExeRecord(string.Format("清除【管理】权限...【{0}->{1}】 开始执行..", TaskName, SName, SID));//({2})
                DBDispatch.ClearSecurity(TaskName, SName, SID, true);
            }
        }

        private void btnRepairSecurity_Click(object sender, EventArgs e)
        {
            string SName = txtTargetSecurityGroups.Text.Trim();
            string SID = txtTargetSecurityGroupSID.Text.Trim();
            string TaskName = txtTargetTask.Text.Trim();

            if (!string.IsNullOrEmpty(SName) && !string.IsNullOrEmpty(SID) && !string.IsNullOrEmpty(TaskName))
            {
                DBDispatch.RepairSecurity(TaskName, SName, SID, false);
            }
        }

        private void btnRepairSecurityManager_Click(object sender, EventArgs e)
        {
            string SName = txtTargetSecurityGroups.Text.Trim();
            string SID = txtTargetSecurityGroupSID.Text.Trim();
            string TaskName = txtTargetTask.Text.Trim();

            if (!string.IsNullOrEmpty(SName) && !string.IsNullOrEmpty(SID) && !string.IsNullOrEmpty(TaskName))
            {
                DBDispatch.RepairSecurity(TaskName, SName, SID, true);
            }
        }

        private void btnInsertSecurity_Click(object sender, EventArgs e)
        {
            string SName = txtTargetSecurityGroups.Text.Trim();
            string SID = txtTargetSecurityGroupSID.Text.Trim();
            string TaskName = txtTargetTask.Text.Trim();

            if (!string.IsNullOrEmpty(SName) && !string.IsNullOrEmpty(SID) && !string.IsNullOrEmpty(TaskName))
            {
                SetExeRecord(string.Format("刷入【查看】权限...【{0}->{1}】 开始执行..", TaskName, SName, SID));//({2})
                DBDispatch.InsertSecurity(TaskName, SName, SID, false);
            }
        }

        private void btnInsertSecurityManager_Click(object sender, EventArgs e)
        {
            string SName = txtTargetSecurityGroups.Text.Trim();
            string SID = txtTargetSecurityGroupSID.Text.Trim();
            string TaskName = txtTargetTask.Text.Trim();

            if (!string.IsNullOrEmpty(SName) && !string.IsNullOrEmpty(SID) && !string.IsNullOrEmpty(TaskName))
            {
                SetExeRecord(string.Format("刷入【管理】权限...【{0}->{1}】 开始执行..", TaskName, SName, SID));//({2})
                DBDispatch.InsertSecurity(TaskName, SName, SID, true);
            }
        }


        private void listViewGroup_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem _Item = this.listViewGroup.GetItemAt(e.X, e.Y);
            if (_Item != null)
            {
                EGroup eGroup = _Item.Tag as EGroup;

                if (eGroup != null)
                {
                    txtTargetSecurityGroups.Text = eGroup.GroupName;
                    txtTargetSecurityGroupSID.Text = eGroup.SID;

                    txtSearchSecurityGroups.Text = eGroup.GroupName;

                    RefreshTaskByGroup(eGroup.SID);
                }
            }

        }

        private void RefreshTaskByGroup(string SID)
        {
            listViewTaskByGroup.Items.Clear();
            var listTaskCount = DBDispatch.ListGroupTasks(SID);
            foreach (ETaskCount eTaskCount in listTaskCount)
            {
                ListViewItem item = new ListViewItem(new string[] {
                             eTaskCount.ProcessName,
                             eTaskCount.AllowAdmin==1?"管理":"查看",
                             eTaskCount.Count.ToString()
                        });

                item.Tag = eTaskCount;
                listViewTaskByGroup.Items.Add(item);
            }
        }

        private void listViewTaskByGroup_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem _Item = this.listViewTaskByGroup.GetItemAt(e.X, e.Y);
            if (_Item != null)
            {
                ETaskCount eTaskCount = _Item.Tag as ETaskCount;
                if (eTaskCount != null)
                {
                    txtTargetTask.Text = eTaskCount.ProcessName;
                    txtSearchProcessName.Text = eTaskCount.ProcessName;
                    //TasksBind(eTaskCount.ProcessName);
                }
            }
        }

        private void listViewTasks_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem _Item = this.listViewTasks.GetItemAt(e.X, e.Y);
            if (_Item != null)
            {
                ETaskCount eTaskCount = _Item.Tag as ETaskCount;
                if (eTaskCount != null)
                {
                    txtTargetTask.Text = eTaskCount.ProcessName;
                }
            }
        }

        private void btnSearchSecurity_Click(object sender, EventArgs e)
        {
            string search = txtSearchSecurityGroups.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                GroupBind(search);
                SetExeRecord(string.Format("搜索安全组...[{0}]", search));
            }
            else
            {
                GroupBind();
                SetExeRecord(string.Format("搜索安全组...[全部]"));
            }
        }

        private void btnSearchTask_Click(object sender, EventArgs e)
        {
            string search = txtSearchProcessName.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                TasksBind(search);
                SetExeRecord(string.Format("搜索流程名称...[{0}]", search));
            }
            else
            {
                TasksBind();
                SetExeRecord(string.Format("搜索流程名称...[全部]"));
            }
        }

        private void SetExeRecord(string msg)
        {
            Invoke(new Action(() =>
            {
                txtExeRecord.AppendText(string.Format("{0} {1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg, Environment.NewLine));
            }));
        }



        #endregion

  
    }
}
