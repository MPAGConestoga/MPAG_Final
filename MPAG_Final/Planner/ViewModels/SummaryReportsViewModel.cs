using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MPAG_Final.Planner.Models;
using MPAG_Final.Utilities;


namespace MPAG_Final.Planner.ViewModels
{
    /**
    * \Class SummaryReportsViewModel
    * \Brief View model for the handling of summary report generation
    * \Details The summaryReports view model loads and handles the information that will be 
    * displayed to the user in the SummaryReports view for the planner role. Will handle 
    * the generation and storage of new summary reports.
    */
    public class SummaryReportsViewModel 
    {


        public ICommand LoadTwoWeekReportCommand { get; private set; }
        public ICommand LoadAllTimeReportCommand { get; private set; }

        public SummaryReportsViewModel(PlannerRole planner)
        {

        }

        public void LoadTwoWeekReport()
        {
            // insert object here
            // ContractsVM.LoadContracts(_service.GetContracts());
        }

        //function for loading contracts; references ContractViewModel LoadContracts function
        public void LoadAllTimeReport()
        {
            // insert object here
            // ContractsVM.LoadContracts(_service.GetContracts());
        }

    }
        

}

