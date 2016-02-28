using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Soil : Item
    {
        public string SoilType { get; set; }
        public int UserID { get; set; }
        public bool Active { get; set; }

        public Soil(int id, string name,
            string soilType, int userID, bool active)
            : base (id, name)
        {
            this.SoilType = soilType;
            this.UserID = userID;
            this.Active = active;
        }
    }
}
