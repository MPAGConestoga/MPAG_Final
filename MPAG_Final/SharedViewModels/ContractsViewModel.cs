using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MPAG_Final.Services;
using MPAG_Final.SharedModels;
using MPAG_Final.Utilities;
using System.Windows;
using System.Data.Linq;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading;
using System.Windows.Threading;
using MPAG_OrderAndTrip;

namespace MPAG_Final.SharedViewModels
{
    public class ContractsViewModel : ObservableObject
    {

        public bool isRunning { get; set; }
        private string _loadingText;
        public string loadingText
        {
            get
            {
                return _loadingText;
            }
            set
            {
                _loadingText = value;
                OnPropertyChanged("loadingText");
            }
        }
        private int _loading;
        public int loading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                OnPropertyChanged("loading");
            }
        }

        //property for the management of selected contracts in the views that utilize lists
        //buyer page binds to them via the BuyerLandingView
        private Contract _selectedContract;
        public Contract SelectedContract
        {
            get { return _selectedContract; }
            set { OnPropertyChanged(ref _selectedContract, value); }
        }

        // Contracts are stored here in a WPF friendly list
        public ObservableCollection<Contract> Contracts { get; private set; }
        public ObservableCollection<CityDepot> Origin { get; private set; }
        public ObservableCollection<CityDepot> Destination { get; private set; }
        public ICommand UpdateCommand { get; private set; }

        //mock data service for testing UI

        //private IContractDataService _contractDataService;

        //ContractsViewModel contructor
        public ContractsViewModel(/*IContractDataService contractDataService*/)
        {
            Contracts = new ObservableCollection<Contract>();
            //_contractDataService = contractDataService;
            UpdateCommand = new RelayCommand(Update);
            isRunning = false;
            loading = 0;
            loadingText = "";
            Origin = new ObservableCollection<CityDepot>();
            Destination = new ObservableCollection<CityDepot>();
            var cityDepots = new TMSDAL().GetCityDepots();
            foreach (CityDepot el in cityDepots)
            {
                Origin.Add(el);
                Destination.Add(el);
            }
        }

        private void Update()
        {
            //add this later
        }

        //command for the loading of contracts
        public void LoadContracts(IList<Contract> contracts)
        {
            // Contracts = new ObservableCollection<Contract>(contracts);
            OnPropertyChanged("Contracts");
        }
        public void PauseDatabase()
        {
            isRunning = false;
            loadingText = "";
        }

        public void EmptyList()
        {
            Contracts.Clear();
        }
        public void DatabaseRun()
        {
            if (!isRunning)
            {
                isRunning = true;
                loadingText = "Loading More Contracts...";
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    string connectionString = ConfigurationManager.ConnectionStrings["contractmarketplace"].ConnectionString;
                    List<Contract> newList = new List<Contract>();
                    //for (int i = 0; i < 3; i++)
                    while (isRunning)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            using (MySqlConnection connection = new MySqlConnection(connectionString))
                            {
                                try
                                {
                                    connection.Open();
                                    string customerQuery = "SELECT * FROM cmp.Contract";
                                    MySqlCommand command = new MySqlCommand(customerQuery, connection);
                                    MySqlDataReader dataReader = command.ExecuteReader();


                                    while (dataReader.Read())
                                    {
                                        string city = dataReader["Origin"] as string;

                                        Contracts.Add(new Contract()
                                        {
                                            Customer = (dataReader["Client_Name"] as string),
                                            JobType = (JobType)((int)dataReader["Job_Type"]),
                                            Origin = dataReader["Origin"] as string,
                                            Destination = dataReader["Destination"] as string,
                                            VanType = (VanType)(dataReader["Van_Type"]),
                                            Quantity = (int)(dataReader["Quantity"])
                                        });

                                    }
                                    //_contracts = newList;
                                    connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                            }
                        }));
                        loading = 0;
                        while (loading < 100)
                        {
                            if (!isRunning)
                            {
                                loading = 0;
                                break;
                            }
                            loading += 2;
                            Thread.Sleep(200);
                        }
                    }
                }).Start();
            }
        }

    }


}
