using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingForTouristTrips.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Client() 
        {
            Bookings = new ObservableCollection<Booking>();
        }

        //Using for DB
        public virtual User User { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }
    }
}
