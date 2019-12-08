using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPAG_Final.SharedModels;
using MySql.Data.MySqlClient;


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
        private string buyerConnectionString = "server=127.0.0.1;user id=buyer;database=tms;password=Conestoga;SslMode=required";
        private string plannerConnectionString = "server=127.0.0.1;user id=planner;database=tms;password=Conestoga;SslMode=required";
        private string adminConnectionString = "server=127.0.0.1;user id=admin;database=tms;password=Conestoga;SslMode=required";


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

        /// \brief To insert an address into the TMS local database
        /// 
        /// \details If the admin would like to add a new customer, employee, or carrier, an address may need to be 
        /// added to the database. This method is used for insertion into the address, city, and province tables.
        /// <param name="address"> - <b>Address</b> - The address to be added to the database</param>
        /// \return none
        /// \see Address
        public void addAddress(Address address)
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

        /// \brief To set an order's status as current
        /// 
        /// \details After the planner has selected the carrier for an order, the order is confirmed. This method is used
        /// to set the order's status as current -> the order is ready to be shipped. 
        /// <param name="order"> - <b>Order</b> - The order to be confirmed in the database</param>
        /// \return none
        /// \see Order::confirmOrder()
        public void confirmOrder(Order order)
        {
            using (var myConn = new MySqlConnection(buyerConnectionString))
            {
                const string sqlStatement = @"  UPDATE _order 
                                                SET 
	                                            Order_Status = 1
                                                WHERE
	                                            Order_Id = @Order_Id; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@Order_Id", order.orderID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
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
                myCommand.Parameters.AddWithValue("@Order_Id", order.orderID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
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
            using (var myConn = new MySqlConnection(buyerConnectionString))
            {
                const string sqlStatement = @"  UPDATE _order 
                                                SET 
	                                            Order_Status = 3
                                                WHERE
	                                            Order_Id = @Order_Id; ";

                var myCommand = new MySqlCommand(sqlStatement, myConn);

                myCommand.Parameters.AddWithValue("@Order_Id", order.orderID);

                myConn.Open();

                myCommand.ExecuteNonQuery();
            }
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

        /// \brief To get a list of carriers by depot
        /// 
        /// \details This method takes in a string containing a city name. A list of carriers with depots
        /// in the specified city is returned.
        /// <param name="city"> - <b>String</b> - The city to look up</param>
        /// \return List of Carriers
        /// \see Carrier::getCarriersWithDepot
        public List<Carrier> GetCarriersByCity(string origin, string destination)
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
                                            INNER JOIN city
	                                        WHERE c.Carrier_Id = (SELECT d.Carrier_Id
							                                        WHERE d.Delivery_City_Id = 
								                                        (SELECT city.City_Id
									                                        WHERE city.City = @origin
                                                                            OR city.City = @destination))
	                                                                        GROUP BY c.Carrier_Id
                                                                            HAVING Count(*) > 1; ";

            using (var myConn = new MySqlConnection(buyerConnectionString))
            {

                var myCommand = new MySqlCommand(sqlStatement, myConn);
                myCommand.Parameters.AddWithValue("@Origin", origin);
                myCommand.Parameters.AddWithValue("@Destination", destination);

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
                    orderID = Convert.ToInt32(row["Order_Id"]),
                    origin = row["Origin"].ToString(),
                    destination = row["Destination"].ToString(),
                    jobType = Convert.ToBoolean(row["Job_Type"]),
                    vanType = Convert.ToBoolean(row["Order_Status"]),
                    //dateCompleted = Convert.ToDateTime(row["Start_Date"])
                }); ;
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
    }
}
