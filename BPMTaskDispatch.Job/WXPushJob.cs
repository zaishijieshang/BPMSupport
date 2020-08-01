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
    public class WXPushJob : IJob
    {
        class EPushData
        {
            public int ID { get; set; }
            public int AgentId { get; set; }
            public string ToUser { get; set; }
            public string PushContent { get; set; }
        }
        //0等待推送，1正在发送，2推送成功，3推送失败
        public void Execute(IJobExecutionContext context)
        {
            //1、把推送小于3次失败设置为待发送状态0
            //2、执行推送动作
            Log.WriteLine("【执行任务】WXPushJob.Execute....");
            Task.Factory.StartNew(() =>
            {
                string TaskName = "WXPushJob";
                //UIDataHelper.ExeResult(TaskName, "【执行任务】WXPushJob.Execute....");

                string sql = "update WXPushQueue set Status = 0,FaultCount=FaultCount+1 where Status=3 and FaultCount<3;";
                using (var db = DB.BPMConnection)
                {
                    db.Execute(sql);
                    sql = "select ID,AgentId,ToUser,PushContent from WXPushQueue where Status=0;";

                    List<EPushData> list = db.Query<EPushData>(sql).ToList();
                    if (list.Count > 0)
                    {
                        foreach (EPushData ePushData in list)
                        {
                            string result = string.Empty;
                            bool pushok = iPush.PushMessageJson(ePushData.ID, ePushData.AgentId, ePushData.PushContent, out result);

                            sql = "update WXPushQueue set  LastResult =@LastResult,LastPushTime= getdate(),FaultCount=FaultCount+1,Status=@Status where ID=@ID;";
                            if (pushok)
                            {
                                db.Execute(sql, new { LastResult = result, Status = 2, ID = ePushData.ID });
                                UIDataHelper.ExeResult(TaskName, string.Format("【企业微信推送】[成功]，推送内容：{0}", ePushData.PushContent));
                            }
                            else
                            {
                                db.Execute(sql, new { LastResult = result, Status = 3, ID = ePushData.ID });
                                UIDataHelper.ExeResult(TaskName, string.Format("【企业微信推送】[失败]，推送反馈：{0}", result));
                            }
                        }
                    }

                }

            });
        }
    }
}
