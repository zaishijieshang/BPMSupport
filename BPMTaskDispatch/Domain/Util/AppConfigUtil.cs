using BPMTaskDispatch.Win.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using BPMTaskDispatch.Win.Domain.Jober;
namespace BPMTaskDispatch.Win.Domain.Util
{
    public class AppConfigUtil
    {
        public static List<EJober> GetJobers()
        {
            List<EJober> list = new List<EJober>();

            try
            {
                Jobs jobs = (Jobs)ConfigurationManager.GetSection("Jobs");

                foreach (NameTypeSetting item in jobs.KeyValues)
                {
                    list.Add(new EJober() { name = item.Name, type = item.Type, cron = item.Cron, desc = item.Desc });
                }

            }
            catch (Exception ex)
            {
                Log.WriteException("AppConfigUtil.GetEJobers :", ex);
            }

            return list;
        }

    }
}
