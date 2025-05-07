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
    public class RoleViewModel
    {
        public Role SelectedRole
        {
            get
            {
                return _selectedRole;
            }
            set
            {
                _selectedRole = value;
                OnPropertyChanged("SelectedRole");
                Edit.CanExecute(true);
            }
        }
        private Role _selectedRole;

        public ObservableCollection<Role> ListRole { get; set; } = new ObservableCollection<Role>();

        public RoleViewModel()
        {
            ListRole = new ObservableCollection<Role>();
            ListRole = GetRoles();
        }
        private ObservableCollection<Role> GetRoles()
        {
            using (var context = new TouristTripsModel())
            {
                if (context.Roles != null)
                {
                    var query = from role in context.Roles
                                orderby role.Name
                                select role;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            ListRole.Add(c);
                    }
                }
            }
            return ListRole;
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListRole)
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
                    Role newRole = new Role() { Id = MaxId() + 1 };
                    NewRoleView nvRole = new NewRoleView
                    {
                        Title = "Новая должность",
                        DataContext = newRole,
                    };
                    nvRole.ShowDialog();
                    if (nvRole.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            try
                            {
                                context.Roles.Add(newRole);
                                context.SaveChanges();
                                ListRole.Clear();
                                ListRole = GetRoles();
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
                    Role editRole = SelectedRole;
                    NewRoleView nvRole = new NewRoleView()
                    {
                        Title = "Редактирование должности",
                        DataContext = editRole,
                    };
                    nvRole.ShowDialog();
                    if (nvRole.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            Role role = context.Roles.Find(editRole.Id);
                            if (role.Name != editRole.Name)
                                role.Name = editRole.Name.Trim();
                            try
                            {
                                context.SaveChanges();
                                ListRole.Clear();
                                ListRole = GetRoles();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                            }
                        }
                    }
                    else
                    {
                        ListRole.Clear();
                        ListRole = GetRoles();
                    }
                }, (obj) => SelectedRole != null && ListRole.Count > 0));
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
                    Role role = SelectedRole;
                    using (var context = new TouristTripsModel())
                    {
                        Role delRole = context.Roles.Find(role.Id);
                        if (delRole != null)
                        {
                            MessageBoxResult result = MessageBox.Show("Удалить данные по должности: " + delRole.Name,
                                "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            if (result == MessageBoxResult.OK)
                            {
                                try
                                {
                                    context.Roles.Remove(delRole);
                                    context.SaveChanges();
                                    ListRole.Remove(role);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                                }
                            }
                        }
                    }
                }, (obj) => SelectedRole != null && ListRole.Count >
                0));
            }
        }
        #endregion
    }
}
