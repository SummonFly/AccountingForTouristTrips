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
    public class BookingViewModel
    {
        public Booking SelectedBooking
        {
            get
            {
                return _selectedBooking;
            }
            set
            {
                _selectedBooking = value;
                OnPropertyChanged(nameof(SelectedBooking));
                Edit.CanExecute(true);
            }
        }
        private Booking _selectedBooking;

        public ObservableCollection<Booking> ListBookings { get; set; } = new ObservableCollection<Booking>();

        public BookingViewModel()
        {
            ListBookings = new ObservableCollection<Booking>();
            ListBookings = GetBookings();
        }
        private ObservableCollection<Booking> GetBookings()
        {
            using (var context = new TouristTripsModel())
            {
                if (context.Bookings != null)
                {
                    var query = from booking in context.Bookings.Include("Tour").Include("Client")
                                orderby booking.Tour.Name
                                select booking;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            ListBookings.Add(c);
                    }
                }
            }
            return ListBookings;
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListBookings)
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
                    Booking newBooking = new Booking() { Id = MaxId() + 1, BookingDate=DateTime.Now };
                    NewBookingView nvBooking = new NewBookingView
                    {
                        Title = "Новая бронь",
                        DataContext = newBooking,
                    };
                    nvBooking.cbTour.ItemsSource = App.TourViewModel.ListTour;
                    nvBooking.cbClient.ItemsSource = App.ClientViewModel.ListClient;


                    nvBooking.ShowDialog();
                    if (nvBooking.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            try
                            {
                                var existingTour = context.Tours.Find(newBooking.Tour.Id);
                                newBooking.Tour = existingTour;

                                var existingClient = context.Clients.Find(newBooking.Client.Id);
                                newBooking.Client = existingClient;

                                context.Bookings.Add(newBooking);
                                context.SaveChanges();
                                ListBookings.Clear();
                                ListBookings = GetBookings();
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
                    Booking editBooking = SelectedBooking;
                    NewBookingView nvBooking = new NewBookingView()
                    {
                        Title = "Редактирование брони",
                        DataContext = editBooking,
                    };
                    nvBooking.cbTour.ItemsSource = App.TourViewModel.ListTour;
                    nvBooking.cbClient.ItemsSource = App.ClientViewModel.ListClient;

                    nvBooking.ShowDialog();
                    if (nvBooking.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            Booking booking = context.Bookings.Find(editBooking.Id);
                            if (booking.TourId != editBooking.TourId)
                                booking.TourId = editBooking.TourId;
                            if (booking.ClientId != editBooking.ClientId)
                                booking.ClientId = editBooking.ClientId;
                            if (booking.BookingDate != editBooking.BookingDate)
                                booking.BookingDate = editBooking.BookingDate;
                            if (booking.NumberOfPeople != editBooking.NumberOfPeople)
                                booking.NumberOfPeople = editBooking.NumberOfPeople;
                            if (booking.Statys != editBooking.Statys)
                                booking.Statys = editBooking.Statys;
                            try
                            {
                                context.SaveChanges();
                                ListBookings.Clear();
                                ListBookings = GetBookings();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                            }
                        }
                    }
                    else
                    {
                        ListBookings.Clear();
                        ListBookings = GetBookings();
                    }
                }, (obj) => SelectedBooking != null && ListBookings.Count > 0));
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
                    Booking booking = SelectedBooking;
                    using (var context = new TouristTripsModel())
                    {
                        Booking delBooking = context.Bookings.Find(booking.Id);
                        if (delBooking != null)
                        {
                            MessageBoxResult result = MessageBox.Show("Удалить данные по брони: " + delBooking.Tour.Name + "\nна " + delBooking.BookingDate,
                                "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            if (result == MessageBoxResult.OK)
                            {
                                try
                                {
                                    context.Bookings.Remove(delBooking);
                                    context.SaveChanges();
                                    ListBookings.Remove(booking);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                                }
                            }
                        }
                    }
                }, (obj) => SelectedBooking != null && ListBookings.Count > 0));
            }
        }
        #endregion
    }
}
