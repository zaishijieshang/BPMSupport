using BPMTaskDispatch.Extend;
using Dapper;
using Quartz;
using Sn.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMTaskDispatch.Job
{
    public class ADPwdExpireRemindJob : IJob
    {
        class EADEmployee
        {
            public int ID { get; set; }
            public string EmpID { get; set; }
            public string EmpName { get; set; }
            public string PwdExpire { get; set; }
            public int PwdExpireFaultCount { get; set; }
        }

        private bool PushMessage(string EmpID, string Message, out string result)
        {
            // iWXPush.iPush.PushMessage(Message, EmpID);

            bool pushOk = iWXPush.iPush.PushMessageExt(Message, EmpID, out result);

            Log.WriteLine(Message);
            //string purl = ConfigUtil.GetConfigVal("BpmPushUrl");
            //string rep = HttpUtil.Post(purl, string.Format("EmpIDs={0}&Message={1}", "SDT14316", Message));
            //Log.WriteLine(Message + "\n" + rep);

            return pushOk;
        }

        private int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());
            TimeSpan sp = end.Subtract(start);
            return sp.Days;
        }
        public void Execute(IJobExecutionContext context)
        {
            Log.WriteLine("【执行任务】ADPwdExpireRemindJob.Execute....");
            Task.Factory.StartNew(() =>
            {
                string TaskName = "ADPwdExpireRemindJob";
                UIDataHelper.ExeResult(TaskName, "【执行任务】ADPwdExpireRemindJob.Execute....");

                using (var db = DBManager.DB.BPMConnection)
                {
                    string sql = @"select ID,EmpID,EmpName,PwdExpire,PwdExpireFaultCount  from (
                                            select ID,EmpID,EmpName,PwdExpire,PwdExpireFaultCount from ADEmployee where PwdExpire is not null and PwdExpire != '从不') p
                                            where PwdExpire > @PwdExpire and isnull(PwdExpireFaultCount,0)<>999";

                    var list = db.Query<EADEmployee>(sql, new { PwdExpire = new DateTime(DateTime.Now.Year, 1, 1) });

                    foreach (EADEmployee item in list)
                    {
                        try
                        {
                            DateTime CurrPwdExpire;
                            if (DateTime.TryParse(item.PwdExpire, out CurrPwdExpire))
                            {
                                int days = DateDiff(DateTime.Now, CurrPwdExpire);

                                //string msgformat = "[域账号密码到期提醒]{0}{1}，您的密码{2}，到期后将影响您使用各业务系统，请及时修改。{0}<a href=\"https://open.weixin.qq.com/connect/oauth2/authorize?appid=ww58877fbb525792d1&redirect_uri=http%3a%2f%2fbpm.skyworthdigital.com%2fASHX%2fWeChatPush%2fMobileOAuth.ashx%3fMobileUrl%3dhttp%3a%2f%2fbpm.skyworthdigital.com%2fMobile%2f%23%2fpasswordCHange&response_type=code&scope=snsapi_base&agentid=1000070&state=#wechat_redirect\">立即修改</a>";

                                string msgformat = "【通知：你的密码到期，无法登陆系统】{0}{1}，你的域账号密码已经过期，密码过期会影响网络及各IT系统、邮箱登录，请尽快修改 。{0}<a href=\"https://open.weixin.qq.com/connect/oauth2/authorize?appid=ww58877fbb525792d1&redirect_uri=http%3a%2f%2fbpm.skyworthdigital.com%2fASHX%2fWeChatPush%2fMobileOAuth.ashx%3fMobileUrl%3dhttp%3a%2f%2fbpm.skyworthdigital.com%2fMobile%2f%23%2fpasswordCHange&response_type=code&scope=snsapi_base&agentid=1000070&state=#wechat_redirect\">立即修改</a>";

                                string msg = string.Empty;
                                //if (days == 7)//到期前7天
                                //{
                                //    msg = string.Format(msgformat, Environment.NewLine, item.EmpName, string.Format("将于{0}天后到期", days));
                                //}
                                //else if (days == 3)//到期前3天
                                //{
                                //    msg = string.Format(msgformat, Environment.NewLine, item.EmpName, string.Format("将于{0}天后到期", days));
                                //}
                                //else if (days == 0)//到期当天
                                //{
                                //    msg = string.Format(msgformat, Environment.NewLine, item.EmpName, string.Format("今日到期", days));
                                //}
                                //else if (days == -1)//过期1天
                                //{
                                //    msg = string.Format(msgformat, Environment.NewLine, item.EmpName, string.Format("已过期", days));
                                //}

                                if (days == 0)//到期当天
                                {
                                    msg = string.Format(msgformat, Environment.NewLine, item.EmpName, string.Format("今日到期", days));
                                }

                                if (!string.IsNullOrEmpty(msg))
                                {
                                    string pushResult;
                                    bool isok = PushMessage(item.EmpID, msg, out pushResult);
                                    string restring = string.Format("【域用户密码过期提醒】推送用户：{0}({1}), 到期时间：{2}，推送反馈：{4}，推送内容：{3}，反馈数据：{5}", item.EmpName, item.EmpID, item.PwdExpire, msg, isok ? "成功" : "失败", pushResult);
                                    UIDataHelper.ExeResult(TaskName, restring);
                                    Log.WriteLine(restring);
                                }
                            }
                            else
                            {
                                UIDataHelper.ExeResult(TaskName, string.Format("【域用户密码过期提醒】日期转换失败，用户：{0}({1}),到期时间：{2}", item.EmpName, item.EmpID, item.PwdExpire));
                            }

                        }
                        catch (Exception ex)
                        {
                            Log.WriteFormat("【异常】Message： {0}，StackTrace：{1}", ex.Message, ex.StackTrace);
                            UIDataHelper.ExceptionResult(TaskName, string.Format("【域用户密码过期提醒】发生异常，请检查！Message： {0}，StackTrace：{1}", ex.Message, ex.StackTrace));
                        }

                    }

                }

            });
        }
    }
}
