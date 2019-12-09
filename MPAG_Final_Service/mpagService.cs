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
            string pathBuyer = AppDomain.CurrentDomain.BaseDirectory + "\\Buyer";
            string pathAdmin = AppDomain.CurrentDomain.BaseDirectory + "\\Admin";
            string pathPlanner = AppDomain.CurrentDomain.BaseDirectory + "\\Planner";

            if (!Directory.Exists(pathBuyer))
            {
                Directory.CreateDirectory(pathBuyer);
            }
            if (!Directory.Exists(pathAdmin))
            {
                Directory.CreateDirectory(pathAdmin);
            }
            if (!Directory.Exists(pathPlanner))
            {
                Directory.CreateDirectory(pathPlanner);
            }

            string filepathBuyer = AppDomain.CurrentDomain.BaseDirectory + "\\Buyer\\BServiceLog_.txt";
            string filepathPlanner = AppDomain.CurrentDomain.BaseDirectory + "\\Admin\\AServiceLog_.txt";
            string filepathAdmin = AppDomain.CurrentDomain.BaseDirectory + "\\Planner\\PServiceLog_.txt";

            RunFile(filepathBuyer, message);
            RunFile(filepathPlanner, message);
            RunFile(filepathAdmin, message);
        }

        private void RunFile(string filepath, string message)
        {
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
