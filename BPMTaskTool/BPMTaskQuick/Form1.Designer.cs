namespace BPMTaskQuick
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtTask = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.RichTextBox();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.comboState = new System.Windows.Forms.ComboBox();
            this.txtTaskID = new System.Windows.Forms.TextBox();
            this.labStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOwnerPositionID = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearSecurityManager = new System.Windows.Forms.Button();
            this.btnRepairSecurityManager = new System.Windows.Forms.Button();
            this.btnInsertSecurityManager = new System.Windows.Forms.Button();
            this.btnClearSecurity = new System.Windows.Forms.Button();
            this.txtTargetSecurityGroupSID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTargetSecurityGroups = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTargetTask = new System.Windows.Forms.TextBox();
            this.btnRepairSecurity = new System.Windows.Forms.Button();
            this.btnInsertSecurity = new System.Windows.Forms.Button();
            this.txtExeRecord = new System.Windows.Forms.TextBox();
            this.btnSearchTask = new System.Windows.Forms.Button();
            this.txtSearchProcessName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listViewGroup = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSearchSecurityGroups = new System.Windows.Forms.TextBox();
            this.btnSearchSecurity = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewTaskByGroup = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(648, 376);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 42);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(245, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "搜 索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTask
            // 
            this.txtTask.Location = new System.Drawing.Point(69, 11);
            this.txtTask.Name = "txtTask";
            this.txtTask.Size = new System.Drawing.Size(170, 21);
            this.txtTask.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "流程编号";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(3, 152);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(790, 202);
            this.txtDesc.TabIndex = 4;
            this.txtDesc.Text = "";
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(3, 53);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuery.Size = new System.Drawing.Size(790, 93);
            this.txtQuery.TabIndex = 5;
            // 
            // comboState
            // 
            this.comboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboState.FormattingEnabled = true;
            this.comboState.Location = new System.Drawing.Point(665, 10);
            this.comboState.Name = "comboState";
            this.comboState.Size = new System.Drawing.Size(112, 20);
            this.comboState.TabIndex = 6;
            this.comboState.SelectedIndexChanged += new System.EventHandler(this.comboState_SelectedIndexChanged);
            // 
            // txtTaskID
            // 
            this.txtTaskID.Enabled = false;
            this.txtTaskID.Location = new System.Drawing.Point(326, 11);
            this.txtTaskID.Name = "txtTaskID";
            this.txtTaskID.Size = new System.Drawing.Size(67, 21);
            this.txtTaskID.TabIndex = 7;
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(622, 14);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(11, 12);
            this.labStatus.TabIndex = 8;
            this.labStatus.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "OwnerPositionID:";
            // 
            // txtOwnerPositionID
            // 
            this.txtOwnerPositionID.Location = new System.Drawing.Point(117, 388);
            this.txtOwnerPositionID.Name = "txtOwnerPositionID";
            this.txtOwnerPositionID.Size = new System.Drawing.Size(84, 21);
            this.txtOwnerPositionID.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(802, 462);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.labStatus);
            this.tabPage1.Controls.Add(this.txtOwnerPositionID);
            this.tabPage1.Controls.Add(this.comboState);
            this.tabPage1.Controls.Add(this.txtTaskID);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtDesc);
            this.tabPage1.Controls.Add(this.txtTask);
            this.tabPage1.Controls.Add(this.txtQuery);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(794, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "流程信息";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.txtExeRecord);
            this.tabPage2.Controls.Add(this.btnSearchTask);
            this.tabPage2.Controls.Add(this.txtSearchProcessName);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.listViewGroup);
            this.tabPage2.Controls.Add(this.listViewTasks);
            this.tabPage2.Controls.Add(this.txtSearchSecurityGroups);
            this.tabPage2.Controls.Add(this.btnSearchSecurity);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.listViewTaskByGroup);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(794, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "权限设置";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClearSecurityManager);
            this.groupBox1.Controls.Add(this.btnRepairSecurityManager);
            this.groupBox1.Controls.Add(this.btnInsertSecurityManager);
            this.groupBox1.Controls.Add(this.btnClearSecurity);
            this.groupBox1.Controls.Add(this.txtTargetSecurityGroupSID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtTargetSecurityGroups);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTargetTask);
            this.groupBox1.Controls.Add(this.btnRepairSecurity);
            this.groupBox1.Controls.Add(this.btnInsertSecurity);
            this.groupBox1.Location = new System.Drawing.Point(539, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 270);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // btnClearSecurityManager
            // 
            this.btnClearSecurityManager.Location = new System.Drawing.Point(138, 217);
            this.btnClearSecurityManager.Name = "btnClearSecurityManager";
            this.btnClearSecurityManager.Size = new System.Drawing.Size(100, 36);
            this.btnClearSecurityManager.TabIndex = 21;
            this.btnClearSecurityManager.Text = "清除权限[管理]";
            this.btnClearSecurityManager.UseVisualStyleBackColor = true;
            this.btnClearSecurityManager.Click += new System.EventHandler(this.btnClearSecurityManager_Click);
            // 
            // btnRepairSecurityManager
            // 
            this.btnRepairSecurityManager.Location = new System.Drawing.Point(138, 171);
            this.btnRepairSecurityManager.Name = "btnRepairSecurityManager";
            this.btnRepairSecurityManager.Size = new System.Drawing.Size(100, 36);
            this.btnRepairSecurityManager.TabIndex = 20;
            this.btnRepairSecurityManager.Text = "修补权限[管理]";
            this.btnRepairSecurityManager.UseVisualStyleBackColor = true;
            this.btnRepairSecurityManager.Click += new System.EventHandler(this.btnRepairSecurityManager_Click);
            // 
            // btnInsertSecurityManager
            // 
            this.btnInsertSecurityManager.Location = new System.Drawing.Point(138, 125);
            this.btnInsertSecurityManager.Name = "btnInsertSecurityManager";
            this.btnInsertSecurityManager.Size = new System.Drawing.Size(100, 35);
            this.btnInsertSecurityManager.TabIndex = 19;
            this.btnInsertSecurityManager.Text = "刷入权限[管理]";
            this.btnInsertSecurityManager.UseVisualStyleBackColor = true;
            this.btnInsertSecurityManager.Click += new System.EventHandler(this.btnInsertSecurityManager_Click);
            // 
            // btnClearSecurity
            // 
            this.btnClearSecurity.Location = new System.Drawing.Point(10, 217);
            this.btnClearSecurity.Name = "btnClearSecurity";
            this.btnClearSecurity.Size = new System.Drawing.Size(100, 36);
            this.btnClearSecurity.TabIndex = 18;
            this.btnClearSecurity.Text = "清除权限[查看]";
            this.btnClearSecurity.UseVisualStyleBackColor = true;
            this.btnClearSecurity.Click += new System.EventHandler(this.btnClearSecurity_Click);
            // 
            // txtTargetSecurityGroupSID
            // 
            this.txtTargetSecurityGroupSID.Enabled = false;
            this.txtTargetSecurityGroupSID.Location = new System.Drawing.Point(10, 87);
            this.txtTargetSecurityGroupSID.Name = "txtTargetSecurityGroupSID";
            this.txtTargetSecurityGroupSID.Size = new System.Drawing.Size(100, 21);
            this.txtTargetSecurityGroupSID.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "安全组";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(159, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "流程名称";
            // 
            // txtTargetSecurityGroups
            // 
            this.txtTargetSecurityGroups.Enabled = false;
            this.txtTargetSecurityGroups.Location = new System.Drawing.Point(10, 60);
            this.txtTargetSecurityGroups.Name = "txtTargetSecurityGroups";
            this.txtTargetSecurityGroups.Size = new System.Drawing.Size(100, 21);
            this.txtTargetSecurityGroups.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "<-";
            // 
            // txtTargetTask
            // 
            this.txtTargetTask.Enabled = false;
            this.txtTargetTask.Location = new System.Drawing.Point(138, 60);
            this.txtTargetTask.Name = "txtTargetTask";
            this.txtTargetTask.Size = new System.Drawing.Size(100, 21);
            this.txtTargetTask.TabIndex = 6;
            // 
            // btnRepairSecurity
            // 
            this.btnRepairSecurity.Location = new System.Drawing.Point(10, 171);
            this.btnRepairSecurity.Name = "btnRepairSecurity";
            this.btnRepairSecurity.Size = new System.Drawing.Size(100, 36);
            this.btnRepairSecurity.TabIndex = 13;
            this.btnRepairSecurity.Text = "修补权限[查看]";
            this.btnRepairSecurity.UseVisualStyleBackColor = true;
            this.btnRepairSecurity.Click += new System.EventHandler(this.btnRepairSecurity_Click);
            // 
            // btnInsertSecurity
            // 
            this.btnInsertSecurity.Location = new System.Drawing.Point(10, 125);
            this.btnInsertSecurity.Name = "btnInsertSecurity";
            this.btnInsertSecurity.Size = new System.Drawing.Size(100, 35);
            this.btnInsertSecurity.TabIndex = 12;
            this.btnInsertSecurity.Text = "刷入权限[查看]";
            this.btnInsertSecurity.UseVisualStyleBackColor = true;
            this.btnInsertSecurity.Click += new System.EventHandler(this.btnInsertSecurity_Click);
            // 
            // txtExeRecord
            // 
            this.txtExeRecord.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtExeRecord.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtExeRecord.ForeColor = System.Drawing.Color.Lime;
            this.txtExeRecord.Location = new System.Drawing.Point(3, 311);
            this.txtExeRecord.Multiline = true;
            this.txtExeRecord.Name = "txtExeRecord";
            this.txtExeRecord.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExeRecord.Size = new System.Drawing.Size(787, 117);
            this.txtExeRecord.TabIndex = 14;
            // 
            // btnSearchTask
            // 
            this.btnSearchTask.Location = new System.Drawing.Point(470, 6);
            this.btnSearchTask.Name = "btnSearchTask";
            this.btnSearchTask.Size = new System.Drawing.Size(63, 23);
            this.btnSearchTask.TabIndex = 11;
            this.btnSearchTask.Text = "搜索";
            this.btnSearchTask.UseVisualStyleBackColor = true;
            this.btnSearchTask.Click += new System.EventHandler(this.btnSearchTask_Click);
            // 
            // txtSearchProcessName
            // 
            this.txtSearchProcessName.Location = new System.Drawing.Point(309, 6);
            this.txtSearchProcessName.Name = "txtSearchProcessName";
            this.txtSearchProcessName.Size = new System.Drawing.Size(155, 21);
            this.txtSearchProcessName.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "流程名:";
            // 
            // listViewGroup
            // 
            this.listViewGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.listViewGroup.FullRowSelect = true;
            this.listViewGroup.GridLines = true;
            this.listViewGroup.Location = new System.Drawing.Point(3, 39);
            this.listViewGroup.Name = "listViewGroup";
            this.listViewGroup.Size = new System.Drawing.Size(285, 125);
            this.listViewGroup.TabIndex = 8;
            this.listViewGroup.UseCompatibleStateImageBehavior = false;
            this.listViewGroup.View = System.Windows.Forms.View.Details;
            this.listViewGroup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewGroup_MouseDown);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "安全组";
            this.columnHeader5.Width = 257;
            // 
            // listViewTasks
            // 
            this.listViewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.GridLines = true;
            this.listViewTasks.Location = new System.Drawing.Point(294, 39);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(239, 265);
            this.listViewTasks.TabIndex = 7;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.View = System.Windows.Forms.View.Details;
            this.listViewTasks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewTasks_MouseDown);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "流程";
            this.columnHeader3.Width = 145;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "数量";
            this.columnHeader4.Width = 69;
            // 
            // txtSearchSecurityGroups
            // 
            this.txtSearchSecurityGroups.Location = new System.Drawing.Point(50, 8);
            this.txtSearchSecurityGroups.Name = "txtSearchSecurityGroups";
            this.txtSearchSecurityGroups.Size = new System.Drawing.Size(114, 21);
            this.txtSearchSecurityGroups.TabIndex = 3;
            // 
            // btnSearchSecurity
            // 
            this.btnSearchSecurity.Location = new System.Drawing.Point(170, 6);
            this.btnSearchSecurity.Name = "btnSearchSecurity";
            this.btnSearchSecurity.Size = new System.Drawing.Size(61, 23);
            this.btnSearchSecurity.TabIndex = 2;
            this.btnSearchSecurity.Text = "搜索";
            this.btnSearchSecurity.UseVisualStyleBackColor = true;
            this.btnSearchSecurity.Click += new System.EventHandler(this.btnSearchSecurity_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "安全组:";
            // 
            // listViewTaskByGroup
            // 
            this.listViewTaskByGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader6});
            this.listViewTaskByGroup.FullRowSelect = true;
            this.listViewTaskByGroup.GridLines = true;
            this.listViewTaskByGroup.Location = new System.Drawing.Point(3, 170);
            this.listViewTaskByGroup.Name = "listViewTaskByGroup";
            this.listViewTaskByGroup.Size = new System.Drawing.Size(285, 134);
            this.listViewTaskByGroup.TabIndex = 0;
            this.listViewTaskByGroup.UseCompatibleStateImageBehavior = false;
            this.listViewTaskByGroup.View = System.Windows.Forms.View.Details;
            this.listViewTaskByGroup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewTaskByGroup_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "流程";
            this.columnHeader1.Width = 122;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类型";
            this.columnHeader2.Width = 49;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "数量";
            this.columnHeader6.Width = 85;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 462);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(818, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(818, 500);
            this.Name = "Form1";
            this.Text = "BPM流程管理辅助工具";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtTask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtDesc;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.ComboBox comboState;
        private System.Windows.Forms.TextBox txtTaskID;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOwnerPositionID;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearchSecurityGroups;
        private System.Windows.Forms.Button btnSearchSecurity;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listViewTaskByGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTargetSecurityGroups;
        private System.Windows.Forms.TextBox txtTargetTask;
        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnSearchTask;
        private System.Windows.Forms.TextBox txtSearchProcessName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView listViewGroup;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnInsertSecurity;
        private System.Windows.Forms.Button btnRepairSecurity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtExeRecord;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTargetSecurityGroupSID;
        private System.Windows.Forms.Button btnClearSecurity;
        private System.Windows.Forms.Button btnInsertSecurityManager;
        private System.Windows.Forms.Button btnRepairSecurityManager;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnClearSecurityManager;
    }
}

