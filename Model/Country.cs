using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingForTouristTrips.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public string Currency { get; set; } <-- Валюта страны, можно не добавлять

        public Country()
        {
            Tours = new ObservableCollection<Tour>();
        }

        //Using for DB
        public virtual ObservableCollection<Tour> Tours { get; set;}
    }
}
