using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
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
using AccountingForTouristTrips.Helper;
using AccountingForTouristTrips.Model;

namespace AccountingForTouristTrips.View
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public User User => _user;

        private User _user;
        private Brush _defaultBackgroundBrush;

        public AuthorizationWindow()
        {
            InitializeComponent();
            _defaultBackgroundBrush = LoginTextBox.Background;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            SelectField(LoginTextBox, _defaultBackgroundBrush);
            SelectField(PasswordTextBox, _defaultBackgroundBrush);

            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                SelectField(LoginTextBox, Brushes.Red);
                return;
            }
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                SelectField(PasswordTextBox, Brushes.Red);
                return;
            }

            User user = await FindUserByLogin(LoginTextBox.Text);
            if (user == null)
            {
                SelectField(LoginTextBox, Brushes.Red);
                return;
            }

            if(PasswordTextBox.Text == user.PasswordHash)
            {
                _user = user;
                DialogResult = true; 
                return;
            }
            else
            {
                SelectField(LoginTextBox, Brushes.Red);
                return;
            }
        }

        private void SelectField(TextBox box, Brush brush)
        {
            box.Focus();
            box.Background = brush;
        }

        public async Task<User> FindUserByLogin(string login)
        {
            using (var context = new TouristTripsModel())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Login == login);
            }
        }

        //TODO add registration
        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
