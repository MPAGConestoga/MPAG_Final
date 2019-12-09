using MPAG_Final.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Services
{
    interface IOrderCreation
    {

        /// <summary>
        ///     Creation of the order
        /// </summary>
        /// <param name="jobType"> <b>bool</b> - FTL(0) or LTL(1)</param>
        /// <param name="quantity"> <b>uint</b> - Quantity that comes with LTL. If FLT, quantity is 0</param>
        /// <param name="origin"><b>string</b> - Origin city where the order starts</param>
        /// <param name="destination"><b>Destination</b> - Final destination for the order</param>
        /// <param name="vanType"><b>bool</b> - Van type of Dry(0) or reefer(1)</param>
        /// <returns></returns>
        Order CreateOrder(bool jobType, uint quantity, string origin, string destination, bool vanType);

        Order CreateOrder(bool jobType, int quantity, string origin, string destination, bool vanType);

    }
}
