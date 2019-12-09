using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MPAG_Final.SharedModels;
using MPAG_Final.Logging;


namespace MPAG_Final
{
    /** 
     * \brief The TMSDAL class is used as the data access layer with the TMS database
     * \details The TMSDAL contains the functionality for interaction with the locally-hosted database and the user, buyer and planner class.
     * Within the class are three connection strings that hold the different login credentials of the three users.
     * \see
     *
     */

    public class TMSDAL
    {
        public TMSDAL()
        {
            buyerConnectionString = ConfigurationManager.ConnectionStrings["TMSBuyer"].ConnectionString;
            plannerConnectionString = ConfigurationManager.ConnectionStrings["TMSPlanner"].ConnectionString;
            adminConnectionString = ConfigurationManager.ConnectionStrings["TMSAdmin"].ConnectionString;
        }
        private string buyerConnectionString;
        private string plannerConnectionString;
        private string adminConnectionString;




        public List<CityDepot> GetCityDepots()
        {
            const string sqlStatement = @"    SELECT 
	                                    c.City_Id, City from 
                                        city as c
                                        INNER JOIN delivery_city as d
                                        Where c.City_Id = d.City_Id;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var cityDepots = DataTableToCityDepotList(dataTable);

                return cityDepots;
            }
        }

        public Depot GetCityDepotByCarrierAndCity(int ID, int origin)
        {
            const string sqlStatement = @"    SELECT * FROM depot
                                                WHERE Carrier_Id = @ID
                                                AND Delivery_City_Id = @origin;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@ID", ID);
                myCommand.Parameters.AddWithValue("@origin", origin);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var depot = new Depot();

                foreach (DataRow row in dataTable.Rows)
                {
                    depot.DepotID = Convert.ToInt32(row["Depot_Id"]);
                    depot.avalibleLTL = Convert.ToInt32(row["LTL_Amount"]);
                    depot.availibleFTL = Convert.ToInt32(row["FTL_Amount"]);
                }
                return depot;
            }
        }

        /// \brief To convert a DataTable into a list of Orders
        /// 
        /// \details When multiple orders are returned from an sql query, this method is used to
        /// convert the returned DataTable into a list of orders.
        /// <param name="table"> - <b>DataTable</b> - The DataTable to be converted</param>
        /// \return A list of orders.
        /// \see TMSDAL:GetOrdersForPlanner
        private List<CityDepot> DataTableToCityDepotList(DataTable table)
        {
            var orders = new List<CityDepot>();

            foreach (DataRow row in table.Rows)
            {
                orders.Add(new CityDepot
                {
                    ID = Convert.ToInt32(row["City_Id"]),
                    cityLocation = row["City"].ToString()
                }); ;
            }
            return orders;
        }
        /// \brief To insert an order into the TMS local database
        /// 
        /// \details After the buyer selects an order from the marketplace, it is uploaded to the database. This method
        /// is called by the Order class to upload the order. Using the buyer login credentials, this method takes the Order
        /// object attributes and inserts the values int a mysql insert statement.
        /// <param name="order"> - <b>Order</b> - The order to be added to the database</param>
        /// \return none
        /// \see Order::AddOrder()
        public void InsertOrder(Order order)
        {
            try
            { 
            using (var myConn = new MySqlConnection(buyerConnectionString))
            {
                const string sqlStatement = @"  INSERT INTO _order (Start_Date, Origin, Destination, Job_Type, Van_Type, Order_Status)
	                                            VALUES (@StartDate, @Origin, @Destination, @Job_Type, @Van_Type, 0); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@StartDate", order.dateCreated);
                myCommand.Parameters.AddWithValue("@Origin", order.origin);
                myCommand.Parameters.AddWithValue("@Destination", order.destination);
                myCommand.Parameters.AddWithValue("@Job_Type", order.jobType);
                myCommand.Parameters.AddWithValue("@Van_Type", order.vanType);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
            }
            catch(Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
            }
        }

        /// \brief To insert an order into the TMS local database
        /// 
        /// \details After the buyer selects an order from the marketplace, it is uploaded to the database. This method
        /// is called by the Order class to upload the order. Using the buyer login credentials, this method takes the Order
        /// object attributes and inserts the values int a mysql insert statement.
        /// <param name="order"> - <b>Order</b> - The order to be added to the database</param>
        /// \return none
        /// \see Order::AddOrder()
        public void InsertContract(Contract contract)
        {
            try
            { 
            using (var myConn = new MySqlConnection(buyerConnectionString))
            {
                const string sqlStatement = @"  INSERT INTO _order (Customer_Id, Origin, Destination, Job_Type, Van_Type, Order_Status, Amount)
	                                            VALUES (@Customer, @Origin, @Destination, @Job_Type, @Van_Type, 0, @Amount); ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@Customer", contract.CustomerID);
                myCommand.Parameters.AddWithValue("@Origin", contract.OriginID);
                myCommand.Parameters.AddWithValue("@Destination", contract.DestinationID);
                myCommand.Parameters.AddWithValue("@Job_Type", contract.JobType);
                myCommand.Parameters.AddWithValue("@Van_Type", contract.VanType);
                myCommand.Parameters.AddWithValue("@Amount", contract.Quantity);


                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            }
            catch(Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
            }
        }

        /// \brief To insert a new carrier into the TMS local database
        /// 
        /// \details The admin is able to add a new carrier into the database
        /// <param name="carrier"> - <b>Carrier</b> - The carrier to be added to the database</param>
        /// \return none
        /// \see Order::AddOrder()
        public void InsertCarrier(Carrier carrier)
        {
            try
            { 
            using (var myConn = new MySqlConnection(buyerConnectionString))
            {
                const string sqlStatement = @"  INSERT INTO carrier (Carrier_Name, LTL_Rate, FTL_Rate, Reefer) VALUES
	                                                ('@CarrierName,', @LTL_Rate, @FTL_Rate, @Reefer),";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@CarrierName", carrier.carrierId);
                myCommand.Parameters.AddWithValue("@LTL_Rate", carrier.LTLRate);
                myCommand.Parameters.AddWithValue("@FTL_Rate", carrier.FTLRate);
                myCommand.Parameters.AddWithValue("@Reefer", carrier.ReeferCharge);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
            }
            catch(Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
            }
        }

        /// \brief To insert an address into the TMS local database
        /// 
        /// \details If the admin would like to add a new customer, employee, or carrier, an address may need to be 
        /// added to the database. This method is used for insertion into the address, city, and province tables.
        /// <param name="address"> - <b>Address</b> - The address to be added to the database</param>
        /// \return none
        /// \see Address
        public void addAddress(Address address)
        {
            try
            {
                using (var myConn = new MySqlConnection(adminConnectionString))
                {
                    const string sqlStatement = @"  INSERT INTO City(City)
                                                            (@city); ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@city", address.city);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
            }
        }

        /// \brief To set an order's status as current
        /// 
        /// \details After the planner has selected the carrier for an order, the order is confirmed. This method is used
        /// to set the order's status as current -> the order is ready to be shipped. 
        /// <param name="order"> - <b>Order</b> - The order to be confirmed in the database</param>
        /// \return none
        /// \see Order::confirmOrder()
        public void confirmOrder(Order order)
        {
            try
            {
                using (var myConn = new MySqlConnection(buyerConnectionString))
                {
                    const string sqlStatement = @"  UPDATE _order 
                                                SET 
	                                            Order_Status = 1
                                                WHERE
	                                            Order_Id = @Order_Id; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@Order_Id", order.OrderID);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());  
            }
        }

        /// \brief To set an order's status as To-Be-Reviewed
        /// 
        /// \details After the order has been fulfilled, the planner will mark the order as to-be-reviewed. The buyer can
        /// then take this order to generate an invoice for the customer. 
        /// <param name="order"> - <b>Order</b> - The order to be set as to-be-reviewed</param>
        /// \return none
        /// \see Order::orderToBeInvoiced()
        public void orderStatusTripFinished(Order order)
        {
            try
            {
                using (var myConn = new MySqlConnection(buyerConnectionString))
                {
                    const string sqlStatement = @"  UPDATE _order 
                                                SET 
	                                            Order_Status = 2
                                                End_Date = @dateCompleted
                                                WHERE
	                                            Order_Id = @Order_Id; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@End_Date", order.dateCompleted);
                    myCommand.Parameters.AddWithValue("@Order_Id", order.OrderID);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
            }
        }

        /// \brief To set an order's status as Finished
        /// 
        /// \details After the order invoice has been generated, the buyer will set its status as finished. There is no
        /// further action required for the order. 
        /// <param name="order"> - <b>Order</b> - The order to be set as finished</param>
        /// \return none
        /// \see Order::orderFinished()
        public void orderStatusFinished(Order order)
        {
            try
            {
                using (var myConn = new MySqlConnection(buyerConnectionString))
                {
                    const string sqlStatement = @"  UPDATE _order 
                                                SET 
	                                            Order_Status = 3
                                                WHERE
	                                            Order_Id = @Order_Id; ";

                    var myCommand = new MySqlCommand(sqlStatement, myConn);

                    myCommand.Parameters.AddWithValue("@Order_Id", order.OrderID);

                    myConn.Open();

                    myCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
            }
        }
        /// \brief To get a customer's id
        /// 
        /// \details This method takes in a person object with partially filled out information:
        /// The customer's first name, last name, and phone number. The returned information is 
        /// set in the person object passed to the method.
        /// <param name="person"> - <b>Person</b> - The customer to look up</param>
        /// \return none
        /// \see Person::getPersonInfo() 
        public int GetCustomerInformation(string customer)
        {
            int customerId = 0;

            const string sqlStatement = @"SELECT 
                                        p.Person_Id
	                                    FROM person AS p Where p.First_Name = @FirstName
                                        ;";
            using (var myConn = new MySqlConnection(buyerConnectionString))
            {
                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@FirstName", customer);

                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var table = new DataTable();

                myAdapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    customerId = Convert.ToInt32(row["Person_Id"]);
                }
            }
            return customerId;
        }
        /// \brief To get a customer's information
        /// 
        /// \details This method takes in a person object with partially filled out information:
        /// The customer's first name, last name, and phone number. The returned information is 
        /// set in the person object passed to the method.
        /// <param name="person"> - <b>Person</b> - The customer to look up</param>
        /// \return none
        /// \see Person::getPersonInfo() 
        public void GetCustomerInformation(Person person)
        {

            const string sqlStatement = @"SELECT 
                                        p.Person_Id, p.First_Name, p.Last_Name, p.Phone, p.Email, a.Street_Address, city.City, prov.Province, a.Postal_Code
	                                    FROM person AS p
                                        INNER JOIN customer as c
                                        INNER JOIN address as a
                                        INNER JOIN city
                                        INNER JOIN province as prov
                                        WHERE a.Address_Id = c.Address_Id
                                        AND a.Province_Id = prov.Province_Id
                                        AND a.City_Id = city.City_Id
                                        AND c.Customer_Id = (SELECT temporaryTable.Person_ID FROM
					                    (Select Person_Id FROM person
					                    WHERE First_Name = @FirstName
                                        AND Last_Name = @LastName
                                        AND Phone = @Phone) 
                                        AS temporaryTable);";

            try
            {
                using (var myConn = new MySqlConnection(buyerConnectionString))
                {
                    var myCommand = new MySqlCommand(sqlStatement, myConn);
                    myCommand.Parameters.AddWithValue("@FirstName", person.firstName);
                    myCommand.Parameters.AddWithValue("@LastName", person.lastName);
                    myCommand.Parameters.AddWithValue("@Phone", person.phoneNum);

                    var myAdapter = new MySqlDataAdapter
                    {
                        SelectCommand = myCommand
                    };

                    var table = new DataTable();

                    myAdapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        person.personID = Convert.ToInt32(row["Person_Id"]);
                        person.firstName = row["First_Name"].ToString();
                        person.lastName = row["Last_Name"].ToString();
                        person.phoneNum = row["Phone"].ToString();
                        person.email = row["Email"].ToString();
                        person.personAddress.streetAddress = (row["Street_Address"].ToString());
                        person.personAddress.city = (row["City"].ToString());
                        person.personAddress.province = (row["Province"].ToString());
                        person.personAddress.postalCode = (row["Postal_Code"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
            }
        }

        /// \brief To get all carriers in the database
        /// 
        /// \details This method returns a list containing the info of all carriers
        /// in the database
        /// 
        /// \return List of Carriers
        /// \see Carrier 
        public List<Carrier> GetAllCarriers()
        {
            const string sqlStatement = @"SELECT 
	                                    *
                                        FROM carrier;;";
            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var carriers = DataTableToCarrierList(dataTable);

                return carriers;
            }
        }

        /// \brief To get a carrier's information by its name
        /// 
        /// \details This method takes in a carrier object and gets all the carrier information
        /// by searching the databse by carrier name.
        /// <param name="carrier"> - <b>Carrier</b> - The customer to look up</param>
        /// \return List of Carriers
        /// \see Carrier 
        public List<Carrier> GetCarrierByName(Carrier carrier)
        {
            const string sqlStatement = @"SELECT 
	                                    c.Carrier_Id,
	                                    c.Carrier_Name,
                                        c.Phone,
                                        c.Email,
                                        c.LTL_Rate,
                                        c.FTL_Rate
                                        FROM carrier AS c
                                        INNER JOIN depot AS d 
	                                    WHERE d.Carrier_Id = (SELECT temporaryTable.Carrier_ID FROM
					                                    (Select Carrier_Id FROM carrier
					                                    WHERE Carrier_Name = @CarrierName) 
                                                        AS temporaryTable);";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@CarrierName", carrier.carrierName);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var carriers = DataTableToCarrierList(dataTable);

                return carriers;
            }
        }

        /// \brief To insert a new customer
        /// 
        /// \details This method takes in a carrier object and gets all the carrier information
        /// by searching the databse by carrier name.
        /// <param name="carrier"> - <b>Carrier</b> - The customer to look up</param>
        /// \return List of Carriers
        /// \see Carrier 
        public int AddCustomer(string companyName)
        {
            const string sqlStatement = @"Insert into person(First_Name)
                                            VALUES (@name);";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@name", companyName);

                //For offline connection we will use  MySqlDataAdapter class.  
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }

            const string sqlStatement2 = @"SELECT Max(Person_Id) as ID FROM person;";
            int customerId = 0;
            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement2, myConn);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    customerId = (Convert.ToInt32(row["ID"]));
                }
            }

            const string sqlStatement3 = @"Insert into customer(Customer_Id)
                                            VALUES (@id);";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement3, myConn);
                myCommand.Parameters.AddWithValue("@id", customerId);

                //For offline connection we will use  MySqlDataAdapter class.  
                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
            return customerId;

        }

        /// \brief To get a carrier's information by its id
        /// 
        /// \details This method takes in a carrier object and gets all the carrier information
        /// by searching the databse by carrier name.
        /// <param name="carrier"> - <b>Carrier</b> - The customer to look up</param>
        /// \return List of Carriers
        /// \see Carrier 
        public Carrier GetCarrierByID(int carrier)
        {
            const string sqlStatement = @"SELECT 
	                                    c.Carrier_Id,
	                                    c.Carrier_Name,
                                        c.Phone,
                                        c.Email,
                                        c.LTL_Rate,
                                        c.FTL_Rate,
                                        c.Reefer 
                                        FROM Carrier as C
	                                    WHERE c.Carrier_Id = @ID;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@ID", carrier);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                Carrier returnedCarrier = new Carrier();
                foreach (DataRow row in dataTable.Rows)
                {
                    returnedCarrier.carrierId = (Convert.ToInt32(row["Carrier_Id"]));
                    returnedCarrier.carrierName = row["Carrier_Name"].ToString();
                    returnedCarrier.Phone = row["Phone"].ToString();
                    returnedCarrier.Email = row["Email"].ToString();
                    returnedCarrier.LTLRate = Convert.ToDouble(row["LTL_Rate"]);
                    returnedCarrier.FTLRate = Convert.ToDouble(row["FTL_Rate"]);
                    returnedCarrier.ReeferCharge = Convert.ToDouble(row["Reefer"]);
                }

                return returnedCarrier;
            }
        }

        public int GetCityIdByName(string name)
        {
            const string sqlStatement = @"Select City_Id FROM
                                                city where City = @name;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@name", name);

                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();
                myAdapter.Fill(dataTable);
                int ID = 0;

                foreach (DataRow row in dataTable.Rows)
                {
                    ID = (Convert.ToInt32(row["City_Id"]));
                }
                return ID;
            }
        }

        public string GetCityNameByID(int id)
        {
            const string sqlStatement = @"Select City FROM
                                                city where City_Id = @id;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@id", id);

                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();
                myAdapter.Fill(dataTable);
                string name = "";

                foreach (DataRow row in dataTable.Rows)
                {
                    name = (row["City"]).ToString();
                }
                return name;
            }
        }

        /// \brief To get a list of carriers by depot
        /// 
        /// \details This method takes in a string containing a city name. A list of carriers with depots
        /// in the specified city is returned.
        /// <param name="city"> - <b>String</b> - The city to look up</param>
        /// \return List of Carriers
        /// \see Carrier::getCarriersWithDepot
        public List<Carrier> GetCarriersByCity(string origin, string destination)
        {

            int cityOrigin = GetCityIdByName(origin);
            int cityDestination = GetCityIdByName(destination);


            const string sqlStatement = @"Select Carrier_Id FROM
                                                (select * from Depot
                                                WHERE Delivery_City_Id = @Origin
                                                OR Delivery_City_Id = @Destination) AS temp
                                                GROUP BY Carrier_Id 
                                                Having Count(*) > 1;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@Origin", cityOrigin);
                myCommand.Parameters.AddWithValue("@Destination", cityDestination);
                //myCommand.Parameters.AddWithValue("@Destination", destination);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                List<int> CarrierId = new List<int>();

                List<Carrier> carriers = new List<Carrier>();
                myAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    CarrierId.Add(Convert.ToInt32(row["Carrier_Id"]));
                }

                foreach (int el in CarrierId)
                {
                    carriers.Add(GetCarrierByID(el));
                }

                return carriers;
            }

        }

        /// \brief To get a list of carriers by depot
        /// 
        /// \details This method takes in a string containing a city name. A list of carriers with depots
        /// in the specified city is returned.
        /// <param name="city"> - <b>String</b> - The city to look up</param>
        /// \return List of Carriers
        /// \see Carrier::getCarriersWithDepot
        public List<Carrier> GetCarriersByCityID(int origin, int destination)
        {

            


            const string sqlStatement = @"Select Carrier_Id FROM
                                                (select * from Depot
                                                WHERE Delivery_City_Id = @Origin
                                                OR Delivery_City_Id = @Destination) AS temp
                                                GROUP BY Carrier_Id 
                                                Having Count(*) > 1;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@Origin", origin);
                myCommand.Parameters.AddWithValue("@Destination", destination);
                //myCommand.Parameters.AddWithValue("@Destination", destination);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                List<int> CarrierId = new List<int>();

                List<Carrier> carriers = new List<Carrier>();
                myAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    CarrierId.Add(Convert.ToInt32(row["Carrier_Id"]));
                }

                foreach (int el in CarrierId)
                {
                    carriers.Add(GetCarrierByID(el));
                }

                return carriers;
            }

        }

        /// \brief To get orders for the planner
        /// 
        /// \details After an order is first added to the database, the planner must then select the carrier(s)
        /// for the order. To get the orders that have yet to be assigned a carrier, the orders are filtered by
        /// order-status. A list of orders is returned from this method.
        /// <param>None</param>
        /// \return A list of orders.
        /// \see Order
        public List<Order> GetOrdersForPlanner()
        {
            const string sqlStatement = @"SELECT
                                         Order_Id,
                                         Start_Date,
                                         Origin,
                                         Destination,
                                         Job_Type,
                                         Van_Type,
                                         Amount
                                         FROM _order
                                         WHERE Order_Status = 0
                                         ORDER BY Order_Id;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var orders = DataTableToOrderList(dataTable);

                return orders;
            }
        }

        /// \brief To get orders for the planner
        /// 
        /// \details After an order is first added to the database, the planner must then select the carrier(s)
        /// for the order. To get the orders that have yet to be assigned a carrier, the orders are filtered by
        /// order-status. A list of orders is returned from this method.
        /// <param>None</param>
        /// \return A list of orders.
        /// \see Order
        public Order GetOrderByID(int id)
        {
            const string sqlStatement = @"SELECT
                                         Order_Id,
                                         Start_Date,
                                         Origin,
                                         Destination,
                                         Job_Type,
                                         Van_Type,
                                         Amount
                                         FROM _order
                                         WHERE Order_Id = @ID; ";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@ID", id);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var orders = DataTableToOrderList(dataTable);

                Order order = new Order();

                foreach(Order el in orders)
                {
                    order = el;
                }
                return order;
            }
        }

        /// \brief To get orders for the planner, filtered by job type
        /// 
        /// \details After an order is first added to the database, the planner must then select the carrier(s)
        /// for the order. To get the orders that have yet to be assigned a carrier, the orders are filtered by
        /// order-status. A list of orders is returned from this method.
        /// <param>None</param>
        /// \return A list of orders.
        /// \see Order
        public List<Order> GetOrdersByJobType(int jobType)
        {
            const string sqlStatement = @"SELECT
                                         Order_Id,
                                         Start_Date,
                                         Origin,
                                         Destination,
                                         Job_Type,
                                         Van_Type,
                                         Amount
                                         FROM _order
                                         WHERE Job_Type = @job
                                         ORDER BY Order_Id;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {
                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@job", jobType);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var orders = DataTableToOrderList(dataTable);
                
                    return orders;
            }
        }

        /// \brief To convert a DataTable into a list of Orders
        /// 
        /// \details When multiple orders are returned from an sql query, this method is used to
        /// convert the returned DataTable into a list of orders.
        /// <param name="table"> - <b>DataTable</b> - The DataTable to be converted</param>
        /// \return A list of orders.
        /// \see TMSDAL:GetOrdersForPlanner
        private List<Order> DataTableToOrderList(DataTable table)
        {
            var orders = new List<Order>();

            foreach (DataRow row in table.Rows)
            {
                orders.Add(new Order
                {
                    OrderID = Convert.ToInt32(row["Order_Id"]),
                    origin = Convert.ToInt32(row["Origin"]),
                    destination = Convert.ToInt32(row["Destination"]),
                    jobType = Convert.ToBoolean(row["Job_Type"]),
                    vanType = Convert.ToBoolean(row["Van_Type"]),
                    quantity = Convert.ToInt32(row["Amount"])
                    //dateCompleted = Convert.ToDateTime(row["Start_Date"])
                }); 
            }

            foreach (Order el in orders)
            {
                if (el.vanType == false)
                {
                    el.vanTypeString = "Dry Van";
                }
                else
                {
                    el.vanTypeString = "Reefer";
                }
                el.originString = GetCityNameByID(el.origin);
                el.destinationString = GetCityNameByID(el.destination);
            }
            return orders;
        }

        /// \brief To convert a DataTable into a list of Carriers
        /// 
        /// \details When multiple carriers are returned from an sql query, this method is used to
        /// convert the returned DataTable into a list of carriers.
        /// <param name="table"> - <b>DataTable</b> - The DataTable to be converted</param>
        /// \return A list of carriers.
        /// \see TMSDAL:GetCarriersByCity, TMSDAL:GetCarrierByName
        private List<Carrier> DataTableToCarrierList(DataTable table)
        {
            var carriers = new List<Carrier>();

            foreach (DataRow row in table.Rows)
            {
                carriers.Add(new Carrier
                {
                    carrierId = Convert.ToInt32(row["Carrier_Id"]),
                    carrierName = row["Carrier_Name"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Email = row["Email"].ToString(),
                });
            }
            return carriers;
        }




        /// \brief To get the x-value(location) of a city
        /// 
        /// \details The X value of a city is needed when calculating the direction of an order.
        /// <param name="cityID"> - <b>int</b> - The City ID to lookup</param>
        /// \return int  x-> x - value of city
        /// \see TMSDAL:DataTableToInt
        public int GetXValue(int cityID)
        {
            const string sqlStatement = @"SELECT
                                         X FROM distanceTimeInfo
                                        Where City_Id = @city;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@city", cityID);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var x = DataTableToInt(dataTable);

                return x;
            }
        }

        /// \brief To get the x-value(location) of a city
        /// 
        /// \details The X value of a city is needed when calculating the direction of an order.
        /// <param name="cityID"> - <b>int</b> - The City ID to lookup</param>
        /// \return int  x-> x - value of city
        /// \see TMSDAL:DataTableToInt
        public void DeleteOrder(int ID)
        {
            const string sqlStatement = @"DELETE
                                         FROM _order
                                        Where Order_Id = @order;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {
                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@order", ID);

                //For offline connection we will use  MySqlDataAdapter class.  
                myConn.Open();

                myCommand.ExecuteNonQuery();

            }
        }

        /// \brief To convert a DataTable into information about the x value of a city
        /// 
        /// \details When the x value is returned from a query. This method converts it into
        /// an int value
        /// <param name="table"> - <b>DataTable</b> - The DataTable to be converted</param>
        /// \return int x -> x value of city
        /// \see TMSDAL:GetXValue
        private int DataTableToInt(DataTable table)
        {
            int x = 0;

            foreach (DataRow row in table.Rows)
            {
                x = Convert.ToInt32(row["X"]);
            }

            return x;

        }
        /// \brief To get the time and KM between two cities
        /// 
        /// \details This call to the database is used to calculate the time and distance between
        /// the origin and destination. This method is called multiple times in the calculation(depending on
        /// how many cities are connected between the origin and destination
        /// <param>int first -> city to get values from(going west)</param>
        /// \return Tuple containing km and time.
        /// \see Order
        public Tuple<int, float> GetTimeAndDistance(int first)
        {
            const string sqlStatement = @"SELECT
                                         KM, Travel_Time FROM distanceTimeInfo
                                         Where X = @Origin;";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@Origin", first);

                //For offline connection we will use  MySqlDataAdapter class.  
                var myAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                var dataTable = new DataTable();

                myAdapter.Fill(dataTable);

                var travelInfo = DataTableToKMTime(dataTable);

                return travelInfo;
            }
        }

        /// \brief To convert a DataTable into information about the distance/time between two cities
        /// 
        /// \details When the km and time info is returned from an sql query, this method is used to
        /// convert the returned DataTable into a tuple containing the data.
        /// <param name="table"> - <b>DataTable</b> - The DataTable to be converted</param>
        /// \return Tuple<int, float> -> km, time
        /// \see TMSDAL:GetCarriersByCity
        private Tuple<int, float> DataTableToKMTime(DataTable table)
        {
            int km = 0;
            decimal time = 0;
            foreach (DataRow row in table.Rows)
            {
                time = Convert.ToDecimal(row["Travel_Time"]);
                km = Convert.ToInt32(row["KM"]);

            }
            var distanceTime = Tuple.Create(km, (float)time);
            return distanceTime;

        }

        /// \brief To create a file backup of the database.
        /// 
        /// \details This method creates a TMS folder(if it doesn't exist) and adds a backup
        /// file to this folder
        /// \return none
        /// 
        public void Backup()
        {
            string folderPath = @"c:\tms\";
            System.IO.Directory.CreateDirectory(folderPath);
            string database = ConfigurationManager.ConnectionStrings["TMSAdmin"].ConnectionString;
            string file = "C:\\tms\\backup.sql";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(database))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        //using (MySqlBackup mb = new MySqlBackup(cmd))
                        //{
                        //    cmd.Connection = conn;
                        //    conn.Open();
                        //    mb.ExportToFile(file);
                        //    conn.Close();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
            }
        }
        public void UpdateConnectionString(string IP, string Port, int dbase)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            try
            {
                switch (dbase)
                {
                    //0 if changing the TMS
                    case 0:
                        connectionStringsSection.ConnectionStrings["TMSAdmin"].ConnectionString = ("server=" + IP + ";Port=" + Port + "; user id=admin;database=tms;password=Conestoga;SslMode=required");
                        connectionStringsSection.ConnectionStrings["TMSBuyer"].ConnectionString = ("server=" + IP + ";Port=" + Port + "; user id=buyer;database=tms;password=Conestoga;SslMode=required");
                        connectionStringsSection.ConnectionStrings["TMSPlanner"].ConnectionString = ("server=" + IP + ";Port=" + Port + "; user id=planner;database=tms;password=Conestoga;SslMode=required");
                        break;
                    //1 if changing the CMP
                    case 1:

                        break;
                    default:
                        break;
                }
                config.Save();

            }
            catch (Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.database, ex.ToString());
            }
            ConfigurationManager.RefreshSection("connectionStrings");
            buyerConnectionString = ConfigurationManager.ConnectionStrings["TMSBuyer"].ConnectionString;
            plannerConnectionString = ConfigurationManager.ConnectionStrings["TMSPlanner"].ConnectionString;
            adminConnectionString = ConfigurationManager.ConnectionStrings["TMSAdmin"].ConnectionString;
        }
    }
}