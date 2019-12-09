using MPAG_Final.Logging;
using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MPAG_Final.Services
{
    public class MockContractMarketplace : IContractDataService
    {
        public ObservableCollection<Contract> _contracts;

        /// <summary>
        ///     Database run to retireve from the contract market place for the contracts
        /// </summary>
        public void DatabaseRun()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["contractmarketplace"].ConnectionString;
            List<Contract> newList = new List<Contract>();
            for (int i = 0; i < 3; i++)
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

                            _contracts.Add(new Contract()
                            {
                                Customer = (dataReader["Client_Name"] as string),
                                JobType = (JobType)((int)dataReader["Job_Type"]),
                                Origin = dataReader["Origin"] as string,
                                Destination = dataReader["Destination"] as string,
                                VanType = (VanType)(dataReader["Van_Type"]),
                                Quantity = (int)(dataReader["Quantity"])
                            });
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        LogType.ErrorType(LogType.LoggingType.buyer, ex.ToString());
                    }
                }
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        ///     Instantiates the start of the retrieval for the contract marketplace
        /// </summary>
        public MockContractMarketplace()
        {
            _contracts = new ObservableCollection<Contract>();
            Thread marketThread = new Thread(new ThreadStart(DatabaseRun));
            marketThread.Start();
        }

        /// <summary>
        ///     Get the contracts that were saved into the collections
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Contract> GetContracts()
        {
            return _contracts;
        }

        /// <summary>
        ///     Save the contracts into a private collections
        /// </summary>
        /// <param name="contracts"><b>ObservableCollection<Contract></b> - Save the contracts into a collection</param>
        public void Save(ObservableCollection<Contract> contracts)
        {
            _contracts = contracts;
        }

    }
}
