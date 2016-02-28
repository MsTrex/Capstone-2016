using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
   
    public class Money
    {
        public int UserID { get; set; }
        public bool Active { get; set; }

        public Money(int userID, bool active)
        {
            this.UserID = userID;
            this.Active = active;
        }
    }
}
