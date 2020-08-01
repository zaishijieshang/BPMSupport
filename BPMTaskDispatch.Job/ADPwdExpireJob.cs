using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Dapper;
using BPMTaskDispatch.Extend;
using System.Threading.Tasks;

namespace BPMTaskDispatch.Job
{
    public class ADPwdExpireJob : IJob
    {
        class EADEmployee
        {
            public int ID { get; set; }
            public string EmpID { get; set; }
            public string EmpName { get; set; }
        }

        class EADPwdInfo
        {
            public string FullName { get; set; }
            public string LastChangePwd { get; set; }
            public string ExpirePwd { get; set; }
        }

        public void Execute(IJobExecutionContext context)
        {
            Log.WriteLine("【执行任务】ADPwdExpireJob.Execute....");
            Task.Factory.StartNew(() =>
            {
                string TaskName = "ADPwdExpireJob";
                UIDataHelper.ExeResult(TaskName, "【执行任务】ADPwdExpireJob.Execute....");
                using (var db = DBManager.DB.BPMConnection)
                {
                    //string sql = "select ID,EmpID,EmpName from ADEmployee where isnull(PwdExpireFaultCount,0)<=3";//3次失败，则以后不检查
                    string sql = @"select ID,EmpID,EmpName,PwdExpire,PwdExpireFaultCount  from (
                                            select ID,EmpID,EmpName,PwdExpire,PwdExpireFaultCount from ADEmployee where PwdExpire is not null and PwdExpire != '从不') p
                                            where PwdExpire > @PwdExpire and isnull(PwdExpireFaultCount,0)<=3";
                    var list = db.Query<EADEmployee>(sql, new { PwdExpire = new DateTime(DateTime.Now.Year, 1, 1) });

                    foreach (EADEmployee item in list)
                    {
                        try
                        {
                            string cmd = string.Format("net user {0} /domain", item.EmpID);
                            string output = string.Empty;
                            RunCmd(cmd, out output);

                            EADPwdInfo pi = OutputHandle(output);
                            if (!string.IsNullOrEmpty(pi.ExpirePwd))
                            {
                                sql = "update ADEmployee set PwdExpire=@PwdExpire where ID=@ID";
                                db.Execute(sql, new { PwdExpire = pi.ExpirePwd, ID = item.ID });

                                UIDataHelper.ExeResult(TaskName, string.Format("更新【{0}】密码到期为:{1}", item.EmpName, pi.ExpirePwd));

                                if (pi.ExpirePwd != "从不")
                                {
                                    DateTime expirePwd;
                                    if (DateTime.TryParse(pi.ExpirePwd, out expirePwd))
                                    {
                                        if (expirePwd.CompareTo(DateTime.Now.AddDays(-365)) == -1)//过期日期小于1年前的时间，则设为999，后续不更新
                                        {
                                            sql = "update ADEmployee set PwdExpireFaultCount=999 where ID=@ID";
                                            db.Execute(sql, new { ID = item.ID });
                                        }
                                    }
                                }
                            }
                            else
                            {
                                sql = "update ADEmployee set PwdExpireFaultCount=isnull(PwdExpireFaultCount,0)+1 where ID=@ID";
                                db.Execute(sql, new { ID = item.ID });

                                UIDataHelper.ExeResult(TaskName, string.Format("更新【{0}】密码到期失败。{1}",
                                    (string.IsNullOrEmpty(item.EmpName) ? item.EmpID : item.EmpName), pi.ExpirePwd));

                                if (string.IsNullOrEmpty(item.EmpName) && !string.IsNullOrEmpty(pi.FullName))
                                {
                                    sql = "update ADEmployee set EmpName=@EmpName where ID=@ID";
                                    db.Execute(sql, new { ID = item.ID, EmpName = pi.FullName });

                                    UIDataHelper.ExeResult(TaskName, string.Format("更新姓名【{0}】为【{1}】。",
                                    (string.IsNullOrEmpty(item.EmpName) ? item.EmpID : item.EmpName), pi.FullName));
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Log.WriteFormat("【异常】Message： {0}，StackTrace：{1}", ex.Message, ex.StackTrace);
                            UIDataHelper.ExceptionResult(TaskName, string.Format("【检查域用户密码过期】发生异常，请检查！Message： {0}，StackTrace：{1}", ex.Message, ex.StackTrace));
                        }
                    }
                }
            });
        }

        private EADPwdInfo OutputHandle(string output)
        {
            string[] outputLines = output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries); //忽略空行想取
            EADPwdInfo pi = new EADPwdInfo();
            foreach (string item in outputLines)
            {
                if (item.Contains("上次设置密码"))
                {
                    pi.LastChangePwd = item.Replace("上次设置密码", "").Trim();
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


//item.Contains("Microsoft Windows [版本 6.1.7601]") ||
//item.Contains("版权所有") ||
//item.Contains("/domain&exit") ||
//item.Contains("这项请求将在域") ||
//item.Contains("这项请求将在域") ||
//item.Contains("命令成功完成。")