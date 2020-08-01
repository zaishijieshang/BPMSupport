using BPMTaskDispatch.Extend;
using Dapper;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMTaskDispatch.Job
{
    public class ADSetFullNameJob : IJob
    {
        class EADEmployee
        {
            public int ID { get; set; }
            public string EmpID { get; set; }
            public string EmpName { get; set; }
        }

        class EADInfo
        {
            public string FullName { get; set; }
            public string LastChangePwd { get; set; }
            public string ExpirePwd { get; set; }
            public string UserName { get; set; }
        }

        public void Execute(IJobExecutionContext context)
        {
            Log.WriteLine("【执行任务】ADSetFullNameJob.Execute....");
            Task.Factory.StartNew(() =>
            {
                string TaskName = "ADSetFullNameJob";
                UIDataHelper.ExeResult(TaskName, "【执行任务】ADSetFullNameJob.Execute....");
                using (var db = DBManager.DB.BPMConnection)
                {
                    string sql = "select ID,EmpID,EmpName from ADEmployee where EmpName is null and PwdExpireFaultCount<3";

                    var list = db.Query<EADEmployee>(sql);

                    foreach (EADEmployee item in list)
                    {
                        try
                        {
                            string cmd = string.Format("net user {0} /domain", item.EmpID);
                            string output = string.Empty;
                            RunCmd(cmd, out output);
                            EADInfo pi = OutputHandle(output);

                            if (!string.IsNullOrEmpty(pi.FullName))
                            {
                                sql = "update ADEmployee set EmpName=@EmpName where ID=@ID";
                                db.Execute(sql, new { ID = item.ID, EmpName = pi.FullName });

                                UIDataHelper.ExeResult(TaskName, string.Format("更新【{0}】姓名为【{1}】。",
                                (string.IsNullOrEmpty(item.EmpName) ? item.EmpID : item.EmpName), pi.FullName));
                            }
                            else
                            {
                                sql = "update ADEmployee set PwdExpireFaultCount=isnull(PwdExpireFaultCount,0)+1 where ID=@ID";
                                db.Execute(sql, new { ID = item.ID });
                                UIDataHelper.ExeResult(TaskName, string.Format("未查到用户【{0}】的全名。", item.EmpID));
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.WriteFormat("【异常】Message： {0}，StackTrace：{1}", ex.Message, ex.StackTrace);
                            UIDataHelper.ExceptionResult(TaskName, string.Format("【更新域用户姓名】发生异常，请检查！Message： {0}，StackTrace：{1}", ex.Message, ex.StackTrace));
                        }
                    }
                }
            });

        }

        private EADInfo OutputHandle(string output)
        {
            string[] outputLines = output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries); //忽略空行想取
            EADInfo pi = new EADInfo();
            foreach (string item in outputLines)
            {
                if (item.Contains("上次设置密码"))
                {
                    pi.LastChangePwd = item.Replace("上次设置密码", "").Trim();
                }
                else if (item.Contains("用户名"))
                {
                    pi.UserName = item.Replace("用户名", "").Trim();
                }
                else if (item.Contains("密码到期"))
                {
                    pi.ExpirePwd = item.Replace("密码到期", "").Trim();
                }
                else if (item.Contains("全名"))
                {
                    pi.FullName = item.Replace("全名", "").Trim();
                }
            }
            return pi;
        }

        public void RunCmd(string cmd, out string output)
        {
            cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            using (Process p = new Process())
            {
                p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口写入命令
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;

                //获取cmd窗口的输出信息
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();

            }
        }

    }
}
