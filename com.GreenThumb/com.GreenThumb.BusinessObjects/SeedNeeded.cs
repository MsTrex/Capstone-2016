using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class SeedNeeded : Seed
    {
        /// <summary>
        /// Author: Poonam
        /// Data Transfer Object to represent Seed Needed for donation
        /// from the Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int NeededID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public SeedNeeded(int id, string name, int userID, bool active,
            int neededID, int quantity, DateTime date)
            : base(id, name, userID, active)
        {
            this.NeededID = neededID;
            this.Quantity = quantity;
            this.Date = date;
        }
    }
}
