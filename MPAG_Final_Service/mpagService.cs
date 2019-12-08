using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final_Service
{
    public partial class mpagService : ServiceBase
    {
        public mpagService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            //Temporary test
            WriteToFile("Service is started at " + DateTime.Now);
            //Bind to the textbox in Admin page
            //Or create textfile to be made and then the admin page pulls
            //the text file information
        }

        public void WriteToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
        }
        protected override void OnStop()
        {
            try
            {
                File.Create(AppDomain.CurrentDomain.BaseDirectory + "OnStop.txt");
            }
            catch(IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
