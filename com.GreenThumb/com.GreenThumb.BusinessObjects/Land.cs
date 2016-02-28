using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Land
    {
        public int LandIdentifier { get; set; }
        public int UserID { get; set; }
        public int Size { get; set; }
        public string Notes { get; set; }
        public bool Active { get; set; }

        public Land(int landIdentifier, int userID, int size, string notes, bool active)
        {
            this.LandIdentifier = landIdentifier;
            this.UserID = userID;
            this.Size = size;
            this.Notes = notes;
            this.Active = active;
        }
    }
}
