using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Extend.Entity
{
    public class EExceptionResult
    {
        public DateTime ExceptionTime { get; set; }
        public string ExceptionTaskName { get; set; }
        public string ExceptionResult { get; set; }
    }
}
