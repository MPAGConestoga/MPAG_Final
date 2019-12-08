using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MPAG_Final_Service
{
    static class Program
    {
        //Things needed for selecting directories for log files??
        //Make separate directory for differet log types

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            mpagService service1 = new mpagService();
            service1.OnDebug();
            Thread.Sleep(Timeout.Infinite);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new mpagService()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
