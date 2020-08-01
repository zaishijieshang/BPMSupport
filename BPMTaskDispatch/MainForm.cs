using BPMTaskDispatch.Extend;
using BPMTaskDispatch.Win.Domain;
using BPMTaskDispatch.Win.Domain.Entity;
using BPMTaskDispatch.Win.Domain.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BPMTaskDispatch.Extend.Entity;
namespace BPMTaskDispatch.Win
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ImageList ImgList = new ImageList();
            ImgList.ImageSize = new Size(1, 20);
            listTasks.SmallImageList = ImgList;

            TaskDispatch.UITasks += (list) =>
            {
                foreach (ETask eTask in list)
                {
                    ListViewItem item = new ListViewItem(new string[] {
                    eTask.Index.ToString(),
                    eTask.TaskName,
                    eTask.TaskDesc,
                    TaskDispatch.Instance.TaskStatus((TaskState)eTask.Status)
                });

                    item.Tag = eTask;

                    listTasks.Items.Add(item);
                }
            };

            UIData.ExeResult += (exeResult) =>
            {

                if (listExeView.Items.Count > 200)
                {
                    listExeView.Items.Clear();
                }

                ListViewItem item = new ListViewItem(new string[] {
                    exeResult.ExeTime.ToTime(),
                    exeResult.TaskName,
                    exeResult.ExeResult
                });

                listExeView.Items.Insert(0, item);
            };

            UIData.ExceptionResult += (exceptionResult) =>
            {

                if (listExpView.Items.Count > 200)
                {
                    listExpView.Items.Clear();
                }

                ListViewItem item = new ListViewItem(new string[] {
                    exceptionResult.ExceptionTime.ToTime(),
                    exceptionResult.ExceptionTaskName,
                    exceptionResult.ExceptionResult
                });
                listExpView.Items.Insert(0, item);

            };

            UIData.ExeTotal += () =>
            {
                int iexe = 0;
                int.TryParse(tsExeCount.Text.Trim(), out iexe);
                tsExeCount.Text = (++iexe).ToString();
            };

            UIData.ExpTotal += () =>
            {
                int iexe = 0;
                int.TryParse(tsExpCount.Text.Trim(), out iexe);
                tsExpCount.Text = (++iexe).ToString();
            };

            TaskDispatch.Instance.LoadJob();
            TaskDispatch.Instance.Scheduler.Start();
        }

        private void listTasks_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem _Item = this.listTasks.GetItemAt(e.X, e.Y);
            if (_Item == null)
            {
                SetBtnEnable();
            }
            else
            {
                ETask eTask = _Item.Tag as ETask;
                SetBtnEnable(eTask);
            }

        }
        private void listTasks_MouseClick(object sender, MouseEventArgs e)
        {
            ListView _ListView = (ListView)sender;
            ListViewItem _Item = _ListView.GetItemAt(e.X, e.Y);

            if (_Item != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.listviewContextMenu.Show(_ListView, e.Location);
                }
                else
                {
                    ETask eTask = _Item.Tag as ETask;
                    SetBtnEnable(eTask);
                }
            }
            else
            {
                SetBtnEnable();
            }
        }

        private void SetBtnEnable(ETask eTask = null)
        {
            if (eTask == null)
            {
                tsStart.Enabled = false;
                tsStop.Enabled = false;
                mitemStart.Enabled = false;
                mitemStop.Enabled = false;
            }
            else
            {
                if (eTask.Status == (int)TaskState.Running)
                {
                    tsStart.Enabled = false;
                    tsStop.Enabled = true;
                    mitemStart.Enabled = false;
                    mitemStop.Enabled = true;
                }
                else
                {
                    tsStart.Enabled = true;
                    tsStop.Enabled = false;
                    mitemStart.Enabled = true;
                    mitemStop.Enabled = false;
                }
            }
        }

        private void mitemStart_Click(object sender, EventArgs e)
        {
            Start();
        }
        private void mitemStop_Click(object sender, EventArgs e)
        {
            Stop();
        }
        private void tsStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void tsStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private ETask GetSelectedTask()
        {
            if (this.listTasks.SelectedIndices.Count <= 0)//无选中信息  
            {
                return null;
            }
            else
            {
                ListView.SelectedIndexCollection c = listTasks.SelectedIndices;
                //string txt = listTasks.Items[c[0]].SubItems[2].Text.ToString();
                return (ETask)listTasks.Items[c[0]].Tag;
            }
        }

        private void SetSelectedTask(ETask eTask)
        {
            if (this.listTasks.SelectedIndices.Count > 0)//无选中信息  
            {
                ListView.SelectedIndexCollection c = listTasks.SelectedIndices;
                listTasks.Items[c[0]].Tag = eTask;
                if (eTask.Status == (int)TaskState.Running)
                {
                    listTasks.Items[c[0]].SubItems[3].Text = "运行中..";
                }
                else
                {
                    listTasks.Items[c[0]].SubItems[3].Text = "停止";
                }
            }
        }
        private void mitemOnlyExeOne_Click(object sender, EventArgs e)
        {
            ETask eTask = GetSelectedTask();
            if (eTask != null)
            {
                try
                {
                    TaskDispatch.Instance.ExeOne(eTask);
                    UIDataHelper.ExeResult(eTask.TaskName, "【任务执行】执行一次任务....");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(string.Format("【发生异常】Message：{0}，StackTrace：{1}", ex.Message, ex.StackTrace));
                    UIDataHelper.ExceptionResult(eTask.TaskName, ex);
                }
            }
        }
        private void Start()
        {
            ETask eTask = GetSelectedTask();
            if (eTask != null)
            {
                try
                {
                    if (eTask.Status == (int)TaskState.Stop)
                    {
                        TaskDispatch.Instance.Start(eTask);
                        eTask.Status = (int)TaskState.Running;
                        SetSelectedTask(eTask);
                        SetBtnEnable(eTask);

                        RefreshRunTotal();

                        UIDataHelper.ExeResult(eTask.TaskName, "【任务启动】开始运行....");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(string.Format("【发生异常】Message：{0}，StackTrace：{1}", ex.Message, ex.StackTrace));
                    UIDataHelper.ExceptionResult(eTask.TaskName, ex);
                }
            }
        }

        private void Stop()
        {
            ETask eTask = GetSelectedTask();
            if (eTask != null)
            {
                try
                {
                    if (eTask.Status == (int)TaskState.Running)
                    {
                        TaskDispatch.Instance.Stop(eTask);
                        eTask.Status = (int)TaskState.Stop;
                        SetSelectedTask(eTask);
                        SetBtnEnable(eTask);

                        RefreshRunTotal();

                        UIDataHelper.ExeResult(eTask.TaskName, "【任务停止】停止运行....");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(string.Format("【发生异常】Message：{0}，StackTrace：{1}", ex.Message, ex.StackTrace));
                    UIDataHelper.ExceptionResult(eTask.TaskName, ex);
                }
            }
        }

        private void listTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ETask eTask = GetSelectedTask();
            if (eTask != null)
            {
                try
                {
                    if (eTask.Status == (int)TaskState.Stop)
                    {
                        TaskDispatch.Instance.Start(eTask);
                        eTask.Status = (int)TaskState.Running;
                        SetSelectedTask(eTask);
                        SetBtnEnable(eTask);

                        RefreshRunTotal();

                        UIDataHelper.ExeResult(eTask.TaskName, "【任务启动】开始运行....");
                    }
                    else if (eTask.Status == (int)TaskState.Running)
                    {
                        TaskDispatch.Instance.Stop(eTask);
                        eTask.Status = (int)TaskState.Stop;
                        SetSelectedTask(eTask);
                        SetBtnEnable(eTask);

                        RefreshRunTotal();

                        UIDataHelper.ExeResult(eTask.TaskName, "【任务停止】停止运行....");
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(string.Format("【发生异常】Message：{0}，StackTrace：{1}", ex.Message, ex.StackTrace));
                    UIDataHelper.ExceptionResult(eTask.TaskName, ex);
                }
            }
        }

        private void RefreshRunTotal()
        {
            if (listTasks.Items.Count > 0)
            {
                int total = 0;
                foreach (ListViewItem lt in listTasks.Items)
                {
                    ETask eTask = lt.Tag as ETask;
                    if (eTask.Status == (int)TaskState.Running)
                    {
                        total++;
                    }
                }
                tsRunTotal.Text = total.ToString();
            }
            else
            {
                tsRunTotal.Text = "0";
            }
        }

        private void CheckTasksStop()
        {
            if (listTasks.Items.Count > 0)
            {
                foreach (ListViewItem lt in listTasks.Items)
                {
                    ETask eTask = lt.Tag as ETask;
                    if (eTask.Status == (int)TaskState.Running)
                    {
                        TaskDispatch.Instance.Stop(eTask);
                    }
                }
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)//当用户点击窗体右上角X按钮或(Alt + F4)时 发生           
            {
                this.Hide();
                e.Cancel = true;
                this.ShowInTaskbar = false;
                this.notifyIcon1.Icon = this.Icon;
            }
        }

        private void SysMenuOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                CheckTasksStop();//找所以在运行的服务停止

                // 关闭所有的线程
                this.Dispose();
                this.Close();
                Application.Exit();
                Environment.Exit(0);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void showTask_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }


    }
}
