using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.SharedModels
{
    public class CityDepot
    {
        string cityLocation;
       // List<WaitingOrders> waitingOrders;

        public CityDepot(string city)
        {
            // Go to the database and check if that city has any depots (from any carrier) on it 
            // if so, go to the databse and get all the waiting orders 
        }

        public void AddWaitingOrder(Order pendingOrder)
        {
            // Based on the direction, add an order to the appropriate waitingOrder 
        }
    }
}
