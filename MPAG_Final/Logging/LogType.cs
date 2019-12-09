using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Logging
{
    public class LogType
    {
        public LoggingType loggingType;

        public enum LoggingType
        {
            buyer,
            planner,
            admin,
            database
        }

       public static void ErrorType(LoggingType type, string message)
       {
            message = DateTime.Now + " [Exception]: " + message;

            if (type == LoggingType.buyer)
            {
                string pathBuyer = "Buyer";
                string filepathBuyer = "Buyer\\BServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

                FilePath(pathBuyer);
                RunFile(filepathBuyer, message);
            }
            else if (type == LoggingType.planner)
            {
                string pathPlanner = "Planner";
                string filepathPlanner = "Planner\\PServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

                FilePath(pathPlanner);
                RunFile(filepathPlanner, message);
            }
            else if (type == LoggingType.admin)
            {
                string pathAdmin = "Admin";
                string filepathAdmin = "Admin\\AServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

                FilePath(pathAdmin);
                RunFile(filepathAdmin, message);
            }
            else if (type == LoggingType.database)
            {
                string pathDatabase = "Database";
                string filepathDatabase = "Database\\DServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

                FilePath(pathDatabase);
                RunFile(filepathDatabase, message);
            }
        }

        public static void FilePath(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void RunFile(string filepath, string message)
        {
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(message);
                    sw.Close();
                }               
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                    sw.Close();
                }
            }
        }

    }
}
