using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class SeedPending : Seed
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Data Transfer Object to represent Pending Seed Donations  from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int DonationID { get; set; }
        public int NeededID { get; set; }
        public DateTime Date { get; set; }

        public SeedPending(int id, string name, int userID, bool active,
            int donationID, int neededID, DateTime date)
            : base(id, name, userID, active)
        {
            this.DonationID = donationID;
            this.NeededID = neededID;
            this.Date = date;
        }
    }
}
