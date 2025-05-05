using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingForTouristTrips.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int ClientId { get; set; }
        public DateTime BookingDate { get; set; }
        public int  NumberOfPeople { get; set; }
        public BookingStatys Statys { get; set; }

        public Booking() { }

        //Using for DB
        public virtual Tour Tour { get; set; }
        public virtual Client Client { get; set; }
    }

    public enum BookingStatys
    {
        Confirmed,
        Canceled,
        Pending
    }
}
