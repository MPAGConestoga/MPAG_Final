using MPAG_Final.SharedModels;
using MPAG_OrderAndTrip;
using System;
using System.Collections.Generic;

namespace MPAG_Final.Planner.Models
{
    /*
    * \brief   The Planner class represents the planner role in the TMS.
    * \details The Planner is the one responsible for assigning a Carriers to Order and for tracking the completion of an Order 
    */
    public class PlannerRole : Employee
    {
        List<Carrier> relevantCarriers = null;

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
        public PlannerRole
           (string firstName, string lastName, string email, string phoneNum,
           string streetAddress, string city, string province, string postalCode) :
           base("Planner", firstName, lastName, email, phoneNum, streetAddress, city, province, postalCode)
        { }

        //---------------Methods-----------------------//

        public List<Carrier> GetRelevantCarrier(string origin, string destination)
        {
            return null;
        }



        // At this point, we know that the carrier has a depot city in that city
        // So we can attribute trips to carrier (through depot) 
        public void AddTrip(Carrier selectedCarrier, Order selectedOrder)
        {
            Depot initalDepot = selectedCarrier.DepotsLocation[selectedOrder.origin];

            // Create Trip
            Trip newTrip = new Trip(selectedOrder, selectedCarrier);

            // Assign trips to Order and Depot
            selectedOrder.AssignedTrips.Add(newTrip);
            selectedCarrier.DepotsLocation[selectedOrder.origin].Trips.Add(newTrip);
        }



        //public void AddTripsToMulipletTrips(List<Order> bundledOrders)
        //{
        //    uint totalPalletsQuantity = 0;

        //    foreach (var order in bundledOrders)
        //    {
        //        totalPalletsQuantity += order.quantity;
        //    }

        //    // Check division
        //    int FTLAmount = (int)totalPalletsQuantity / Carrier.MaxLot;
        //    totalPalletsQuantity -= (uint)(FTLAmount * Carrier.MaxLot);

        //    string origin = bundledOrders[0].origin;
        //    string destination = bundledOrders[0].destination;

        //    // Get the amount of LTL pallets in a City 
        //    List<Carrier> relevantCarriers = GetRelevantCarrier(origin, destination);

        //    int totalPallets = 0;
        //    foreach (var carrier in Carriers)
        //    {
        //        totalPallets = carrier.DepotsLocation[origin].avalibleLTL;
        //    }


        //    for (int counter = 0; counter < bundledOrders.Count; counter)
        //    {
        //        relevantCarriers[counter]. -= bundledOrders[counter].quantity;
        //    }

        //}

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
