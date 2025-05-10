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
using System.Windows.Shapes;

namespace AccountingForTouristTrips.View
{
    /// <summary>
    /// Логика взаимодействия для TourView.xaml
    /// </summary>
    public partial class TourView : Page
    {
        public TourView()
        {
            InitializeComponent();
            DataContext = App.TourViewModel;
            if(App.LoginUser.Role.Name == "Клиент")
            {
                this.btnAdd.Visibility = Visibility.Hidden;
                this.btnEdit.Visibility = Visibility.Hidden;
                this.btnDelete.Visibility = Visibility.Hidden;
                this.btnBook.Visibility = Visibility.Visible;
            }
        }
    }
}
