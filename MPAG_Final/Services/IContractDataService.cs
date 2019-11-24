using MPAG_Final.Buyer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Services
{
    public interface IContractDataService
    {
        IEnumerable<Contract> GetContracts();
        void Save(IEnumerable<Contract> contracts);

    }
}
