using BPMTaskDispatch.Win.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Win.Domain.Ext
{
    public static class Ext
    {
        public static List<ETask> ToTask(this List<EJober> jobers)
        {
            List<ETask> list = new List<ETask>();
            if (jobers != null && jobers.Count > 0)
            {
                int index = 1;
                foreach (EJober item in jobers)
                {
                    list.Add(new ETask { Index = index, TaskName = item.name, Cron = item.cron, Type = item.type, TaskDesc = item.desc, Status = (int)TaskState.Stop }); ;
                    index++;
                }
            }
            return list;
        }
    }
}
