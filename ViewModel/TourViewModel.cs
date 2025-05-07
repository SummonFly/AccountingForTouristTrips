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
    public class TourViewModel
    {
        public Tour SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                Edit.CanExecute(true);
            }
        }
        private Tour _selectedTour;

        public ObservableCollection<Tour> ListTour { get; set; } = new ObservableCollection<Tour>();

        public TourViewModel()
        {
            ListTour = new ObservableCollection<Tour>();
            ListTour = GetTours();
        }
        private ObservableCollection<Tour> GetTours()
        {
            using (var context = new TouristTripsModel())
            {
                if (context.Tours != null)
                {
                    var query = from tour in context.Tours.Include("Country")
                                orderby tour.Name
                                select tour;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            ListTour.Add(c);
                    }
                }
            }
            return ListTour;
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListTour)
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
                    Tour newTour = new Tour() 
                    { 
                        Id = MaxId() + 1, 
                        StartDate=DateTime.Now, 
                        EndDate=DateTime.Now.AddDays(5)
                    };
                    NewTourView nvTour = new NewTourView
                    {
                        Title = "Новый тур",
                        DataContext = newTour,
                    };
                    nvTour.cbCountry.ItemsSource = App.CountryViewModel.ListCountry;

                    nvTour.ShowDialog();
                    if (nvTour.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            try
                            {
                                var  existingCountry = context.Countries.Find(newTour.Country.Id);
                                newTour.Country = existingCountry;

                                context.Tours.Add(newTour);
                                context.SaveChanges();
                                ListTour.Clear();
                                ListTour = GetTours();
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
                    Tour editTour = SelectedTour;
                    NewTourView nvTour = new NewTourView()
                    {
                        Title = "Редактирование тура",
                        DataContext = editTour,
                    };
                    nvTour.cbCountry.ItemsSource = App.CountryViewModel.ListCountry;

                    nvTour.ShowDialog();
                    if (nvTour.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            Tour tour = context.Tours.Find(editTour.Id);
                            if (tour != null)
                            {
                                if (tour.Name != editTour.Name)
                                    tour.Name = editTour.Name.Trim();
                                if (tour.CountryId != editTour.CountryId)
                                    tour.CountryId = editTour.CountryId;
                                if (tour.Price != editTour.Price)
                                    tour.Price = editTour.Price;
                                if (tour.StartDate != editTour.StartDate)
                                    tour.StartDate = editTour.StartDate;
                                if (tour.EndDate != editTour.EndDate)
                                    tour.EndDate = editTour.EndDate;
                                if (tour.MaxSeats != editTour.MaxSeats)
                                    tour.MaxSeats = editTour.MaxSeats;
                                try
                                {
                                    context.SaveChanges();
                                    ListTour.Clear();
                                    ListTour = GetTours();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                                }
                            }
                        }
                    }
                    else
                    {
                        ListTour.Clear();
                        ListTour = GetTours();
                    }
                }, (obj) => SelectedTour != null && ListTour.Count > 0));
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
                    Tour tour = SelectedTour;
                    using (var context = new TouristTripsModel())
                    {
                        Tour delTour = context.Tours.Find(tour.Id);
                        if (delTour != null)
                        {
                            MessageBoxResult result = MessageBox.Show("Удалить данные по туру: " + delTour.Name,
                                "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            if (result == MessageBoxResult.OK)
                            {
                                try
                                {
                                    context.Tours.Remove(delTour);
                                    context.SaveChanges();
                                    ListTour.Remove(tour);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                                }
                            }
                        }
                    }
                }, (obj) => SelectedTour != null && ListTour.Count > 0));
            }
        }
        #endregion
    }
}
