using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Seed : Item
    {
        public int UserID { get; set; }
        public bool Active { get; set; }

        public Seed(int id, string name,
            int userID, bool active)
            : base (id, name)
        {
            this.UserID = userID;
            this.Active = active;
        }
    }
}
