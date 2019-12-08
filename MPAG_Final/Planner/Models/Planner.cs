using MPAG_Final.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Planner.Models
{
    /**
    * \brief   The Planner class represents the planner role in the TMS.
    * \details The Planner is the one responsible for assigning a Carriers to Order and for tracking the completion of an Order 
    */
    class Planner : Employee
    {
        /// <summary>
        /// Constructor for the Planner. It requires their role name ,first, last name, email, phone number and address
        /// </summary>
        /// <param name="role"> <b>string</b> - Name of the role </param>
        /// <param name="firstName"> <b>string</b> - Employee's first name</param>
        /// <param name="lastName"> <b>string</b> - Employee's last name</param>
        /// <param name="email"> <b>string</b> - Employee's email</param>
        /// <param name="phoneNum"> <b>string</b> - Employee's phone number</param>
        /// <param name="streetAddress"> <b>string</b> - Part of the Address object that will be generated</param>
        /// <param name="city"> <b>string</b> - Part of the Address object that will be generated</param>
        /// <param name="province"> <b>string</b> - Part of the Address object that will be generated</param>
        /// <param name="postalCode"> <b>string</b> - Part of the Address object that will be generated</param>
        public Planner
           (string firstName, string lastName, string email, string phoneNum,
           string streetAddress, string city, string province, string postalCode) :
           base("Planner", firstName, lastName, email, phoneNum, streetAddress, city, province, postalCode)
        { }

        //---------------Methods-----------------------//
        public void GetOrder(Order pendingOrder)
        {
            if (pendingOrder.jobType == false) // FTL Trip
            {
                // GetRelevantCarriers     
            }

        }

        /// <summary>
        /// Used to get a list of carriers that have a depot in the origin city
        /// </summary>
        /// <returns></returns>
        public List<Carrier> GetRelevantCarriers(string origin, string destination)
        {
            var carriers = new TMSDAL().GetCarriersByCity(origin, destination);
            return carriers;
        }

        //public List<Carrier> GetCarriersWithDepot()
        //{

        //}


        public bool SelectCarriers(List<Carrier> SelectedCarriers, Order pendingOrder)
        {
            // Stubbed Out ----------------------- IMPLEMENT -----------------------//
            return true;
        }

        public void ChangeOrderSpeedTime(Order activeOrder, int daysToAdvance)
        {
            // Stubbed Out ----------------------- IMPLEMENT -----------------------// 
        }

        public bool ConfirmOrderCompletion(Order activeOrder)
        {
            // Stubbed Out ----------------------- IMPLEMENT -----------------------//
            return true;
        }

        public List<Invoice> ShowInvoiceSummary(Enum selectTime)
        {
            // Stubbed Out ----------------------- IMPLEMENT -----------------------//
            List<Invoice> SummaryInvoice = new List<Invoice>();
            return SummaryInvoice;
        }
    }
}
