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

namespace AccountingForTouristTrips
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User LoginUser { get; set; }

        public static RoleViewModel RoleViewModel = new RoleViewModel();
        public static CountryViewModel CountryViewModel = new CountryViewModel();
    }
}
