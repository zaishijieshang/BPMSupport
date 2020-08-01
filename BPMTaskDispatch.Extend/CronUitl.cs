using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Extend
{
    public class CronUitl
    {

        public static DateTime CronToDateTime(string cron)
        {
            CronExpression expression = new CronExpression(cron);
            DateTimeOffset? newDate = expression.GetNextInvalidTimeAfter(DateTime.Now);
            if (newDate != null)
            {
                return newDate.Value.DateTime;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 时间格式转换成Quartz任务调度器Cron表达式
        /// </summary>
        /// <param name="time">时间值,支持HH:mm:ss | HH:mm</param>
        /// <returns></returns>
        public static string DateTimeToCron(string time)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(time)) return "";
                string error = "传入的时间值[" + time + "]格式有误!";
                int ss = 0, mi = 0, hh = 0;
                if (time.Length < 5) throw new Exception(error);
                if (time.Substring(2, 1) != ":") throw new Exception(error);

                if (!int.TryParse(time.Substring(0, 2), out hh))
                    throw new Exception(error);
                if (!int.TryParse(time.Substring(3, 2), out mi))
                    throw new Exception(error);
                if (time.Length > 5)
                {
                    if (time.Substring(5, 1) != ":") throw new Exception(error);
                    if (!int.TryParse(time.Substring(6), out ss))
                        throw new Exception(error);
                }
                if (ss > 59) throw new Exception(error);
                if (mi > 59) throw new Exception(error);
                if (hh > 23) throw new Exception(error);
                string cronValue = ss + " " + mi + " " + hh + " " + "* * ?";
                return cronValue;
            }
            catch (Exception ea)
            {
                throw ea;
            }
        }
    }
}
