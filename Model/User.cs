using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingForTouristTrips.Model
{
    public class User
    {
        [Key]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }


        //Using for DB
        public virtual Client Client { get; set; }
        public virtual Role Role { get; set; }
    }
}
