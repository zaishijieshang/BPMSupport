using BPMTaskQuick.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using BPMTaskDispatch.DBManager;
using System.Threading.Tasks;

namespace BPMTaskQuick.Code
{
    public class DBDispatch
    {
        public static Action<string> ShowDataToUI = null;
        public static Action <string> RefreshUI = null;
        public static List<EGroup> ListGroup(string groupName = null)
        {
            List<EGroup> results = new List<EGroup>();
            using (var db = DB.BPMConnection)
            {
                string sql = "select GroupName,SID from BPMSecurityGroups where 1=1 ";
                if (!string.IsNullOrEmpty(groupName))
                {
                    sql += "and GroupName like '%'+@GroupName+'%'";
                    results = db.Query<EGroup>(sql, new { GroupName = groupName }).ToList();
                }
                else
                {
                    results = db.Query<EGroup>(sql).ToList();
                }
            }
            return results;
        }

        public static List<ETaskCount> ListGroupTasks(string SID)
        {
            using (var db = DB.BPMConnection)
            {
                string sql = @"select t.ProcessName,s.AllowAdmin,count(1) [Count] from BPMSecurityTACL  s 
                                      left join BPMInstTasks t on s.TaskID = t.TaskID where s.SID = @SID
                                      group by t.ProcessName,s.AllowAdmin";
                return db.Query<ETaskCount>(sql, new { SID = SID }).ToList();
            }
        }

        public static List<ETaskCount> ListTasksCount(string ProcessName = null)
        {
            List<ETaskCount> list = new List<ETaskCount>();
            using (var db = DB.BPMConnection)
            {
                string sql = @"select ProcessName,count(1) [Count] from BPMInstTasks where 1=1";

                if (!string.IsNullOrEmpty(ProcessName))
                {
                    sql += " and ProcessName like '%'+@ProcessName+'%' group by ProcessName";
                    list = db.Query<ETaskCount>(sql, new { ProcessName = ProcessName }).ToList();
                }
                else
                {
                    sql += " group by ProcessName";
                    list = db.Query<ETaskCount>(sql, new { ProcessName = ProcessName }).ToList();
                }

                return list;
            }
        }
        public static void RepairSecurity(string TaskName, string SName, string SID, bool IsManager)
        {
            Task.Factory.StartNew(() =>
            {

                string stype = IsManager ? "【管理】" : "【查看】";

                using (var db = DB.BPMConnection)
                {
                    ShowDataToUI?.Invoke(string.Format("执行修补{0}权限数据 开始执行..", stype));

                    string sql = @"select TaskID,CreateAt from BPMInstTasks where ProcessName=@ProcessName
                              and TaskID not in (select s.TaskID from BPMSecurityTACL s
                              left join BPMInstTasks t on s.TaskID = t.TaskID where s.SID = @SID and t.ProcessName = @ProcessName and s.AllowAdmin=@AllowAdmin)";

                    var list = db.Query<ETasks>(sql, new { ProcessName = TaskName, SID = SID, AllowAdmin = IsManager ? 1 : 0 });

                    foreach (ETasks eTask in list)
                    {
                        if (CheckSecurity(eTask.TaskID, SID, IsManager) == false)
                        {
                            if (AddTaskSecurity(eTask.TaskID, SID, eTask.CreateAt, IsManager) > 0)
                            {
                                ShowDataToUI?.Invoke(string.Format("[{1}]添加{3}流程权限[{2}({0})] 成功.", eTask.TaskID, SName, TaskName, stype));
                            }
                            else
                            {
                                ShowDataToUI?.Invoke(string.Format("[{1}]添加{3}流程权限[{2}({0})] 失败.", eTask.TaskID, SName, TaskName, stype));
                            }
                        }
                        else
                        {
                            ShowDataToUI?.Invoke(string.Format("[{1}]流程{3}权限[{2}({0})]已存在,跳过..", eTask.TaskID, SName, TaskName, stype));
                        }
                    }

                    //清除重复数据
                    sql = @"delete from BPMSecurityTACL where ID in(
                                  select max(ID) from BPMSecurityTACL s 
                                  left join BPMInstTasks t on t.TaskID = s.TaskID where s.SID = @SID
                                  and t.ProcessName=@ProcessName group by s.TaskID having count(1)>1);";

                    int re = db.Execute(sql, new { ProcessName = TaskName, SID = SID });
                    if (re > 0)
                    {
                        ShowDataToUI?.Invoke(string.Format("[{0}]修补{1}权限数据 删除重复数据{2}条.", SName, stype, re));
                    }

                    ShowDataToUI?.Invoke(string.Format("[{0}]修补{1}权限数据 执行完毕.", SName, stype));
                    RefreshUI?.Invoke(SID);
                }
            });
        }

