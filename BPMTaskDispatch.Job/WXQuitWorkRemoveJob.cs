using BPMTaskDispatch.DBManager;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using BPMTaskDispatch.Job.Entity;
using Sn.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using BPMTaskDispatch.Extend;

namespace BPMTaskDispatch.Job
{
    //离职移除企业微信job
    public class WXQuitWorkRemoveJob : IJob
    {
        public class EEmpInfo
        {
            public string AppAccount { get; set; }
            public string AppName { get; set; }
            public string AppDept { get; set; }
            public string AppTitle { get; set; }
            public string AppDate { get; set; }
        }

        private List<EEmpInfo> GetEmpList(string AppDate = "")
        {
            List<EEmpInfo> list = new List<EEmpInfo>();
            using (var db = DB.BPMConnection)
            {
                string sql = @"select AppAccount,AppName,AppDept,AppTitle,AppDate from HR_Resign_Approval r
                                        left join BPMInstTasks t on r.TaskID = t.TaskID
                                        where t.State = 'Approved'";

                if (!string.IsNullOrEmpty(AppDate))
                {
                    sql += " and r.AppDate>=@AppDate";
                    list.AddRange(db.Query<EEmpInfo>(sql, new { AppDate = AppDate }));
                }
                else
                {
                    list.AddRange(db.Query<EEmpInfo>(sql));
                }
            }

            using (var db = DB.HRConnection)
            {
                //添加HR里最近日期更新为离职的用户，也从企业微信中删除
                if (!string.IsNullOrEmpty(AppDate))
                {
                    string sql = "select EmpID AppAccount,EmpName AppName,DeptName1 AppDept,PostName AppTitle,UpdateDate AppDate from Employee r where EmpStatus=2 and UpdateDate>=@UpdateDate";
                    list.AddRange(db.Query<EEmpInfo>(sql, new { UpdateDate = AppDate }));
                }
            }

            return list;
        }

        private EWXUser GetWXUser(string EmpId)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN&userid=USERID";

            string token = iWXPush.iPush.GetToken("Department");
            url = url.Replace("ACCESS_TOKEN", token).Replace("USERID", EmpId);

            HttpStatusCode httpStatusCode = HttpStatusCode.NotFound;
            string exMsg = string.Empty;
            string sUser = HttpHelper.Post(url, out httpStatusCode, out exMsg);
            EWXUser eUser = null;
            try
            {
                if (httpStatusCode == HttpStatusCode.OK)
                {
                    eUser = JsonConvert.DeserializeObject<EWXUser>(sUser);
                }
            }
            catch (Exception ex)
            {
                UIDataHelper.ExceptionResult("WXQuitWorkRemoveJob", ex);
            }
            return eUser;
        }

        private EWXResult RemoveWXAccount(string EmpID)
        {
            string url = string.Format("http://bpm.skyworthdigital.com/ASHX/WeChatPush/WXMemberHandler.ashx?action=DeleteUser&EmpID={0}&r={1}",
                EmpID, DateTime.Now.Ticks);

            EWXResult ere = null;
            try
            {
                string re = HttpHelper.GET(url);
                ere = JsonConvert.DeserializeObject<EWXResult>(re);
            }
            catch (Exception ex)
            {
                UIDataHelper.ExceptionResult("WXQuitWorkRemoveJob", ex);
            }
            return ere;
        }

        public void Execute(IJobExecutionContext context)
        {
            Log.WriteLine("【执行任务】WXQuitWorkRemoveJob.Execute....");
            string TaskName = "WXQuitWorkRemoveJob";
            Task.Factory.StartNew(new Action(() =>
            {
                UIDataHelper.ExeResult(TaskName, "【执行任务】WXQuitWorkRemoveJob.Execute....");
                List<EEmpInfo> list = GetEmpList(DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"));
                foreach (EEmpInfo info in list)
                {
                    EWXUser eWXUser = GetWXUser(info.AppAccount.ToUpper());
                    if (eWXUser != null)
                    {
                        UIDataHelper.ExeResult(TaskName, string.Format("【辞职员工检查反馈】【{0}({1})】【存在】企业微信通讯录中....立即执行移除。", info.AppName, info.AppAccount, info.AppDate));

                        EWXResult eWXResult = RemoveWXAccount(info.AppAccount);
                        if (eWXResult != null)
                        {
                            if (eWXResult.success)
                            {
                                UIDataHelper.ExeResult(TaskName, string.Format("【辞职员工检查反馈】【{0}({1})】移除【成功】。辞职流程提交时间：{2} ", info.AppName, info.AppAccount, info.AppDate));
                            }
                            else
                            {
                                UIDataHelper.ExeResult(TaskName, string.Format("【辞职员工检查反馈】【{0}({1})】移除【失败】，稍后将重试。[1]返回成功，但success为false.", info.AppName, info.AppAccount));
                            }
                        }
                        else
                        {
                            UIDataHelper.ExeResult(TaskName, string.Format("【辞职员工检查反馈】【{0}({1})】移除【失败】，稍后将重试。[2]返回失败.", info.AppName, info.AppAccount));
                        }
                    }
                    else
                    {
                        UIDataHelper.ExeResult(TaskName, string.Format("【辞职员工检查反馈】【{0}({1})】【不存在】企业微信通讯录中。辞职流程提交时间：{2} ", info.AppName, info.AppAccount, info.AppDate));
                    }
                }

            }));


        }
    }
}
