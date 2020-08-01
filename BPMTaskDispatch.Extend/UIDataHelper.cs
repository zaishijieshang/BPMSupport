using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Extend
{
    public class UIDataHelper
    {
        public static void ExeResult(string TaskName, string ExeResult)
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                UIData.ExeResult?.Invoke(new Entity.EExeResult() { ExeTime = DateTime.Now, TaskName = TaskName, ExeResult = ExeResult });
                Log.WriteLine(ExeResult);
                UIData.ExeTotal?.Invoke();
            }
        }

        public static void ExceptionResult(string TaskName, string ExpResult)
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                UIData.ExceptionResult?.Invoke(new Entity.EExceptionResult() { ExceptionTime = DateTime.Now, ExceptionTaskName = TaskName, ExceptionResult = ExpResult });
                Log.WriteLine(ExpResult);
                UIData.ExpTotal?.Invoke();
            }
        }

        public static void ExceptionResult(string TaskName, Exception ex)
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                string expMsg = string.Format("【{0} 异常】Message：{1}，StackTrace：{2}", TaskName, ex.Message, ex.StackTrace);
                UIData.ExceptionResult?.Invoke(new Entity.EExceptionResult()
                {
                    ExceptionTime = DateTime.Now,
                    ExceptionTaskName = TaskName,
                    ExceptionResult = expMsg
                });
                Log.WriteLine(expMsg);
                UIData.ExpTotal?.Invoke();
            }
        }
    }
}
