using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        public IList<Contract> _contracts;
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

                            newList.Add(new Contract()
                            {
                                Customer = (dataReader["Client_Name"] as string),
                                JobType = (JobType)((int)dataReader["Job_Type"]),
                                Origin = dataReader["Origin"] as string,
                                Destination = dataReader["Destination"] as string,
                                VanType = (VanType)(dataReader["Van_Type"]),
                                Quantity = (int)(dataReader["Quantity"])
                            });
                        }
                        _contracts = newList;
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                Thread.Sleep(5000);
            }
        }

        public MockContractMarketplace()
        {
            Thread marketThread = new Thread(new ThreadStart(DatabaseRun));
            marketThread.Start();
            DatabaseRun();
        }

        public IList<Contract> GetContracts()
        {
            return _contracts;
        }

        public void Save(IList<Contract> contracts)
        {
            _contracts = contracts;
        }

    }
}
