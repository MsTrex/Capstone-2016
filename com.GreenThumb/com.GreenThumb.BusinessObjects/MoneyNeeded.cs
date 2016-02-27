using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class MoneyNeeded : Money
    {
        public int NeedID { get; set; }
        public string Location { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }

        public MoneyNeeded(int userID, bool active,
            int needID, string location, decimal amount, DateTime dateCreated)
            : base(userID, active)
        {
            this.NeedID = needID;
            this.Location = location;
            this.Amount = amount;
            this.DateCreated = dateCreated;
        }
    }
}
