using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingForTouristTrips.Model
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxSeats { get; set; }

        public Tour()
        {
            Bookings = new ObservableCollection<Booking>();
        }

        //Using for DB
        public virtual Country Country { get; set; }
        public virtual ObservableCollection<Booking> Bookings { get; set; }

    }
}
