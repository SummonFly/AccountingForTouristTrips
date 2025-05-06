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
            //Login();

            //MainFrame.Navigate(new MainClientWindow());
            MainFrame.Navigate(new TestingPage());
        }

        public void Login()
        {
            AuthorizationWindow authorization = new AuthorizationWindow();
            if(authorization.ShowDialog() == false) 
                this.Close();
            else
                App.LoginUser = authorization.User;
        }
    }
}
