using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class MoneyDonated : Money
    {
        public int DonationID { get; set; }
        public string Location { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }

        public MoneyDonated(int userID, bool active,
            int donationID, string location, decimal amount, DateTime dateCreated)
            : base(userID, active)
        {
            this.DonationID = donationID;
            this.Location = location;
            this.Amount = amount;
            this.DateCreated = dateCreated;
        }
    }
}
