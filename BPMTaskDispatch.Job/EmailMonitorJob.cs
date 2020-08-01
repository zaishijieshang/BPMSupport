using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using BPMTaskDispatch.DBManager;
using iWXPush;
using BPMTaskDispatch.Extend;
using System.Threading.Tasks;

namespace BPMTaskDispatch.Job
{
    public class EmailMonitorJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Log.WriteLine("【执行任务】EmailMonitorJob.Execute....");
            Task.Factory.StartNew(() =>
            {
                string TaskName = "EmailMonitorJob";
                UIDataHelper.ExeResult(TaskName, "【执行任务】EmailMonitorJob.Execute....");
                try
                {
                    using (var db = DB.BPMConnection)
                    {
                        string sql = "select  count(1) from BPMSysMessagesFailed where  Address ='LiuXiaoRong@skyworth.com'  and CreateAt>=@CreateAt";
                        string ZeroToday = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                        //大于今天的
                        int faultCount = db.ExecuteScalar<int>(sql, new { CreateAt = ZeroToday });

                        if (faultCount > 0)
                        {
                            sql = "select Title,CreateAt,Error from BPMSysMessagesFailed where  Address ='LiuXiaoRong@skyworth.com'  and CreateAt>=@CreateAt";

                            var list = db.Query<EMessageFalut>(sql, new { CreateAt = ZeroToday });

                            string msg = string.Empty;

                            if (list.Count() > 0)
                            {
                                msg = string.Format("BPM电子流邮件【LiuXiaoRong@skyworth.com】发送异常，请检测！" + Environment.NewLine);
                                foreach (var item in list)
                                {
                                    msg += "内容：" + item.Title + Environment.NewLine;
                                    msg += "时间：" + item.CreateAt.ToTime() + Environment.NewLine;
                                    msg += "错误：" + item.Error + Environment.NewLine + Environment.NewLine;
                                }
                            }

                            //发出报警
                            iPush.PushMessage(msg);
                            Log.WriteFormat("【发送报警】：{0}", msg);
                            UIDataHelper.ExeResult(TaskName, "BPM电子流邮件【LiuXiaoRong@skyworth.com】发送异常，请检测！");
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

        class EMessageFalut
        {
            public string Title { get; set; }
            public DateTime CreateAt { get; set; }
            public string Error { get; set; }
        }
    }
}
