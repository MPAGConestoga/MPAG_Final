using MPAG_Final.Logging;
using MPAG_Final.SharedModels;
using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MPAG_Final.Admin.ViewModels
{
    /**
    * \Class AdminLandingViewModel
    * \Brief View model for the handling of Administrative model
    * \Details The Admin view model loads and handles the information that will be 
    * displayed to the user for logging information and changing the database IP and root
    */
    public class AdminLandingViewModel : ObservableObject
    {
        /// <summary>
        ///      Retrieves the date of today from the calendar view.
        ///      If the calendar has been changed the date property is changed.
        /// </summary>
        private Nullable<DateTime> myDateTimeProperty;
        public Nullable<DateTime> MyDateTimeProperty
        {
            get
            {
                if (myDateTimeProperty == null)
                {
                    myDateTimeProperty = DateTime.Today;
                    DateTime dateBuffer = (DateTime)myDateTimeProperty;
                    SelectedDate = dateBuffer.ToString("yyyy-MM-dd");
                }
                return myDateTimeProperty;
            }
            set
            {
                myDateTimeProperty = value;
                DateTime dateBuffer = (DateTime)myDateTimeProperty;
                SelectedDate = dateBuffer.ToString("yyyy-MM-dd");
                OnPropertyChanged("MyDateTimeProperty");
            }
        }
        public ObservableCollection<Carrier> AllCarriers { get; set; }
        public ObservableCollection<string> Logs { get; set; }


        public ICommand UpdateCarrierCommand { get; private set; }
        public ICommand UpdateCMPCommand { get; private set; }
        public ICommand BackupCommand { get; private set; }
        public ICommand ViewLogCommand { get; private set; }
        public ICommand UpdateIPCommand { get; private set; }

        private string _logFile;
        public string LogFile
        {
            get { return _logFile; }
            set
            {
                _logFile = value;
                OnPropertyChanged("LogFile");
            }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged("Error");
            }
        }
        /// <summary>
        ///     The selected dat is converted into a string to be readied 
        ///     and prepared to find the file that is required to be accessed
        /// </summary>
        private String selectedDate;
        public String SelectedDate
        {
            get { return selectedDate; }
            set { OnPropertyChanged(ref selectedDate, value); }
        }
        public AdminLandingViewModel()
        {
            UpdateIPCommand = new UpdateIP(this);
            UpdateCarrierCommand = new RelayCommand(UpdateCarrierRates);
            UpdateCMPCommand = new UpdateIP(this);
            ViewLogCommand = new ViewLog(this);
            AllCarriers = new ObservableCollection<Carrier>(new TMSDAL().GetAllCarriers());
            Logs = new ObservableCollection<string>();
            GetLogs("Database\\");
            BackupCommand = new RelayCommand(BackupDatabase);
        }
        public void BackupDatabase()
        {
            new TMSDAL().Backup();
        }

        public void updateIP(object o)
        {
            var list = (object[])o;

            string IP = (list[0].ToString() + "." + list[1].ToString() + "." + list[2].ToString() + "." + list[3].ToString());
            new TMSDAL().UpdateConnectionString(IP, Convert.ToInt32(list[4]));

    
        }
        public void UpdateCarrierRates()
        {
            foreach (Carrier el in AllCarriers)
            {
                new TMSDAL().UpdateCarrierRates(el);
            }
            AllCarriers.Clear();
            var list = new TMSDAL().GetAllCarriers();
            foreach(Carrier el in list)
            {
                AllCarriers.Add(el);
            }
        }

        public void UpdateCMPLogin(object o)
        {
            string ip = (string)o;
            new TMSDAL().UpdateConnectionString(ip, 0); //0 for CMP
        }

        public void GetLogs(string date)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(date);
            FileInfo[] info = dirInfo.GetFiles(("*.*"), SearchOption.AllDirectories);
            //Process.Start((dirInfo.FullName) + (info[0]));
            var list = dirInfo.GetFiles();

            foreach (FileInfo el in list)
            {
                Logs.Add(el.FullName);
               
            }
        }

        public void OpenLog(object o)
        {
            Error = "";
            try
            {
                string path = (string)o;
                LogFile = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
                Error = "Please select a file";
            }
        }

    }
}
