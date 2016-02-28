using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class TimePledgePending : TimePledge
    {
        public int TimePledgeID { get; set; }
        public int TimeNeededID { get; set; }
        public DateTime DateNeeded { get; set; }
        public DateTime DateMatched { get; set; }
    }
}
