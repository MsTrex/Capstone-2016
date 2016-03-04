using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class SupplyNeeded : Supply
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Data Transfer Object to represent Supply Needed from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int NeededID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public SupplyNeeded(int id, string name, int userID, bool active, int neededID, decimal amount, DateTime date) : base(id, name, userID, active)
        {
            this.NeededID = neededID;
            this.Amount = amount;
            this.Date = date;
        }
    }
}
