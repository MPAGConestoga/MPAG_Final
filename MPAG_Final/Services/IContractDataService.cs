using MPAG_Final.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Services
{
    public interface IContractDataService
    {
        IList<Contract> GetContracts();
        void Save(IList<Contract> contracts);

    }
}
