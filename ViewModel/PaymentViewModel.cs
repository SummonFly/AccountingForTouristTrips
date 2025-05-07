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
    public class PaymentViewModel
    {
        public Payment SelectedPayment
        {
            get
            {
                return _selectedPayment;
            }
            set
            {
                _selectedPayment = value;
                OnPropertyChanged(nameof(SelectedPayment));
                Edit.CanExecute(true);
            }
        }
        private Payment _selectedPayment;

        public ObservableCollection<Payment> ListPayments { get; set; } = new ObservableCollection<Payment>();

        public PaymentViewModel()
        {
            ListPayments = new ObservableCollection<Payment>();
            ListPayments = GetPayments();
        }
        private ObservableCollection<Payment> GetPayments()
        {
            using (var context = new TouristTripsModel())
            {
                if (context.Payments != null)
                {
                    var query = from payment in context.Payments
                                .Include("Booking")
                                .Include("Booking.Tour")
                                .Include("Booking.Client")
                                orderby payment.PaymentDate
                                select payment;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            ListPayments.Add(c);
                    }
                }
            }
            return ListPayments;
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListPayments)
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
                    Payment newPayment = new Payment() { Id = MaxId() + 1, PaymentDate=DateTime.Now };
                    NewPaymentView nvPayment = new NewPaymentView
                    {
                        Title = "Новая операция",
                        DataContext = newPayment,
                    };
                    nvPayment.cbBooking.ItemsSource = App.BookingViewModel.ListBookings;

                    nvPayment.ShowDialog();
                    if (nvPayment.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            try
                            {
                                var existingBooking = context.Bookings.Find(newPayment.Booking.Id);
                                newPayment.Booking = existingBooking;

                                context.Payments.Add(newPayment);
                                context.SaveChanges();
                                ListPayments.Clear();
                                ListPayments = GetPayments();
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
                    Payment editPayment = SelectedPayment;
                    NewPaymentView nvPayment = new NewPaymentView()
                    {
                        Title = "Редактирование операции",
                        DataContext = editPayment,
                    };
                    nvPayment.cbBooking.ItemsSource = App.BookingViewModel.ListBookings;

                    nvPayment.ShowDialog();
                    if (nvPayment.DialogResult == true)
                    {
                        using (var context = new TouristTripsModel())
                        {
                            Payment payment = context.Payments.Find(editPayment.Id);
                            if (payment.BookingId != editPayment.BookingId)
                                payment.BookingId = editPayment.BookingId;
                            if (payment.Amount != editPayment.Amount)
                                payment.Amount = editPayment.Amount;
                            if (payment.PaymentDate != editPayment.PaymentDate)
                                payment.PaymentDate = editPayment.PaymentDate;
                            try
                            {
                                context.SaveChanges();
                                ListPayments.Clear();
                                ListPayments = GetPayments();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                            }
                        }
                    }
                    else
                    {
                        ListPayments.Clear();
                        ListPayments = GetPayments();
                    }
                }, (obj) => SelectedPayment != null && ListPayments.Count > 0));
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
                    Payment payment = SelectedPayment;
                    using (var context = new TouristTripsModel())
                    {
                        Payment delPayment = context.Payments.Find(payment.Id);
                        if (delPayment != null)
                        {
                            MessageBoxResult result = MessageBox.Show("Удалить данные по операции?",
                                "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                            if (result == MessageBoxResult.OK)
                            {
                                try
                                {
                                    context.Payments.Remove(delPayment);
                                    context.SaveChanges();
                                    ListPayments.Remove(payment);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                                }
                            }
                        }
                    }
                }, (obj) => SelectedPayment != null && ListPayments.Count > 0));
            }
        }
        #endregion
    }
}
