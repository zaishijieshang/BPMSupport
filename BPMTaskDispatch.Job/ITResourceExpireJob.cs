using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BPMTaskDispatch.DBManager;
using SkyworthMail;
using Sn.Utility;
using BPMTaskDispatch.Extend;
using BPMTaskDispatch.Job.Entity;

namespace BPMTaskDispatch.Job
{
    public class ITResourceExpireJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Log.WriteLine("【执行任务】ITResourceExpireJob.Execute....");

            Task.Factory.StartNew(() =>
            {
                string TaskName = "ITResourceExpireJob";
                UIDataHelper.ExeResult(TaskName, "【执行任务】ITResourceExpireJob.Execute....");
                string resultExeUI = string.Empty;
                try
                {
                    DateTime curDateTime = DateTime.Now;
                    //IT资源申请 检查是否到期
                    string sql = @"select * from (select isnull(ra.IsSend4000,0) IsSendEmail,ra.ProjectDesc AppMark,dbo.Fun_GetSerialNumByTaskID(t.TaskID)TaskSN,ra.AppName AppEmpName,t.TaskID,ra.AppDate,ra.AppID,DurationType,DurationMonth,dbo.Fun_GetStepFinishAt('资源分配',t.TaskID) StartTime 
                                        from IT_Resource_Application ra
                                        left join BPMInstTasks t on ra.TaskID = t.TaskID
                                        where t.ProcessName = 'IT资源申请' and ra.DurationMonth is not null
                                        and t.State in('Approved')
                                        ) tab where StartTime is not null";
                    //and(ra.IsSend4000 is null or ra.IsSend4000 = 0)

                    IEnumerable<EITResource> list = null;
                    using (var db = DB.BPMConnection)
                    {
                        list = db.Query<EITResource>(sql);
                    }

                    if (list != null && list.Any())
                    {
                        resultExeUI += string.Format("申请资源数：{0} ,到期：@Expire@   流程：IT资源申请  ", list.Count());
                        string sqlu = "update IT_Resource_Application set IsSend4000=@IsSend4000 where TaskID=@TaskID";
                        int expireCount = 0;
                        foreach (var item in list)
                        {
                            DateTime ExpireTime = item.StartTime.AddMonths(item.DurationMonth);
                            if (curDateTime.CompareTo(ExpireTime) > 0 && item.IsSendEmail == 0)//-1 少于，1大于 并且未发邮件
                            {

                                expireCount++;
                                string eUserEmpID;
                                string html = BuilderExpireHtml(item.TaskID, out eUserEmpID);
                                resultExeUI += string.Format("检查到【{0}】资源到期。", eUserEmpID);

                                if (!string.IsNullOrEmpty(html))
                                {
                                    string userEmail = GainAttach.GetMailByEmpID(eUserEmpID);
                                    bool flag = SendEmail(html, userEmail);
                                    if (flag)
                                    {
                                        resultExeUI += string.Format("，【{0}】发送提醒邮件成功。", userEmail);
                                        using (var db = DB.BPMConnection)
                                        {
                                            db.Execute(sqlu, new { IsSend4000 = 1, TaskID = item.TaskID });
                                        }
                                    }
                                    else
                                    {
                                        resultExeUI += string.Format("，【{0}】发送提醒邮件失败，将下次检查时重发。", userEmail);
                                    }
                                }
                            }

                            //添加到到期记录表
                            AddPurviewExpire(new PurviewExpire()
                            {
                                AppDate = item.AppDate,
                                AppEmpID = item.AppID,
                                IsSendEmail = item.IsSendEmail,
                                AppEmpName = item.AppEmpName,
                                TaskSN = item.TaskSN,
                                AppMark = item.AppMark,
                                StartDate = item.StartTime,
                                ExpireDate = ExpireTime,
                                RoleType = item.DurationType,
                                TaskID = item.TaskID
                            });
                        }

                        if (!string.IsNullOrEmpty(resultExeUI))
                        {
                            resultExeUI = resultExeUI.Replace("@Expire@", expireCount.ToString());
                            UIDataHelper.ExeResult(TaskName, resultExeUI);
                            resultExeUI = string.Empty;
                        }
                    }


                    //IT故障的IT资源选项  检查是否到期
                    sql = @"select * from (
                             select  isnull(ita.IsSend4000,0) IsSendEmail,ita.Description AppMark,dbo.Fun_GetSerialNumByTaskID(t.TaskID)TaskSN,t.TaskID,ita.AppDate,ita.Name AppEmpName,ita.HRID AppID,DurationType,DurationMonth,dbo.Fun_GetStepFinishAt('IT工程师',t.TaskID) StartTime from IT_HDApplicant ita
                             left join BPMInstTasks t on ita.TaskID = t.TaskID
                             where t.ProcessName='IT终端服务申请' 
                             and ita.ServerModel='IT资源申请'
                             and ita.DurationMonth is not null 
                             and t.State in('Approved')
                             ) tab where StartTime is not null";
                    //and (ita.IsSend4000 is null or ita.IsSend4000 =0)
                    using (var db = DB.BPMConnection)
                    {
                        list = db.Query<EITResource>(sql);
                    }

                    if (list != null && list.Any())
                    {
                        resultExeUI += string.Format("申请资源数：{0} ,到期：@Expire@   流程：IT终端服务申请-IT资源申请  ", list.Count());
                        string sqlu = "update IT_HDApplicant set IsSend4000=@IsSend4000 where TaskID=@TaskID";
                        int expireCount = 0;
                        foreach (var item in list)
                        {
                            DateTime ExpireTime = item.StartTime.AddMonths(item.DurationMonth);
                            if (curDateTime.CompareTo(ExpireTime) > 0 && item.IsSendEmail == 0)//-1 少于，1大于  并且未发邮件
                            {
                                expireCount++;
                                string eUserEmpID;
                                string html = BuilderExpireHtml2(item.TaskID, out eUserEmpID);
                                resultExeUI += string.Format("检查到【{0}】资源到期。", eUserEmpID);

                                if (!string.IsNullOrEmpty(html))
                                {
                                    string userEmail = GainAttach.GetMailByEmpID(eUserEmpID);
                                    bool flag = SendEmail(html, userEmail);
                                    if (flag)
                                    {
                                        resultExeUI += string.Format("【{0}】发送提醒邮件成功。", userEmail);
                                        using (var db = DB.BPMConnection)
                                        {
                                            db.Execute(sqlu, new { IsSend4000 = 1, TaskID = item.TaskID });
                                        }
                                    }
                                    else
                                    {
                                        resultExeUI += string.Format("，【{0}】发送提醒邮件失败，将下次检查时重发。", userEmail);
                                        //检查是否离职
                                    }
                                }

                            }

                            //添加到到期记录表
                            AddPurviewExpire(new PurviewExpire()
                            {
                                AppDate = item.AppDate,
                                AppEmpID = item.AppID,
                                IsSendEmail = item.IsSendEmail,
                                AppEmpName = item.AppEmpName,
                                TaskSN = item.TaskSN,
                                AppMark = item.AppMark,
                                StartDate = item.StartTime,
                                ExpireDate = ExpireTime,
                                RoleType = item.DurationType,
                                TaskID = item.TaskID
                            });

                        }

                        if (!string.IsNullOrEmpty(resultExeUI))
                        {
                            resultExeUI = resultExeUI.Replace("@Expire@", expireCount.ToString());
                            UIDataHelper.ExeResult(TaskName, resultExeUI);
                            resultExeUI = string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(TaskName, ex);
                    UIDataHelper.ExceptionResult(TaskName, ex);
                }
            });
        }

        private void AddPurviewExpire(PurviewExpire ePurviewExpire)
        {
            string sql = "select count(*) Count from PurviewExpire where TaskID=@TaskID";
            using (var db = DB.BPMConnection)
            {
                int re = db.ExecuteScalar<int>(sql, new { TaskID = ePurviewExpire.TaskID });
                if (re <= 0)
                {
                    sql = "insert into PurviewExpire(TaskID,TaskSN,IsSendEmail,AppDate,AppEmpID,AppEmpName,AppMark,StartDate,ExpireDate,RoleType,IsHandle,HandleMark,HandleEmpID,HandleDate) values(@TaskID,@TaskSN,@IsSendEmail,@AppDate,@AppEmpID,@AppEmpName,@AppMark,@StartDate,@ExpireDate,@RoleType,null,null,null,null);";
                    db.Execute(sql, ePurviewExpire);
                }
            }
        }

        private bool SendEmail(string sHtml, string toEmail)
        {
            bool flag = false;
            try
            {
                string emailAccount = ConfigUtil.GetConfigVal("EmailAccount");
                string[] aemailAccount = emailAccount.Split('丨');
                string CopyFor = ConfigUtil.GetConfigVal("CopyToEmail");
                string to = string.IsNullOrEmpty(toEmail) ? CopyFor : toEmail;
                string from = aemailAccount[0];
                string title = "用户申请资源到期提醒";
                string body = sHtml;
                string pwd = aemailAccount[1];


                SendMail sm = new SendMail(to, from, body, title, pwd, CopyFor);
                //string attachs = GainAttach.DownLoadAttach("201909260294");
                //if (!string.IsNullOrEmpty(attachs))
                //{
                //    sm.Attachments(attachs);
                //}
                sm.Send();
                flag = true;
            }
            catch (Exception ex)
            {
                UIDataHelper.ExceptionResult("ITResourceExpireJob.SendEmail", ex);
            }
            return flag;
        }

        private string EmailHtmlTemp()
        {
            string htmlTemp = @"<!DOCTYPE html>" +
          "<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">" +
          "<head>" +
          "<meta charset=\"utf-8\" />" +
          "<title>资源到期提醒</title>" +
          "</head>" +
          "<body>" +
          "<div>" +
          "<p>用户申请的IT资源到期提醒，请处理。</p>" +
          "<p style=\"padding-left:20px;\">流程编号：@SerialNum@</p>" +
          "<p style=\"padding-left:20px;\">申请人：@AppName@</p>" +
          "<p style=\"padding-left:20px;\">申请时间：@AppDate@</p>" +
          "<p style=\"padding-left:20px;\">申请周期：@AppUseCycle@</p>" +
          "<p style=\"padding-left:20px;\">申请说明：@AppDesc@</p>" +
          "<p style=\"padding-left:20px;\">IT处理：@ITHandle@</p>" +
          "</div>" +
          "<div>" +
          "<p style=\"color:blue;font-size:13px\">BPM系统自动发送，请勿直接回复。</p>" +
          "</div>" +
          "</body>" +
          "</html>";
            return htmlTemp;
        }

        private string BuilderExpireHtml(int TaskID, out string EmpID)
        {
            EmpID = string.Empty;
            string thtml = string.Empty;
            using (var db = DB.BPMConnection)
            {
                string sql = "select top 1 AppID,AppSN SerialNum,DurationType,AppName,DurationMonth,AppDate, ProjectDesc AppDesc,DisposeComment ITHandle from IT_Resource_Application where TaskID=@TaskID";
                var edata = db.Query<EExpireData>(sql, new { TaskID = TaskID }).FirstOrDefault();
                if (edata != null)
                {
                    thtml = EmailHtmlTemp();
                    thtml = thtml.Replace("@SerialNum@", edata.SerialNum)
                        .Replace("@AppUseCycle@", string.Format("{0} , {1}个月", edata.DurationType, edata.DurationMonth))
                        .Replace("@AppName@", string.Format("{0}({1})", edata.AppName, edata.AppID))
                        .Replace("@AppDate@", edata.AppDate.ToDate())
                        .Replace("@AppDesc@", edata.AppDesc)
                        .Replace("@ITHandle@", edata.ITHandle);

                    EmpID = edata.AppID;
                }
                return thtml;
            }
        }

        private string BuilderExpireHtml2(int TaskID, out string EmpID)
        {
            EmpID = string.Empty;
            string thtml = string.Empty;
            using (var db = DB.BPMConnection)
            {
                string sql = "select top 1 HRID AppID,SN SerialNum,DurationType,Name AppName,DurationMonth,AppDate, Description AppDesc,HandleOpinion ITHandle from IT_HDApplicant where TaskID=@TaskID";
                var edata = db.Query<EExpireData>(sql, new { TaskID = TaskID }).FirstOrDefault();
                if (edata != null)
                {
                    thtml = EmailHtmlTemp();
                    thtml = thtml.Replace("@SerialNum@", edata.SerialNum)
                        .Replace("@AppUseCycle@", string.Format("{0} , {1}个月", edata.DurationType, edata.DurationMonth))
                        .Replace("@AppName@", string.Format("{0}({1})", edata.AppName, edata.AppID))
                        .Replace("@AppDate@", edata.AppDate.ToDate())
                        .Replace("@AppDesc@", edata.AppDesc)
                        .Replace("@ITHandle@", edata.ITHandle);

                    EmpID = edata.AppID;
                }
                return thtml;
            }
        }

        private class EExpireData
        {
            public string SerialNum { get; set; }
            public string DurationType { get; set; }
            public string AppID { get; set; }//申请人工号
            public string AppName { get; set; }//申请人姓名
            public int DurationMonth { get; set; }
            public DateTime AppDate { get; set; }
            public string AppDesc { get; set; }
            public string ITHandle { get; set; }
        }

        private class EITResource
        {
            public int TaskID { get; set; }
            /// <summary>
            /// 长期/短期
            /// </summary>
            public string DurationType { get; set; }
            /// <summary>
            /// 月份数
            /// </summary>
            public int DurationMonth { get; set; }
            /// <summary>
            /// 是否发送通知邮件
            /// </summary>
            public int IsSendEmail { get; set; }
            public DateTime AppDate { get; set; }
            public string AppID { get; set; }
            public string AppEmpName { get; set; }
            public string TaskSN { get; set; }
            public string AppMark { get; set; }
            /// <summary>
            /// 起始时间
            /// </summary>
            public DateTime StartTime { get; set; }
        }
    }
}
