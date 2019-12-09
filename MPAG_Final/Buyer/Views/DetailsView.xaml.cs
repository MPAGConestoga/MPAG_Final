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
            if (originCombo.SelectedIndex != -1)
            {
                originTxt.Text = ((CityDepot)originCombo.SelectedItem).cityLocation;
            }
            originCombo.SelectedIndex = -1;
        }

        private void DestinationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (destCombo.SelectedIndex != -1)
            {
                destTxt.Text = ((CityDepot)destCombo.SelectedItem).cityLocation;
            }
            destCombo.SelectedIndex = -1;
        }

        private void jobCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (jobCombo.SelectedIndex != -1)
            {
                jobTxt.Text = (string)jobCombo.SelectedItem;
            }jobCombo.SelectedIndex = -1;
        }

        private void vanCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(vanCombo.SelectedIndex != -1) { 
            vanTxt.Text = (string)vanCombo.SelectedItem;
            }
            vanCombo.SelectedIndex = -1;
        }

        private void quantityText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (quantityText.Text == "... Change Quantity ...")
            {
                quantityText.Text = "";
            }
        }

        private void quantityText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (quantityText.Text != "... Change Quantity ...")
            {

                quantityTxt.Text = (string)quantityText.Text;
            }
            
        }

        private void quantityText_LostFocus(object sender, RoutedEventArgs e)
        {
            quantityText.Text = "... Change Quantity ...";
        }
    }
}
