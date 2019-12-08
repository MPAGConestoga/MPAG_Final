using MPAG_Final.SharedModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Services
{
    public interface IContractDataService
    {
        ObservableCollection<Contract> GetContracts();
        void Save(ObservableCollection<Contract> contracts);

    }
}
