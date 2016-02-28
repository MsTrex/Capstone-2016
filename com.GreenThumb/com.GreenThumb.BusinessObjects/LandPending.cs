using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class LandPending : Land
    {
        public int DonatedID { get; set; }
        public int NeededID { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? DateOfExpiration { get; set; }

        public LandPending(int landIdentifier, int userID, int size, string notes, bool active,
            int donatedID, int neededID, DateTime? dateCompleted, DateTime? dateOfExpiration)
            : base(landIdentifier, userID, size, notes, active)
        {
            
        }

    }
}
