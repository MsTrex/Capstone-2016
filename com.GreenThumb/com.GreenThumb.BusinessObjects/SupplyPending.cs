using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class SupplyPending : Supply
    {
        public int DonatedID { get; set; }
        public int NeededID { get; set; }
        public DateTime Date { get; set; }

        public SupplyPending(int id, string name, int userID, bool active,
                int donatedID, int neededID, DateTime date) 
            : base(id, name, userID, active)
        {
            this.DonatedID = donatedID;
            this.NeededID = neededID;
            this.Date = date;
        }
    }
}
