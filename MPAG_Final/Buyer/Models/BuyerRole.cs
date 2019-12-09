using MPAG_Final.Services;
using MPAG_Final.SharedModels;
using System;
using System.Collections.Generic;
using MPAG_Final.Logging;

namespace MPAG_Final.Buyer.Models
{
    /**
      * \Class BuyerRole
      * \brief The Buyer class represents the Employee responsible for Order creation
      * \details Inherits from the employee class and implements methods from IOrderCreation interface class.
      * Buyer can create orders and track a list of orders. 
      * \see Employee
      *
      */
    public class BuyerRole : Employee, IOrderCreation
    {
        private List<Order> CreatedOrders;

        /// <summary>
        /// This constructor is used to instantiate a Buyer with the specified field values
        /// </summary>
        /// <param name="firstName"> <b>string</b> - Buyer first name</param>
        /// <param name="lastName"> <b>string</b> - Buyer last name</param>
        /// <param name="email"> <b>string</b> - Buyer email</param>
        /// <param name="phoneNum"> <b>string</b> - Buyer phone number</param>
        /// <param name="streetAddress"> <b>string</b> - Buyer street address</param>
        /// <param name="city"> <b>string</b> - Buyer's city</param>
        /// <param name="province"> <b>string</b> - Buyer's province</param>
        /// <param name="postalCode"> <b>string</b> - Buyer's postal code</param> 

        public BuyerRole
            (string firstName, string lastName, string email, string phoneNum,
            string streetAddress, string city, string province, string postalCode) :
            base("Buyer", firstName, lastName, email, phoneNum, streetAddress, city, province, postalCode)
        {
            CreatedOrders = new List<Order>();
        }

        /// <summary>
        /// This method is used to Create a new order given specified values
        /// </summary>
        /// <param name="jobType"> <b>bool</b> - Order job type(LTL or FTL)</param>
        /// <param name="quantity"> <b>unint</b> - Quantity of the job</param>
        /// <param name="origin"> <b>string</b> - Where the order will be shipped from</param>
        /// <param name="destination"> <b>string</b> - Where the order will be shipped to/param>
        /// <param name="vanType"> <b>bool</b> - Van type:dry or reefer</param>
        /// \see Order 
        public Order CreateOrder(bool jobType, int quantity, string origin, string destination, bool vanType)
        {
            Order newOrder = null;
            try
            {
                newOrder = new Order(jobType, quantity, origin, destination, vanType);
                CreatedOrders.Add(newOrder);      // Attribute the Order to the buyer
                
            }
            catch(Exception ex)
            {
                LogType.ErrorType(LogType.LoggingType.buyer, ex.ToString());
                newOrder = null;
            }

            return newOrder;
        }

        public Order CreateOrder(bool jobType, uint quantity, string origin, string destination, bool vanType)
        {
            throw new NotImplementedException();
        }

        public string GenerateInvoice(Order reviewedOrder)
        {
            //STUBBED---------------------------------------//
            return "TEWST";
        }

    }
}
