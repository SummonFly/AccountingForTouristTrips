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
    public class CountryViewModel
    {
        public Country SelectedCountry
        {
            get
            {
                return _selectedCountry;
            }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
                Edit.CanExecute(true);
            }
        }
        private Country _selectedCountry;

        public ObservableCollection<Country> ListCountry { get; set; } = new ObservableCollection<Country>();

        public CountryViewModel()
        {
            ListCountry = new ObservableCollection<Country>();
            ListCountry = GetCountries();
        }
        private ObservableCollection<Country> GetCountries()
        {
            using (var context = new TouristTripsModel())
            {
                if (context.Countries != null)
                {
                    var query = from country in context.Countries
                                orderby country.Name
                                select country;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            ListCountry.Add(c);
                    }
                }
            }
            return ListCountry;
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListCountry)
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
                    Country newCountry = new Country() { Id = MaxId() + 1 };
                    NewCountryView nvCountry = new NewCountryView
                    {
                        Title = "Новая страна",
                        DataContext = newCountry,
                    };
                    nvCountry.ShowDialog();
                    if (nvCountry.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            try
                            {
                                context.Countries.Add(newCountry);
                                context.SaveChanges();
                                ListCountry.Clear();
                                ListCountry = GetCountries();
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
                    Country editCountry = SelectedCountry;
                    NewCountryView nvCountry = new NewCountryView()
                    {
                        Title = "Редактирование страны",
                        DataContext = editCountry,
                    };
                    nvCountry.ShowDialog();
                    if (nvCountry.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            Country country = context.Countries.Find(editCountry.Id);
                            if (country.Name != editCountry.Name)
                                country.Name = editCountry.Name.Trim();
                            try
                            {
                                context.SaveChanges();
                                ListCountry.Clear();
                                ListCountry = GetCountries();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                            }
                        }
                    }
                    else
                    {
                        ListCountry.Clear();
                        ListCountry = GetCountries();
                    }
                }, (obj) => SelectedCountry != null && ListCountry.Count > 0));
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
                    Country country = SelectedCountry;
                    using (var context = new TouristTripsModel())
                    {
                        Country delCountry = context.Countries.Find(country.Id);
                        if (delCountry != null)
                        {
                            MessageBoxResult result = MessageBox.Show("Удалить данные по стране: " + delCountry.Name,
                                "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            if (result == MessageBoxResult.OK)
                            {
                                try
                                {
                                    context.Countries.Remove(delCountry);
                                    context.SaveChanges();
                                    ListCountry.Remove(country);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                                }
                            }
                        }
                    }
                }, (obj) => SelectedCountry != null && ListCountry.Count > 0));
            }
        }
        #endregion
    }
}
