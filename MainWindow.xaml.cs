using AccountingForTouristTrips.Model;
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
using AccountingForTouristTrips.View;
using System.Data.Entity;

namespace AccountingForTouristTrips
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var context = new TouristTripsModel())
            {
                //var cl = context.Clients.Add(new Client() { FirstName = "test client" });
                //context.Users.Add(new User() { Client = cl, PasswordHash = "123" });
                //context.SaveChanges();
            }

            MainFrame.Navigate(new MainClientWindow());
        }
    }
}
