using BPMTaskDispatch.Extend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Extend
{
    public class UIData
    {
        public static Action<EExeResult> ExeResult = null;
        public static Action<EExceptionResult> ExceptionResult = null;
        public static Action ExeTotal = null;
        public static Action ExpTotal = null;

    }
}
