using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class SoilDonated : Soil
    {
        public int DonatedID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public SoilDonated(int id, string name, string soilType, int userID, bool active,
            int donatedID, int quantity, DateTime date)
            : base(id, name, soilType, userID, active)
        {
            this.DonatedID = donatedID;
            this.Quantity = quantity;
            this.Date = date;
        }
    }
}
