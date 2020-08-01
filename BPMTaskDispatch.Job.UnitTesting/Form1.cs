using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BPMTaskDispatch.Job.UnitTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new ITResourceExpireJob().Execute(null);

            new ADPwdExpireRemindJob().Execute(null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dt = BPMTaskDispatch.Extend.CronUitl.CronToDateTime("0 5 8,9,10,11,12,13,14,15,16,17,18,19,20,21,22 * * ? *");

            MessageBox.Show(dt.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ITResourceExpireJob().Execute(null);
        }
    }
}
