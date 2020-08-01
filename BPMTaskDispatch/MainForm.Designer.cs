namespace BPMTaskDispatch.Win
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsStart = new System.Windows.Forms.ToolStripButton();
            this.tsStop = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsRunTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsExeCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsExpCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listTasks = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTaskStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listviewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mitemStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemStop = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listExeView = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listExpView = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showTask = new System.Windows.Forms.ToolStripMenuItem();
            this.SysMenuOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mitemOnlyExeOne = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.listviewContextMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStart,
            this.tsStop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(734, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsStart
            // 
            this.tsStart.Enabled = false;
            this.tsStart.Image = ((System.Drawing.Image)(resources.GetObject("tsStart.Image")));
            this.tsStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsStart.Name = "tsStart";
            this.tsStart.Size = new System.Drawing.Size(52, 22);
            this.tsStart.Text = "开始";
            this.tsStart.Click += new System.EventHandler(this.tsStart_Click);
            // 
            // tsStop
            // 
            this.tsStop.Enabled = false;
            this.tsStop.Image = ((System.Drawing.Image)(resources.GetObject("tsStop.Image")));
            this.tsStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsStop.Name = "tsStop";
            this.tsStop.Size = new System.Drawing.Size(52, 22);
            this.tsStop.Text = "停止";
            this.tsStop.Click += new System.EventHandler(this.tsStop_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.tsRunTotal,
            this.toolStripStatusLabel3,
            this.tsExeCount,
            this.toolStripStatusLabel4,
            this.tsExpCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 320);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(734, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "运行：";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // tsRunTotal
            // 
            this.tsRunTotal.Name = "tsRunTotal";
            this.tsRunTotal.Size = new System.Drawing.Size(29, 17);
            this.tsRunTotal.Text = "  -  ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(96, 17);
            this.toolStripStatusLabel3.Text = "       执行次数：";
            // 
            // tsExeCount
            // 
            this.tsExeCount.Name = "tsExeCount";
            this.tsExeCount.Size = new System.Drawing.Size(29, 17);
            this.tsExeCount.Text = "  -  ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(96, 17);
            this.toolStripStatusLabel4.Text = "       异常次数：";
            // 
            // tsExpCount
            // 
            this.tsExpCount.Name = "tsExpCount";
            this.tsExpCount.Size = new System.Drawing.Size(29, 17);
            this.tsExpCount.Text = "  -  ";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listTasks);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(726, 269);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "任务列表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listTasks
            // 
            this.listTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnTaskStatus});
            this.listTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTasks.FullRowSelect = true;
            this.listTasks.GridLines = true;
            this.listTasks.Location = new System.Drawing.Point(3, 3);
            this.listTasks.MultiSelect = false;
            this.listTasks.Name = "listTasks";
            this.listTasks.Size = new System.Drawing.Size(720, 263);
            this.listTasks.TabIndex = 0;
            this.listTasks.UseCompatibleStateImageBehavior = false;
            this.listTasks.View = System.Windows.Forms.View.Details;
            this.listTasks.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listTasks_MouseClick);
            this.listTasks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listTasks_MouseDoubleClick);
            this.listTasks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listTasks_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "任务";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "简要说明";
            this.columnHeader3.Width = 250;
            // 
            // columnTaskStatus
            // 
            this.columnTaskStatus.Text = "状态";
            this.columnTaskStatus.Width = 100;
            // 
            // listviewContextMenu
            // 
            this.listviewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemStart,
            this.mitemStop,
            this.mitemOnlyExeOne});
            this.listviewContextMenu.Name = "listviewContextMenu";
            this.listviewContextMenu.Size = new System.Drawing.Size(125, 70);
            // 
            // mitemStart
            // 
            this.mitemStart.Image = global::BPMTaskDispatch.Win.Properties.Resources.start;
            this.mitemStart.Name = "mitemStart";
            this.mitemStart.Size = new System.Drawing.Size(180, 22);
            this.mitemStart.Text = "启动";
            this.mitemStart.Click += new System.EventHandler(this.mitemStart_Click);
            // 
            // mitemStop
            // 
            this.mitemStop.Image = global::BPMTaskDispatch.Win.Properties.Resources.Stop_red_512px_1186323_easyicon_net;
            this.mitemStop.Name = "mitemStop";
            this.mitemStop.Size = new System.Drawing.Size(180, 22);
            this.mitemStop.Text = "停止";
            this.mitemStop.Click += new System.EventHandler(this.mitemStop_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(734, 295);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listExeView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(726, 269);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "执行日志";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listExeView
            // 
            this.listExeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listExeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listExeView.FullRowSelect = true;
            this.listExeView.GridLines = true;
            this.listExeView.Location = new System.Drawing.Point(3, 3);
            this.listExeView.MultiSelect = false;
            this.listExeView.Name = "listExeView";
            this.listExeView.Size = new System.Drawing.Size(720, 263);
            this.listExeView.TabIndex = 3;
            this.listExeView.UseCompatibleStateImageBehavior = false;
            this.listExeView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "时间";
            this.columnHeader7.Width = 125;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "任务";
            this.columnHeader8.Width = 150;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "执行情况";
            this.columnHeader9.Width = 400;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listExpView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(726, 269);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "异常日志";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listExpView
            // 
            this.listExpView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listExpView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listExpView.FullRowSelect = true;
            this.listExpView.GridLines = true;
            this.listExpView.Location = new System.Drawing.Point(3, 3);
            this.listExpView.MultiSelect = false;
            this.listExpView.Name = "listExpView";
            this.listExpView.Size = new System.Drawing.Size(720, 263);
            this.listExpView.TabIndex = 2;
            this.listExpView.UseCompatibleStateImageBehavior = false;
            this.listExpView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "时间";
            this.columnHeader4.Width = 125;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "任务";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "异常消息";
            this.columnHeader6.Width = 400;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "任务服务中心";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTask,
            this.SysMenuOut});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // showTask
            // 
            this.showTask.BackColor = System.Drawing.SystemColors.ControlDark;
            this.showTask.Image = global::BPMTaskDispatch.Win.Properties.Resources.Rainbow_1001px_1188498_easyicon_net;
            this.showTask.Name = "showTask";
            this.showTask.Size = new System.Drawing.Size(100, 22);
            this.showTask.Text = "显示";
            this.showTask.Click += new System.EventHandler(this.showTask_Click);
            // 
            // SysMenuOut
            // 
            this.SysMenuOut.Image = global::BPMTaskDispatch.Win.Properties.Resources._1183587;
            this.SysMenuOut.Name = "SysMenuOut";
            this.SysMenuOut.Size = new System.Drawing.Size(100, 22);
            this.SysMenuOut.Text = "退出";
            this.SysMenuOut.Click += new System.EventHandler(this.SysMenuOut_Click);
            // 
            // mitemOnlyExeOne
            // 
            this.mitemOnlyExeOne.Image = ((System.Drawing.Image)(resources.GetObject("mitemOnlyExeOne.Image")));
            this.mitemOnlyExeOne.Name = "mitemOnlyExeOne";
            this.mitemOnlyExeOne.Size = new System.Drawing.Size(180, 22);
            this.mitemOnlyExeOne.Text = "执行一次";
            this.mitemOnlyExeOne.Click += new System.EventHandler(this.mitemOnlyExeOne_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 342);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(750, 380);
            this.Name = "MainForm";
            this.Text = "任务服务中心 V1.0";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.listviewContextMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listTasks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnTaskStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripButton tsStart;
        private System.Windows.Forms.ContextMenuStrip listviewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem mitemStart;
        private System.Windows.Forms.ToolStripMenuItem mitemStop;
        private System.Windows.Forms.ToolStripButton tsStop;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tsRunTotal;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listExpView;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView listExeView;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SysMenuOut;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tsExeCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tsExpCount;
        private System.Windows.Forms.ToolStripMenuItem showTask;
        private System.Windows.Forms.ToolStripMenuItem mitemOnlyExeOne;
    }
}

