using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class LandNeeded : Land
    {
        public int NeededID { get; set; }
        public DateTime DateNeeded { get; set; }
        public DateTime DateRequested { get; set; }
        public string Zip { get; set; }

        public LandNeeded(int landIdentifier, int userID, int size, string notes, bool active,
            int neededID, DateTime dateNeeded, DateTime dateRequested, string zip)
            : base(landIdentifier, userID, size, notes, active)
        {
            this.NeededID = neededID;
            this.DateNeeded = dateNeeded;
            this.DateRequested = dateRequested;
            this.Zip = zip;
        }
    }
}
