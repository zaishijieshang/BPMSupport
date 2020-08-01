using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskQuick.Entity
{
    public class ETask
    {
        public int TaskID { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string Queryfield { get; set; }
        public int OwnerPositionID { get; set; }
    }
}
