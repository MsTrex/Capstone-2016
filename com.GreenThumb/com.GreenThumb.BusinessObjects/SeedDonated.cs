using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class SeedDonated : Seed
    {
        public int DonationID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public SeedDonated(int id, string name, int userID, bool active,
            int donationID, int quantity, DateTime date)
            : base(id, name, userID, active)
        {
            this.DonationID = donationID;
            this.Quantity = quantity;
            this.Date = date;
        }
    }
}
