using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class TimePledgeDonated : TimePledge
    {
        public int TimePledgeID { get; set; }
        public int UserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Location { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
