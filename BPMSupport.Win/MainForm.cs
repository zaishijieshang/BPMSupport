using BPMSupport.Win.Module.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BPMSupport.Win
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void TasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LXREmail cf = new LXREmail(); //ChidForm为子窗体
            cf.ShowIcon = false;//标题栏不显示Icon
            cf.MdiParent = this;
            cf.Show();
        }
    }
}
