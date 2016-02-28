using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class EquipmentDonated : Equipment
    {
        public int DonatedID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public EquipmentDonated(int id, string name, int userID, bool active, int donatedID, int quantity, DateTime date) : base(id, name, userID, active)
        {
            this.DonatedID = donatedID;
            this.Quantity = quantity;
            this.Date = date;
        }
    }
}
