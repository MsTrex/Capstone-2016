using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class LandDonated : Land
    {
        public int DonatedID { get; set; }
        public DateTime DateDonated { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public LandDonated(int landIdentifier, int userID, int size, string notes, bool active,
            int donatedID, DateTime dateDonated, string address, string city, string state, string zip)
            : base(landIdentifier, userID, size, notes, active)
        {
            this.DonatedID = donatedID;
            this.DateDonated = dateDonated;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
        }
    }
}
