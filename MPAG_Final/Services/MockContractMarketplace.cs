using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MPAG_Final.Services
{
    public class MockContractMarketplace : IContractDataService
    {
        private IEnumerable<Contract> _contracts;

        public MockContractMarketplace()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["contractmarketplace"].ConnectionString;

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

                        //string bell = "Bellville";
                        //City City = City.bell;
                        //Insert into contracts
                        _contracts = new List<Contract>()
                        {
                            new Contract
                            {
                                Customer = (dataReader["Client_Name"] as string),
                                JobType = (JobType)((int)dataReader["Job_Type"]),
                                //Origin = City.(dataReader["Origin"] as string),
                                //Destination = (City)((int)dataReader["Destination"]),
                                VanType = (VanType)(dataReader["Van_Type"]),
                                //Quantity = (int)(dataReader["Quantity"])
                            }
                        };
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            //_contracts = new List<Contract>()
            //{
            //    new Contract
            //    {
            //        OrderID = 00,
            //        Customer = "Jev's Robotics",
            //        JobType = JobType.LTL,
            //        Origin = City.Hamilton,
            //        Destination = City.London,
            //        VanType = VanType.Dry
            //    },
            //    new Contract
            //    {
            //        OrderID = 00,
            //        Customer = "Tim's Ice Makers",
            //        JobType = JobType.FTL,
            //        Origin = City.Kingston,
            //        Destination = City.Oshawa,
            //        VanType = VanType.Reefer
            //    },
            //    new Contract
            //    {
            //        OrderID = 00,
            //        Customer = "Cats",
            //        JobType = JobType.FTL,
            //        Origin = City.Toronto,
            //        Destination = City.Ottawa,
            //        VanType = VanType.Dry,
            //        Quantity = 0
            //    }
            //};
        }

        public IEnumerable<Contract> GetContracts()
        {
            return _contracts;
        }

        public void Save(IEnumerable<Contract> contracts)
        {
            _contracts = contracts;
        }

    }
}