        public static void RepairSecurity2(string TaskName, string SName, string SID, bool IsManager)
        {
            Task.Factory.StartNew(() =>
            {
                string sql = @"select count(1) from BPMInstTasks where ProcessName=@ProcessName
                                      and TaskID not in (select s.TaskID from BPMSecurityTACL s
                                      left join BPMInstTasks t on s.TaskID = t.TaskID where s.SID = @SID and t.ProcessName=@ProcessName and s.AllowAdmin=@AllowAdmin)";

                string stype = IsManager ? "【管理】" : "【查看】";

                using (var db = DB.BPMConnection)
                {
                    int RepairCount = db.ExecuteScalar<int>(sql, new { ProcessName = TaskName, SID = SID, AllowAdmin = IsManager ? 1 : 0 });
                    if (RepairCount > 0)
                    {
                        ShowDataToUI?.Invoke(string.Format("角色[{0}]发现{1}条遗漏{2}权限 .", SName, RepairCount, stype));
                        ShowDataToUI?.Invoke(string.Format("执行修补{0}权限数据 开始执行..", stype));

                        sql = @"select TaskID,CreateAt from BPMInstTasks where ProcessName=@ProcessName
                              and TaskID not in (select s.TaskID from BPMSecurityTACL s
                              left join BPMInstTasks t on s.TaskID = t.TaskID where s.SID = @SID and t.ProcessName = @ProcessName and s.AllowAdmin=@AllowAdmin)";

                        var list = db.Query<ETasks>(sql, new { ProcessName = TaskName, SID = SID, AllowAdmin = IsManager ? 1 : 0 });

                        foreach (ETasks eTask in list)
                        {
                            if (AddTaskSecurity(eTask.TaskID, SID, eTask.CreateAt, IsManager) > 0)
                            {
                                ShowDataToUI?.Invoke(string.Format("[{1}]添加{3}流程权限[{2}({0})] 成功.", eTask.TaskID, SName, TaskName, stype));
                            }
                            else
                            {
                                ShowDataToUI?.Invoke(string.Format("[{1}]添加{3}流程权限[{2}({0})] 失败.", eTask.TaskID, SName, TaskName, stype));
                            }
                        }

                        ShowDataToUI?.Invoke(string.Format("[{0}]修补{1}权限数据 执行完毕.", SName, stype));
                    }
                    else
                    {
                        ShowDataToUI?.Invoke(string.Format("角色[{0}]未发现遗漏{1}权限 .", SName, stype));
                    }
                }
            });
        }

        public static void ClearSecurity(string TaskName, string SName, string SID, bool IsManager)
        {
            Task.Factory.StartNew(() =>
            {
                string sql = "select TaskID from BPMInstTasks where ProcessName=@ProcessName";
                string stype = IsManager ? "【管理】" : "【查看】";
                using (var db = DB.BPMConnection)
                {
                    var list = db.Query<int>(sql, new { ProcessName = TaskName });
                    foreach (int TaskID in list)
                    {
                        if (CheckSecurity(TaskID, SID, IsManager))
                        {
                            if (RemoveTaskSecurity(TaskID, SID, IsManager) > 0)
                            {
                                ShowDataToUI?.Invoke(string.Format("[{1}]删除流程{3}权限[{2}({0})] 成功.", TaskID, SName, TaskName, stype));
                            }
                            else
                            {
                                ShowDataToUI?.Invoke(string.Format("[{1}]删除流程{3}权限[{2}({0})] 失败.", TaskID, SName, TaskName, stype));
                            }
                        }
                        else
                        {
                            ShowDataToUI?.Invoke(string.Format("[{1}]流程{3}权限[{2}({0})]不存在,跳过..", TaskID, SName, TaskName, stype));
                        }
                    }

                    ShowDataToUI?.Invoke(string.Format("清除{2}权限...[{0}->{1}] 执行完毕.", TaskName, SName, stype));
                    RefreshUI?.Invoke(SID);
                }
            });
        }

