using MPAG_Final.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MPAG_Final.Buyer.Views
{
    /// <summary>
    /// Interaction logic for DetailsView.xaml
    /// </summary>
    public partial class DetailsView : UserControl
    {
        public DetailsView()
        {
            InitializeComponent();
        }

        private void OriginComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            originTxt.Text = ((CityDepot)originCombo.SelectedItem).cityLocation;
        }

        private void DestinationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            destTxt.Text = ((CityDepot)destCombo.SelectedItem).cityLocation;
        }

        private void jobCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            jobTxt.Text = (string)jobCombo.SelectedItem;
        }

        private void vanCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vanTxt.Text = (string)vanCombo.SelectedItem;
        }
    }
}
