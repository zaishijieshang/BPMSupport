using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Job.Entity
{
    public class PurviewExpire
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public int IsSendEmail { get; set; }
        public DateTime? AppDate { get; set; }
        public string TaskSN { get; set; }
        public string AppEmpID { get; set; }
        public string AppEmpName { get; set; }
        public string AppMark { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string RoleType { get; set; }
    }


}
