using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private RelayCommand _openCommand;
        public RelayCommand OpenCommand
        {
            get; set;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog oDlg = new OpenFileDialog();                 //new OpenFileDialog is defined for later use
            oDlg.FileName = "Document";                                 //Open File messages and filters established
            oDlg.DefaultExt = ".txt";
            oDlg.Filter = "Text documents (.txt)|*.txt";
            int retCode = 0;

            //retCode = Text_Check();                                     //calls function for checking textbox contents
            //if (retCode == 1 && (oDlg.ShowDialog() == true))            //user chooses to save
            //{
            //    SaveAs_Executed(null, new RoutedEventArgs());           //save function is called
            //    textEditor.Text = File.ReadAllText(oDlg.FileName);      //file is opened
            //}
            //else if (retCode == 2 && (oDlg.ShowDialog() == true))       //user opts to open file without saving
            //{
            //    textEditor.Text = File.ReadAllText(oDlg.FileName);
            //}
            //else if (retCode == 0 && (oDlg.ShowDialog() == true))       //no text is detected
            //{                                                           //file is opened
            //    textEditor.Text = File.ReadAllText(oDlg.FileName);
            //}
        }
    }
}
