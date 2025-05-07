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
    public class ClientViewModel
    {
        public Client SelectedClient
        {
            get
            {
                return _selectedClient;
            }
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
                Edit.CanExecute(true);
            }
        }
        private Client _selectedClient;

        public ObservableCollection<Client> ListClient { get; set; } = new ObservableCollection<Client>();

        public ClientViewModel()
        {
            ListClient = new ObservableCollection<Client>();
            ListClient = GetClient();
        }
        private ObservableCollection<Client> GetClient()
        {
            using (var context = new TouristTripsModel())
            {
                if (context.Clients != null)
                {
                    var query = from client in context.Clients.Include("User")
                                orderby client.FirstName
                                select client;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            ListClient.Add(c);
                    }
                }
            }
            return ListClient;
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListClient)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                };
            }
            return max;
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
                    Client newClient = new Client() { Id = MaxId() + 1, DateOfBirth=DateTime.Now};
                    NewClientView nvClient = new NewClientView
                    {
                        Title = "Новый клиент",
                        DataContext = newClient,
                    };
                    nvClient.ShowDialog();
                    if (nvClient.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            try
                            {
                                context.Clients.Add(newClient);
                                context.SaveChanges();
                                ListClient.Clear();
                                ListClient = GetClient();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("\nОшибка добавления данных!\n" + ex.Message, "Предупреждение");
                            }
                        }
                    }
                }, (obj) => true));
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
                    Client editClient = SelectedClient;
                    NewClientView nvClient = new NewClientView()
                    {
                        Title = "Редактирование клиента",
                        DataContext = editClient,
                    };
                    nvClient.ShowDialog();
                    if (nvClient.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            Client client = context.Clients.Find(editClient.Id);
                            if (client.FirstName != editClient.FirstName)
                                client.FirstName = editClient.FirstName.Trim();
                            if (client.LastName != editClient.LastName)
                                client.LastName = editClient.LastName.Trim();
                            if (client.Email != editClient.Email)
                                client.Email = editClient.Email.Trim();
                            if (client.Phone != editClient.Phone)
                                client.Phone = editClient.Phone.Trim();
                            if (client.DateOfBirth != editClient.DateOfBirth)
                                client.DateOfBirth = editClient.DateOfBirth;
                            try
                            {
                                context.SaveChanges();
                                ListClient.Clear();
                                ListClient = GetClient();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                            }
                        }
                    }
                    else
                    {
                        ListClient.Clear();
                        ListClient = GetClient();
                    }
                }, (obj) => SelectedClient != null && ListClient.Count > 0));
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
                    Client client = SelectedClient;
                    using (var context = new TouristTripsModel())
                    {
                        Client delClient = context.Clients.Find(client.Id);
                        User delUser = delClient.User;
                        if (delClient != null)
                        {
                            MessageBoxResult result = MessageBox.Show("Удалить данные по клиенту: " 
                                + delClient.FirstName + " " + delClient.LastName,
                                "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            if (result == MessageBoxResult.OK)
                            {
                                try
                                {
                                    if( delUser != null ) context.Users.Remove(delUser);
                                    context.Clients.Remove(delClient);
                                    context.SaveChanges();
                                    App.UserViewModel.ListUsers.Remove(delUser);
                                    ListClient.Remove(client);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                                }
                            }
                        }
                    }
                }, (obj) => SelectedClient != null && ListClient.Count > 0));
            }
        }
        #endregion
    }
}