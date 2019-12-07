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
        Order CreateOrder(bool jobType, uint quantity, string origin, string destination, bool vanType);

    }
}
