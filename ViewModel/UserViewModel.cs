using AccountingForTouristTrips.Helper;
using AccountingForTouristTrips.Model;
using AccountingForTouristTrips.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountingForTouristTrips.ViewModel
{
    public class UserViewModel
    {
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                Edit.CanExecute(true);
            }
        }
        private User _selectedUser;

        public ObservableCollection<User> ListUsers { get; set; } = new ObservableCollection<User>();

        public UserViewModel()
        {
            ListUsers = new ObservableCollection<User>();
            ListUsers = GetUsers();
        }
        private ObservableCollection<User> GetUsers()
        {
            using (var context = new TouristTripsModel())
            {
                if (context.Users != null)
                {
                    var query = from user in context.Users.Include("Client").Include("Role")
                                orderby user.Login
                                select user;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            ListUsers.Add(c);
                    }
                }
            }
            return ListUsers;
        }

        //public int MaxId()
        //{
        //    int max = 0;
        //    foreach (var r in this.ListUsers)
        //    {
        //        if (max < r.ClientId)
        //        {
        //            max = r.ClientId;
        //        };
        //    }
        //    return max;
        //}

        private List<Client> GetClientsWithoutUser()
        {
            var clients = App.ClientViewModel.ListClient;
            List<Client> clientsWithoutUsers = new List<Client>();
            foreach(var client in clients)
            {
                if(ListUsers.All(u => u.Client?.Id != client.Id))
                {
                    clientsWithoutUsers.Add(client);
                }
            }
            return clientsWithoutUsers;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region command Add
        private RelayCommand _add;
        public RelayCommand Add
        {
            get
            {
                return _add ??
                (_add = new RelayCommand(obj =>
                {
                    var clientsWithoutUser = GetClientsWithoutUser();
                    User newUser = new User();
                    NewUserView nvUser = new NewUserView
                    {
                        Title = "Новый пользователь",
                        DataContext = newUser,
                    };
                    nvUser.cbClient.ItemsSource = clientsWithoutUser;
                    nvUser.cbRole.ItemsSource = App.RoleViewModel.ListRole;

                    nvUser.ShowDialog();
                    if (nvUser.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            try
                            {
                                var existingClient = context.Clients.Find(newUser.Client.Id);
                                newUser.Client = existingClient;

                                var existingRole = context.Roles.Find(newUser.Role.Id);
                                newUser.Role = existingRole;

                                context.Users.Add(newUser);
                                context.SaveChanges();
                                ListUsers.Clear();
                                ListUsers = GetUsers();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("\nОшибка добавления данных!\n" + ex.Message, "Предупреждение");
                            }
                        }
                    }
                }, (obj) => GetClientsWithoutUser().Count > 0)); //Need optimization, check in each frame
            }
        }
        #endregion
        #region Command Edit
        private RelayCommand _edit;
        public RelayCommand Edit
        {
            get
            {
                return _edit ??
                (_edit = new RelayCommand(obj =>
                {
                    User editUser = SelectedUser;
                    MessageBox.Show($"Login: {editUser.Login}\nClient id: {editUser.ClientId}\nEmail: {editUser.Client.Email}\nHash: {editUser.PasswordHash}");
                    NewUserView nvUser = new NewUserView()
                    {
                        Title = "Редактирование пользователя",
                        DataContext = editUser,
                    };
                    nvUser.cbClient.SelectedItem = editUser;
                    nvUser.cbClient.IsEnabled = false;
                    nvUser.cbRole.ItemsSource= App.RoleViewModel.ListRole;

                    nvUser.ShowDialog();
                    if (nvUser.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            User user = context.Users.Find(editUser.ClientId);
                            if (user.Login != editUser.Login)
                                user.Login = editUser.Login.Trim();
                            if (user.PasswordHash != nvUser.PasswordHashBox.Text)
                                user.PasswordHash = nvUser.PasswordHashBox.Text;
                            if (user.RoleId != editUser.RoleId)
                                user.RoleId = editUser.RoleId;
                            try
                            {
                                context.SaveChanges();
                                ListUsers.Clear();
                                ListUsers = GetUsers();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                            }
                        }
                    }
                    else
                    {
                        ListUsers.Clear();
                        ListUsers = GetUsers();
                    }
                }, (obj) => SelectedUser != null && ListUsers.Count > 0));
            }
        }
        #endregion
        #region Delete 
        private RelayCommand _delete;
        public RelayCommand Delete
        {
            get
            {
                return _delete ??
                (_delete = new RelayCommand(obj =>
                {
                    User user = SelectedUser;
                    using (var context = new TouristTripsModel())
                    {
                        User delUser = context.Users.Find(user.ClientId);
                        if (delUser != null)
                        {
                            MessageBoxResult result = MessageBox.Show("Удалить данные по пользователю: " + delUser.Login,
                                "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            if (result == MessageBoxResult.OK)
                            {
                                try
                                {
                                    context.Users.Remove(delUser);
                                    context.SaveChanges();
                                    ListUsers.Remove(user);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                                }
                            }
                        }
                    }
                }, (obj) => SelectedUser != null && ListUsers.Count > 0));
            }
        }
        #endregion
    }
}
