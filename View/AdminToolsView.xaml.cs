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
    /// Логика взаимодействия для AdminToolsView.xaml
    /// </summary>
    public partial class AdminToolsView : Page
    {
        public AdminToolsView()
        {
            InitializeComponent();
        }

        private void ShowClientViewBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\ClientView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowUserViewBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\UserView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowRoleViewBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\RoleView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowCountryViewBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\ContryView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowTourViewBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\TourView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowBookingViewBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri(@"View\BookingView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ReloadViewModelBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ClientCheck.IsChecked == true) App.ClientViewModel = new ViewModel.ClientViewModel();
            if(UserCheck.IsChecked == true) App.UserViewModel = new ViewModel.UserViewModel();
            if(RoleCheck.IsChecked == true) App.RoleViewModel = new ViewModel.RoleViewModel();
        }

    }
}
