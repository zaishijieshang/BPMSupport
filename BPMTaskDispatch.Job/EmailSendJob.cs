using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMTaskDispatch.Job
{
    public class EmailSendJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {

            Task.Factory.StartNew(() => { 
            

            
            });


        }
    }
}
