using Newtonsoft.Json;
using Sn.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WXDeptTool.Code.Entity;
using Dapper;
using BPMTaskDispatch.DBManager;

namespace WXDeptTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string deptId = textBox1.Text.Trim();
            CreateDept(Convert.ToInt32(deptId));
        }

        string token = "lSgmTcUlnHzDfoJXHcwh-Qj8eHRcdod6qLIBXK8XlI2NDSEIlKn_T-2w6GMfZoQFB-jekvyboz1dk_upvgPEC6f5G3EVb_116cHy_JSZ63lWVZ_AUEK9oJjoeBnBBRppmbwGU0ap9maB-qtEWlaD5pH2DL921QDhaL5wmsEws0s0oZRffrBRIFReFjnwEUnfpcvuhBvXMs_8FvY-6JwBFA";
        // {
        //   "name": "创维液晶",        
        //   "parentid": 1,
        //   "id": 880
        //}
        public void PostDeptData(EPostDept ePostDept)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token=" + token;
            string result = HttpUtil.Post(url, JsonConvert.SerializeObject(ePostDept));

            label1.Text = label1.Text + '\n' + result;
        }

        public void CreateDept(int DeptID)
        {
            string sql = "select DeptID id,DeptName name,ParentID parentid from Department where DeptID=@DeptID";
            EPostDept ePostDept = null;
            using (var db = DB.HRConnection)
            {
                ePostDept = db.Query<EPostDept>(sql, new { DeptID = DeptID }).FirstOrDefault();
            }

            if (ePostDept != null)
            {
                // PostDeptData(ePostDept);
                CreareChildDept(ePostDept.id);
            }
        }

        public void CreareChildDept(int ParentID)
        {
            string sql = "select DeptID id,DeptName name,ParentID parentid from Department where ParentID=@ParentID";
            using (var db = DB.HRConnection)
            {
                IEnumerable<EPostDept> list = db.Query<EPostDept>(sql, new { ParentID = ParentID });

                if (list.Count() > 0)
                {
                    foreach (EPostDept item in list)
                    {
                        PostDeptData(item);
                        CreareChildDept(item.id);
                    }
                }

            }
        }
    }
}
