using AccountingForTouristTrips.Model;
using AccountingForTouristTrips.View;
using AccountingForTouristTrips.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AccountingForTouristTrips;
using System.Diagnostics;

namespace AccountingForTouristTrips
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User LoginUser { get; set; }

        public static RoleViewModel RoleViewModel;
        public static CountryViewModel CountryViewModel;
        public static ClientViewModel ClientViewModel;
        public static UserViewModel UserViewModel;
        public static TourViewModel TourViewModel;
        public static BookingViewModel BookingViewModel;
        public static PaymentViewModel PaymentViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using(var context = new TouristTripsModel())
            {
                try
                {
                    context.Database.Initialize(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Инициализация не выполнена. Ошибка: " + ex.Message, "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            RoleViewModel = new RoleViewModel();
            CountryViewModel = new CountryViewModel();
            ClientViewModel = new ClientViewModel();
            UserViewModel = new UserViewModel();
            TourViewModel = new TourViewModel();
            BookingViewModel = new BookingViewModel();
            PaymentViewModel = new PaymentViewModel();
        }
    }
}