        public static void InsertSecurity(string TaskName, string SName, string SID, bool IsManager)
        {
            Task.Factory.StartNew(() =>
            {
                string sql = "select TaskID,CreateAt from BPMInstTasks where ProcessName=@ProcessName";
                using (var db = DB.BPMConnection)
                {
                    string stype = IsManager ? "【管理】" : "【查看】";
                    var list = db.Query<ETasks>(sql, new { ProcessName = TaskName });
                    foreach (ETasks eTask in list)
                    {
                        if (CheckSecurity(eTask.TaskID, SID, IsManager) == false)
                        {
                            if (AddTaskSecurity(eTask.TaskID, SID, eTask.CreateAt, IsManager) > 0)
                            {
                                ShowDataToUI?.Invoke(string.Format("[{1}]添加流程{3}权限[{2}({0})] 成功.", eTask.TaskID, SName, TaskName, stype));
                            }
                            else
                            {
                                ShowDataToUI?.Invoke(string.Format("[{1}]添加流程{3}权限[{2}({0})] 失败.", eTask.TaskID, SName, TaskName, stype));
                            }
                        }
                        else
                        {
                            ShowDataToUI?.Invoke(string.Format("[{1}]流程{3}权限[{2}({0})]已存在,跳过..", eTask.TaskID, SName, TaskName, stype));
                        }
                    }

                    ShowDataToUI?.Invoke(string.Format("刷入{3}权限...[{0}->{1}] 执行完毕.", TaskName, SName, SID, stype));
                    RefreshUI?.Invoke(SID);
                }
            });
        }

        private static bool CheckSecurity(int TaskID, string SID, bool IsManager)
        {
            string sql = "select count(1) from BPMSecurityTACL where TaskID=@TaskID and SID=@SID and AllowAdmin=@AllowAdmin";
            using (var db = DB.BPMConnection)
            {
                return db.ExecuteScalar<int>(sql, new { TaskID = TaskID, SID = SID, AllowAdmin = IsManager ? 1 : 0 }) > 0;
            }
        }

        private static bool CheckSecurity2(int TaskID, string SID)
        {
            string sql = "select count(1) from BPMSecurityTACL where TaskID=@TaskID and SID=@SID";
            using (var db = DB.BPMConnection)
            {
                return db.ExecuteScalar<int>(sql, new { TaskID = TaskID, SID = SID }) > 0;
            }
        }

        private static int AddTaskSecurity(int TaskID, string SID, DateTime CreateAt, bool IsManager)
        {
            string sql = "Insert Into BPMSecurityTACL(TaskID,SID,AllowRead,AllowAdmin,ShareByUser,CreateDate,CreateBy) values(@TaskID,@SID,1,@AllowAdmin,0,@CreateAt,'sa');";
            using (var db = DB.BPMConnection)
            {
                return db.Execute(sql, new { TaskID = TaskID, SID = SID, CreateAt = CreateAt, AllowAdmin = IsManager ? 1 : 0 });
            }
        }

        private static int RemoveTaskSecurity(int TaskID, string SID, bool IsManager)
        {
            string sql = "delete from BPMSecurityTACL where TaskID=@TaskID and SID=@SID and AllowAdmin=@AllowAdmin;";
            using (var db = DB.BPMConnection)
            {
                return db.Execute(sql, new { TaskID = TaskID, SID = SID, AllowAdmin = IsManager ? 1 : 0 });
            }
        }
        private static int RemoveTaskSecurity2(int TaskID, string SID)
        {
            string sql = "delete from BPMSecurityTACL where TaskID=@TaskID and SID=@SID;";
            using (var db = DB.BPMConnection)
            {
                return db.Execute(sql, new { TaskID = TaskID, SID = SID });
            }
        }
    }
}
