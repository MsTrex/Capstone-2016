using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class EquipmentNeeded : Equipment
    {
        public int NeededID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public EquipmentNeeded(int id, string name, int userID, bool active, int neededID, int quantity, DateTime date) : base(id, name, userID, active)
        {
            this.NeededID = neededID;
            this.Quantity = quantity;
            this.Date = date;
        }
    }
}
