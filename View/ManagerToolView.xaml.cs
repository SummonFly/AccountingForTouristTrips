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

namespace AccountingForTouristTrips.View
{
    /// <summary>
    /// Логика взаимодействия для ManagerToolView.xaml
    /// </summary>
    public partial class ManagerToolView : Page
    {
        public ManagerToolView()
        {
            InitializeComponent();
        }

        private void ShowClientViewBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\ClientView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowTourViewBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\TourView.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
