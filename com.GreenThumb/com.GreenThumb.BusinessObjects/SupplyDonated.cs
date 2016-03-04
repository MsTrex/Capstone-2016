using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class SupplyDonated : Supply
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Data Transfer Object to represent Supplies Donated from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int DonatedID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public SupplyDonated(int id, string name, int userID, bool active, int donatedID, decimal amount, DateTime date) : base(id, name, userID, active)
        {
            this.DonatedID = donatedID;
            this.Amount = amount;
            this.Date = date;
        }
    }
}
