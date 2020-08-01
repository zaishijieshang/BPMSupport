using BPMTaskDispatch.Extend;
using BPMTaskDispatch.Win.Domain.Entity;
using BPMTaskDispatch.Win.Domain.Ext;
using BPMTaskDispatch.Win.Domain.Jober;
using BPMTaskDispatch.Win.Domain.Util;
using Quartz;
using Quartz.Impl;
using Sn.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Win.Domain
{
    public class TaskDispatch
    {
        private ISchedulerFactory SchedulerFactory = new StdSchedulerFactory();
        public IScheduler Scheduler = null;

        private static TaskDispatch _TaskDispatch = null;
        public static TaskDispatch Instance
        {
            get
            {
                if (_TaskDispatch == null)
                {
                    _TaskDispatch = new TaskDispatch();
                }

                return _TaskDispatch;
            }
        }

        public static Action<List<ETask>> UITasks = null;
        

        public void LoadJob()
        {
            Scheduler = SchedulerFactory.GetScheduler();

            var jobs = AppConfigUtil.GetJobers();

            UITasks?.Invoke(jobs.ToTask());
        }

        public void CreateJobByType(Type jobType, JobKey jobKey, int seconds)
        {
            if (seconds > 0)
            {
                IJobDetail job = JobBuilder.Create(jobType).WithIdentity(jobKey).Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .StartNow()
                    .WithIdentity(jobKey.Name, jobKey.Group)
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(seconds).RepeatForever())
                    .Build();

                Scheduler.ScheduleJob(job, trigger);
            }
        }

        public void CreateJobByType(Type jobType, JobKey jobKey, string Cron)
        {
            if (!string.IsNullOrEmpty(Cron))
            {
                IJobDetail job = JobBuilder.Create(jobType).WithIdentity(jobKey).Build();

                ITrigger trigger = TriggerBuilder.Create()
                   .StartNow()
                   .WithIdentity(jobKey.Name, jobKey.Group)
                   .WithCronSchedule(Cron)
                   .Build();

                Scheduler.ScheduleJob(job, trigger);
            }
        }

        public string TaskStatus(TaskState taskState)
        {
            string re = string.Empty;
            if (taskState == TaskState.Running)
            { re = "运行中"; }
            else if (taskState == TaskState.Stop)
            { re = "停止"; }
            return re;
        }

        public void Start(JobKey jobKey, int seconds)
        {
            try
            {
                if (!Scheduler.CheckExists(jobKey))//不存在
                {
                    CreateJobByType(BuilderJob.CreateJobByName(jobKey.Name).GetType(), jobKey, seconds);
                }
                else
                {
                    Scheduler.ResumeJob(jobKey);//存在则恢复
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(string.Format("【{0}】服务启动异常：{1}", jobKey.Name, ex.Message), ex);
                throw ex;
            }
        }

        public void Start(JobKey jobKey, string cron)
        {
            try
            {
                if (!Scheduler.CheckExists(jobKey))//不存在
                {
                    CreateJobByType(BuilderJob.CreateJobByName(jobKey.Name).GetType(), jobKey, cron);
                }
                else
                {
                    Scheduler.ResumeJob(jobKey);//存在则恢复
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(string.Format("【{0}】服务启动异常：{1}", jobKey.Name, ex.Message), ex);
                throw ex;
            }
        }


        public void Start(ETask eTask)
        {
            JobKey jobKey = null;
            //Start(jobKey, eTask.Cron);
            try
            {
                jobKey = JobKey.Create(eTask.TaskName, "Default");
                if (!Scheduler.CheckExists(jobKey))//不存在
                {
                    CreateJobByType(BuilderJob.CreateJobByType(eTask.Type).GetType(), jobKey, eTask.Cron);
                }
                else
                {
                    Scheduler.ResumeJob(jobKey);//存在则恢复
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(string.Format("【{0}】服务启动异常：{1}", jobKey.Name, ex.Message), ex);
                throw ex;
            }
        }

        public void ExeOne(ETask eTask)
        {
            try
            {
                IJob job = BuilderJob.CreateJobByType(eTask.Type);
                job.Execute(null);
            }
            catch (Exception ex)
            {
                UIDataHelper.ExceptionResult(eTask.TaskName, ex);
            }
        }


        public void Stop(ETask eTask)
        {
            JobKey jobKey = null;
            try
            {
                jobKey = JobKey.Create(eTask.TaskName, "Default");
                if (Scheduler.CheckExists(jobKey))
                {
                    //_Scheduler.Shutdown();
                    //_Scheduler.PauseJob(jobKey);
                    Scheduler.DeleteJob(jobKey);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(string.Format("【{0}】停止异常：{1}", jobKey.Name, ex.Message), ex);
                throw ex;
            }
        }

    }
}
