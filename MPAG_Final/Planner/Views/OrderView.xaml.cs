using MPAG_Final.Planner.ViewModels;
using MPAG_Final.SharedModels;
using System.Windows;
using System.Windows.Controls;

namespace MPAG_Final.Planner.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
        } 


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tempVM = (OrderViewModel)this.DataContext;
            Order selectedOrder = (Order)LTLPending.SelectedItem;
            tempVM.OrderChecked(selectedOrder);
        }
    }
}
