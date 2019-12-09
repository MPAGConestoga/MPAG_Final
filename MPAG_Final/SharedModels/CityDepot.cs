using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.SharedModels
{
    /**
     * \Class   CityDepot
     * \brief   Used for creating text logs
     * \details Creates the types of logs for the different types which are
     *          for the buyer, planner, admin and the database
     */

    public class CityDepot
    {
        public int ID {get; set;}
        public string cityLocation { get; set; }
       // List<WaitingOrders> waitingOrders;
        
        //Default constructor : needed for getting all citydepot
        /// <summary>
        ///     Constructor needed for gettng all city depot
        /// </summary>
        public CityDepot()
        {

        }
        /// <summary>
        ///         City depot and check if the city has any depot from varrier
        /// </summary>
        /// <param name="city"><b>string</b> - Choice of city</param>
        public CityDepot(string city)
        {
            // Go to the database and check if that city has any depots (from any carrier) on it 
            // if so, go to the databse and get all the waiting orders 
        }

        /// <summary>
        ///     Add waiting order for the list
        /// </summary>
        /// <param name="pendingOrder"><b>Order</b> Order based on the direction</param>
        public void AddWaitingOrder(Order pendingOrder)
        {
            // Based on the direction, add an order to the appropriate waitingOrder 
        }
    }
}
