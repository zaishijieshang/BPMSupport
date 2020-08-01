using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskQuick.Entity
{
    public class EGroup
    {
        public string GroupName { get; set; }
        public string SID { get; set; }
    }

    public class ETaskCount
    {
        public string ProcessName { get; set; }
        public int Count { get; set; }
        public int AllowAdmin { get; set; }
    }

    public class ETasks
    {
        public int TaskID { get; set; }
        public DateTime CreateAt { get; set; }

    }
}
