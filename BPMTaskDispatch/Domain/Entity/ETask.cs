using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Win.Domain.Entity
{
    public class ETask
    {
        public int Index { get; set; }
        public string TaskName { get; set; }
        public string TaskDesc { get; set; }
        public string Cron { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
    }
}
