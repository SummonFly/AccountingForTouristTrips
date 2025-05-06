using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingForTouristTrips.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Role()
        {
            Roles = new ObservableCollection<User>();
        }

        //Using for DB
        public virtual ObservableCollection<User> Roles { get; set; }
    }
}
